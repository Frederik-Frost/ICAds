using System;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ICAds.Content.Integrations
{
    public class ApiHelper
    {
        public HttpClient ApiClient { get; set; }
        public ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.UserAgent.TryParseAdd("IC ads");
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var resp = await ApiClient.GetAsync(new Uri(url));
            return resp;
        }

        public async Task<HttpResponseMessage> PostAsync(Uri url, StringContent body)
        {
            var resp = await ApiClient.PostAsync(url, body); 
            return resp;
        }

        public void SetDefaultHeader(string name, string val)
        {
            ApiClient.DefaultRequestHeaders.Add(name, val);
        }
    }

}

