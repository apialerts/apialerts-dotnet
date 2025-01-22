using APIAlerts.service;

namespace APIAlerts;

/// <summary>
/// Provides methods to configure and send event using the ApiAlerts service.
/// Implements the singleton pattern.
/// </summary>
public static class Client
{
    private static Lazy<IService> _defaultClient = new(() => new Service());
    
    /// <summary>
    /// Sets a custom client for testing purposes.
    /// </summary>
    /// <param name="service">The custom client to use.</param>
    internal static void SetClient(IService service) =>
        _defaultClient = new Lazy<IService>(() => service);

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
    /// <param name="alerts">The event to send. Cannot be null.</param>
    public static void Send(Alert alerts)
    {
        Task.Run(() => _defaultClient.Value.SendAsync(null, alerts)).Wait();
    }
    
    /// <summary>
    /// Sends an event synchronously in the background.
    /// </summary>
    /// <param name="apiKey">API key override for a single send request.</param>
    /// <param name="alerts">The event to send. Cannot be null.</param>
    public static void SendWithApiKey(string apiKey, Alert alerts)
    {
        Task.Run(() => _defaultClient.Value.SendAsync(apiKey, alerts)).Wait();
    }

    /// <summary>
    /// Sends an event asynchronously and waits for the result. Useful in serverless or script environments. Serverside should use Send()
    /// </summary>
    /// <param name="alerts">The event to send. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SendAsync(Alert alerts)
    {
        await _defaultClient.Value.SendAsync(null, alerts);
    }

    /// <summary>
    /// Sends an event asynchronously and waits for the result. Useful in serverless or script environments. Serverside should use Send()
    /// </summary>
    /// <param name="apiKey">API key override for a single send request.</param>
    /// <param name="alerts">The event to send. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SendWithApiKeyAsync(string apiKey, Alert alerts)
    {
        await _defaultClient.Value.SendAsync(apiKey, alerts);
    }
        
}