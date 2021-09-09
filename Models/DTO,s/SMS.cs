using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class SMS
    {
        public string MobileNumber { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public string Status { get; set; }
        public string Mask { get; set; }
        public int FKId { get; set; }
        public string UserId { get; set; }
        public string Remarks { get; set; }
    }
}
