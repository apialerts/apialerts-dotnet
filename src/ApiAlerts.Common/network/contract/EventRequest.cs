using System.Text.Json.Serialization;

namespace ApiAlerts.Common.network.contract;

internal class EventRequest
{
    [JsonPropertyName("channel")]
    internal string? Channel { get; set; }
    
    [JsonPropertyName("message")]
    internal string Message { get; set; } = string.Empty;
    
    [JsonPropertyName("link")]
    internal string? Link { get; set; }
    
    [JsonPropertyName("tags")]
    internal List<string>? Tags { get; set; }
}