using System.Text.Json.Serialization;

namespace APIAlerts.network.contract;

internal class ErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
