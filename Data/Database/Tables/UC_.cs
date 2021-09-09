using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class UC_
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string UCNumber { get; set; }
        public int? UCMO { get; set; }
        public int? AIC { get; set; }
        public int? UCPOs { get; set; }
        public int? UCSP { get; set; }
        public int? UCCO { get; set; }
        public int? SMs { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
    }
}
