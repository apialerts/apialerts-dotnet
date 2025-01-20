using ApiAlerts.Common.network.contract;
using ApiAlerts.Common.util;

namespace ApiAlerts.Common.network;

internal class Endpoints
{
    private readonly Network _network;

    internal Endpoints(HttpClient? httpClient = null)
    {
        _network = new Network(httpClient);
    }

    internal async Task<Result<EventResponse>> SendEvent(string apiKey, string? channel, string message,
        List<string>? tags, string? link)
    {
        var payload = new EventRequest
        {
            Channel = channel,
            Message = message,
            Tags = tags,
            Link = link
        };
        return await _network.ApiRequest<EventResponse>(apiKey, HttpMethod.Post, "/event", payload);
    }
}