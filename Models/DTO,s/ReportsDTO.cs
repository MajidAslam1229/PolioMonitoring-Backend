using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class ReportsDTO
    {
        public class MobilieTeammonitoringReport
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string DistrictName { get; set; }
            public string TehsilName { get; set; }

            public string UCNumber { get; set; }

            public string Designation { get; set; }

            public string Count { get; set; }

        }

    }
}
