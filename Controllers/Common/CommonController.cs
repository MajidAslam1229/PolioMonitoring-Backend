using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Services.Common;

namespace PolioMonitoringSystem.Controllers.Common
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : Controller
    {
        private CommonService _commonServices;

        public CommonController()
        {
            _commonServices = new CommonService();

        }

        #region MonitoringFormsList
        [HttpGet]
        [Route("MonitoringFormsList")]
        public IActionResult MonitoringFormsList()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetMonitoringForms()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetIndicatorsListByFormId
        [HttpGet]
        [Route("GetIndicatorsListByFormId/{UserType}/{FormId}")]
        public IActionResult GetIndicatorsListByFormId(string UserType,int FormId)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetIndicatorsListByFormId(UserType, FormId)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region DivisionList
        [HttpGet]
        [Route("GetDivisionList")]
        public IActionResult GetDivisionList(string geoLevel)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetDivision(geoLevel)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region DistrictList
        [HttpGet]
        [Route("GetDistrictList")]
        public IActionResult GetDistrictList(string geoLevel)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetDistrict(geoLevel)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }

        }
        #endregion

        #region TehsilList
        [HttpGet]
        [Route("GetTehsilList")]
        public IActionResult GetTehsilList(string geoLevel)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetTehsil(geoLevel)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }

        }
        #endregion

        #region HealthFacilityList
        [HttpGet]
        [Route("GetHealthFacility")]
        public IActionResult GetHealthFacilityList(string TehsilCode)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetHealthFacility(TehsilCode)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetUC
        [HttpGet]
        [Route("GetUC")]
        public IActionResult GetUC(string geoLevel)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLModel>>(_commonServices.GetUC(geoLevel)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetDesignation
        [HttpGet]
        [Route("GetType")]
        public IActionResult GetType(string userlevel,int Organization)
        {
            string Code = this.GetGEOLVL();
            try
            {
                return Ok(UtilService.GetResponse<List<TypeModel>>(_commonServices.GetType(userlevel, Organization,Code)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFilterDesignation
        [HttpGet]
        [Route("GetFilterDesignation")]
        public IActionResult GetFilterDesignation(int Organization)
        {
            string Code = this.GetGEOLVL();
            try
            {
                return Ok(UtilService.GetResponse<List<TypeModel>>(_commonServices.GetFilterDesignation(Organization)));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetOrganization
        [HttpGet]
        [Route("GetOrganization")]
        public IActionResult GetOrganization(string userlevel)
        {
            try
            {
                return Ok(UtilService.GetResponse<List<OrganizationModel>>(_commonServices.GetOrganization()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetCampaings
        [HttpGet]
        [Route("GetCampaings")]
        public IActionResult GetCampaings()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLCampaigns>>(_commonServices.GetCampaigns()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
    }
}
