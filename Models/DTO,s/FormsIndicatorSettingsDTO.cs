using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class FormsIndicatorSettingsDTO
    {
        public int Id { get; set; }
        public string IndicatorName { get; set; }
        public string Type { get; set; }
        public List<OptionList> optionList { get; set; }
        public List<OptionList> optionListToRemove { get; set; }
        public bool? Comments { get; set; }
        public bool? Isrequired { get; set; }
        public string IndicatorCategory { get; set; }
        public int? FormId { get; set; }
        public bool? HavingSubIndicator { get; set; }
        public List<SubIndicatorList> SubIndicatorListDTOs { get; set; }
        public string SubIndicatorDependency { get; set; }
    }
}
