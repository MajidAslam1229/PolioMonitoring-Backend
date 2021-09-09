using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class FormsIndicatorDetails
    {
        public FormsIndicatorDetails()
        {
            IndicatorAnswers = new HashSet<IndicatorAnswers>();
        }

        public int Id { get; set; }
        public int? FormsIndicatorMasterId { get; set; }
        public int? FormsIndicatorId { get; set; }
        public int? SubIdDetail { get; set; }
        public string Comments { get; set; }
        public bool? RecordStatus { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual FormsIndicator FormsIndicator { get; set; }
        public virtual ICollection<IndicatorAnswers> IndicatorAnswers { get; set; }
    }
}
