using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class UserRolesResult
    {

        public IList<string> UserRoles { get; set; }
        public IList<string> AllRoles { get; set; }

    }
}
