using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using VsitPrinter.Model;
using BeTall.Serilog.Sinks.Fluentd;

namespace WebApplication1.Controllers
{
    //[ServiceFilter(typeof(ClientIpFilter))]
    [EnableCors("CorsPolicy")]
    //[Authorize]
    public class BaseController : Controller
    {
        protected ILogger<BaseController> Log;
        public BaseController(ILogger<BaseController> log)
        {
            Log = log;
        }   
        protected ApiJsonResult ProcessException(Exception ex)
        {
            if (ex is UserException)
            {
                return new ApiJsonResult { Success = false, Data = ex.Message };
            }
            if (ex is SecurityException)
            {
                return new ApiJsonResult { Success = false, Data = ex.Message };
            }
            LogEx(ex);
            return new ApiJsonResult { Success = false, Data = "SYSTEM_ERROR" };
        }

        private void LogEx(Exception ex)
        {
            if (Log == null)
                return;
            var st = new StackTrace(ex, true); // create the stack trace
            var firstSt = st.GetFrames().FirstOrDefault();
            StringBuilder stb = new StringBuilder();
            stb.Append("ERROR_EX ");
            if (firstSt != null)
            {
                stb.Append("\nDetails: ");
                stb.Append(ex.Message);
                stb.Append("\nFile: ");
                stb.Append(firstSt.GetFileName());
                stb.Append("\nLine: ");
                stb.Append("\nColumn: ");
                stb.Append(firstSt.GetFileColumnNumber());
                stb.Append("\nMethod: ");
                stb.Append(firstSt.GetMethod());
                stb.Append("\nClass: ");
                stb.Append(firstSt.GetMethod().DeclaringType);
                Serilog.Log.Logger.Information(stb.ToString());
            }

        }
    }
}
