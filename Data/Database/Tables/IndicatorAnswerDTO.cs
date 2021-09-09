using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public class IndicatorAnswerDTO
    {
        public int Id { get; set; }
        public int FormIndicatorDetailId { get; set; }
        public int AnswerId { get; set; }
        public string AnswerDesc { get; set; }
    }
}
