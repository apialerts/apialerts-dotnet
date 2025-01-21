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

        ApiAlerts.Send(message: "test message");

        Assert.Null(mockClient.SentApiKey);
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

        await ApiAlerts.SendAsync(message: "test message");

        Assert.Null(mockClient.SentApiKey);
        Assert.Null(mockClient.SentChannel);
        Assert.Equal("test message", mockClient.SentMessage);
        Assert.Null(mockClient.SentTags);
        Assert.Null(mockClient.SentLink);
    }
}