using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class GetFormsIndicatorSettingsDTO
    {
        public int Id { get; set; }

        public int? OrderNo { get; set; }
        public string IndicatorName { get; set; }
        public string Type { get; set; }

        public bool? Isrequired { get; set; }

        public List<GetOptionListDTO> optionList { get; set; }
        public bool? Comments { get; set; }
        public int? CommentsForOptionList { get; set; }
        public string IndicatorCategory { get; set; }
        public int? FormId { get; set; }

        public string SubIndicatorDependency { get; set; }
        public bool? HavingSubIndicator { get; set; }

        public int? SubIndicatorForOptionList { get; set; }

        public List<GetSubIndicatorList> SubIndicatorListDTOs { get; set; }
       
    }
}
