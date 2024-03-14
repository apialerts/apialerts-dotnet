using ApiAlerts.Common.Configurations;
using ApiAlerts.Common.Services;
using ApiUtilities.Common.Handlers;
using ApiUtilities.Common.Interfaces;
using Moq;
using Xunit;

namespace ApiAlerts.Tests.Services
{
    public class AlertService_Tests
    {
        private Mock<IApiConfig> ApiConfigMock { get; set; }
        private Mock<IRequestHandler> RequestHandlerMock { get; set; }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Activate_WhenApiKeyIsInvalid_ShouldNotAddHeader(string apikey)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(apikey);
            RequestHandlerMock.Verify(a => a.AddHeader(It.IsAny<string>(),It.IsAny<string>()),Times.Never);
        }

        [Theory]
        [InlineData("AAAAA-AAAAA-AAAAA-AAAAA")]
        [InlineData("11111-11111-11111-11111")]
        public void Activate_WhenApiKeyIsValid_ShouldAddHeader(string apikey)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(apikey);
            RequestHandlerMock.Verify(a => a.AddHeader(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        //Publish Alert
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void PublishAlert_WhenApiKeyIsInvalid_AndApiAlertIsInvalid_ShouldNotCallPostAsync(string apikey)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(apikey);
            alertService.PublishAlert(new Common.Models.ApiAlert(string.Empty, string.Empty));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Theory]
        [InlineData("Message","Link")]
        [InlineData("Message Two","Link Two")]
        [InlineData("Message Three Words", "Link Two Words")]
        public void PublishAlert_WhenApiKeyIsValid_AndApiAlertIsValid_ShouldCallPostAsync(string message, string link)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate("AAAAA-AAAAA-AAAAA-AAAAA");
            alertService.PublishAlert(new Common.Models.ApiAlert(message, link));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Theory]
        [InlineData("Message", "Link")]
        [InlineData("Message Two", "Link Two")]
        [InlineData("Message Three Words", "Link Two Words")]
        public void PublishAlert_WhenApiKeyIsInvalid_AndApiAlertIsValid_ShouldNotCallPostAsync(string message, string link)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(string.Empty);
            alertService.PublishAlert(new Common.Models.ApiAlert(message, link));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Theory]
        [InlineData("AAAAA-AAAAA-AAAAA-AAAAA")]
        [InlineData("11111-11111-11111-11111")]
        public void PublishAlert_WhenApiKeyIsValid_AndApiAlertIsInvalid_ShouldNotCallPostAsync(string apikey)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(apikey);
            alertService.PublishAlert(new Common.Models.ApiAlert(string.Empty, string.Empty));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        //Publish Alert Async
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task PublishAlertAsync_WhenApiKeyIsInvalid_AndApiAlertIsInvalid_ShouldNotCallPostAsync(string apikey)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(apikey);
            await alertService.PublishAlertAsync(new Common.Models.ApiAlert(string.Empty, string.Empty));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Theory]
        [InlineData("Message", "Link")]
        [InlineData("Message Two", "Link Two")]
        [InlineData("Message Three Words", "Link Two Words")]
        public async Task PublishAlertAsync_WhenApiKeyIsValid_AndApiAlertIsValid_ShouldCallPostAsync(string message, string link)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate("AAAAA-AAAAA-AAAAA-AAAAA");
            await alertService.PublishAlertAsync(new Common.Models.ApiAlert(message, link));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Theory]
        [InlineData("Message", "Link")]
        [InlineData("Message Two", "Link Two")]
        [InlineData("Message Three Words", "Link Two Words")]
        public async Task PublishAlertAsync_WhenApiKeyIsInvalid_AndApiAlertIsValid_ShouldNotCallPostAsync(string message, string link)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(string.Empty);
            await alertService.PublishAlertAsync(new Common.Models.ApiAlert(message, link));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Theory]
        [InlineData("AAAAA-AAAAA-AAAAA-AAAAA")]
        [InlineData("11111-11111-11111-11111")]
        public async Task PublishAlertAsync_WhenApiKeyIsValid_AndApiAlertIsInvalid_ShouldNotCallPostAsync(string apikey)
        {
            ApiConfigMock = new Mock<IApiConfig>();
            RequestHandlerMock = new Mock<IRequestHandler>();

            var alertService = new AlertService(ApiConfigMock.Object, RequestHandlerMock.Object);
            alertService.Activate(apikey);
            await alertService.PublishAlertAsync(new Common.Models.ApiAlert(string.Empty, string.Empty));
            RequestHandlerMock.Verify(a => a.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }
    }
}