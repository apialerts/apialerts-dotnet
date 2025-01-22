using System.Text.Json;
using APIAlerts.network;
using APIAlerts.network.contract;
using Xunit;

namespace APIAlerts.Tests.network.contract;

public class EventRequestTests
{
    [Fact]
    public void SerializeEventRequest_ValidObject_ReturnsJson()
    {
        var eventRequest = new EventRequest
        {
            Channel = "my-channel",
            Message = "Test message",
            Link = "https://example.com",
            Tags = new List<string> { "tag1", "tag2" }
        };

        var json = JsonSerializer.Serialize(eventRequest, Json.JsonOptions);

        Assert.NotNull(json);
        Assert.Contains("\"channel\":\"my-channel\"", json);
        Assert.Contains("\"message\":\"Test message\"", json);
        Assert.Contains("\"link\":\"https://example.com\"", json);
        Assert.Contains("\"tags\":[\"tag1\",\"tag2\"]", json);
    }

    [Fact]
    public void DeserializeEventRequest_ValidJson_ReturnsEventRequest()
    {
        const string json = "{\"channel\":\"my-channel\",\"message\":\"Test message\",\"link\":\"https://example.com\",\"tags\":[\"tag1\",\"tag2\"]}";

        var eventRequest = JsonSerializer.Deserialize<EventRequest>(json, Json.JsonOptions);

        Assert.NotNull(eventRequest);
        Assert.Equal("my-channel", eventRequest?.Channel);
        Assert.Equal("Test message", eventRequest?.Message);
        Assert.Equal("https://example.com", eventRequest?.Link);
        Assert.NotNull(eventRequest?.Tags);
        Assert.Equal(2, eventRequest?.Tags?.Count);
        Assert.Equal("tag1", eventRequest?.Tags?[0]);
        Assert.Equal("tag2", eventRequest?.Tags?[1]);
    }

    [Fact]
    public void DeserializeEventRequest_InvalidJson_ReturnsNull()
    {
        const string json = "Invalid JSON";

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<EventRequest>(json, Json.JsonOptions));
    }
}