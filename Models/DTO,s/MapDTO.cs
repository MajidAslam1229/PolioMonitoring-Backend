using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class MapDTO
    {
        public int Id { get; set; }
        public string DivisionName { get; set; }

        public string DistrictName { get; set; }

        public string TehsilName { get; set; }

        public string UCNumber { get; set; }
        public string Dayofwork { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string IndicatorName { get; set; }
        public string Icon { get; set; }
    }
}
