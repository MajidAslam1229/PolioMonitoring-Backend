using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services;
using PolioMonitoringSystem.Services.Common;
using static PolioMonitoringSystem.Models.DTO_s.DashboardFiltersDTO;

namespace PolioMonitoringSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
       
        #region Fields
        private DashboardService _dashboardService;
        #endregion

        #region Constructor
        public DashboardController()
        {
            _dashboardService = new DashboardService();
        }
        #endregion

        #region GetIndicatorCount
        [HttpPost]
        [Route("GetIndicatorCount")]
        public IActionResult GetIndicatorCount()
        {
            try
            {

                string Code = this.GetGEOLVL();

                //var data = _dashboardService.GetIndicatorCount(IndicatorId);

                var formfilled = _dashboardService.GetFormFilledCount(Code);

                return Ok(UtilService.GetResponse(new { formfilled } ));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormFilledCount
        [HttpGet]
        [Route("GetFormFilledCount")]
        public IActionResult GetFormFilledCount()
        {
            try
            {
                //string UserName = this.GetUserName();


                string Code = this.GetGEOLVL();

                var formfilled = _dashboardService.GetFormFilledCount(Code);

               

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetTotalFormFilledCount
        [HttpGet]
        [Route("GetTotalFormFilledCount")]
        public IActionResult GetTotalFormFilledCount(int Dayofwork)
        {
            try
            {

                string Code = null;

                if (this.GetGEOLVL() != "0")
                {
                    Code = this.GetGEOLVL().ToString();

                }

                var formfilled = _dashboardService.GetTotalFormFilledCount(Code,Dayofwork);



                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetSearchedFormFilledCount
        [HttpGet]
        [Route("GetSearchedFormFilledCount")]
        public IActionResult GetFormFilledCount(string Code)
        {
            try
            {

                var formfilled = _dashboardService.GetFormFilledCount(Code);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormFilledCategoryWiseCount
        [HttpGet]
        [Route("GetFormFilledCategoryWiseCount/{Code}/{Dayofwork}")]
        public IActionResult GetFormFilledCategoryWiseCount(string Code,int Dayofwork)
        {
            try
            {
                string DisCode = "0";

                if (this.GetGEOLVL() != "0")
                {
                    DisCode = this.GetGEOLVL().ToString();

                }

                var formfilled = _dashboardService.GetFormFilledCategoryWiseCount(DisCode, Code, Dayofwork);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetFormFilledDesignationWise
        [HttpGet]
        [Route("GetFormFilledDesignationWise/{Code}/{Dayofwork}")]
        public IActionResult GetFormFilledDesignationWise(string Code,int Dayofwork)
        {
            try
            {
                string DisCode = "0";

                if (this.GetGEOLVL() != "0")
                {
                    DisCode = this.GetGEOLVL().ToString();

                }
                var formfilled = _dashboardService.GetFormFilledDesignationWise(DisCode,Code, Dayofwork);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetMobileTeamMonitoringCount
        [HttpGet]
        [Route("GetMobileTeamMonitoringCount")]
        public IActionResult GetMobileTeamMonitoringCount(int Dayofwork)
        {
            try
            {
                string Code = null;

                if (this.GetGEOLVL() != "0")
                {
                    Code = this.GetGEOLVL().ToString();
                   
                }

                var formfilled = _dashboardService.GetMobileTeamMonitoringCount(Code,Dayofwork);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetVaccinateandUnVaccinatedChild
        [HttpGet]
        [Route("GetVaccinateandUnVaccinatedChild")]
        public IActionResult GetVaccinateandUnVaccinatedChild(int Dayofwork)
        {
            try
            {
                string Code = "0";

                if (this.GetGEOLVL() != "0")
                {
                    Code = this.GetGEOLVL().ToString();
                }

                var formfilled = _dashboardService.GetVaccinateandUnVaccinatedChild(Code,Dayofwork);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GeReasonForMissedChild
        [HttpGet]
        [Route("GeReasonForMissedChild")]
        public IActionResult GeReasonForMissedChild(int Dayofwork)
        {
            try
            {
                string Code = "0";

                if (this.GetGEOLVL() != "0")
                {
                    Code = this.GetGEOLVL().ToString();
                }

                var formfilled = _dashboardService.GeReasonForMissedChild(Code, Dayofwork);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetHouseHoldClusterCount
        [HttpGet]
        [Route("GetHouseHoldClusterCount")]
        public IActionResult GetHouseHoldClusterCount(int Dayofwork)
        {
            try
            {
                string Code = null;

                if (this.GetGEOLVL() != "0")
                {
                    Code = this.GetGEOLVL().ToString();
                }
                var formfilled = _dashboardService.GetHouseHoldClusterCount(Code,Dayofwork);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion


        #region GetMobileTeamMonitoringReport
        [HttpPost]
        [Route("GetMobileTeamMonitoringReport")]
        public IActionResult GetMobileTeamMonitoringReport([FromBody] PaginatedFilterDTO paginatedFilterDTO)
        {
            try
            {
                if(paginatedFilterDTO.reporttype=="Mobile Team Monitoring")
                {
                    var formfilled = _dashboardService.GetTeamMonitoringReport(paginatedFilterDTO);
                    return Ok(UtilService.GetResponse(new { formfilled }));
                }

                if (paginatedFilterDTO.reporttype == "House Hold Cluster")
                {
                    var formfilled = _dashboardService.GetHouseHoldReport(paginatedFilterDTO);
                    return Ok(UtilService.GetResponse(new { formfilled }));
                }

                return Ok(UtilService.GetResponse(new { }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetExcelReport
        //[Route("GetExcelReport")]
        //[HttpPost]
        //public IActionResult GetExcelReport([FromBody] PaginatedFilterDTO paginatedFilterDTO)
        //{
        //    try
        //    {
        //        if(paginatedFilterDTO.reporttype=="Mobile Team Monitoring")
        //        { 
        //            var result = _dashboardService.GetTeamMonitoringExcelReport(paginatedFilterDTO);

        //            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NotifiableUsers.xlsx");
        //        }

        //        if (paginatedFilterDTO.reporttype == "House Hold Cluster")
        //        {
        //            var result = _dashboardService.GetHouseHoldExcelReport(paginatedFilterDTO);

        //            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NotifiableUsers.xlsx");
        //        }

        //        return File("", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NotifiableUsers.xlsx");
               
        //    }
        //    catch (Exception ex)
        //    {
        //        while (ex.InnerException != null)
        //        {
        //            ex = ex.InnerException;
        //        }

        //        return BadRequest(UtilService.GetExResponse<Exception>(ex));
        //    }
        //}
        #endregion


        //////////////////////////////////////////////////////New Dashboard /////////////////////////////////////////

        #region HomeTab

           #region RegistrationCompaliance
        [HttpPost]
        [Route("RegistrationCompaliance")]
        public IActionResult RegistrationCompaliance([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if(homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.RegistrationCompliance(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

           #region Organizationwiseregistration
        [HttpPost]
        [Route("Organizationwiseregistration")]
        public IActionResult Organizationwiseregistration([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.Organizationwiseregistration(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

           #region AdministrativeComplaince
        [HttpPost]
        [Route("AdministrativeComplaince")]
        public IActionResult AdministrativeComplaince([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.Administrativecompliance(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

           #region FixedSiteMonitoringTotalFormFilled
        [HttpPost]
        [Route("FixedSiteMonitoringTotalFormFilled")]
        public IActionResult FixedSiteMonitoringTotalFormFilled([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringTotalFormFilled(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

           #region FixedSiteMonitoringTotalFormFilledDesigantionWise
        [HttpPost]
        [Route("FixedSiteMonitoringTotalFormFilledDesigantionWise")]
        public IActionResult FixedSiteMonitoringTotalFormFilledDesigantionWise([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringTotalFormFilledDesigantionWise(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

           #region FixedSiteMonitoringFunctionalSIAFixedSite
        [HttpPost]
        [Route("FixedSiteMonitoringFunctionalSIAFixedSite")]
        public IActionResult FixedSiteMonitoringFunctionalSIAFixedSite([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringFunctionalSIAFixedSite(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

           #region SuperVisorTrainedUnTrainedStaff
        [HttpPost]
        [Route("SuperVisorTrainedUnTrainedStaff")]
        public IActionResult SuperVisorTrainedUnTrainedStaff([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SuperVisorTrainedUnTrainedStaff(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion


          #region UCMOAICHHCluster
        [HttpPost]
        [Route("UCMOAICHHCluster")]
        public IActionResult UCMOAICHHCluster([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.UCMOAICHHCluster(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region UCMOAICSupervisorCluster
        [HttpPost]
        [Route("UCMOAICSupervisorCluster")]
        public IActionResult UCMOAICSupervisorCluster([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.UCMOAICSupervisorCluster(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region MobileTeamMonitoringNeapComposition
        [HttpPost]
        [Route("MobileTeamMonitoringNeapComposition")]
        public IActionResult MobileTeamMonitoringNeapComposition([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringNeapComposition(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region MobileTeamMonitoringTrainedUnTrainedStaff
        [HttpPost]
        [Route("MobileTeamMonitoringTrainedUnTrainedStaff")]
        public IActionResult MobileTeamMonitoringTrainedUnTrainedStaff([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringTrainedUnTrainedStaff(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region MobileTeamMonitoringVaccineCondition
        [HttpPost]
        [Route("MobileTeamMonitoringVaccineCondition")]
        public IActionResult MobileTeamMonitoringVaccineCondition([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringVaccineCondition(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion


          #region TransitTeamMonitoringNeapComposition
        [HttpPost]
        [Route("TransitTeamMonitoringNeapComposition")]
        public IActionResult TransitTeamMonitoringNeapComposition([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TransitTeamMonitoringNeapComposition(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region TransitTeamMonitoringLogisticsForEachMember
        [HttpPost]
        [Route("TransitTeamMonitoringLogisticsForEachMember")]
        public IActionResult TransitTeamMonitoringLogisticsForEachMember([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TransitTeamMonitoringLogisticsForEachMember(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region TransitTeamMonitoringVaccineCondition
        [HttpPost]
        [Route("TransitTeamMonitoringVaccineCondition")]
        public IActionResult TransitTeamMonitoringVaccineCondition([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TransitTeamMonitoringVaccineCondition(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region TransitTeamMonitoringLEAsSupport
        [HttpPost]
        [Route("TransitTeamMonitoringLEAsSupport")]
        public IActionResult TransitTeamMonitoringLEAsSupport([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TransitTeamMonitoringLEAsSupport(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region HouseHoldClusterPopulationType
        [HttpPost]
        [Route("HouseHoldClusterPopulationType")]
        public IActionResult HouseHoldClusterPopulationType([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterPopulationType(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region HouseHoldClusterVaccinatedandUnVaccinatedAgeWise
        [HttpPost]
        [Route("HouseHoldClusterVaccinatedandUnVaccinatedAgeWise")]
        public IActionResult HouseHoldClusterVaccinatedandUnVaccinatedAgeWise([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterVaccinatedandUnVaccinatedAgeWise(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region HouseHoldClusterreasonformissed
        [HttpPost]
        [Route("HouseHoldClusterreasonformissed")]
        public IActionResult HouseHoldClusterreasonformissed([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterreasonformissed(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region HouseHoldClusterZeroDoseEIChildren
        [HttpPost]
        [Route("HouseHoldClusterZeroDoseEIChildren")]
        public IActionResult HouseHoldClusterZeroDoseEIChildren([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterZeroDoseEIChildren(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

          #region CatchUpHouseHoldClusterPopulationType
        [HttpPost]
        [Route("CatchUpHouseHoldClusterPopulationType")]
        public IActionResult CatchUpHouseHoldClusterPopulationType([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.CatchUpHouseHoldClusterPopulationType(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
         
          #region CatchupHouseHoldChecked
        [HttpPost]
        [Route("CatchupHouseHoldChecked")]
        public IActionResult CatchupHouseHoldChecked([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.CatchupHouseHoldChecked(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion

        #region Compliance

        #region RegistrationCompliance
        [HttpPost]
        [Route("RegistrationComplianceUClevel")]
        public IActionResult RegistrationComplianceUClevel([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.RegistrationComplianceUClevel(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [HttpPost]
        [Route("RegistrationComplianceTehsillevel")]
        public IActionResult RegistrationComplianceTehsillevel([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.RegistrationComplianceTehsillevel(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [HttpPost]
        [Route("RegistrationComplianceDistrictlevel")]
        public IActionResult RegistrationComplianceDistrictlevel([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.RegistrationComplianceDistrictlevel(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [HttpPost]
        [Route("RegistrationComplianceDivisionlevel")]
        public IActionResult RegistrationComplianceDivisionlevel([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.RegistrationComplianceDivisionlevel(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpPost]
        [Route("RegistrationComplianceProvincelevel")]
        public IActionResult RegistrationComplianceProvincelevel([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.RegistrationComplianceProvincelevel(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion

        #region FixedSiteUCMOandAICComplaince
        [HttpPost]
        [Route("FixedSiteUCMOAICComplaince")]
        public IActionResult FixedSiteUCMOAICComplaince([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteUCMOAICComplaince(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region FixedSiteOrganizationComplaince
        [HttpPost]
        [Route("FixedSiteOrganizationComplaince")]
        public IActionResult FixedSiteOrganizationComplaince([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteOrganizationComplaince(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region SupervisormonitoringUCMOandAICComplaince
        [HttpPost]
        [Route("SupervisormonitoringUCMOandAICComplaince")]
        public IActionResult SupervisormonitoringUCMOandAICComplaince([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SupervisormonitoringUCMOandAICComplaince(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region SupervisormonitoringOrganizationComplaince
        [HttpPost]
        [Route("SupervisormonitoringOrganizationComplaince")]
        public IActionResult SupervisormonitoringOrganizationComplaince([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SupervisormonitoringOrganizationComplaince(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region TeammonitoringUCMOandAICComplaince
        [HttpPost]
        [Route("TeammonitoringUCMOandAICComplaince")]
        public IActionResult TeammonitoringUCMOandAICComplaince([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TeammonitoringUCMOandAICComplaince(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region TeammonitoringOrganizationComplaince
        [HttpPost]
        [Route("TeammonitoringOrganizationComplaince")]
        public IActionResult TeammonitoringOrganizationComplaince([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TeammonitoringOrganizationComplaince(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region MobileTeamMonitoringUCMOandAICCompliance
        [HttpPost]
        [Route("MobileTeamMonitoringUCMOandAICCompliance")]
        public IActionResult MobileTeamMonitoringUCMOandAICCompliance([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringUCMOandAICCompliance(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region MobileTeamOrganizationCompliance
        [HttpPost]
        [Route("MobileTeamOrganizationCompliance")]
        public IActionResult MobileTeamOrganizationCompliance([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamOrganizationCompliance(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region HouseHoldMonitoringUCMOandAICCompliance
        [HttpPost]
        [Route("HouseHoldMonitoringUCMOandAICCompliance")]
        public IActionResult HouseHoldMonitoringUCMOandAICCompliance([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldUCMOandAICCompliance(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region HouseHoldOrganizationCompliance
        [HttpPost]
        [Route("HouseHoldOrganizationCompliance")]
        public IActionResult HouseHoldOrganizationCompliance([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldOrganizationCompliance(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion

        #region FixedSite

        #region FixedSiteMonitoringNeap
        [HttpPost]
        [Route("FixedSiteMonitoringNeap")]
        public IActionResult FixedSiteMonitoringNeap([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringNeap(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region FixedSiteMonitoringVaccineCondition
        [HttpPost]
        [Route("FixedSiteMonitoringVaccineCondition")]
        public IActionResult FixedSiteMonitoringVaccineCondition([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringVaccineCondition(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region FixedSiteMonitoringTemperatureCondition
        [HttpPost]
        [Route("FixedSiteMonitoringTemperatureCondition")]
        public IActionResult FixedSiteMonitoringTemperatureCondition([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringTemperatureCondition(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region FixedSiteMonitoringEssentialImmunizationduringSIA
        [HttpPost]
        [Route("FixedSiteMonitoringEssentialImmunizationduringSIA")]
        public IActionResult FixedSiteMonitoringEssentialImmunizationduringSIA([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringEssentialImmunizationduringSIA(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region FixedSiteMonitoringZeroDoseReferral
        [HttpPost]
        [Route("FixedSiteMonitoringZeroDoseReferral")]
        public IActionResult FixedSiteMonitoringZeroDoseReferral([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringZeroDoseReferral(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region FixedSiteMonitoringAFPCaseDefinition
        [HttpPost]
        [Route("FixedSiteMonitoringAFPCaseDefinition")]
        public IActionResult FixedSiteMonitoringAFPCaseDefinition([FromBody] FilterDTO fixedSiteTabFilter)
        {
            try
            {
                if (fixedSiteTabFilter.Code == "" || fixedSiteTabFilter.Code == null)
                {
                    fixedSiteTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSiteMonitoringAFPCaseDefinition(fixedSiteTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion

        #region SuperVisorMonitoring

        #region SuperVisorNecessaryitems
        [HttpPost]
        [Route("SuperVisorNecessaryitems")]
        public IActionResult SuperVisorNecessaryitems([FromBody] FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                if (superVisorMonitoringTabFilter.Code == "" || superVisorMonitoringTabFilter.Code == null)
                {
                    superVisorMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SuperVisorNecessaryitems(superVisorMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region SuperVisorTeamMonitornigchecklist
        [HttpPost]
        [Route("SuperVisorTeamMonitornigchecklist")]
        public IActionResult SuperVisorTeamMonitornigchecklist([FromBody] FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                if (superVisorMonitoringTabFilter.Code == "" || superVisorMonitoringTabFilter.Code == null)
                {
                    superVisorMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SuperVisorTeamMonitornigchecklist(superVisorMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region SuperVisorHRMPVisits
        [HttpPost]
        [Route("SuperVisorHRMPVisits")]
        public IActionResult SuperVisorHRMPVisits([FromBody] FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                if (superVisorMonitoringTabFilter.Code == "" || superVisorMonitoringTabFilter.Code == null)
                {
                    superVisorMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SuperVisorHRMPVisits(superVisorMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region SuperVisorHHClustertaken
        [HttpPost]
        [Route("SuperVisorHHClustertaken")]
        public IActionResult SuperVisorHHClustertaken([FromBody] FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                if (superVisorMonitoringTabFilter.Code == "" || superVisorMonitoringTabFilter.Code == null)
                {
                    superVisorMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.SuperVisorHHClustertaken(superVisorMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion

        #region Teammonitoring

        #region MobileTeamMonitoringDataRecording
        [HttpPost]
        [Route("MobileTeamMonitoringDataRecording")]
        public IActionResult MobileTeamMonitoringDataRecording([FromBody] FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                if (teamMonitoringTabFilter.Code == "" || teamMonitoringTabFilter.Code == null)
                {
                    teamMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringDataRecording(teamMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region MobileTeamMonitoringRouteMaps
        [HttpPost]
        [Route("MobileTeamMonitoringRouteMaps")]
        public IActionResult MobileTeamMonitoringRouteMaps([FromBody] FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                if (teamMonitoringTabFilter.Code == "" || teamMonitoringTabFilter.Code == null)
                {
                    teamMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringRouteMaps(teamMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region MobileTeamMonitoringIPCquestions
        [HttpPost]
        [Route("MobileTeamMonitoringIPCquestions")]
        public IActionResult MobileTeamMonitoringIPCquestions([FromBody] FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                if (teamMonitoringTabFilter.Code == "" || teamMonitoringTabFilter.Code == null)
                {
                    teamMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringIPCquestions(teamMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region MobileTeamMonitoringZeroDoserecording
        [HttpPost]
        [Route("MobileTeamMonitoringZeroDoserecording")]
        public IActionResult MobileTeamMonitoringZeroDoserecording([FromBody] FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                if (teamMonitoringTabFilter.Code == "" || teamMonitoringTabFilter.Code == null)
                {
                    teamMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.MobileTeamMonitoringZeroDoserecording(teamMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #endregion

        #region TransitTeamMonitoring
        #region TransitTeamMonitoringCNIC
        [HttpPost]
        [Route("TransitTeamMonitoringCNIC")]
        public IActionResult TransitTeamMonitoringCNIC([FromBody] FilterDTO transitTeamMonitoringTabFilter)
        {
            try
            {
                if (transitTeamMonitoringTabFilter.Code == "" || transitTeamMonitoringTabFilter.Code == null)
                {
                    transitTeamMonitoringTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TransitTeamMonitoringCNIC(transitTeamMonitoringTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #endregion

        #region HouseHoldCluster
        #region HouseHoldClusterGuestChildrenVaccination
        [HttpPost]
        [Route("HouseHoldClusterGuestChildrenVaccination")]
        public IActionResult HouseHoldClusterGuestChildrenVaccination([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterGuestChildrenVaccination(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region HouseHoldClusterFingermarkingstatus 
        [HttpPost]
        [Route("HouseHoldClusterFingermarkingstatus")]
        public IActionResult HouseHoldClusterFingermarkingstatus([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterFingermarkingstatus(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region HouseHoldClusterEIVaccinationlessthantwoyears 
        [HttpPost]
        [Route("HouseHoldClusterEIVaccinationlessthantwoyears")]
        public IActionResult HouseHoldClusterEIVaccinationlessthantwoyears([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.HouseHoldClusterEIVaccinationlessthantwoyears(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion

        #region CatchUpDaysHouseHoldCluster

        #region CatchupHouseHoldFindings 
        [HttpPost]
        [Route("CatchupHouseHoldFindings")]
        public IActionResult CatchupHouseHoldFindings([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.CatchupHouseHoldFindings(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion

        #region Maps
        #region FixedSitePinDrops 
        [HttpPost]
        [Route("FixedSitePinDrops")]
        public IActionResult FixedSitePinDrops([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.FixedSitePinDrops(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region TransitPinDrops 
        [HttpPost]
        [Route("TransitPinDrops")]
        public IActionResult TransitPinDrops([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.TransitPinDrops(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region ReasonForMissedMarkers 
        [HttpPost]
        [Route("ReasonForMissedMarkers")]
        public IActionResult ReasonForMissedMarkers([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var formfilled = _dashboardService.ReasonForMissedMarkers(homeTabFilter);

                return Ok(UtilService.GetResponse(new { formfilled }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #endregion

        #region Polygon

        [HttpPost]
        [Route("HouseholdClustersMarkers")]
        public IActionResult HouseholdClustersMarkers([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var householdclustermarkers = _dashboardService.HouseHoldClusterMarkers(homeTabFilter);

                return Ok(UtilService.GetResponse(new { householdclustermarkers }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpPost]
        [Route("HouseholdClusterMissedArea")]
        public IActionResult HouseholdClusterMissedArea([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var missedarea = _dashboardService.HouseholdClusterMissedArea(homeTabFilter);

                return Ok(UtilService.GetResponse(new { missedarea }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [HttpPost]
        [Route("HouseholdClusterPoorlyCoveredArea")]
        public IActionResult HouseholdClusterPoorlyCoveredArea([FromBody] FilterDTO homeTabFilter)
        {
            try
            {
                if (homeTabFilter.Code == "" || homeTabFilter.Code == null)
                {
                    homeTabFilter.Code = this.GetGEOLVL();
                }

                var PoorlyCoverdArea = _dashboardService.HouseholdClusterPoorlyCoveredArea(homeTabFilter);

                return Ok(UtilService.GetResponse(new { PoorlyCoverdArea }));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion

        #region Reports

        #region GetReport
        [HttpPost]
        [Route("GetReport")]
        public IActionResult GetReport([FromBody] ReportFilterDTO reportFilterDTO)
        {
            try
            {

                if (reportFilterDTO.FormId == 0 && reportFilterDTO.ReportType == 1)
                {
                    var reportdto = _dashboardService.RegistrationComplainceUserWise(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }
                if (reportFilterDTO.FormId==0 && reportFilterDTO.ReportType==2)
                {
                    var reportdto = _dashboardService.MonitoringComplaince(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }
                if (reportFilterDTO.Code == "" || reportFilterDTO.Code == null)
                {
                    reportFilterDTO.Code = this.GetGEOLVL();
                }

                if(reportFilterDTO.FormId==1)
                {
                    var reportdto = _dashboardService.FixedSiteReports(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }

                if (reportFilterDTO.FormId == 2)
                {
                    var reportdto = _dashboardService.SupervisormonitoringReports(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }

                if (reportFilterDTO.FormId == 3)
                {
                    var reportdto = _dashboardService.TeammonitoringReports(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }

                if (reportFilterDTO.FormId == 4)
                {
                    var reportdto = _dashboardService.TransitTeammonitoringReports(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }

                if (reportFilterDTO.FormId == 5 && reportFilterDTO.UserType=="Administrative")
                {
                    var reportdto = _dashboardService.AdministrativeHouseHoldClusterReport(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }

                if (reportFilterDTO.FormId == 5 && reportFilterDTO.UserType == "Technical")
                {
                    var reportdto = _dashboardService.HouseHoldClusterReport(reportFilterDTO);

                    return Ok(UtilService.GetResponse(new { reportdto }));
                }

                return Ok("No Data Found!");
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetExcelReport
        [Route("GetExcelReport")]
        [HttpPost]
        public IActionResult GetExcelReport([FromBody] ReportFilterDTO reportFilterDTO)
        {
            try
            {
                    var result = _dashboardService.GetExcelReport(reportFilterDTO);

                    return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NotifiableUsers.xlsx");

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #endregion
    }
}
