using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class UserRole
    {
        public const string admin = "admin";
        public const string user = "user";

        public string UserId { get; set; }

        public string[] AddRoleId { get; set; }

        public string[] DeleteRoleId { get; set; }
    }
}
