using ApiAlerts.Common.Configurations;
using ApiAlerts.Common.Interfaces;
using ApiAlerts.Common.Services;
using ApiUtilities.Common;
using ApiUtilities.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAlerts.Common
{
    public class RegistrationContainer : BaseRegistrationContainer
    {
        public RegistrationContainer(IServiceCollection collection) : base(collection)
        {
            collection.AddSingleton<IApiConfig, ApiConfig>();
        }

        public override void ExtendRegistration(IServiceCollection collection)
        {
            base.ExtendRegistration(collection);
            collection.AddSingleton<IAlertService, AlertService>();
        }
    }
}
