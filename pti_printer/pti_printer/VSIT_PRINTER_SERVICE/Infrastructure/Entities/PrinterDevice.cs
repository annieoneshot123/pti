using System;

namespace VsitPrinter.Infrastructure.Entities
{
    public class PrinterDevice
    {
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public string PrinterName { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? ShutdownDate { get; set; }
        public bool IsActive { get; set; }
    }
}