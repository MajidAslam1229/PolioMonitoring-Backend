using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class districtdesignationcounter
    {
        public string District { get; set; }
        public double? Chief_Executive_Officer__Health_ { get; set; }
        public double? Deputy_District_Officer_Health { get; set; }
        public double? District_Coordinator_IRMNCH { get; set; }
        public double? District_Officer_Health__MIS___HRM_ { get; set; }
        public double? District_Officer_Health__MS_ { get; set; }
        public double? District_Officer_Health__Preventive_ { get; set; }
        public double? Medical_Superintendent { get; set; }
        public double? Program_Director { get; set; }
    }
}
