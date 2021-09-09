using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.Auth
{
    public class AuthService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserRolesResult _userRolesResult;
        private readonly CommonService _commonService;
        #endregion

        #region Constructors
        public AuthService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._userRolesResult = new UserRolesResult();
            _commonService = new CommonService(mapper);
            _mapper = mapper;
        }
        #endregion

        #region GetUsersList
        public PaginationResult<UserDto> GetUsers(PaginationViewModel model)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    List<V_Userlist> userlist = db.V_Userlist.Where(x=>x.RecordStatus==true).OrderBy(x => x.UserName).ToList();

                    var TotalCount = userlist.Count();

                    //userlist = userlist.OrderBy(x => x.Id).Skip((model.Page - 1) * (model.PageSize)).Take(model.PageSize).ToList();

                    var res = this._mapper.Map<List<UserDto>>(userlist.ToList());

                    return new PaginationResult<UserDto> { Data = res, RecordsTotal = TotalCount };
                }
            }
            catch
            {
                throw;
            }
        }

        public PaginationResult<MobRegistrationDTO> GetMobUsers(string userid, string uccode, string userlvl)
        {
            try
            {
                using (var db = new PMSDbContext())
                {

                    List<MobRegistrationDTO> mobRegistrationDTOs = new List<MobRegistrationDTO>();
                    List<V_Userlist> userlist = db.V_Userlist.Where(x => x.CreatedBy == userid && x.RecordStatus==true && x.CampaignId==_commonService.GetCurrentCampaignId()).OrderByDescending(x => x.Id).ToList();

                    if(userlvl=="UC")
                    {
                        List<DesignationDto> designationCountDTOs = new List<DesignationDto>();

                        mobRegistrationDTOs = this._mapper.Map<List<MobRegistrationDTO>>(userlist.ToList());

                        var DesignationList = db.Designation.Where(x => x.DesignationLvl == "UC" && x.RecordStatus == true);

                        int uccode1 = Convert.ToInt32(uccode);

                        var TotalCount = userlist.Count();

                        foreach(var desigantion in DesignationList)
                        {
                            DesignationDto designationDto = new DesignationDto();

                            designationDto.Title = desigantion.Designation1;

                            designationDto.Total = TotalDesignationCount(uccode1,desigantion.Designation1);

                            designationDto.Registered = userlist.Where(x => x.Designation == desigantion.Designation1).ToList().Count();

                            designationCountDTOs.Add(designationDto);

                        }

                        mobRegistrationDTOs[0].designationCountDTOs = designationCountDTOs;

                        return new PaginationResult<MobRegistrationDTO> {Data=mobRegistrationDTOs,TotalAIC=mobRegistrationDTOs.Count};
                    }

                    mobRegistrationDTOs = this._mapper.Map<List<MobRegistrationDTO>>(userlist.ToList());
                    //if (userlvl == "District")
                    //{
                    //    List<DesignationDto> designationCountDTOs = new List<DesignationDto>();

                    //    List<MobRegistrationDTO> mobRegistrationDTOs = this._mapper.Map<List<MobRegistrationDTO>>(userlist.ToList());

                    //    var DesignationList = db.Designation.Where(x => x.DesignationLvl == "District" && x.Code==districtcode && x.RecordStatus == true);

                    //    int uccode1 = Convert.ToInt32(uccode);

                    //    var TotalCount = userlist.Count();

                    //    foreach (var desigantion in DesignationList)
                    //    {
                    //        DesignationDto designationDto = new DesignationDto();

                    //        designationDto.Title = desigantion.Designation1;

                    //        designationDto.Total = TotalDesignationCount(uccode1, desigantion.Designation1);

                    //        designationDto.Registered = userlist.Where(x => x.Designation == desigantion.Designation1).ToList().Count();

                    //        designationCountDTOs.Add(designationDto);

                    //    }

                    //    mobRegistrationDTOs[0].designationCountDTOs = designationCountDTOs;

                    //    return new PaginationResult<MobRegistrationDTO> { Data = mobRegistrationDTOs, TotalAIC = mobRegistrationDTOs.Count };
                    //}



                    return new PaginationResult<MobRegistrationDTO> { Data=mobRegistrationDTOs };


                }


            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region GetUserRoles
        public async Task<UserRolesResult> GetUserRolesAsync(string userId)
        {
            try
            {
                ApplicationUser applicationUser = await this.userManager.FindByIdAsync(userId);

                _userRolesResult.UserRoles = await this.userManager.GetRolesAsync(applicationUser);

                _userRolesResult.AllRoles = roleManager.Roles.OrderBy(x => x.Name).Select(y => y.Name).ToList();

                return _userRolesResult;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region GetMobUsers

        #endregion

        #region Getuserinfo
        public V_Userlist Getuserinfo(string userid)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    V_Userlist userlist = db.V_Userlist.Where(x => x.RecordStatus == true).OrderBy(x => x.UserName).FirstOrDefault();
                    return userlist;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region GetTotalDesignationCount
        public int? TotalDesignationCount (int uccode1,string Designation)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var TotalUCCMo = db.AppLocationsUC.Where(x => x.Id == uccode1).ToList();
                    if(Designation=="UCMO")
                    {
                        int? totalCount = TotalUCCMo[0].UCMO;
                        return totalCount;
                    }
                    if (Designation == "AIC")
                    {
                        int? totalCount = TotalUCCMo[0].AIC;
                        return totalCount;
                    }
                    if (Designation == "UCPO")
                    {
                        int? totalCount = TotalUCCMo[0].UCPO;
                        return totalCount;
                    }
                    if (Designation == "UCSP")
                    {
                        int? totalCount = TotalUCCMo[0].UCSP;
                        return totalCount;
                    }
                    if (Designation == "UCCO")
                    {
                        int? totalCount = TotalUCCMo[0].UCCO;
                        return totalCount;
                    }
                    if (Designation == "SM")
                    {
                        int? totalCount = TotalUCCMo[0].SM;
                        return totalCount;
                    }
                }
                return 0;
            }
            catch(Exception ex)
            {
                throw  ex;
            }
        }

        #endregion

        #region GetUserUCDisDiv&ProName
        public string GetName(string Code,string userlvl)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    if(userlvl=="Tehsil" || userlvl=="District" || userlvl=="Division")
                    {
                        var name = db.AppLocations.Where(x => x.Code == Code && x.Type == userlvl).ToList();
                        return name[0].Name;
                    }
                    if(userlvl == "UC")
                    {
                        var name = db.AppLocationsUC.Where(x => x.Id.ToString() == Code).ToList();
                        return name[0].UCNumber;
                    }
                    if (userlvl == "Province")
                    {
                        return "Punjab";
                    }
                        return "";
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
