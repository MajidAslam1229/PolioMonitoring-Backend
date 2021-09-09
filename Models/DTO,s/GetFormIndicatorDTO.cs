using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class GetFormIndicatorDTO
    {
        public int Id { get; set; }
        public string FormName { get; set; }

        public int FormId { get; set; }
        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }

        public string TehsilName { get; set; }
        public string UCCode { get; set; }

        public string UCName { get; set; }
        public string Area { get; set; }
        public string SiteName { get; set; }
        public int? HealthCenterId { get; set; }
        public int? DesignationId { get; set; }
        public int? TeamNo { get; set; }
        public string Category { get; set; }
        public string Dayofwork { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorAgency { get; set; }
        public string AICName { get; set; }
        public string Name { get; set; }
        public string Designationofmonitor { get; set; }
        public string TypeofPost { get; set; }
        public string Village_Mohallaha_St { get; set; }
        public string PopulationType { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public List<FormsIndicatorDetailDTO> formsIndicatorDetailDTOs { get; set; }
    }
}
