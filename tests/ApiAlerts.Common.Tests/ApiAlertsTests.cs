using ApiAlerts.Common.models;
using ApiAlerts.Common.Tests.mocks;
using Xunit;

namespace ApiAlerts.Common.Tests;

public class ApiAlertsTests
{
    [Fact]
    public void Configure_SetsDefaultApiKeyAndDebugMode()
    {
        var mockClient = new MockClient();
        ApiAlerts.SetClient(mockClient);

        ApiAlerts.Configure("test-api-key", true);

        Assert.Equal("test-api-key", mockClient.ApiKey);
        Assert.True(mockClient.Debug);
    }

    [Fact]
    public void Send_ValidRequest_LogsSuccess()
    {
        var mockClient = new MockClient();
        ApiAlerts.SetClient(mockClient);

        var alert = new AlertEvent
        {
            Message = "test message"
        };
        ApiAlerts.Send(alert);
        Assert.Null(mockClient.SentApiKey);
        
        ApiAlerts.SendWithApiKey("test-api-key", alert);
        
        Assert.Equal("test-api-key", mockClient.SentApiKey);
        Assert.Null(mockClient.SentChannel);
        Assert.Equal("test message", mockClient.SentMessage);
        Assert.Null(mockClient.SentTags);
        Assert.Null(mockClient.SentLink);
    }

    [Fact]
    public async Task SendAsync_ValidRequest_LogsSuccess()
    {
        var mockClient = new MockClient();
        ApiAlerts.SetClient(mockClient);

        var alert = new AlertEvent
        {
            Message = "test message"
        };
        await ApiAlerts.SendAsync(alert);
        Assert.Null(mockClient.SentApiKey);

        await ApiAlerts.SendWithApiKeyAsync("test-api-key", alert);
        
        Assert.Equal("test-api-key", mockClient.SentApiKey);
        Assert.Null(mockClient.SentChannel);
        Assert.Equal("test message", mockClient.SentMessage);
        Assert.Null(mockClient.SentTags);
        Assert.Null(mockClient.SentLink);
    }
}