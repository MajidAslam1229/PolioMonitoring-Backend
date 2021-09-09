using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class PaginatedFilterDTO
    {
        public string division  { get; set; } 
        public string  district { get; set; }
        public string tehsil { get; set; }
        public string uc { get; set; }
        public string day { get; set; }
        public string reporttype { get; set; }

    }
}
