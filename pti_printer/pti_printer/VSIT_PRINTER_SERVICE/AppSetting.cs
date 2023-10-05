using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsitPrinter.Model;

namespace VsitPrinter
{
    public class AppSetting
    {
        public static string A4_PRINTER_ID { get; set; }

        public static string A5_PRINTER_ID { get; set; }

        public static string A4_NORMAL_PRINTER_ID { get; set; }

        public static string ApiProxyUrl { get; set; }

        public static string ContractSlugName { get; set; }

        public static ClientAppIdSecretModel ClientAppIdSecret { get; set; } = new ClientAppIdSecretModel();

        // ZIPKIN SETTINGS START
        public static string ZipkinType { get; set; }
        public static string ZipkinLoggerName { get; set; }
        public static string ZipkinUrl { get; set; }
        public static string ZipkipServiceName { get; set; }
        // ZIPKIN SETTINGS END

        public static string ConnectionString { get; set; }
    }
}
