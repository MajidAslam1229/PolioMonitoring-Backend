using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models
{
    public class LogsSMSSession
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public Nullable<System.DateTime> LastActivityTime { get; set; }
        public Nullable<System.DateTime> RequestDateTime { get; set; }
        public string RequestedBy { get; set; }
    }
}
