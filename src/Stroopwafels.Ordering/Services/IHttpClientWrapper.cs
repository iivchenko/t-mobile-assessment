using System;
using System.Net.Http;

namespace Stroopwafels.Ordering.Services
{
    public interface IHttpClientWrapper
    {
        HttpContent Get(HttpRequestMessage request);

        HttpResponseMessage Post(Uri requestUri, HttpContent content);
    }
}