using ApiAlerts.Common.network;
using ApiAlerts.Common.util;

namespace ApiAlerts.Common;

internal interface IClient
{
    internal void Configure(string apiKey, bool debug = false);
    internal Task SendAsync(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null, string? link = null);
}

internal class Client : IClient
{
    private readonly Endpoints _endpoints;
    private string? _defaultApiKey;
    private readonly Logger _logger = new();

    internal Client(HttpClient? httpClient = null)
    {
        _endpoints = new Endpoints(httpClient);
    }

    public void Configure(string apiKey, bool debug = false)
    {
        _defaultApiKey = apiKey;
        _logger.Configure(debug);
    }

    public async Task SendAsync(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null, string? link = null)
    {
        var useKey = apiKey ?? _defaultApiKey;

        if (string.IsNullOrEmpty(useKey))
        {
            _logger.Error("API Key not provided. Use Configure() to set a default key, or pass the key as a parameter to the Send/SendAsync function.");
            return;
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            _logger.Error("Message is required");
            return;
        }

        var result = await _endpoints.SendEvent(useKey, channel, message, tags, link);
        if (result.IsSuccess)
        {
            _logger.Success($"Alert sent to {result.Data?.Workspace ?? "?"} ({result.Data?.Channel ?? "?"}) successfully.");
            var errors = result.Data?.Errors ?? new List<string>();
            foreach (var error in errors)
            {
                _logger.Warning(error);
            }
        }
        else
        {
            _logger.Error(result.Error?.Message ?? "Unknown error");
        }
    }
}