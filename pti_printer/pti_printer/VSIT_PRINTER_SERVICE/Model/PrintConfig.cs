using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static VsitPrinter.GoogleCloudPrintService;

namespace VsitPrinter.Model
{
    public class PrintConfig
    {
        public long ContractId { get; set; }

        public PrintOption PrintOption { get; set; }

        /// <summary>
        /// Reuse this variable.
        /// Path: old solution.
        /// Url: new solution.
        /// </summary>
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public bool DeleteAfterPrinted { get; set; }

        public bool RequireCallback { get; set; }
    }

    public class PrinterConfigDetail: PrintConfig
    {
        public int? FromPage { get; set; }
        public int? ToPage { get; set; }
        public bool? IsDuplex { get; set; }
        public bool? IsHorizontal { get; set; }
        public string PrinterName { get; set; }
        public string FileType { get; set; }
        public string PrinterDeviceName { get; set; }
    }
}
