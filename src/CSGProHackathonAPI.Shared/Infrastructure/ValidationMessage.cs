using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Infrastructure
{
    public class ValidationMessage
    {
        public ValidationMessage(string message)
            : this(null, message)
        {
        }

        public ValidationMessage(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
