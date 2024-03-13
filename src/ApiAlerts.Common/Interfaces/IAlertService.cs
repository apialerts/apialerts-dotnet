using ApiAlerts.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAlerts.Common.Interfaces
{
    public interface IAlertService
    {
        /// <summary>
        /// Adds the default api key to the service
        /// </summary>
        /// <param name="apiKey">Default Api Key to be used on all non overriden calls</param>
        public void Activate(string apiKey);

        /// <summary>
        /// Async call to trigger an alert
        /// </summary>
        /// <param name="alert">Data to send in the alert request</param>
        public Task<ApiAlertResponse> PublishAlertAsync(ApiAlert alert);

        /// <summary>
        /// Non Async call to trigger an alert
        /// </summary>
        /// <param name="alert">Data to send in the alert request</param>
        public void PublishAlert(ApiAlert alert);

        /// <summary>
        /// Async call to trigger an alert
        /// </summary>
        /// <param name="apiKey">Api Key to use as an override</param>
        /// <param name="alert">Data to send in the alert request</param>
        /// <returns></returns>
        public Task<ApiAlertResponse> PublishAlertAsync(string apiKey, ApiAlert alert);

        /// <summary>
        /// Non Async call to trigger an alert
        /// </summary>
        /// <param name="apiKey">Api Key to use as an override</param>
        /// <param name="alert">Data to send in the alert request</param>
        /// <returns></returns>
        public void PublishAlert(string apiKey, ApiAlert alert);
    }
}
