namespace ApiAlerts.Common.Tests.mocks;

internal class MockClient : IClient
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

    public Task SendAsync(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null,
        string? link = null)
    {
        SentApiKey = apiKey;
        SentChannel = channel;
        SentMessage = message;
        SentTags = tags;
        SentLink = link;
        return Task.CompletedTask;
    }
}
