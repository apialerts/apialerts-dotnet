using System.Text.Json;
using APIAlerts.network.contract;
using Xunit;

namespace APIAlerts.Tests.network.contract;

public class ErrorResponseTests
{
    [Fact]
    public void SerializeErrorResponse_ValidObject_ReturnsJson()
    {
        var errorResponse = new ErrorResponse
        {
            Message = "An error occurred"
        };

        var json = JsonSerializer.Serialize(errorResponse);

        Assert.NotNull(json);
        Assert.Contains("\"message\":\"An error occurred\"", json);
    }

    [Fact]
    public void DeserializeErrorResponse_ValidJson_ReturnsErrorResponse()
    {
        const string json = "{\"message\":\"An error occurred\"}";

        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(errorResponse);
        Assert.Equal("An error occurred", errorResponse?.Message);
    }

    [Fact]
    public void DeserializeErrorResponse_InvalidJson_ReturnsNull()
    {
        const string json = "Invalid JSON";

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ErrorResponse>(json));
    }
}