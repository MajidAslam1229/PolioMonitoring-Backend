using PolioMonitoringSystem.Data.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class SubIndicatorDetailsDTO
    {
        public string IndicatorName { get; set; }
        public int Id { get; set; }
        public int? FormsIndicatorId { get; set; }
        public int? SubIdDetail { get; set; }

        public string Comments { get; set; }

        public string Lat { get; set; }
        public string Long { get; set; }

        public List<IndicatorAnswerDTO> IndicatorAnswers { get; set; }
    }
}
