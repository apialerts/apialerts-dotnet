using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAlerts.Common.Models
{
    public class ApiAlert
    {
        /// <summary>
        /// Required: true
        /// Limit: 500 characters
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Required: false
        /// Limit: Up to 10 tags at 50 characters per tag.
        /// Notes: Non compliant tags will be dropped from the event.
        /// </summary>
        public List<string> tags { get; set; }

        /// <summary>
        /// Required: true
        /// Limit: 1000 characters
        /// </summary>
        public string link { get; set; }

        public ApiAlert(string messageText, List<string> tagItems, string linkText)
        {
            message = messageText;
            tags = tagItems;
            link = linkText;
        }

        public ApiAlert(string messageText, List<string> tagItems)
        {
            message = messageText;
            tags = tagItems;
        }

        public ApiAlert(string messageText, string linkText)
        {
            message = messageText;
            link = linkText;
        }

        public ApiAlert(string messageText)
        {
            message = messageText;
        }

        public bool ValidateMessage()
        {
            return !string.IsNullOrWhiteSpace(message) && message.Length <= 500;
        }

        public bool ValidateTags()
        {
            if (tags != null && tags.Count > 0 && tags.Count < 11)
            {
                foreach (var tag in tags)
                {
                    if (!ValidateTag(tag))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool ValidateTag(string tag)
        {
            return !string.IsNullOrWhiteSpace(tag) && tag.Length <= 50;
        }

        public bool ValidateLink()
        {
            return !string.IsNullOrWhiteSpace(link) && link.Length <= 1000;
        }
    }
}