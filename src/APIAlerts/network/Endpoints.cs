using APIAlerts.network.contract;
using APIAlerts.util;

namespace APIAlerts.network;

internal class Endpoints
{
    private readonly Network _network;

    internal Endpoints(HttpClient? httpClient = null)
    {
        _network = new Network(httpClient);
    }

    internal async Task<Result<EventResponse>> SendEvent(string apiKey, Alert model)
    {
        var payload = new EventRequest
        {
            Channel = model.Channel,
            Message = model.Message,
            Tags = model.Tags,
            Link = model.Link
        };
        return await _network.ApiRequest<EventResponse>(apiKey, HttpMethod.Post, "/event", payload);
    }
}