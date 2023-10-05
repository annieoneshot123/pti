using System;

namespace VsitPrinter.Infrastructure.Entities
{
    /// <summary>
    /// Defined job printer was excuted
    /// </summary>
    public class PrinterJobExecuted
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public string PrinterName { get; set; }
        public string PrinterSetting { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PrinterType { get; set; }
        public string GoogleCloudSetting { get; set; }
        public long ContractId { get; set; }

        public int? FromPage { get; set; }
        public int? ToPage { get; set; }
        public bool? IsDuplex { get; set; } = true;
        public bool? IsHorizontal { get; set; } = true;
        public string FileType { get; set; }

        public string PrinterDeviceName { get; set; }
    }
}