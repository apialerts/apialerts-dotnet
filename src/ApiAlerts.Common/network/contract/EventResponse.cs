using System.Text.Json.Serialization;

namespace ApiAlerts.Common.network.contract;

internal class EventResponse
{
    [JsonPropertyName("workspace")]
    internal string? Workspace { get; set; }
    
    [JsonPropertyName("channel")]
    internal string? Channel { get; set; }
    
    [JsonPropertyName("errors")]
    internal List<string>? Errors { get; set; }
}