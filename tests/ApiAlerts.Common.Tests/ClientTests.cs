using System.Net;
using ApiAlerts.Common.util;
using ApiAlerts.Common.Tests.mocks;
using Xunit;

namespace ApiAlerts.Common.Tests
{
    public class ClientTests
    {
        [Fact]
        public void Configure_SetsDefaultApiKey()
        {
            var client = new Client();
            const string apiKey = "test-api-key";

            client.Configure(apiKey);

            // Use reflection to access the private field _defaultApiKey
            var field = typeof(Client).GetField("_defaultApiKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var value = field?.GetValue(client) as string;

            Assert.Equal(apiKey, value);
        }

        [Fact]
        public async Task SendAsync_ValidRequest_LogsSuccess()
        {
            const HttpStatusCode statusCode = HttpStatusCode.OK;
            const string response = "{\"workspace\":\"my-workspace\",\"channel\":\"my-channel\",\"errors\":[\"no\",\"errors\",\"here\"],\"extra\":\"property\"}";
            var network = MockHttp.Client(statusCode, response);
            var client = new Client(network);
            client.Configure("test-api-key");

            var logger = new TestLogger();
            var loggerField = typeof(Client).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            loggerField?.SetValue(client, logger);

            await client.SendAsync(message: "test message");

            Assert.Contains("Alert sent to my-workspace (my-channel) successfully.", logger.Logs);
        }

        [Fact]
        public async Task SendAsync_MissingApiKey_LogsError()
        {
            var client = new Client();
            var logger = new TestLogger();
            var loggerField = typeof(Client).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            loggerField?.SetValue(client, logger);

            await client.SendAsync(message: "test message");

            Assert.Contains("API Key not provided. Use Configure() to set a default key, or pass the key as a parameter to the Send/SendAsync function.", logger.Logs);
        }

        [Fact]
        public async Task SendAsync_EmptyMessage_LogsError()
        {
            var client = new Client();
            client.Configure("test-api-key");

            var logger = new TestLogger();
            var loggerField = typeof(Client).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            loggerField?.SetValue(client, logger);

            await client.SendAsync(message: "");

            Assert.Contains("Message is required", logger.Logs);
        }

        [Fact]
        public async Task SendAsync_InvalidRequest_LogsError()
        {
            const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            const string response = "{\"message\":\"Bad Request\"}";
            var network = MockHttp.Client(statusCode, response);
            var client = new Client(network);
            client.Configure("test-api-key");

            var logger = new TestLogger();
            var loggerField = typeof(Client).GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            loggerField?.SetValue(client, logger);

            await client.SendAsync(message: "test message");

            Assert.Contains("Bad Request", logger.Logs);
        }
    }

    internal class TestLogger : Logger
    {
        public List<string> Logs { get; } = new();

        internal override void Error(string message) => Logs.Add(message);
        internal override void Success(string message) => Logs.Add(message);
        internal override void Warning(string message) => Logs.Add(message);
    }
}