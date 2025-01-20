using ApiUtilities.Common.Models;

namespace ApiAlerts.Common.Models
{
    public class ApiAlertResponse : BaseResponse
    {
        public string project { get; set; }
        public int remainingQuota { get; set; }
        public List<string> errors { get; set; }
    }
}
