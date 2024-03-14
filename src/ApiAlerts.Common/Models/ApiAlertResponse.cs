using ApiUtilities.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAlerts.Common.Models
{
    public class ApiAlertResponse : BaseResponse
    {
        public string project { get; set; }
        public int remainingQuota { get; set; }
        public List<string> errors { get; set; }
    }
}
