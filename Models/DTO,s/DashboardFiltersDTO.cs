using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class DashboardFiltersDTO
    {
        public class FilterDTO
        {
            public string FilterLvl { get; set; } 
            public string Code { get; set; }
            public int campaignId { get; set; }    
            public string day { get; set; }
            public string Organization { get; set; }
            public string Designation { get; set; }
            public string PopulationType { get; set; }
            public string ModuleWise { get; set; }
            public string ReasonForMissedType { get; set; }

        }

        public class ReportFilterDTO
        {
            public string FilterLvl { get; set; }
            public int IndicatorId { get; set; }
            public int OffSet { get; set; }
            public int RowLimit { get; set; }
            public string Code { get; set; }
            public string DivisionCode { get; set; }
            public string DistrictCode { get; set; }
            public string TehsilCode { get; set; }
            public string UCCode { get; set; }
            public int campaignId { get; set; }
            public string day { get; set; }
            public string Organization { get; set; }
            public string Designation { get; set; }
            public string PopulationType { get; set; }
            public int FormId { get; set; }
            public int ReportType { get; set; }

            public string UserType { get; set; }

        }


    }
}
