using System;
using System.Collections.Generic;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class UserAssignedDistricts
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Geolvl { get; set; }
        public bool? RecordStatus { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RawPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string UserType { get; set; }
    }
}
