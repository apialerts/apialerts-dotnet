using Xunit;

namespace APIAlerts.Tests
{
    public class ConstantsTests
    {
        [Fact]
        public void BaseUrl_ShouldBeCorrect()
        {
            Assert.Equal("https://api.apialerts.com", Constants.BaseUrl);
        }

        [Fact]
        public void IntegrationName_ShouldBeCorrect()
        {
            Assert.Equal("dotnet", Constants.IntegrationName);
        }

        [Fact]
        public void Version_ShouldBeValidSemver()
        {
            const string semverPattern = @"^\d+\.\d+\.\d+(-\w+(\.\w+)*)?$";
            Assert.Matches(semverPattern, Constants.Version);
        }
    }
}