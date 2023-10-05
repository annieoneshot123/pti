using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VsitPrinter.Infrastructure.Entities;
using VsitPrinter.Model;

namespace VsitPrinter.Infrastructure.Service
{
    public partial class PrinterService : IPrinterService
    {
        private readonly PrinterDbContext _printerDbContext;
        private readonly object _jobs = new object();
        static readonly object _object = new object();

        public PrinterService(PrinterDbContext printerDbContext)
        {
            _printerDbContext = printerDbContext;
        }

        public void AddJob(string printerName, string filePath, string printerType, long contractId)
        {
            PrinterJobPending job = new PrinterJobPending()
            {
                Id = Guid.NewGuid().ToString(),
                FilePath = filePath,
                PrinterName = printerName,
                PrinterType = printerType,
                PrinterSetting = null,
                CreatedDate = DateTime.Now,
                GoogleCloudSetting = null,
                ContractId = contractId,
                PrinterDeviceName = printerName
            };

            _printerDbContext.PrinterJobPendings.Add(job);
            _printerDbContext.SaveChanges();
        }

        public List<PrinterJobPending> GetJobs(string id, string printerName, int limit = 3)
        {
            List<PrinterJobPending> jobs = new List<PrinterJobPending>();
            if (IsPrinterValid(id, printerName))
            {
                jobs = _printerDbContext.PrinterJobPendings.AsQueryable().Where(x => x.PrinterName == printerName && string.IsNullOrEmpty(x.PrinterProcessingId)).Take(limit).ToList();

                foreach (var job in jobs)
                {
                    job.PrinterProcessingId = id;
                    _printerDbContext.PrinterJobPendings.Update(job);
                }

                _printerDbContext.SaveChanges();
            }

            return jobs;
        }

        private bool IsPrinterValid(string id, string printerName)
        {
            return _printerDbContext.PrinterDevices.Any(c => (c.Id == id) && (c.PrinterName == printerName) && c.IsActive);
        }

        public PrinterDevice RegisterDevice(PrinterRegisterDTO printer)
        {
            lock (_object)
            {
                PrinterDevice device = IsPrinterExisted(printer.PrinterName, printer.DeviceName);
                if (device != null)
                {
                    //if (device.IsActive) throw new Exception("Printer was running in this device");
                    StartPrinter(device);
                    return device;
                }

                PrinterDevice deviceInsert = new PrinterDevice()
                {
                    Id = Guid.NewGuid().ToString(),
                    DeviceName = printer.DeviceName,
                    PrinterName = printer.PrinterName,
                    RegisterDate = DateTime.Now,
                    IsActive = true
                };

                _printerDbContext.PrinterDevices.Add(deviceInsert);
                _printerDbContext.SaveChanges();

                return deviceInsert;
            }
        }

        private void StartPrinter(PrinterDevice device)
        {
            device.IsActive = true;
            device.RegisterDate = DateTime.Now;
            device.ShutdownDate = null;

            _printerDbContext.PrinterDevices.Update(device);
            _printerDbContext.SaveChanges();
        }

        private PrinterDevice IsPrinterExisted(string printerName,string deviceName)
        {
            return _printerDbContext.PrinterDevices.FirstOrDefault(c => (c.PrinterName == printerName) && (c.DeviceName == deviceName));
        }

        public void ShutdownDevice(string id)
        {
            lock (_object)
            {
                PrinterDevice device = _printerDbContext.PrinterDevices.AsQueryable().FirstOrDefault(x => x.Id == id);

                if (device != null)
                {
                    device.IsActive = false;
                    device.ShutdownDate = DateTime.Now;

                    _printerDbContext.PrinterDevices.Update(device);
                    _printerDbContext.SaveChanges();
                }
            }
        }

        public void UpdateFailed(string jobId, string reason)
        {
            var job = _printerDbContext.PrinterJobPendings.AsQueryable().FirstOrDefault(x => x.Id == jobId);

            if (job == null) throw new UserException("Can not find job by id");

            //insert and delete job
            PrepareInsertJobAfterPrint(job, reason);
            PrepareDeletePrinterJobPending(job);

            _printerDbContext.SaveChanges();

            //update status in contract
            if (job.ContractId > 0)
            {
                OrderSubmitInfoHelper.UpdatePrintCommandForOrder(job.ContractId, false, "PRINT COMMAND PUSH FAIL: " + reason);
            }
        }

        public void UpdateSuccess(string jobId)
        {
            var job = _printerDbContext.PrinterJobPendings.AsQueryable().FirstOrDefault(x => x.Id == jobId);

            if (job == null) throw new UserException("Can not find job by id");

            //insert and delete job
            PrepareInsertJobAfterPrint(job);
            PrepareDeletePrinterJobPending(job);

            _printerDbContext.SaveChanges();

            //update status in contract
            if (job.ContractId > 0)
            {
                OrderSubmitInfoHelper.UpdatePrintCommandForOrder(job.ContractId, true);
            }
        }

        private void PrepareDeletePrinterJobPending(PrinterJobPending job)
        {
            _printerDbContext.PrinterJobPendings.Remove(job);
        }

        private void PrepareInsertJobAfterPrint(PrinterJobPending job, string reason = null)
        {
            if (string.IsNullOrEmpty(reason))
            {
                PrintSuccessJob(job);

                return;
            }
            PrintFailedJob(job, reason);
        }

        private void PrintFailedJob(PrinterJobPending job, string reason)
        {
            PrinterJobFailed failedJob = new PrinterJobFailed()
            {
                Id = job.Id,
                PrinterName = job.PrinterName,
                PrinterSetting = job.PrinterSetting,
                PrinterType = job.PrinterType,
                GoogleCloudSetting = job.GoogleCloudSetting,
                FilePath = job.FilePath,
                ContractId = job.ContractId,
                ErrorMessage = reason,
                CreatedDate = DateTime.Now,
                IsDuplex = job.IsDuplex,
                FromPage = job.FromPage,
                ToPage = job.ToPage,
                IsHorizontal = job.IsHorizontal,
                FileType = job.FileType,
                PrinterDeviceName = job.PrinterDeviceName
            };

            _printerDbContext.PrinterJobFaileds.Add(failedJob);
        }

        private void PrintSuccessJob(PrinterJobPending job)
        {
            PrinterJobExecuted sucessJob = new PrinterJobExecuted()
            {
                Id = job.Id,
                PrinterName = job.PrinterName,
                PrinterSetting = job.PrinterSetting,
                PrinterType = job.PrinterType,
                GoogleCloudSetting = job.GoogleCloudSetting,
                FilePath = job.FilePath,
                ContractId = job.ContractId,
                CreatedDate = DateTime.Now,
                IsDuplex = job.IsDuplex,
                FromPage = job.FromPage,
                ToPage = job.ToPage,
                IsHorizontal = job.IsHorizontal,
                FileType = job.FileType,
                PrinterDeviceName = job.PrinterDeviceName
            };

            _printerDbContext.PrinterJobExecuteds.Add(sucessJob);
        }
    }
}