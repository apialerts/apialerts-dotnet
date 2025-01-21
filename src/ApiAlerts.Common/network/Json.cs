using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiAlerts.Common.network;

public static class Json
{
    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
