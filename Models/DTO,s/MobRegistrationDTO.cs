using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class MobRegistrationDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CNIC { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Type { get; set; }

        public int OrganizationId { get; set; }

        public string DivisionCode { get; set; }

        public string Division_Name { get; set; }
   
        public string DistrictCode { get; set; }

        public string District_Name { get; set; }

        public string TehsilCode { get; set; }

        public string Tehsil_Name { get; set; }

        public string UCCode { get; set; }

        public string UserType { get; set; }

       public List<DesignationDto> designationCountDTOs { get; set; }


    }
}
