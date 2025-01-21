using System.Net;
using ApiAlerts.Common.network;
using ApiAlerts.Common.Tests.mocks;
using Xunit;

namespace ApiAlerts.Common.Tests.network;

public class EndpointsTests
{
    [Fact]
    public async Task SendEvent_ValidRequest_ReturnsSuccessResult()
    {
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const string response = "{\"workspace\":\"my-workspace\",\"channel\":\"my-channel\",\"errors\":[\"no\",\"errors\",\"here\"],\"extra\":\"property\"}";
        
        const string apiKey = "test-api-key";
        const string channel = "test-channel";
        const string message = "test message";
        var tags = new List<string> { "tag1", "tag2" };
        const string link = "https://example.com";

        var network = MockHttp.Client(statusCode, response);
        var endpoints = new Endpoints(network);
        var result = await endpoints.SendEvent(apiKey, channel, message, tags, link);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Null(result.Error);
        Assert.Equal("my-workspace", result.Data?.Workspace);
        Assert.Equal("my-channel", result.Data?.Channel);
        Assert.Equal(3, result.Data?.Errors?.Count);
        Assert.Equal("no", result.Data?.Errors?[0]);
        Assert.Equal("errors", result.Data?.Errors?[1]);
        Assert.Equal("here", result.Data?.Errors?[2]);
    }

    [Fact]
    public async Task SendEvent_InvalidRequest_ReturnsFailureResult()
    {
        const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
        const string response = "{\"message\":\"Bad Request\"}";
        
        const string apiKey = "test-api-key";
        const string channel = "test-channel";
        const string message = "test message";
        var tags = new List<string> { "tag1", "tag2" };
        const string link = "https://example.com";

        var network = MockHttp.Client(statusCode, response);
        var endpoints = new Endpoints(network);
        var result = await endpoints.SendEvent(apiKey, channel, message, tags, link);

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal("Bad Request", result.Error.Message);
    }

    [Fact]
    public async Task SendEvent_InvalidApiKey_ReturnsFailureResult()
    {
        const HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
        const string response = "{\"message\":\"Invalid API Key\"}";
        
        const string apiKey = "test-api-key";
        const string channel = "test-channel";
        const string message = "test message";
        var tags = new List<string> { "tag1", "tag2" };
        const string link = "https://example.com";

        var network = MockHttp.Client(statusCode, response);
        var endpoints = new Endpoints(network);
        var result = await endpoints.SendEvent(apiKey, channel, message, tags, link);

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal("Invalid API Key", result.Error.Message);
    }
}
