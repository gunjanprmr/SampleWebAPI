using Newtonsoft.Json;
using SampleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SampleWebAPI.BusinessLogic
{
    public class BulkSmsApi
    {
        public const string MediaType = "application/json";
        private const string MyUri = "https://api.bulksms.com/v1/messages";

        /// <summary>
        /// Send SMS to the user.
        /// </summary>
        /// <param name="bulkSmsRequest"></param>
        /// <returns></returns>
        public BulkSmsResponse BulkSmsResponse(BulkSmsRequest bulkSmsRequest)
        {
            var myUsername = _getBulkSmsCredentials("BulkSmsUserName");
            var myPassword = _getBulkSmsCredentials("BulkSmsPassword");

            var content = _createJsonContent(bulkSmsRequest);

            var httpClient = _getHttpClient(myUsername, myPassword);

            var responseMessage = _getHttpResponseMessage(httpClient, MyUri, content);

            var bulkSmsResponse = responseMessage.IsSuccessStatusCode
                ? _response(responseMessage)
                : throw new Exception((int)responseMessage.StatusCode + "-" + responseMessage.StatusCode);

            return bulkSmsResponse;
        }

        /// <summary>
        /// Get BulkSms Credentials.
        /// </summary>
        /// <param name="configurationKey"></param>
        /// <returns></returns>
        private string _getBulkSmsCredentials(string configurationKey)
        {
            var credential = ConfigurationManager.AppSettings[configurationKey];
            return credential;
        }

        /// <summary>
        /// Create JSON content.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private StringContent _createJsonContent(object request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, MediaType);
            return content;
        }

        /// <summary>
        /// Create HttpClient waith credentials.
        /// </summary>
        /// <param name="myUsername"></param>
        /// <param name="myPassword"></param>
        /// <returns></returns>
        private HttpClient _getHttpClient(string myUsername, string myPassword)
        {
            var credentials = new NetworkCredential(myUsername, myPassword);
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credentials });
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            return httpClient;
        }

        /// <summary>
        /// Get response from BulkSms.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="myUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private HttpResponseMessage _getHttpResponseMessage(HttpClient httpClient, string myUri, StringContent content)
        {
            var responseMessage = httpClient.PostAsync(myUri, content).Result;
            return responseMessage;
        }

        /// <summary>
        /// Convery BulkSms response to custom class.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private BulkSmsResponse _response(HttpResponseMessage responseMessage)
        {
            var data = responseMessage.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<List<BulkSmsResponse>>(data);
            return apiResponse[0];
        }
    }
}