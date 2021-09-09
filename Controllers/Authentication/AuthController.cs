using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PolioMonitoringSystem.Models;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PolioMonitoringSystem.Services.Auth;
using PolioMonitoringSystem.Services;
using System.Text.RegularExpressions;

namespace PolioMonitoringSystem.Controllers.Authentication
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        #region Fields
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly CommonService _commonService;
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly QuickMessageService _quickMessageService;
        #endregion

        #region Constructor
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _commonService = new CommonService(mapper);
            _quickMessageService = new QuickMessageService();
            //_userServices = new UserServices(this.userManager, this.roleManager, mapper);
            _authService = new AuthService(mapper,this.userManager, this.roleManager);


        }
        #endregion

        #region Register

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO model)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.UserName);
                //Duplicate Check
                if (userExists != null)
                    return Ok(new { Status = "Warning", Message = "User Already Exists!" });


                ApplicationUser user = new ApplicationUser()
                {
                    FullName = model.FullName,
                    Designation = model.Designation,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    RawPassword = model.Password,
                    DivisionCode = model.DivisionCode,
                    DistrictCode = model.DistrictCode,
                    TehsilCode = model.TehsilCode,
                    UCCode = model.UCCode,
                    UserType = model.UserType,
                    CampaignId = _commonService.GetCurrentCampaignId(),
                    RecordStatus=true
                    

                };
                ///Check GEO_LVL
                if (model.DivisionCode != null)
                {
                    user.GEOLVL = model.DivisionCode;
                    user.UserLVL = "Division";
                }
                if (model.DistrictCode != null)
                {
                    user.GEOLVL = model.DistrictCode;
                    user.UserLVL = "District";
                }
                if (model.TehsilCode != null && model.TehsilCode != "0" && model.TehsilCode != "")
                {
                    user.GEOLVL = model.TehsilCode;
                    user.UserLVL = "Tehsil";
                }
                if (model.UCCode != null && model.UCCode != "0" && model.UCCode != "")
                {
                    user.GEOLVL = model.UCCode;
                    user.UserLVL = "UC";
                }


                ///
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return Ok(new { Status = "Success", Message = "User created successfully!" });
            }
            catch(Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
            
        }

        #endregion

        #region MobileRegistration
        [Authorize]
        [HttpPost]
        [Route("mobregister")]
        public async Task<IActionResult> MobRegister([FromBody] MobRegistrationDTO model)
        {
            try

            {
                //Current Campaign  Validation
                if (_commonService.IsCurrentCampaignExist())
                {
                    return Ok(UtilService.GetExResponse<Exception>("No Current Campaign is running!"));
                }

                //CNIC Validation
                if (_commonService.ISUserCNICExist(model.CNIC))
                {
                    return Ok(UtilService.GetExResponse<Exception>("This CNIC is already Exist!"));
                }

                //PhoneNumber Validation
                if (_commonService.ISUserPhoneNumberExist(model.PhoneNumber))
                {
                    return Ok(UtilService.GetExResponse<Exception>("This Phone number already registered!"));
                }
                
                //skip the dash from mobile number
                model.PhoneNumber = model.PhoneNumber.Replace("-", string.Empty);
                ///////
                    ApplicationUser user = new ApplicationUser()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.PhoneNumber +'.'+ _commonService.GetCurrentCampaignId(),
                        Email = model.FirstName + model.LastName + "@gmail.com",
                        FullName = model.FirstName + ' ' + model.LastName,
                        CNIC = model.CNIC,
                        Designation = model.Type,
                        ProvinceCode="1",
                        OrganizationId=model.OrganizationId,
                        PhoneNumber = model.PhoneNumber,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        RawPassword = model.Password,
                        DivisionCode = model.DivisionCode,
                        DistrictCode = model.DistrictCode,
                        TehsilCode = model.TehsilCode,
                        UCCode = model.UCCode,
                        UserType = "Technical",
                        CampaignId = _commonService.GetCurrentCampaignId(),
                        CreatedBy = this.GetUserId(),
                        CreaedDate = UtilService.GetPkCurrentDateTime(),
                        RecordStatus=true

                    };

                ///Check GEO_LVL
                ///
                if (user.ProvinceCode != null && user.ProvinceCode != "")
                {
                    user.GEOLVL = user.ProvinceCode;
                    user.UserLVL = "Province";
                }
                if (model.DivisionCode != null && model.DistrictCode!="")
                {
                    user.GEOLVL = model.DivisionCode;
                    user.UserLVL = "Division";
                }

                if (model.DistrictCode != null && model.DistrictCode !="")
                {
                    user.GEOLVL = model.DistrictCode;
                    user.UserLVL = "District";
                }

                if (model.TehsilCode != null && model.TehsilCode !="")
                {
                    user.GEOLVL = model.TehsilCode;
                    user.UserLVL = "Tehsil";
                }
                ///
                if (model.UCCode != null && model.UCCode !="")
                {
                        user.GEOLVL = model.UCCode;
                        user.UserLVL = "UC";
                 }
                //else
                //{
                //    user.GEOLVL = "1";
                //    user.UserLVL = "Province";
                //}

            
                ////Check Designation Counter
                ///

                if(user.UserLVL!="UC")
                {
                    if(user.OrganizationId ==2 || user.OrganizationId ==3)
                    {
                        if (!_commonService.CheckUserDesignaionLimit(user.GEOLVL, user.OrganizationId.ToString(), user.UserLVL, user.Designation))
                        {
                            return Ok(UtilService.GetExResponse<Exception>("You can't register against this designation. Limit has Ended!"));
                        }
                    }
                  
                }

                if(user.UserLVL=="UC")
                {
                    if (!_commonService.CheckUCDesignaionLimit(user.GEOLVL, user.OrganizationId.ToString(), user.UserLVL, user.Designation))
                    {
                        return Ok(UtilService.GetExResponse<Exception>("You can't register against this designation. Limit has Ended!"));
                    }
                }
               

                ////
                ///
                var result = await userManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                    if (result.Succeeded)
                    {
                        SMS sMS = new SMS();
                        sMS.MobileNumber = model.PhoneNumber ;

                        string url = "https://hisduapps.pshealthpunjab.gov.pk";
                        sMS.Message = "Dear" + " " + model.Type + "," + Environment.NewLine +
                                      "It is to notify that your login has been " + Environment.NewLine + " successfully created for" + Environment.NewLine + " Polio Monitoring App, powered by HISDU, Primary and Secondary Health Care " + Environment.NewLine + " Department, Punjab." + Environment.NewLine +
                                      "Use this link to download the application on your android phone :" + url + Environment.NewLine +
                                      "and use the following credentials to log in." + Environment.NewLine +
                                      "Username :" + model.PhoneNumber + '.' + _commonService.GetCurrentCampaignId() + Environment.NewLine +
                                      "Password :" + model.Password + Environment.NewLine +
                                      "Thanks;";

                        sMS.UserId = this.GetUserId();
                        sMS.From = "03124011419";
                        sMS.Remarks = "Registered New User";
                        var LogsSM = _quickMessageService.SendSMSTelenor(sMS);

                        if (LogsSM != null)
                        {
                            UserRole userroles = new UserRole();
                            userroles.UserId = Convert.ToString(user.Id);
                            List<string> listAddId = new List<string>();

                            listAddId.Add("Fixed Site Form");
                            listAddId.Add("Supervisor Monitoring Form");
                            listAddId.Add("Team Monitoring Form");
                            listAddId.Add("SIA Transit Site Monitoring Checklist");
                            listAddId.Add("House Hold Form");
                            listAddId.Add("Catch up days HH Cluster");


                            List<string> listDeleteId = new List<string>();
                            userroles.AddRoleId = listAddId.ToArray();
                            userroles.DeleteRoleId = listDeleteId.ToArray();
                            await AssignUserRoles(userroles);

                            return Ok(new { Status = "Success", Message = "User created successfully!" });
                        }
                    }

                

             
                return Ok(new { Status = "Success", Message = "User created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }

        }

        #endregion

        #region UpdateMobileRegistration
        [Authorize]
        [HttpPost]
        [Route("mobregisterupdate")]
        public async Task<IActionResult> mobregisterupdate([FromBody] MobRegistrationDTO model)
        {
            try
            {
                var finduser = await userManager.FindByIdAsync(model.Id.ToString());
                //CNIC Validation
                if (!_commonService.ISUserCNICExist(finduser.CNIC) && finduser.CNIC != model.CNIC)
                {
                    return Ok(UtilService.GetExResponse<Exception>("This CNIC is already Exist!"));
                }

                //PhoneNumber Validation
                if (!_commonService.ISUserPhoneNumberExist(model.PhoneNumber) && finduser.PhoneNumber != model.PhoneNumber)
                {
                    return Ok(UtilService.GetExResponse<Exception>("This Phone number already registered!"));
                }

               

                finduser.FirstName = model.FirstName;
                finduser.LastName = model.LastName;
                finduser.UserName = model.FirstName + model.LastName;
                finduser.Email = model.FirstName + model.LastName + "@gmail.com";
                finduser.FullName = model.FirstName + ' ' + model.LastName;
                finduser.CNIC = model.CNIC;
                finduser.Designation = model.Type;
                finduser.PhoneNumber = model.PhoneNumber;
                finduser.SecurityStamp = Guid.NewGuid().ToString();
                finduser.RawPassword = model.Password;
                finduser.DivisionCode = model.DivisionCode;
                finduser.DistrictCode = model.DistrictCode;
                finduser.TehsilCode = model.TehsilCode;
                finduser.UCCode = model.UCCode;
                finduser.UserType = model.UserType;
                finduser.CampaignId = _commonService.GetCurrentCampaignId();
                finduser.UpdatedBy = this.GetUserId();
                finduser.UpdatedDate = UtilService.GetPkCurrentDateTime();

                ///Check GEO_LVL
                ///
                if (model.UCCode != null)
                {
                    finduser.GEOLVL = model.UCCode;
                    finduser.UserLVL = "UC";
                }

                ///
                var result = await userManager.UpdateAsync(finduser);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                if (result.Succeeded)
                {
                    SMS sMS = new SMS();
                    sMS.MobileNumber = model.PhoneNumber;

                    string url = "https://play.google.com/store/apps";
                    sMS.Message = "Dear" + " " + model.Type + "," + Environment.NewLine +
                                  "It is to notify that your login has been " + Environment.NewLine + " successfully created for" + Environment.NewLine + " Polio Monitoring App, powered by HISDU, Primary and Secondary Health Care " + Environment.NewLine + " Department, Punjab." + Environment.NewLine +
                                  "Use this link to download the application on your android phone :" + url + Environment.NewLine +
                                  "and use the following credentials to log in." + Environment.NewLine +
                                  "Username :" + model.PhoneNumber + Environment.NewLine +
                                  "Password :" + model.Password + Environment.NewLine +
                                  "Thanks;";

                    sMS.UserId = this.GetUserId();
                    sMS.From = "03124011419";
                    sMS.Remarks = "Update User";
                    var LogsSM = _quickMessageService.SendSMSTelenor(sMS);

                    if (LogsSM != null)
                    {
                        UserRole userroles = new UserRole();
                        userroles.UserId = Convert.ToString(finduser.Id);
                        List<string> listAddId = new List<string>();

                        listAddId.Add("Fixed Site Form");
                        listAddId.Add("Supervisor Monitoring Form");
                        listAddId.Add("Team Monitoring Form");
                        listAddId.Add("SIA Transit Site Monitoring Checklist");
                        listAddId.Add("HouseHold Forrm");


                        List<string> listDeleteId = new List<string>();
                        userroles.AddRoleId = listAddId.ToArray();
                        userroles.DeleteRoleId = listDeleteId.ToArray();
                        await AssignUserRoles(userroles);

                        return Ok(new { Status = "Success", Message = "User Updated successfully!" });
                    }
                }




                return Ok(new { Status = "Success", Message = "User Updated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }

        }

        #endregion

        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var authClaims = new List<Claim>();
                    string Name = "";

                    ApplicationUser user = await userManager.FindByNameAsync(model.UserName);

                    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var userRoles = await userManager.GetRolesAsync(user);

                        ////Get User Roles Name with FormId
                        var userrole = (from role in db.AspNetRoles
                                        join userRole in db.AspNetUserRoles on role.Id equals userRole.RoleId
                                        where userRole.UserId==user.Id
                                        select new
                                        {
                                            role.Name,
                                            role.FormId
                                        }).ToList();

                      

                        ////////////////////////////////////
                        ///
                        if (user.UserLVL == "Province")
                        {
                            authClaims = new List<Claim>
                             {
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim("FacilityCode",user.FacilityCode ?? null),
                            new Claim("GEOLVL",user.GEOLVL?? null),
                            new Claim("UserLVL",user.UserLVL??null),
                            new Claim("Category",user.UserType??null),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                             };

                             Name = _authService.GetName(user.ProvinceCode,user.UserLVL);
                        }



                        if (user.UserLVL == "Division")
                        {
                            authClaims = new List<Claim>
                             {
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim("FacilityCode",user.FacilityCode ?? null),
                            new Claim("GEOLVL",user.GEOLVL?? null),
                            new Claim("UserLVL",user.UserLVL??null),
                            new Claim("Category",user.UserType??null),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                             };
                             Name = _authService.GetName(user.DivisionCode, user.UserLVL);
                        }


                        if (user.UserLVL=="District")
                        {
                              authClaims = new List<Claim>
                             {
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim("FacilityCode",user.FacilityCode ?? null),
                            new Claim("GEOLVL",user.GEOLVL?? null),
                            new Claim("UserLVL",user.UserLVL??null),
                            new Claim("DistrictCode",user.DistrictCode??null),
                            new Claim("Category",user.UserType??null),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                             };

                             Name = _authService.GetName(user.DistrictCode, user.UserLVL);
                        }


                        if (user.UserLVL == "Tehsil")
                        {
                            authClaims = new List<Claim>
                             {
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim("FacilityCode",user.FacilityCode ?? null),
                            new Claim("GEOLVL",user.GEOLVL?? null),
                            new Claim("UserLVL",user.UserLVL??null),
                            new Claim("DistrictCode",user.DistrictCode??null),
                            new Claim("Category",user.UserType??null),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                             };
                            Name = _authService.GetName(user.TehsilCode, user.UserLVL);
                        }

                        if (user.UserLVL == "UC")
                        {
                                    authClaims = new List<Claim>
                             {
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim("FacilityCode",user.FacilityCode ?? null),
                            new Claim("GEOLVL",user.GEOLVL?? null),
                            new Claim("UCCode",user.UCCode.ToString()?? null),
                            new Claim("UserLVL",user.UserLVL??null),
                            new Claim("DistrictCode",user.DistrictCode??null),
                            new Claim("Category",user.UserType??null),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                             };
                             Name = _authService.GetName(user.UCCode, user.UserLVL);
                        }

                      if(user.UserLVL=="Admin")
                        {

                            authClaims = new List<Claim>
                             {
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim("FacilityCode",user.FacilityCode ?? null),
                            new Claim("GEOLVL",user.GEOLVL?? null),
                            new Claim("UCCode",user.UCCode.ToString()?? null),
                            new Claim("UserLVL",user.UserLVL??null),
                            new Claim("DistrictCode",user.DistrictCode??null),
                            new Claim("Category",user.UserType??null),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                             };
                        }

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddDays(7),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo.AddDays(365),
                            user,
                            userrole,
                            Name
                        });
                    }

                    return Unauthorized();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
          

           
        }

        #endregion

        #region MobUserList
        [Authorize]
        [HttpGet]
        [Route("MobGetUsers")]
        public IActionResult MobGetUsers(string UCCode)
        {
            try
            {
                string userid = this.GetUserId();

                string userlvl1 = this.GetGEOLVL();

               
                string userlvl = this.GetUserLVL();

                var UserList = _authService.GetMobUsers(userid, UCCode, userlvl);

                return Ok(new { UserList.Data, UserList.RecordsTotal });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetUserList
        [Authorize]
        [HttpPost]
        [Route("GetUsers")]
        public IActionResult GetAllUsers([FromBody] PaginationViewModel model)
        {
            try
            {
                var UserList = _authService.GetUsers(model);

                return Ok(new { UserList.Data, UserList.RecordsTotal });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion

        #region DeleteUserById
      
        [Authorize]
        [HttpDelete]
        [Route("DeleteUserById")]
        public IActionResult DeleteUserById(Guid id)
        {

            try
            {
                using var db = new PMSDbContext();

                var selectedUser = db.AspNetUsers.FirstOrDefault(x => x.Id == id);
                selectedUser.RecordStatus = false;
                selectedUser.LockoutEnabled = false;

                db.SaveChanges();

                    return Ok(new { message = "User Deleted Sucessfully" }); 
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region Create Role

        [Authorize]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string role )
        {
            try
            {
                bool x = await roleManager.RoleExistsAsync(role);
                if (!x)
                {
                    using var db = new PMSDbContext();



                    var formid = db.MonitoringForms.Where(x => x.FormName.StartsWith(role)).Select(x => new DDLModel { Id = x.Id }).FirstOrDefault();

                    if (formid != null)
                    {

                        var userRole = new ApplicationRole { Name = role, FormId = formid.Id };

                        var result = await roleManager.CreateAsync(userRole);

                        if (result.Succeeded)
                        {
                            return Ok(new
                            {
                                userroles = roleManager.Roles.OrderBy(x => x.Name).Select(y => y.Name).ToList()
                            });
                        }
                        else
                        {
                            return BadRequest(result.Errors);
                        }

                    }
                    else
                    {
                        var userRole = new ApplicationRole { Name = role, FormId = 0 };

                        var result = await roleManager.CreateAsync(userRole);

                        if (result.Succeeded)
                        {
                            return Ok(new
                            {
                                userroles = roleManager.Roles.OrderBy(x => x.Name).Select(y => y.Name).ToList()
                            });
                        }
                        else
                        {
                            return BadRequest(result.Errors);
                        }


                    }
                }
                return BadRequest("Role Already Exists");
            }
            catch(Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
           
        }

        #endregion

        #region GetAllRoles
        [Authorize]
        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> Roles()
        {
            try
            {
                return Ok(new { userroles = roleManager.Roles.OrderBy(x => x.Name).Select(y => y.Name).ToList() });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion

        #region Delete Role

        [Authorize]
        [HttpDelete]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string role)
        {

            try
            {
                var userRole = await roleManager.FindByNameAsync(role);

                var result = await roleManager.DeleteAsync(userRole);

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        userroles = roleManager.Roles.OrderBy(x => x.Name).Select(y => y.Name).ToList()
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }

            return BadRequest("");

        }

        #endregion

        #region AssignUserRoles
        [Authorize]
        [HttpPost]
        [Route("AssignUserRoles")]
        public async Task<IActionResult> AssignUserRoles([FromBody] UserRole userroles)
        {
            List<string> deleteRoleIds = new List<string>();
            List<string> addRoleIds = new List<string>();

            try
            {
                ApplicationUser user = await userManager.FindByIdAsync(userroles.UserId);

                if (user != null)
                {
                    foreach (string role in userroles.DeleteRoleId)
                    {
                        if (await userManager.IsInRoleAsync(user, role))
                        {
                            deleteRoleIds.Add(role);
                        }
                    }

                    foreach (string role in userroles.AddRoleId)
                    {
                        //Debug.WriteLine(role);
                        if (!await userManager.IsInRoleAsync(user, role))
                        {
                            addRoleIds.Add(role);
                        }
                    }
                    await userManager.RemoveFromRolesAsync(user, deleteRoleIds);
                    await userManager.AddToRolesAsync(user, addRoleIds);
                }

                return Ok(new  { Status = "Succeed", Message = "Roles Assigned Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }

        }

        #endregion

        #region AssignUserdistrict
        [Authorize]
        [HttpPost]
        [Route("AssignUserdistrict")]
        public async Task<IActionResult> AssignUserdistrict([FromBody] Assignuserdistrict assignuserdistrict)
        {

            try
            {
                var res = _commonService.AssignUserdistrict(assignuserdistrict);

                return Ok(UtilService.GetResponse(res));
            }
            catch(Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }

        }
        #endregion

        #region GetUserassigneddistrictList
        [Authorize]
        [HttpGet]
        [Route("GetUserassigneddistrictList")]
        public IActionResult GetUserassigneddistrictList(string UserId)
        {
            try
            {
                var UserList = _commonService.AssignUserdistrict(UserId);

                return Ok(UtilService.GetResponse(UserList));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        #endregion

        #region Delete District

        [Authorize]
        [HttpPost]
        [Route("DeleteDistrict")]
        public async Task<IActionResult> DeleteDistrict(Assignuserdistrict deleteDistrict)
        {
            try
            {
                var res = _commonService.DeleteDistricts(deleteDistrict);
                return Ok(new { message = "District Deleted Sucessfully" });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
            return BadRequest("");
        }


        #endregion

        #region GetUserRoles

        [Authorize]
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            try
            {

                UserRolesResult userRolesResult = await _authService.GetUserRolesAsync(userId);

                return Ok(new { allRoles = userRolesResult.AllRoles, userRoles = userRolesResult.UserRoles });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }

        }
        #endregion

        #region MyRegister

        [HttpPost]
        [Route("myregister")]
        public async Task<IActionResult> MyRegister()
        {
            try
            {
                List<UCmissing> uslist = new List<UCmissing>();
                
                using (var db = new PMSDbContext())
                {
                    var UC = db.AppLocationsUC.ToList();

                  
                    foreach (var t in UC)
                    {
                        string text = t.UCNumber;

                        var c = t.Code.Substring(0, 6);
                        var c1 = t.Code.Substring(0, 3);

                        
                        var divcode = db.AppLocations.Where(x => x.Code.StartsWith(c) && x.Type == "District").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();

                        //var c1 = t.Code.Substring(0, 3);

                        string UserName = String.Concat(text.Where(text => !Char.IsWhiteSpace(text)));

                        string districtname1 = String.Concat(divcode[0].Name.Where(c => !Char.IsWhiteSpace(c)));

                        //var finaldistrictname = Regex.Replace(districtname1.ToLower(), @"[^0-9a-zA-Z]+", "");

                        string Finalusername = Regex.Replace(UserName.ToLower(), @"[^0-9a-zA-Z]+", "");



                        //Duplicate Check

                        ApplicationUser user = new ApplicationUser()
                        {
                            FullName = "uc"+Finalusername.ToString() ,
                            FirstName = "UC",
                            LastName = Finalusername.ToString(),
                            Designation = "Technical",
                            PhoneNumber = "12345678",
                            Email = "technical" + "@hisdu.com",
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = districtname1+t.Code + ".uc"+ "."+ Finalusername.ToString() ,
                            //+ "." + "administrative",
                            RawPassword = "123456",
                            ProvinceCode = "1",
                            DivisionCode = c1.ToString(),
                            DistrictCode = c.ToString(),
                            TehsilCode = t.Code,
                            UCCode = t.Id.ToString(),
                            UserType = "Technical",
                            UserLVL = "UC",
                            GEOLVL = t.Id.ToString(),
                            CampaignId = UtilService.GetCurrentCampaignId(),
                            RecordStatus=true,
                            CreaedDate=UtilService.GetPkCurrentDateTime(),
                            CreatedBy="Admin"

                        };

                        ///Check GEO_LVL
                        ///
                       
                        var result = await userManager.CreateAsync(user, user.RawPassword);

                        UCmissing err = new UCmissing();

                           if(!result.Succeeded)
                        {
                            err.UCID = t.Id.ToString();
                            err.Name = Finalusername.ToString();
                            err.error = result.Errors.ToString();
                            uslist.Add(err);

                        }

                            if (result.Succeeded)
                            {
                                
                                UserRole userroles = new UserRole();
                                userroles.UserId = Convert.ToString(user.Id);
                                List<string> listAddId = new List<string>();

                                 listAddId.Add("UserRegister");
                            //listAddId.Add("Fixed Site Form");
                            //listAddId.Add("Supervisor Monitoring Form");
                            //listAddId.Add("Team Monitoring Form");
                            //listAddId.Add("SIA Transit Site Monitoring Checklist");
                            //listAddId.Add("HouseHold Forrm");


                            List<string> listDeleteId = new List<string>();
                                userroles.AddRoleId = listAddId.ToArray();
                                userroles.DeleteRoleId = listDeleteId.ToArray();
                                await AssignUserRoles(userroles);
                            }

                        }


                }
                    return Ok(new { Status = "Success", Message = "Users created successfully!"  , uslist });
                
            }

            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }

        }

        #endregion

        #region GetUserInfo
        [Authorize]
        [HttpGet]
        [Route("GetUserInfo")]
        public IActionResult GetUserInfo(string userId)
        {
            try
            {
                var Userinfo = _authService.Getuserinfo(userId);

                return Ok(new { Userinfo });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion

        #region GetUserInfo
        [AllowAnonymous]
        [HttpPost]
        [Route("SendSms")]
        public IActionResult GetUserInfo(List<String> mobileList)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    SMS sMS = new SMS();
                    foreach (var mobile in mobileList)
                    {
                        sMS.MobileNumber = mobile;

                        var user = db.AspNetUsers.Where(x => x.PhoneNumber == sMS.MobileNumber).FirstOrDefault();

                        string url = "https://hisduapps.pshealthpunjab.gov.pk";
                        sMS.Message = "Dear" + " " + user.Designation + "," + Environment.NewLine +
                                      "It is to notify that your login has been " + Environment.NewLine + " successfully created for" + Environment.NewLine + " Polio Monitoring App, powered by HISDU, Primary and Secondary Health Care " + Environment.NewLine + " Department, Punjab." + Environment.NewLine +
                                      "Use this link to download the application on your android phone :" + url + Environment.NewLine +
                                      "and use the following credentials to log in." + Environment.NewLine +
                                      "Username :" + user.PhoneNumber + '.' + _commonService.GetCurrentCampaignId() + Environment.NewLine +
                                      "Password :" + user.RawPassword + Environment.NewLine +
                                      "Thanks;";

                        sMS.UserId = this.GetUserId();
                        sMS.From = "03124011419";
                        sMS.Remarks = "Registered New User";
                        var LogsSM = _quickMessageService.SendSMSTelenor(sMS);
                    }
                    return Ok(new { message="Successfuly Send" });
                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion




    }
    public class UCmissing
    {
        public string UCID { get; set; }

        public string Name { get; set; }
        public string error{ get; set; }
    }
}

