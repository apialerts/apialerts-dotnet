using System.Text.Json.Serialization;

namespace ApiAlerts.Common.network.contract;

internal class ErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
