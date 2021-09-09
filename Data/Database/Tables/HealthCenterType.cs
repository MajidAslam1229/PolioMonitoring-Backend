using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class HealthCenterType
    {
        public HealthCenterType()
        {
            FormsIndicatorMaster = new HashSet<FormsIndicatorMaster>();
        }

        public int Id { get; set; }
        public string HealthCenterName { get; set; }

        public virtual ICollection<FormsIndicatorMaster> FormsIndicatorMaster { get; set; }
    }
}
