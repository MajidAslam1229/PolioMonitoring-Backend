using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class Designation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DesignationLvl { get; set; }
        public string Designation1 { get; set; }
        public int? OrganizationId { get; set; }
        public int? NoOfStaff { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
