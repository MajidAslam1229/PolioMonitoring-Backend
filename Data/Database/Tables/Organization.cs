using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class Organization
    {
        public Organization()
        {
            Designation = new HashSet<Designation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDae { get; set; }

        public virtual ICollection<Designation> Designation { get; set; }
    }
}
