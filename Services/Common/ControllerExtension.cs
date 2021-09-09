using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.Common
{
    public static class ControllerExtension
    {
        public static string GetUserId(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value;
        }

        public static string GetUserName(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
        }

        public static string GetFacilityCode(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == "FacilityCode").Value;
        }

        public static string GetUCCode(this ControllerBase controllerBase)
        {
            var a = controllerBase.HttpContext.User.Claims.First(i => i.Type == "UCCode").Value;
            return a;
        }

        public static string GetDistrictCode(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == "DistrictCode").Value;
             
        }

        public static string GetGEOLVL(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == "GEOLVL").Value;
        }

        public static string GetUserLVL(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == "UserLVL").Value;
        }

        public static string GetUserCatgory(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.User.Claims.First(i => i.Type == "Category").Value;
        }


    }
}
