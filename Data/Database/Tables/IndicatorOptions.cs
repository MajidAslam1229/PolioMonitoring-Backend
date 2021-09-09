using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class IndicatorOptions
    {
        public int Id { get; set; }
        public int? IndicatorId { get; set; }
        public string Label { get; set; }
        public string InputType { get; set; }
        public bool? ForSubindicator { get; set; }
        public bool? ForComments { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual FormsIndicator Indicator { get; set; }
    }
}
