namespace ApiAlerts.Common;

/// <summary>
/// Provides methods to configure and send alerts using the ApiAlerts service.
/// </summary>
public static class ApiAlerts
{
    private static readonly Lazy<IClient> DefaultClient = 
        new(() => new Client());

    /// <summary>
    /// Configures the ApiAlerts client with the specified API key and debug mode.
    /// </summary>
    /// <param name="apiKey">The default API key to use in all requests.</param>
    /// <param name="debug">Set true to enable debug logging.</param>
    public static void Configure(string apiKey, bool debug = false) =>
        DefaultClient.Value.Configure(apiKey, debug);

    /// <summary>
    /// Sends an alert synchronously in the background.
    /// </summary>
    /// <param name="apiKey">Optional API key override for a sinlge send request. If null, the default API key is used.</param>
    /// <param name="channel">The channel to send the alert to. If null, the default channel is used.</param>
    /// <param name="message">The message to send. Cannot be null or empty.</param>
    /// <param name="tags">A list of tags to include with the alert. If null, no tags are included.</param>
    /// <param name="link">A link to include with the alert. If null, no link is included.</param>
    public static void Send(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null, string? link = null)
    {
        Task.Run(() => DefaultClient.Value.SendAsync(apiKey, channel, message, tags, link)).Wait();
    }

    /// <summary>
    /// Sends an alert asynchronously and waits for the result. Useful in serverless or script environments. Serverside should use Send()
    /// </summary>
    /// <param name="apiKey">Optional API key override for a sinlge send request. If null, the default API key is used.</param>
    /// <param name="channel">The channel to send the alert to. If null, the default channel is used.</param>
    /// <param name="message">The message to send. Cannot be null or empty.</param>
    /// <param name="tags">A list of tags to include with the alert. If null, no tags are included.</param>
    /// <param name="link">A link to include with the alert. If null, no link is included.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SendAsync(string? apiKey = null, string? channel = null, string message = "", List<string>? tags = null, string? link = null)
    {
        await DefaultClient.Value.SendAsync(apiKey, channel, message, tags, link);
    }
        
}