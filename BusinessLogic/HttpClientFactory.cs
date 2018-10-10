using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SampleWebAPI.BusinessLogic
{
    public class HttpClientFactory
    {
        public const string MediaType = "application/json";

        /// <summary>
        ///     Get BulkSms Credentials.
        /// </summary>
        /// <param name="configurationKey"></param>
        /// <returns></returns>
        public string GetBulkSmsCredentials(string configurationKey)
        {
            var credential = ConfigurationManager.AppSettings[configurationKey];
            return credential;
        }

        /// <summary>
        ///     General HttpClient.
        /// </summary>
        /// <returns></returns>
        public HttpClient HttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            return httpClient;
        }

        /// <summary>
        ///     HttpClient with Network Credentials.
        /// </summary>
        /// <param name="myUsername"></param>
        /// <param name="myPassword"></param>
        /// <returns></returns>
        public HttpClient HttpClient(string myUsername, string myPassword)
        {
            var credentials = new NetworkCredential(myUsername, myPassword);
            var httpClient = new HttpClient(new HttpClientHandler {Credentials = credentials});
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            return httpClient;
        }

        /// <summary>
        ///     Create JSON content of any object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StringContent CreateJsonContent(object request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, MediaType);
            return content;
        }

        /// <summary>
        ///     Get response from BulkSms.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="myUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public HttpResponseMessage HttpResponseMessage(HttpClient httpClient, string myUri, StringContent content)
        {
            var responseMessage = httpClient.PostAsync(myUri, content).Result;
            return responseMessage;
        }

        /// <summary>
        ///     Read HttpResponseMessage.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public string ReadHttpResponseMessage(HttpResponseMessage responseMessage)
        {
            var data = responseMessage.Content.ReadAsStringAsync().Result;
            return data;
        }
    }
}