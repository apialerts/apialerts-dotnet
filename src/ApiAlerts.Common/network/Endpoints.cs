using ApiAlerts.Common.models;
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

    internal async Task<Result<EventResponse>> SendEvent(string apiKey, AlertEvent model)
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