using ApiAlerts.Common.models;

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
    /// <param name="alert">The event to send. Cannot be null.</param>
    public static void Send(AlertEvent alert)
    {
        Task.Run(() => _defaultClient.Value.SendAsync(null, alert)).Wait();
    }
    
    /// <summary>
    /// Sends an event synchronously in the background.
    /// </summary>
    /// <param name="apiKey">API key override for a single send request.</param>
    /// <param name="alert">The event to send. Cannot be null.</param>
    public static void SendWithApiKey(string apiKey, AlertEvent alert)
    {
        Task.Run(() => _defaultClient.Value.SendAsync(apiKey, alert)).Wait();
    }

    /// <summary>
    /// Sends an event asynchronously and waits for the result. Useful in serverless or script environments. Serverside should use Send()
    /// </summary>
    /// <param name="alert">The event to send. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SendAsync(AlertEvent alert)
    {
        await _defaultClient.Value.SendAsync(null, alert);
    }

    /// <summary>
    /// Sends an event asynchronously and waits for the result. Useful in serverless or script environments. Serverside should use Send()
    /// </summary>
    /// <param name="apiKey">API key override for a single send request.</param>
    /// <param name="alert">The event to send. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SendWithApiKeyAsync(string apiKey, AlertEvent alert)
    {
        await _defaultClient.Value.SendAsync(apiKey, alert);
    }
        
}