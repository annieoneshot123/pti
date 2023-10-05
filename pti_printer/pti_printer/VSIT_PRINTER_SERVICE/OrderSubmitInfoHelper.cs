using Microsoft.Extensions.Logging;
using RestApiHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsitPrinter.Model;
using WebApplication1.Controllers;

namespace VsitPrinter
{
    public class OrderSubmitInfoHelper
    {
        /// <summary>
        /// Send info to contract service about pushing print command
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="_log"></param>
        public static void UpdatePrintCommandForOrder(long contractId, bool success, string message = "")
        {
            string apiUrl = AppSetting.ApiProxyUrl + "/" + AppSetting.ContractSlugName + "/v2/Order/UpdateOrderWithPrintCommand";
            var restApi = new Cloud_Net_Sdk_Hmac_Lib.Signers.RestApiHmac<object>();
            restApi.DoRequestV1(HttpMethod.POST, apiUrl, AppSetting.ClientAppIdSecret.ClientId, AppSetting.ClientAppIdSecret.SecretKey,
                new { ContractId = contractId, Success = success, Message = message });
        }
    }
}
