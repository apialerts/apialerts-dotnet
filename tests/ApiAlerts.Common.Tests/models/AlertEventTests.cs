using ApiAlerts.Common.models;
using Xunit;

namespace ApiAlerts.Common.Tests.models;

public class AlertEventTests
{
    [Fact]
    public void AlertEvent_DefaultValues_ShouldBeSetCorrectly()
    {
        var alertEvent = new AlertEvent();

        Assert.Equal(string.Empty, alertEvent.Message);
        Assert.Null(alertEvent.Channel);
        Assert.Null(alertEvent.Link);
        Assert.Null(alertEvent.Tags);
    }
    [Fact]
    public void AlertEvent_DefaultValues_OnlyMessage()
    {
        const string message = "hello";
        
        var alertEvent = new AlertEvent
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

        var alertEvent = new AlertEvent
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