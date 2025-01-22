using System.Text.Json.Serialization;

namespace APIAlerts.network.contract;

internal class EventRequest
{
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    
    [JsonPropertyName("link")]
    public string? Link { get; set; }
    
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}