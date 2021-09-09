using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class IndicatorAnswers
    {
        public int Id { get; set; }
        public int FormIndicatorDetailId { get; set; }
        public int AnswerId { get; set; }
        public string AnswerDesc { get; set; }
        public bool RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual FormsIndicatorDetails FormIndicatorDetail { get; set; }
    }
}
