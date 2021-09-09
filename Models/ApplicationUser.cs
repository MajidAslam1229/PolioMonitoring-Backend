using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CNIC { get; set; }

        public string FullName { get; set; }

        public string Designation { get; set; }

        public string RawPassword { get; set; }

        public string ProvinceCode { get; set; }

        public string DistrictCode { get; set; }

        public string DivisionCode { get; set; }

        public string TehsilCode { get; set; }

        public string FacilityCode { get; set; }

        public string UCCode { get; set; }

        public string UserType { get; set; }

        [StringLength(50)]
        public string GEOLVL { get; set; }

        public string UserLVL { get; set; }

        public int CampaignId { get; set; }

        public int OrganizationId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreaedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Boolean RecordStatus { get; set; }

    }
}
