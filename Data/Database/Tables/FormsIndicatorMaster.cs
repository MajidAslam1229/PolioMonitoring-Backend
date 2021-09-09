using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class FormsIndicatorMaster
    {
        public int Id { get; set; }
        public int? FormId { get; set; }
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public string UCCode { get; set; }
        public string Area { get; set; }
        public string SiteName { get; set; }
        public int? HealthCenterId { get; set; }
        public string DesignationId { get; set; }
        public int? TeamNo { get; set; }
        public string Dayofwork { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorAgency { get; set; }
        public string AICName { get; set; }
        public string Supervisordesignation { get; set; }
        public string Name { get; set; }
        public string designationofmonitor { get; set; }
        public string NameofPost { get; set; }
        public string TypeofPost { get; set; }
        public string Village_Mohallaha_St { get; set; }
        public string PopulationType { get; set; }
        public string Member1 { get; set; }
        public string Member2 { get; set; }
        public string Type { get; set; }
        public int? CampaignId { get; set; }
        public string Category { get; set; }
        public bool? RecordStatus { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual MonitoringForms Form { get; set; }
        public virtual HealthCenterType HealthCenter { get; set; }
    }
}
