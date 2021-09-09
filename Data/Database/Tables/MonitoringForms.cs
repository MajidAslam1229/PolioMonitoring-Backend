using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class MonitoringForms
    {
        public MonitoringForms()
        {
            FormsIndicator = new HashSet<FormsIndicator>();
            FormsIndicatorMaster = new HashSet<FormsIndicatorMaster>();
        }

        public int Id { get; set; }
        public string FormName { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<FormsIndicator> FormsIndicator { get; set; }
        public virtual ICollection<FormsIndicatorMaster> FormsIndicatorMaster { get; set; }
    }
}
