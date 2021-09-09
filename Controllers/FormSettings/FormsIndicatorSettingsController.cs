using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolioMonitoringSystem.Data.Database.Tables;
//using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using PolioMonitoringSystem.Services.FormSettings;

namespace PolioMonitoringSystem.Controllers.FormSettings
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FormsIndicatorSettingsController : ControllerBase
    {
        #region Fields
        private FormsIndicatorSettingsService _FormsIndicatorService;
        #endregion

        #region Constructor
        public FormsIndicatorSettingsController(IMapper mapper)
        {
            _FormsIndicatorService = new FormsIndicatorSettingsService(mapper);
        }
        #endregion

        #region AddFormsIndicator
        [HttpPost]
        [Route("AddFormsIndicator")]
        public IActionResult AddFormsIndicator([FromBody] FormsIndicatorSettingsDTO model)
        {
            try
            {
                /////Get userId
                
                string userid = this.GetUserId();

                ////////////////

                var res = _FormsIndicatorService.AddFormsIndicator(model, userid);

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

                return Ok(new { FormsIndicatorList,FormsIndicatorList.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormsIndicatorById
        [HttpGet]
        [Route("GetFormsIndicatorById/{id}")]
        public IActionResult GetFormsIndicatorById(int id)
        {
            try
            {
                FormsIndicatorSettingsDTO formsIndicator = _FormsIndicatorService.GetFormsIndicatorById(id);
                if (formsIndicator == null)
                {
                    return Ok(UtilService.GetExResponse<FormsIndicator>(new Exception("Record not found")));
                }
                return Ok(UtilService.GetResponse(formsIndicator));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region DeleteFormsIndicatorById
        [HttpDelete]
        [Route("DeleteFormsIndicatorById/{Id}")]
        public IActionResult DeleteFormsIndicatorById(int id)
        {
            try
            {
                FormsIndicator formsIndicator = _FormsIndicatorService.DeleteFormIndicatorsById(id);
                if (formsIndicator == null)
                {
                    return Ok(UtilService.GetExResponse<FormsIndicator>(new Exception("Record not found")));
                }
                return Ok(UtilService.GetExResponse<FormsIndicator>(new Exception("Record Deleted Successfully")));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region DeleteFormsIndicatorOptionsById
        [HttpDelete]
        [Route("DeleteFormsIndicatorOptionsById/{Id}")]
        public IActionResult DeleteFormsIndicatorOptionsById(int id)
        {
            try
            {
                IndicatorOptions indicatorOptions = _FormsIndicatorService.DeleteFormIndicatorsOptionsById(id);
                if (indicatorOptions == null)
                {
                    return Ok(UtilService.GetExResponse<IndicatorOptions>(new Exception("Record not found")));
                }
                return Ok(UtilService.GetExResponse<IndicatorOptions>(new Exception("Record Deleted Successfully")));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region SearchIndicatorsByFormId
        [HttpGet]
        [Route("SearchIndicatorsByFormId/{Id}/{value}")]
        public IActionResult SearchIndicatorsByFormId(int Id, string value)
        {
            try
            {
                //string UserName = this.GetUserName();

                var FormsIndicatorList = _FormsIndicatorService.SearchIndicatorsByFormId(Id, value);

                return Ok(new { FormsIndicatorList });
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormsIndicatorById
        [HttpGet]
        [Route("GetFormsIndicatorByFormId/{Id}/{Category}")]
        public IActionResult GetFormsIndicatorByFormId(int Id,string Category)
        {
            try
            {
                var FormsIndicatorList = _FormsIndicatorService.GetFormsIndicatorByFormId(Id, Category);
                if (FormsIndicatorList == null)
                {
                    return Ok(UtilService.GetExResponse<FormsIndicator>(new Exception("Record not found")));
                }
                return Ok(UtilService.GetResponse(FormsIndicatorList));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

    }
}
