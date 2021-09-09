using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class ApplocationsUCCopy
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string UCNumber { get; set; }
        public string Name { get; set; }
        public int? UCCMO { get; set; }
        public int? AIC { get; set; }
        public string RecordStatus { get; set; }
    }
}
