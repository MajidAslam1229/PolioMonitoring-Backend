using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class FormList
    {
        public string Lat { get; set; }
        public string Long { get; set; }
        public List<FormsIndicatorDetailDTO> formsIndicatorDetailDTOs { get; set; }
    }
}
