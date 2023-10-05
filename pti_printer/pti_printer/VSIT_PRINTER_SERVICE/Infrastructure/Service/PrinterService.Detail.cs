using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsitPrinter.Infrastructure.Entities;
using VsitPrinter.Model;

namespace VsitPrinter.Infrastructure.Service
{
    public partial class PrinterService
    {
        public void AddJob(PrinterConfigDetail detail)
        {
            PrinterJobPending job = new PrinterJobPending()
            {
                Id = Guid.NewGuid().ToString(),
                FilePath = detail.FilePath,
                PrinterName = detail.PrinterName,
                PrinterType = detail.PrinterName,
                PrinterSetting = null,
                CreatedDate = DateTime.Now,
                GoogleCloudSetting = null,
                ContractId = -1,
                IsDuplex = detail.IsDuplex,
                FromPage = detail.FromPage,
                ToPage = detail.ToPage,
                IsHorizontal = detail.IsHorizontal,
                FileType = detail.FileType,
                PrinterDeviceName = detail.PrinterDeviceName
            };

            _printerDbContext.PrinterJobPendings.Add(job);
            _printerDbContext.SaveChanges();
        }
    }
}
