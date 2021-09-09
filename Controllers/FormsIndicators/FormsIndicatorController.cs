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
using PolioMonitoringSystem.Services.FormSettings;

namespace PolioMonitoringSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FormsIndicatorController : ControllerBase
    {
        #region Fields
        private FormsIndicatorsService _FormsIndicatorService;
        #endregion

        #region Constructor
        public FormsIndicatorController(IMapper mapper)
        {
            _FormsIndicatorService = new FormsIndicatorsService(mapper);
        }
        #endregion

        #region AddFormsIndicator
        [HttpPost]
        [Route("AddFormsIndicator")]
        public IActionResult AddFormsIndicator([FromBody] FormsIndicatorDTO model)
           {
            try
            {
                ///Get userid
                string userid = this.GetUserId();
                string category = this.GetUserCatgory();

                var res = _FormsIndicatorService.AddFormsIndicator(model, userid,category);

                return Ok(UtilService.GetResponse(res));

            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormsIndicatorList
        [HttpGet]
        [Route("GetFormsIndicatorList")]
        public IActionResult GetFormsIndicatorList()
        {
            try
            {
                //string UserName = this.GetUserName();

                var FormsIndicatorList = _FormsIndicatorService.GetFormsIndicatorsList();

                return Ok(UtilService.GetResponse<List<FormsIndicatorDTO>>(FormsIndicatorList));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormsIndicatorById
        [HttpGet]
        [Route("GetFormsIndicatorById/{Id}")]
        public IActionResult GetFormsIndicatorById(int Id)
        {
            try
            {
                GetFormIndicatorDTO formsIndicator = _FormsIndicatorService.GetFormsIndicatorById(Id);
                if (formsIndicator == null)
                {
                    return Ok(UtilService.GetExResponse<GetFormIndicatorDTO>(new Exception("Record not found")));
                }
                return Ok(UtilService.GetResponse(formsIndicator));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        //#region DeleteFormsIndicatorById
        //[HttpDelete]
        //[Route("DeleteFormsIndicatorById/{Id}")]
        //public IActionResult DeleteFormsIndicatorById(int id)
        //{
        //    try
        //    {
        //        FormsIndicator formsIndicator = _FormsIndicatorService.DeleteFormIndicatorsById(id);
        //        if (formsIndicator == null)
        //        {
        //            return Ok(UtilService.GetExResponse<FormsIndicator>(new Exception("Record not found")));
        //        }
        //        return Ok(UtilService.GetExResponse<FormsIndicator>(new Exception("Record Deleted Successfully")));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(UtilService.GetExResponse<Exception>(ex));
        //    }
        //}
        //#endregion

    }
}
