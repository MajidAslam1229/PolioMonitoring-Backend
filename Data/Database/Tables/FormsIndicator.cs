using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class FormsIndicator
    {
        public FormsIndicator()
        {
            FormsIndicatorDetails = new HashSet<FormsIndicatorDetails>();
            IndicatorOptions = new HashSet<IndicatorOptions>();
        }

        public int Id { get; set; }
        public int? OrderNo { get; set; }
        public int? ParentIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string Type { get; set; }
        public string InputType { get; set; }
        public bool? Isrequired { get; set; }
        public bool? Comments { get; set; }
        public string IndicatorCategory { get; set; }
        public int? FormId { get; set; }
        public bool? HavingSubIndicator { get; set; }
        public string SubIndicatorDependency { get; set; }
        public int? CampaignId { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual MonitoringForms Form { get; set; }
        public virtual ICollection<FormsIndicatorDetails> FormsIndicatorDetails { get; set; }
        public virtual ICollection<IndicatorOptions> IndicatorOptions { get; set; }
    }
}
