using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VsitPrinter.Model;
using WebApplication1.Controllers;

namespace VsitPrinter
{
    public class PrintQueue
    {
        private static Queue<PrintConfig> _queue;
        private static readonly Lazy<PrintQueue> lazy =
        new Lazy<PrintQueue>(() => new PrintQueue());
        private ILogger<BaseController> _log;
        private static object lockObject = new object();

        public static PrintQueue Instance { get { return lazy.Value; } }

        private PrintQueue()
        {
            _queue = new Queue<PrintConfig>();
        }

        public void AddWorkToQueue(PrintConfig printConfig, ILogger<BaseController> log)
        {
            _log = log;
            _queue.Enqueue(printConfig);
            lock (lockObject)
                Run();
        }

        private void Run()
        {
            if (_queue.Count > 0)
            {
                PrintConfig printConfig = _queue.Dequeue();
                var thread = new Thread(RunThreadFunction(printConfig));
                thread.Start();
            }
        }

        private ThreadStart RunThreadFunction(PrintConfig printConfig)
        {
            return () => {
                try
                {
                    var result = SendPrintSignalHelper.SendPrintData(printConfig);
                    if (result.success)
                    {
                        _log.LogInformation("PRINT COMMAND PUSH SUCCESS: " + printConfig.FileName + " - msg: " + result.message);
                        if (printConfig.RequireCallback)
                            OrderSubmitInfoHelper.UpdatePrintCommandForOrder(printConfig.ContractId, true);
                    }
                    else
                    {
                        _log.LogError("PRINT COMMAND PUSH FAIL: " + printConfig.FileName + " - msg: " + result.message);
                        if (printConfig.RequireCallback)
                            OrderSubmitInfoHelper.UpdatePrintCommandForOrder(printConfig.ContractId, false,
                                "PRINT COMMAND PUSH FAIL: " + result.message);
                    }
                }
                catch(Exception ex)
                {
                    string extraError = ex.InnerException != null ? ex.InnerException.Message : "";
                    _log.LogError("PRINT FAIL: " + printConfig.FileName + " - msg: " + ex.Message + " innerEx: " + extraError);
                    if (printConfig.RequireCallback)
                        OrderSubmitInfoHelper.UpdatePrintCommandForOrder(printConfig.ContractId, false, "HAVING_EXCEPTION");
                    Run();
                }
                finally
                {
                    //if (printConfig.DeleteAfterPrinted)
                    //    File.Delete(printConfig.FilePath);
                }
            };
        }
    }
}
