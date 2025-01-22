using APIAlerts.service;

namespace APIAlerts.Tests.mocks;

internal class MockService : IService
{
    public string? ApiKey { get; private set; }
    public bool Debug { get; private set; }
    public string? SentApiKey { get; private set; }
    public string? SentChannel { get; private set; }
    public string? SentMessage { get; private set; }
    public List<string>? SentTags { get; private set; }
    public string? SentLink { get; private set; }
    
    public void Configure(string apiKey, bool debug = false)
    {
        ApiKey = apiKey;
        Debug = debug;
    }

    public Task SendAsync(string? apiKey, Alert alerts)
    {
        SentApiKey = apiKey;
        SentChannel = alerts.Channel;
        SentMessage = alerts.Message;
        SentTags = alerts.Tags;
        SentLink = alerts.Link;
        return Task.CompletedTask;
    }
}
