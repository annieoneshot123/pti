using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApiHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of return data</typeparam>
    public class RestApiHelper<T>
    {
        private string _accessToken = "";
        public const string DATETIME_FORMAT = "dd/MM/yyyy";
        private ResponFormatOption _responseFormatOption = ResponFormatOption.ApiJsonResult;

        public RestApiHelper() : this("")
        {
        }

        public RestApiHelper(string accessToken)
        {
            //if (string.IsNullOrEmpty(accessToken))
            //    _accessToken = "Bearer ";
            //else
                _accessToken = accessToken;
        }

        public void SetResponFormat(ResponFormatOption responseFormatOption)
        {
            _responseFormatOption = responseFormatOption;
        }

        /// <summary>
        /// Excute sending request to destination by <paramref name="url"/>.
        /// <paramref name="postData"/> is null if method option is GET
        /// </summary>
        /// <param name="methodOption"></param>
        /// <param name="url"></param>
        /// <param name="postData">null if method option is GET</param>
        /// <returns></returns>
        public async Task<ApiJsonResult<T>> DoRequest(MethodOption methodOption, string url, object postData = null)
        {
            if (methodOption == MethodOption.Post && postData == null)
            {
                ApiJsonResult<T> exeptionResult = new ApiJsonResult<T>() { Success = false, ErrorMessage = "DATA_POST_NULL" };
                return exeptionResult;
            }

            try
            {
                var response = new HttpResponseMessage();
                if (methodOption == MethodOption.Get)
                    response = await GetAsync(url);
                else
                    response = await PostAsync(url, postData);

                var result = ReadResponse(response);
                return result;
            }
            catch(Exception ex)
            {
                ApiJsonResult<T> exeptionResult = new ApiJsonResult<T>() { Success = false, ErrorMessage = ex.Message };
                return exeptionResult;
            }
        }

        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpClient client = CreateHttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            return response;
        }

        private async Task<HttpResponseMessage> PostAsync(string url, object postData)
        {
            string s = JsonConvert.SerializeObject(postData).ToString();
            HttpClient client = CreateHttpClient();
            StringContent data = new StringContent(JsonConvert.SerializeObject(postData).ToString(),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, data);

            return response;
        }

        private ApiJsonResult<T> ReadResponse(HttpResponseMessage responseMessage)
        {
            var result = new ApiJsonResult<T>();
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = DATETIME_FORMAT };
                string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                if(_responseFormatOption == ResponFormatOption.ApiJsonResult)
                {
                    result = JsonConvert.DeserializeObject<ApiJsonResult<T>>(responseContent, dateTimeConverter);
                }
                else
                {
                    var normalResult = JsonConvert.DeserializeObject<T>(responseContent, dateTimeConverter);
                    result.Success = true;
                    result.Data = normalResult;
                }

            }
            else
            {
                result.Success = false;
                result.ErrorMessage = responseMessage.ReasonPhrase;
            }

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            if(!string.IsNullOrEmpty(_accessToken))
                httpClient.DefaultRequestHeaders.Add("Authorization", _accessToken);

            return httpClient;
        }
    }

    public enum MethodOption
    {
        Get,
        Post,
    }

    public enum ResponFormatOption
    {
        ApiJsonResult,
        NormalObject,
    }
}
