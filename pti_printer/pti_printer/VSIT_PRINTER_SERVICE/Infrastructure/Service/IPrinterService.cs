using System.Collections.Generic;
using VsitPrinter.Infrastructure.Entities;
using VsitPrinter.Model;

namespace VsitPrinter.Infrastructure.Service
{
    public interface IPrinterService
    {
        List<PrinterJobPending> GetJobs(string id, string printerName, int limit = 3);

        void UpdateSuccess(string jobId);

        void UpdateFailed(string jobId, string reason);

        void AddJob(string printerName, string filePath, string printerType, long contractId);

        void AddJob(PrinterConfigDetail detail);

        PrinterDevice RegisterDevice(PrinterRegisterDTO printer);

        void ShutdownDevice(string id);
    }
}