using System.Text.Json;
using System.Text.Json.Serialization;

namespace APIAlerts.network;

internal static class Json
{
    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
