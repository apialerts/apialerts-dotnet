using System.Text.Json.Serialization;

namespace ApiAlerts.Common.network.contract;

internal class EventResponse
{
    [JsonPropertyName("workspace")]
    public string? Workspace { get; set; }
    
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }
    
    [JsonPropertyName("errors")]
    public List<string>? Errors { get; set; }
}