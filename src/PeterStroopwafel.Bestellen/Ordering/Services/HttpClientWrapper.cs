using System;
using System.Net.Http;

namespace Ordering.Services
{
    public class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private bool _disposed;

        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            this._httpClient = new HttpClient();
        }

        public HttpContent Get(HttpRequestMessage request)
        {
            HttpResponseMessage response = this._httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            return response.Content;
        }

        public HttpResponseMessage Post(Uri requestUri, HttpContent content)
        {
            HttpResponseMessage response = this._httpClient.PostAsync(requestUri, content).Result;
            response.EnsureSuccessStatusCode();

            return response;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this._disposed)
            {
                return;
            }

            this._disposed = true;
            this._httpClient?.Dispose();
        }
    }
}