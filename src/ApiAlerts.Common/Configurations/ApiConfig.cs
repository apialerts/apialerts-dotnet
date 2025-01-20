using ApiUtilities.Common.Interfaces;

namespace ApiAlerts.Common.Configurations
{
    public class ApiConfig : IApiConfig
    {
        public string BaseUrl { get => Constants.BaseUrl; set => Console.WriteLine("Base Url Set"); }
    }
}
