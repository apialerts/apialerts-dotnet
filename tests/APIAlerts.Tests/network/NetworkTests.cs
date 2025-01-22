using System.Net;
using APIAlerts.network.contract;
using APIAlerts.Tests.mocks;
using Xunit;

namespace APIAlerts.Tests.network;

public class NetworkTests
{
    
    [Fact]
    public async Task ApiRequest_SuccessResponse_ReturnsSuccessResult()
    {
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const string response = "{\"workspace\":\"my-workspace\",\"channel\":\"my-channel\",\"errors\":[\"no\",\"errors\",\"here\"],\"extra\":\"property\"}";

        var network = MockHttp.Network(statusCode, response);
        var result = await network.ApiRequest<EventResponse>("api-key", HttpMethod.Get, "/test", null);

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
    public async Task ApiRequest_SuccessResponseIncorrectDataType_ReturnsSuccessResult()
    {
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const string response = "{\"workspace\":\"my-workspace\",\"channel\":64,\"errors\":[\"no\",\"errors\",\"here\"],\"extra\":\"property\"}";

        var network = MockHttp.Network(statusCode, response);
        var result = await network.ApiRequest<EventResponse>("api-key", HttpMethod.Get, "/test", null);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Equal("Failed to deserialize response", result.Error?.Message);
    }

    [Fact]
    public async Task ApiRequest_ErrorResponse_ReturnsFailureResult()
    {
        const HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
        const string response = "{\"message\":\"Invalid API Key\"}";

        var network = MockHttp.Network(statusCode, response);
        var result = await network.ApiRequest<EventResponse>("api-key", HttpMethod.Get, "/test", null);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Equal("Invalid API Key", result.Error?.Message);
    }

    [Fact]
    public async Task ApiRequest_InvalidJson_ReturnsJsonParsingError()
    {
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const string response = "Invalid string JSON";
        
        var network = MockHttp.Network(statusCode, response);
        var result = await network.ApiRequest<EventResponse>("api-key", HttpMethod.Get, "/test", null);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Equal("Failed to deserialize response", result.Error?.Message);
    }
}