using System;
using System.Net.Http;

namespace Ordering.Services
{
    public interface IHttpClientWrapper
    {
        HttpContent Get(HttpRequestMessage request);

        HttpResponseMessage Post(Uri requestUri, HttpContent content);
    }
}