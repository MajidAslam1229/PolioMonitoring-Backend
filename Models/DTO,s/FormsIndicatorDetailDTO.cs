using PolioMonitoringSystem.Data.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class FormsIndicatorDetailDTO
    {
        public int Id { get; set; }
        public int? FormsIndicatorMasterId { get; set; }
        public int? FormsIndicatorId { get; set; }
        public string FormsIndicatorName { get; set; }
        public int? SubIdDetail { get; set; }
        public string Comments { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public List<IndicatorAnswerDTO> IndicatorAnswers{get;set;}
        public List<SubIndicatorDetailsDTO> subIndicatorDetailsDTOs { get; set; }

    }
}
