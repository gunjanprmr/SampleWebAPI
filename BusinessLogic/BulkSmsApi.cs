using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SampleWebAPI.Models;

namespace SampleWebAPI.BusinessLogic
{
    public class BulkSmsApi
    {
        #region Variables

        private const string MyUri = "https://api.bulksms.com/v1/messages";
        private readonly HttpClientFactory _httpClientFactory;

        #endregion Variables

        #region Constructor

        public BulkSmsApi()
        {
            _httpClientFactory = new HttpClientFactory();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        ///     Send SMS to the user.
        /// </summary>
        /// <param name="bulkSmsRequest"></param>
        /// <returns></returns>
        public BulkSmsResponse BulkSmsResponse(BulkSmsRequest bulkSmsRequest)
        {
            // Get BulkSms Credentials.
            var myUsername = _httpClientFactory.GetBulkSmsCredentials("BulkSmsUserName");
            var myPassword = _httpClientFactory.GetBulkSmsCredentials("BulkSmsPassword");

            // Create JSON content of any object.
            var content = _httpClientFactory.CreateJsonContent(bulkSmsRequest);

            // HttpClient with Network Credentials.
            var httpClient = _httpClientFactory.HttpClient(myUsername, myPassword);

            // Get response from client.
            var responseMessage = _httpClientFactory.HttpResponseMessage(httpClient, MyUri, content);

            // Return response.
            var bulkSmsResponse = responseMessage.IsSuccessStatusCode
                ? _response(responseMessage)
                : throw new Exception((int) responseMessage.StatusCode + "-" + responseMessage.StatusCode);

            return bulkSmsResponse;
        }

        /// <summary>
        ///     Convery BulkSms response to custom class.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private BulkSmsResponse _response(HttpResponseMessage responseMessage)
        {
            var data = _httpClientFactory.ReadHttpResponseMessage(responseMessage);
            var apiResponse = JsonConvert.DeserializeObject<List<BulkSmsResponse>>(data);
            return apiResponse[0];
        }

        #endregion Methods
    }
}