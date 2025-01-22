using APIAlerts.network.contract;
using APIAlerts.util;
using Xunit;

namespace APIAlerts.Tests.util
{
    public class ResultTests
    {
        [Fact]
        public void Success_ReturnsResultWithData()
        {
            const string data = "Test data";

            var result = Result<string>.Success(data);

            Assert.True(result.IsSuccess);
            Assert.Equal(data, result.Data);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Failure_ReturnsResultWithError()
        {
            var error = new ErrorResponse { Message = "Test error" };

            var result = Result<string>.Failure(error);

            Assert.False(result.IsSuccess);
            Assert.Equal(error, result.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Success_ReturnsResultWithCorrectType()
        {
            const int data = 123;

            var result = Result<int>.Success(data);

            Assert.True(result.IsSuccess);
            Assert.Equal(data, result.Data);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Failure_ReturnsResultWithCorrectErrorType()
        {
            var error = new ErrorResponse { Message = "Another test error" };

            var result = Result<int>.Failure(error);

            Assert.False(result.IsSuccess);
            Assert.Equal(error, result.Error);
        }
    }
}