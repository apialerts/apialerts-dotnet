using ApiUtilities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAlerts.Common.Configurations
{
    public class ApiConfig : IApiConfig
    {
        public string BaseUrl { get => Constants.BaseUrl; set => Console.WriteLine("Base Url Set"); }
    }
}
