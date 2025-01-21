namespace ApiAlerts.Common;

/// <summary>
/// Provides methods to configure and send event using the ApiAlerts service.
/// Implements the singleton pattern.
/// </summary>
public static class ApiAlerts
{
    private static Lazy<IClient> _defaultClient = new(() => new Client());
    
    /// <summary>
    /// Sets a custom client for testing purposes.
    /// </summary>
    /// <param name="client">The custom client to use.</param>
    internal static void SetClient(IClient client) =>
        _defaultClient = new Lazy<IClient>(() => client);

    /// <summary>
    /// Configures the ApiAlerts client with the specified API key and debug mode.
    /// </summary>
    /// <param name="apiKey">The default API key to use in all requests.</param>
    /// <param name="debug">Set true to enable debug logging.</param>
    public static void Configure(string apiKey, bool debug = false) =>
        _defaultClient.Value.Configure(apiKey, debug);

    /// <summary>
    /// Sends an event synchronously in the background.
    /// </summary>
    /// <param name="apiKey">Optional API key override for a single send request. If null, the default API key is used.</param>
    /// <param name="channel">The channel to send the alert to. If null, the default channel is used.</param>
    /// <param name="message">The message to send. Cannot be null or empty.</param>
    /// <param name="tags">A list of tags to include with the event. If null, no tags are included.</param>
    /// <param name="link">A link to include with the event. If null, no link is included.</param>
    public static void Send(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null, string? link = null)
    {
        Task.Run(() => _defaultClient.Value.SendAsync(apiKey, channel, message, tags, link)).Wait();
    }

    /// <summary>
    /// Sends an event asynchronously and waits for the result. Useful in serverless or script environments. Serverside should use Send()
    /// </summary>
    /// <param name="apiKey">Optional API key override for a single send request. If null, the default API key is used.</param>
    /// <param name="channel">The workspace channel to send the alert to. If null, the default channel is used.</param>
    /// <param name="message">The message to send. Cannot be null or empty.</param>
    /// <param name="tags">A list of tags to include with the event. If null, no tags are included.</param>
    /// <param name="link">A link to include with the event. If null, no link is included.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SendAsync(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null, string? link = null)
    {
        await _defaultClient.Value.SendAsync(apiKey, channel, message, tags, link);
    }
        
}