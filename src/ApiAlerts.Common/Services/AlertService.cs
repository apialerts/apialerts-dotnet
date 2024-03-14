using ApiAlerts.Common.Interfaces;
using ApiAlerts.Common.Models;
using ApiUtilities.Common.Handlers;
using ApiUtilities.Common.Interfaces;
using ApiUtilities.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ApiAlerts.Common.Services
{
    public class AlertService : BaseService, IAlertService
    {
        private readonly IRequestHandler _requestHandler;
        private string defaultApiKey { get; set; }

        public AlertService(IApiConfig apiConfig, IRequestHandler requestHandler) : base(apiConfig, requestHandler)
        {
            _requestHandler = requestHandler;
        }

        public void Activate(string apiKey)
        {
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                defaultApiKey = apiKey;
                _requestHandler.AddHeader("Authorization", $"Bearer {defaultApiKey}");
                _requestHandler.AddHeader("X-Intergration", "dotnet");
                _requestHandler.AddHeader("X-Version", "1.0.0");
            }
        }

        public void PublishAlert(ApiAlert alert)
        {
            if (alert != null && alert.ValidateMessage() && alert.ValidateLink() && !string.IsNullOrWhiteSpace(defaultApiKey))
            {
                _requestHandler.AddHeader("Authorization", $"Bearer {defaultApiKey}");
                Post<ApiAlertResponse>("event", alert, true).ConfigureAwait(false);
            }
        }

        public void PublishAlert(string apiKey, ApiAlert alert)
        {
            if (alert != null && alert.ValidateMessage() && alert.ValidateLink())
            {
                _requestHandler.AddHeader("Authorization", $"Bearer {apiKey}");
                Post<ApiAlertResponse>("event", alert, true).ConfigureAwait(false);
            }
        }

        public async Task<ApiAlertResponse> PublishAlertAsync(ApiAlert alert)
        {
            if(alert != null && alert.ValidateMessage() && alert.ValidateLink() && !string.IsNullOrWhiteSpace(defaultApiKey))
            {
                _requestHandler.AddHeader("Authorization", $"Bearer {defaultApiKey}");
                var response = await Post<ApiAlertResponse>("event", alert, true);
                return response.Data;
            }
            return null;
        }

        public async Task<ApiAlertResponse> PublishAlertAsync(string apiKey, ApiAlert alert)
        {
            if (alert != null && alert.ValidateMessage() && alert.ValidateLink())
            {
                _requestHandler.AddHeader("Authorization", $"Bearer {apiKey}");
                var response = await Post<ApiAlertResponse>("event", alert, true);
                return response.Data;
            }
            return null;
        }
    }
}
