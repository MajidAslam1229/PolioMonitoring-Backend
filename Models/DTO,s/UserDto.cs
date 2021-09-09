using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Division_Name { get; set; }
        public string District_Name { get; set; }
        public string Tehsil_Name { get; set; }
        public string Health_Facility_Name { get; set; }
    }
}
