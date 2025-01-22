using APIAlerts.network;
using APIAlerts.util;

namespace APIAlerts.service;

internal interface IService
{
    void Configure(string apiKey, bool debug = false);
    Task SendAsync(string? apiKey, Alert model);
}

internal class Service : IService
{
    private readonly Endpoints _endpoints;
    private string? _defaultApiKey;
    private readonly Logger _logger = new();

    internal Service(HttpClient? httpClient = null)
    {
        _endpoints = new Endpoints(httpClient);
    }

    public void Configure(string apiKey, bool debug = false)
    {
        _defaultApiKey = apiKey;
        _logger.Configure(debug);
    }

    public async Task SendAsync(string? apiKey, Alert model)
    {
        var useKey = apiKey ?? _defaultApiKey;

        if (string.IsNullOrEmpty(useKey))
        {
            _logger.Error("API Key not provided. Use Configure() to set a default key, or pass the key as a parameter to the Send/SendAsync function.");
            return;
        }

        if (string.IsNullOrWhiteSpace(model.Message))
        {
            _logger.Error("Message is required");
            return;
        }

        var result = await _endpoints.SendEvent(useKey, model);
        if (result.IsSuccess)
        {
            _logger.Success($"Alert sent to {result.Data?.Workspace ?? "?"} ({result.Data?.Channel ?? "?"}) successfully.");
            var errors = result.Data?.Errors ?? new List<string>();
            foreach (var error in errors)
            {
                _logger.Warning(error);
            }
            return;
        }
        
        _logger.Error(result.Error?.Message ?? "Unknown error");
    }
}