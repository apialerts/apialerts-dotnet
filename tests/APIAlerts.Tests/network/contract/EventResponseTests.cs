using System.Text.Json;
using APIAlerts.network;
using APIAlerts.network.contract;
using Xunit;

namespace APIAlerts.Tests.network.contract;

public class EventResponseTests
{
    [Fact]
    public void DeserializeEventResponse_ValidJson_ReturnsEventResponse()
    {
        const string json = "{\"workspace\":\"my-workspace\",\"channel\":\"my-channel\",\"errors\":[\"no\",\"errors\",\"here\"]}";

        var eventResponse = JsonSerializer.Deserialize<EventResponse>(json, Json.JsonOptions);

        Assert.NotNull(eventResponse);
        Assert.Equal("my-workspace", eventResponse?.Workspace);
        Assert.Equal("my-channel", eventResponse?.Channel);
        Assert.NotNull(eventResponse?.Errors);
        Assert.Equal(3, eventResponse?.Errors?.Count);
        Assert.Equal("no", eventResponse?.Errors?[0]);
        Assert.Equal("errors", eventResponse?.Errors?[1]);
        Assert.Equal("here", eventResponse?.Errors?[2]);
    }
    
    [Fact]
    public void DeserializeEventResponse_ValidJsonWithExtraProperties_ReturnsEventResponse()
    {
        const string json = "{\"workspace\":\"my-workspace\",\"channel\":\"my-channel\",\"errors\":[\"no\",\"errors\",\"here\"],\"extra\":\"property\"}";

        var eventResponse = JsonSerializer.Deserialize<EventResponse>(json, Json.JsonOptions);

        Assert.NotNull(eventResponse);
        Assert.Equal("my-workspace", eventResponse?.Workspace);
        Assert.Equal("my-channel", eventResponse?.Channel);
        Assert.NotNull(eventResponse?.Errors);
        Assert.Equal(3, eventResponse?.Errors?.Count);
        Assert.Equal("no", eventResponse?.Errors?[0]);
        Assert.Equal("errors", eventResponse?.Errors?[1]);
        Assert.Equal("here", eventResponse?.Errors?[2]);
    }
    
    [Fact]
    public void DeserializeEventResponse_ValidJsonWithInvalidDataType_ReturnsEventResponse()
    {
        const string json = "{\"workspace\":33,\"channel\":\"my-channel\",\"errors\":[\"no\",\"errors\",\"here\"],\"extra\":\"property\"}";

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<EventResponse>(json, Json.JsonOptions));
    }
}