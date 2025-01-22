namespace APIAlerts;

/// <summary>
/// Represents an event to be sent.
/// </summary>
public class Alert
{
    /// <summary>
    /// Gets or sets the message to send. Cannot be null or empty.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the channel to send the alert to. If null, the default channel is used.
    /// </summary>
    public string? Channel { get; init; }

    /// <summary>
    /// Gets or sets a link to include with the event. If null, no link is included.
    /// </summary>
    public string? Link { get; init; }

    /// <summary>
    /// Gets or sets a list of tags to include with the event. If null, no tags are included.
    /// </summary>
    public List<string>? Tags { get; init; }
}
