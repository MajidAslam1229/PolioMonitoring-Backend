using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class AppLocationsUCBackUp
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string UCNumber { get; set; }
        public string Name { get; set; }
        public int? UCMO { get; set; }
        public int? AIC { get; set; }
        public int? UCPO { get; set; }
        public int? UCSP { get; set; }
        public int? UCCO { get; set; }
        public int? SM { get; set; }
        public string RecordStatus { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
    }
}
