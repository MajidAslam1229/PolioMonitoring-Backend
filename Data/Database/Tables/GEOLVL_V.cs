using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class GEOLVL_V
    {
        public string PKCODE { get; set; }
        public string FKCODE { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string LVL { get; set; }
        public long? LNTH { get; set; }
    }
}
