using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class Assignuserdistrict
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Geolvl { get; set; }

    }
}
