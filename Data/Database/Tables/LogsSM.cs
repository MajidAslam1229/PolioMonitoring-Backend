using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class LogsSM
    {
        public int Id { get; set; }
        public int? SMS_Session_Id { get; set; }
        public string MessageId { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public int? FKId { get; set; }
        public string Status { get; set; }
        public string Mask { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? RecordStatus { get; set; }
    }
}
