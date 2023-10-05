using GoogleCloudPrint.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VsitPrinter.Model;
using static VsitPrinter.GoogleCloudPrintService;

namespace VsitPrinter
{
    public class SendPrintSignalHelper
    {
        public static CloudPrintJob SendPrintData(PrintConfig printConfig)
        {
            GoogleCloudPrintService service = new GoogleCloudPrintService("printer@using-cloud-print-project.iam.gserviceaccount.com", "Key/printer_key.p12", "notasecret", "gcp console test");

            //string printerId = printConfig.PrintOption == PrintOption.A4TNDS ? A4_PRINTER_ID : A5_PRINTER_ID;
            string printerId = "";
            DuplexType printOption = DuplexType.NO_DUPLEX;
            PageSize pageSize = PageSize.Letter;
            switch (printConfig.PrintOption)
            {
                case PrintOption.A4TNDS:
                    printerId = AppSetting.A4_PRINTER_ID;
                    printOption = DuplexType.SHORT_EDGE;
                    pageSize = PageSize.A4;
                    break;
                case PrintOption.A5GCN:
                    printerId = AppSetting.A5_PRINTER_ID;
                    printOption = DuplexType.LONG_EDGE;
                    pageSize = PageSize.A5;
                    break;
                case PrintOption.A4NORMAL:
                    printerId = AppSetting.A4_NORMAL_PRINTER_ID;
                    printOption = DuplexType.NO_DUPLEX;
                    pageSize = PageSize.A4;
                    break;
            }
            if (service.PrinterIsOnline(printerId))
            {
                byte[] fileBytes;
                using (WebClient webClient = new WebClient())
                {
                    fileBytes = webClient.DownloadData(printConfig.FilePath);
                }
                //byte[] bytes = System.IO.File.ReadAllBytes(printConfig.FilePath);
                var result = service.PrintDocument(printerId, printConfig.FileName, fileBytes, "application/pdf", pageSize, printOption, "");
                return result;
            } else
            {
                return new CloudPrintJob { success = false, message = "PRINTER_OFFLINE"};
            }
        }
    }
}
