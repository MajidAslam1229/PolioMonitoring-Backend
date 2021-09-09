using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class GetOptionListDTO
    {
        public int Id { get; set; }
        public int? ParentIndicatorId { get; set; }
        public string Label { get; set; }
        public string InputType { get; set; }
    }
}
