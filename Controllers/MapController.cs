using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services;
using PolioMonitoringSystem.Services.Common;

namespace PolioMonitoringSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : Controller
    {

        #region Fields
        private MapService _mapService;
        #endregion

        #region Constructor
        public MapController(IMapper mapper)
        {
            _mapService = new MapService(mapper);
        }
        #endregion

        #region GetFormsIndicatorList
        [HttpGet]
        [Route("GetFormsLocation")]
        public IActionResult GetFormsLocation(int Dayofwork)
        {
            try
            {
                string Code = null;
                
                if(this.GetGEOLVL() != "0")
                {
                    Code = this.GetGEOLVL().ToString();

                }
                
               var data = _mapService.GetFormsIndicatorsList(Code,Dayofwork);
               return Ok(UtilService.GetResponse(new
               {
                   Data = data,
               }));
                


               

            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetDashboardStat
        [HttpPost]
        [Route("GetDashboardStat")]
        public IActionResult GetDashboardStat()
         {
            try
            {
                string UserName = this.GetUserId();

                var data = _mapService.GetDashboardStat(UserName);

                return Ok(UtilService.GetResponse<DashboardStat>(data));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }


        #endregion


    }
}
