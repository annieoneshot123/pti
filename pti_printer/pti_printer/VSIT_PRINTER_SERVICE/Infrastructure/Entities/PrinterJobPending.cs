using System;

namespace VsitPrinter.Infrastructure.Entities
{
    /// <summary>
    /// Defined job printer was fire
    /// </summary>
    public class PrinterJobPending
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public string PrinterName { get; set; }
        public string PrinterSetting { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PrinterType { get; set; }
        public string GoogleCloudSetting { get; set; }
        public long ContractId { get; set; }
        public string PrinterProcessingId { get; set; }

        public int? FromPage { get; set; }
        public int? ToPage { get; set; }
        public bool? IsDuplex { get; set; }
        public bool? IsHorizontal { get; set; }
        public string FileType { get; set; }

        public string PrinterDeviceName { get; set; }
    }
}