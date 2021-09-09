using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class SubIndicatorList
    {
        public int Id { get; set; }
        public int? ParentIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string Type { get; set; }
        public string inputType { get; set; }
        public bool? Isrequired { get; set; }

        public List<OptionList> optionList { get; set; }
        public bool? Comments { get; set; }
        public string SubIndicatorDependency { get; set; }
        public List<OptionList> OptionListToRemove { get; set; }
    }
}
