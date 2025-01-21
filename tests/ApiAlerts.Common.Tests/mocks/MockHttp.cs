using System.Net;
using ApiAlerts.Common.network;

namespace ApiAlerts.Common.Tests.mocks;

static class MockHttp
{
    public static HttpClient Client(HttpStatusCode statusCode, string response)
    {
        return new HttpClient(new MockHttpMessageHandler(statusCode, response))
        {
            BaseAddress = new Uri("https://api.example.com")
        };
    }
    public static Network Network(HttpStatusCode statusCode, string response)
    {
        return new Network(Client(statusCode, response));
    }
}

class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _responseContent;

    public MockHttpMessageHandler(HttpStatusCode statusCode, string responseContent)
    {
        _statusCode = statusCode;
        _responseContent = responseContent;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_responseContent)
        };
        return Task.FromResult(response);
    }
}