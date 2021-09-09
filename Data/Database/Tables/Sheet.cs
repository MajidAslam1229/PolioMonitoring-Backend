using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class Sheet
    {
        public string DistID { get; set; }
        public string TehsilID { get; set; }
        public string F3 { get; set; }
        public string UCID { get; set; }
        public double? Area_Incharge { get; set; }
        public double? Medical_Officer_ZS { get; set; }
        public string F7 { get; set; }
        public string F8 { get; set; }
    }
}
