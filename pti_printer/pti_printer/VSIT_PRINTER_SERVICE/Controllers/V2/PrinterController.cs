using System;
using Cloud_Net_Sdk_Hmac_Lib.Signers;
using Cloud_Net_Sdk_Hmac_Lib.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VsitPrinter;
using VsitPrinter.Infrastructure.Service;
using VsitPrinter.Model;
using static VsitPrinter.GoogleCloudPrintService;

namespace WebApplication1.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = HMACAuthOptions.Scheme)]
    public class PrinterController : BaseController
    {
        private readonly IPrinterService _printerService;

        public PrinterController(ILogger<PrinterController> log, IPrinterService printerService) : base(log)
        {
            _printerService = printerService;
        }

        [Route("PrintQueuely")]
        [HttpPost]
        [AllowAnonymous]
        public ApiJsonResult PrintQueuely([FromBody]PrintConfig printConfig)
        {
            try
            {
                Log.LogWarning("PrintQueuely FIRED");
                string requestAsJson = JsonConvert.SerializeObject(printConfig);
                Log.LogWarning("PrintQueuely -- (" + requestAsJson + ")");
                if (printConfig.PrintOption == PrintOption.NOT_DEFINE || string.IsNullOrEmpty(printConfig.FileName))
                    return new ApiJsonResult { Success = false, Data = "NOT_DEFINE" };

                Log.LogWarning("PRINT COMMAND ADDED: " + printConfig.FileName);


                if (printConfig.PrintOption == PrintOption.A4TNDS) // testing on TNDS template 
                {
                    _printerService.AddJob(PrintOption.A4TNDS.ToString(), printConfig.FilePath, PrintOption.A4TNDS.ToString(), printConfig.ContractId);
                } 
                else if (printConfig.PrintOption == PrintOption.A4NORMAL)
                {
                    _printerService.AddJob(PrintOption.A4NORMAL.ToString(), printConfig.FilePath, PrintOption.A4NORMAL.ToString(), printConfig.ContractId);
                }
                else if (printConfig.PrintOption == PrintOption.A5GCN)
                {
                    _printerService.AddJob(PrintOption.A5GCN.ToString(), printConfig.FilePath, PrintOption.A5GCN.ToString(), printConfig.ContractId);
                }
                else
                {
                    PrintQueue.Instance.AddWorkToQueue(printConfig, Log);
                }
                              
                return new ApiJsonResult { Success = true, Data = "PRINT_COMMAND _ADDED" };
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                return ProcessException(e);
            }
        }

        [Route("Invite")]
        [HttpGet]
        [AllowAnonymous]
        public ApiJsonResult Invite(string printId)
        {
            try
            {
                var gcp = new GoogleCloudPrintService("printer@using-cloud-print-project.iam.gserviceaccount.com", "Key/printer_key.p12", "notasecret", "gcp console test");
                var response = gcp.ProcessInvite(printId);

                return new ApiJsonResult { Success = false, Data = response };
            }
            catch (Exception e)
            {
                return ProcessException(e);
            }
        }

        [AllowAnonymous]
        [Route("GetPrinters")]
        [HttpGet]
        public ApiJsonResult GetPrinters()
        {
            try
            {
                GoogleCloudPrintService service = new GoogleCloudPrintService("printer@using-cloud-print-project.iam.gserviceaccount.com", "Key/printer_key.p12", "notasecret", "gcp console test");
                var result = service.GetPrinters();

                return new ApiJsonResult { Success = false, Data = result };
            }
            catch (Exception e)
            {
                return ProcessException(e);
            }
        }

        [AllowAnonymous]
        [Route("GetPrinter")]
        [HttpGet]
        public ApiJsonResult GetPrinter(string printerId)
        {
            try
            {
                GoogleCloudPrintService service = new GoogleCloudPrintService("printer@using-cloud-print-project.iam.gserviceaccount.com", "Key/printer_key.p12", "notasecret", "gcp console test");
                var result = service.GetPrinter(printerId);

                return new ApiJsonResult { Success = true, Data = result };
            }
            catch (Exception e)
            {
                return ProcessException(e);
            }
        }

        [AllowAnonymous]
        [Route("CheckPrinterOnline")]
        [HttpGet]
        public ApiJsonResult CheckPrinterOnline(string printerId)
        {
            try
            {
                GoogleCloudPrintService service = new GoogleCloudPrintService("printer@using-cloud-print-project.iam.gserviceaccount.com", "Key/printer_key.p12", "notasecret", "gcp console test");
                var result = service.PrinterIsOnline(printerId);

                return new ApiJsonResult { Success = true, Data = result };
            }
            catch (Exception e)
            {
                return ProcessException(e);
            }
        }

        [AllowAnonymous]
        [Route("GetJob")]
        [HttpGet]
        public ApiJsonResult GetJobPending(string id, string printerName, int limit = 1)
        {
            try
            {
                return new ApiJsonResult { Success = true, Data = _printerService.GetJobs(id, printerName, limit) };
            } 
            catch(Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [AllowAnonymous]
        [Route("AddJobSuccess")]
        [HttpPost]
        public ApiJsonResult UpdateJobSuccess([FromBody]PrinterJobDTO job)
        {
            try
            {
                _printerService.UpdateSuccess(job.Id);

                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [AllowAnonymous]
        [Route("AddJobFailed")]
        [HttpPost]
        public ApiJsonResult UpdateJobFailed([FromBody]PrinterJobDTO job)
        {
            try
            {
                _printerService.UpdateFailed(job.Id, job.Reason);

                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [AllowAnonymous]
        [Route("RegisterDevice")]
        [HttpPost]
        public ApiJsonResult RegisterDevice([FromBody] PrinterRegisterDTO printer)
        {
            try
            {
                var registeredJob = _printerService.RegisterDevice(printer);

                return new ApiJsonResult { Success = true, Data = registeredJob };
            }
            catch(Exception ex)
            {
                return new ApiJsonResult { Success = false, Data = ex.Message };
            }
        }

        [AllowAnonymous]
        [Route("ShutdownDevice")]
        [HttpPut]
        public ApiJsonResult ShutdownDevice([FromBody] PrinterRegisterDTO printer)
        {
            try
            {
                _printerService.ShutdownDevice(printer.Id);

                return new ApiJsonResult { Success = true, Data = printer.Id };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public ApiJsonResult PrintV2([FromBody]PrinterConfigDetail printConfig)
        {
            try
            {
                Log.LogError("PrintV2 FIRED");
                string requestAsJson = JsonConvert.SerializeObject(printConfig);
                Log.LogInformation("PrintV2 -- (" + requestAsJson + ")");
                if (string.IsNullOrEmpty(printConfig.PrinterName) || string.IsNullOrEmpty(printConfig.FileName))
                    return new ApiJsonResult { Success = false, Data = "NOT_DEFINE" };

                Log.LogError("PRINT PrintV2 COMMAND ADDED: " + printConfig.FileName + " --- " + printConfig.PrinterName);

                _printerService.AddJob(printConfig);

                return new ApiJsonResult { Success = true, Data = "PRINT_COMMAND _ADDED" };
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                return ProcessException(e);
            }
        }
    }
}