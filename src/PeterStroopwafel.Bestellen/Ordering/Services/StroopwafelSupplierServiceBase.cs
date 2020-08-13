using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ordering.Services
{
    public abstract class StroopwafelSupplierServiceBase
    {
        protected const string MediaType = "application/json";
        protected readonly IHttpClientWrapper HttpClient;

        protected StroopwafelSupplierServiceBase(IHttpClientWrapper httpClientWrapper)
        {
            this.HttpClient = httpClientWrapper;
        }

        protected JToken ExecuteGet(Uri uri)
        {
            HttpRequestMessage request = this.GetRequest(uri, MediaType);
            HttpContent content = this.HttpClient.Get(request);

            return JToken.Parse(content.ReadAsStringAsync().Result);
        }

        protected void ExecutePost(Uri uri, Order order)
        {
            HttpContent content = this.GetContent(order);
            HttpResponseMessage response = this.HttpClient.Post(uri, content);
            string result = response.Content.ReadAsStringAsync().Result;
        }

        private HttpContent GetContent(Order order)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(order));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return content;
        }

        private HttpRequestMessage GetRequest(Uri uri, string mediaType)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            
            return request;
        }


    }
}
