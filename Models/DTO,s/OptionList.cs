using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class OptionList
    {
        public int Id { get; set; }
        public int? ParentIndicatorId { get; set; }
        public string Label { get; set; }
        public string InputType { get; set; }
        public bool? ForSubindicator { get; set; }
        public bool? ForComments { get; set; }
    }
}
