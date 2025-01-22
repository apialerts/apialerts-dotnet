using Xunit;

namespace APIAlerts.Tests;

public class AlertTests
{
    [Fact]
    public void AlertEvent_DefaultValues_ShouldBeSetCorrectly()
    {
        var alertEvent = new Alert();

        Assert.Equal(string.Empty, alertEvent.Message);
        Assert.Null(alertEvent.Channel);
        Assert.Null(alertEvent.Link);
        Assert.Null(alertEvent.Tags);
    }
    [Fact]
    public void AlertEvent_DefaultValues_OnlyMessage()
    {
        const string message = "hello";
        
        var alertEvent = new Alert
        {
            Message = message
        };

        Assert.Equal(message, alertEvent.Message);
        Assert.Null(alertEvent.Channel);
        Assert.Null(alertEvent.Link);
        Assert.Null(alertEvent.Tags);
    }

    [Fact]
    public void AlertEvent_SetValues_ShouldBeSetCorrectly()
    {
        const string message = "Test message";
        const string channel = "Test channel";
        const string link = "https://example.com";
        var tags = new List<string> { "tag1", "tag2" };

        var alertEvent = new Alert
        {
            Message = message,
            Channel = channel,
            Link = link,
            Tags = tags
        };

        Assert.Equal(message, alertEvent.Message);
        Assert.Equal(channel, alertEvent.Channel);
        Assert.Equal(link, alertEvent.Link);
        Assert.Equal(tags, alertEvent.Tags);
    }
}