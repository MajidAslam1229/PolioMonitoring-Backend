using AutoMapper;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.Common
{
    public class CommonService
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion
        
        #region Constructors
        public CommonService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public CommonService()
        {
     
        }
        #endregion

        #region GetMonitoringFormsList
        public List<DDLModel> GetMonitoringForms()
        {
            try
            {
                using (var db = new PMSDbContext())
                {

                    return db.MonitoringForms.Where(x => x.RecordStatus == true).Select(x => new DDLModel { Id = x.Id, Name = x.FormName }).ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetIndicatorsListByFormId
        public List<DDLModel> GetIndicatorsListByFormId(string UserType, int FormId)
        {
            try
            {
                using (var db = new PMSDbContext())
                {

                    return db.FormsIndicator.Where(x => x.RecordStatus == true && x.IndicatorCategory==UserType && x.FormId==FormId  && x.Type!="heading").Select(x => new DDLModel { Id = x.Id, Name = x.IndicatorName }).ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetDivisionList
        public List<DDLModel> GetDivision(string geoLevel)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    if (geoLevel == "0" || geoLevel == null)
                        return db.AppLocations.Where(x => x.Type == "Division").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();
                    else
                    {
                        var c = geoLevel.Substring(0, 3);
                        return db.AppLocations.Where(x => x.Type == "Division" && x.Code.StartsWith(c)).Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetDistrictList
        public List<DDLModel> GetDistrict(string geoLevel)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    if (geoLevel != null)
                        if (geoLevel.Length == 3)
                            return db.AppLocations.Where(x => x.Code.StartsWith(geoLevel) && x.Type == "District").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();

                    if (geoLevel == null)
                        return db.AppLocations.Where(x => x.Type == "District").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();
                    else
                    {
                        var c = geoLevel.Substring(0, 6);
                        return db.AppLocations.Where(x => x.Code.StartsWith(c) && x.Type == "District").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();

                    }

                }
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region GetTehsil
        public List<DDLModel> GetTehsil(string geoLevel)
        {

            try
            {
                using (var db = new PMSDbContext())
                {
                    if (geoLevel == null)
                        return db.AppLocations.Where(x => x.Type == "Tehsil").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();

                    if (geoLevel.Length == 3)
                        return db.AppLocations.Where(x => x.Code.StartsWith(geoLevel) && x.Type == "Tehsil").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();


                    if (geoLevel.Length == 6)
                        return db.AppLocations.Where(x => x.Code.StartsWith(geoLevel) && x.Type == "Tehsil").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();
                    else
                    {
                        var c = geoLevel.Substring(0, 9);
                        return db.AppLocations.Where(x => x.Code.StartsWith(c) && x.Type == "Tehsil").Select(x => new DDLModel { Code = x.Code, Name = x.Name, LVL = x.Type }).ToList();

                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region GetHealthFacility
        public List<DDLModel> GetHealthFacility(string geoLevel)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    return db.HealthFacility.Where(x => x.TehsilCode == geoLevel).Select(x => new DDLModel { Code = x.HFMISCode, Name = x.FullName, LVL = "Health Facility" }).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region GetUC
        public List<DDLModel> GetUC(string geoLevel)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    if (geoLevel == "0" || geoLevel == null)
                        return db.AppLocationsUC.Where(x => x.RecordStatus == "Y").Select(x => new DDLModel { Id = x.Id, Code = x.Code, Name = x.UCNumber, LVL = "UC" }).ToList();

                    if (geoLevel.Length == 3)
                        return db.AppLocationsUC.Where(x => x.Code.StartsWith(geoLevel)).Select(x => new DDLModel { Id = x.Id, Code = x.Code, Name = x.UCNumber, LVL = "UC" }).ToList();


                    if (geoLevel.Length == 6)
                        return db.AppLocationsUC.Where(x => x.Code.StartsWith(geoLevel)).Select(x => new DDLModel { Id = x.Id, Code = x.Code, Name = x.UCNumber, LVL = "UC" }).ToList();

                    if (geoLevel.Length == 9)
                        return db.AppLocationsUC.Where(x => x.Code.StartsWith(geoLevel)).Select(x => new DDLModel { Id = x.Id, Code = x.Code, Name = x.UCNumber, LVL = "UC" }).ToList();
                    else
                    {
                        var c = geoLevel;
                        return db.AppLocationsUC.Where(x => x.Code.ToString() == geoLevel).Select(x => new DDLModel { Id = x.Id, Code = x.Code, Name = x.UCNumber, LVL = "UC" }).ToList();

                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        
        #region GetDesignation
        public List<TypeModel> GetType(string userlevel,int Organization,string Code)
        {
            try
            {

                using (var db = new PMSDbContext())
                {
                    if (userlevel == "District")
                        return db.Designation.Where(x => x.DesignationLvl == "District" && x.Code== Code && Organization ==x.OrganizationId && x.RecordStatus == true).Select(x => new TypeModel { Id = x.Id, Name = x.Designation1 }).ToList();

                    if (userlevel == "Division")
                        return db.Designation.Where(x => x.DesignationLvl == "Division" && x.Code == Code && Organization == x.OrganizationId && x.RecordStatus == true).Select(x => new TypeModel { Id = x.Id, Name = x.Designation1 }).ToList();

                    if (userlevel == "Province")
                        return db.Designation.Where(x => x.DesignationLvl == "Province" && x.Code == Code && Organization == x.OrganizationId && x.RecordStatus == true).Select(x => new TypeModel { Id = x.Id, Name = x.Designation1 }).ToList();

                    else if (userlevel == "UC")
                    {
                        return db.Designation.Where(x => x.DesignationLvl == "UC" && Organization == x.OrganizationId && x.RecordStatus == true).Select(x => new TypeModel { Id = x.Id, Name = x.Designation1 }).ToList();

                    }
                    if (userlevel == "Tehsil")
                        return db.Designation.Where(x => x.DesignationLvl == "Tehsil" && x.Code == Code && Organization == x.OrganizationId && x.RecordStatus == true).Select(x => new TypeModel { Id = x.Id, Name = x.Designation1 }).ToList();

                    return new List<TypeModel>();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region GetFilterDesignation
        public List<TypeModel> GetFilterDesignation(int Organization)
        {
            try
            {

                using (var db = new PMSDbContext())

                {

                   return db.Designation.Where(x=>x.OrganizationId== Organization  && x.RecordStatus == true).Select(x => new TypeModel {  Name = x.Designation1 }).Distinct().ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region GetOrganization
        public List<OrganizationModel> GetOrganization()
        {
            try
            {
                using (var db = new PMSDbContext())
                {

                 return db.Organization.Where(x =>x.RecordStatus == true).Select(x => new OrganizationModel { Id = x.Id, Name = x.Name }).ToList();

                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CheckUserCNICduplication
        public bool ISUserCNICExist(string cnic)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.CNIC == cnic && x.CampaignId==GetCurrentCampaignId());

                    if (user == null)
                        return false;
                    else
                        return true;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region IsCurrentCampaignExist
        public bool IsCurrentCampaignExist()
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var pmsevent = db.PMSEvents.FirstOrDefault(x => x.Status == true);

                    if (pmsevent == null)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region CheckUserPhoneNumberduplication
        public bool ISUserPhoneNumberExist(string phonenumber)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.PhoneNumber == phonenumber  && x.CampaignId == GetCurrentCampaignId());

                    if (user == null)
                        return false;
                    else
                        return true;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region CheckUCDesignaionLimit
        public bool CheckUCDesignaionLimit(string GEOLVL, string OrganizationId, string UserLVL, string Designation)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var registeredcount = db.AspNetUsers.Where(x => x.GEOLVL == GEOLVL && x.OrganizationId.ToString() == OrganizationId && x.CampaignId==GetCurrentCampaignId() && x.UserLVL == UserLVL && x.Designation == Designation && x.RecordStatus == true).ToList();

                    var totalcount = db.AppLocationsUC.Where(x => x.Id.ToString() == GEOLVL && x.RecordStatus == "Y").ToList();

                    if(Designation=="UCMO")
                    {
                        if (registeredcount.Count == totalcount[0].UCMO)
                            return false;
                        else
                            return true;
                    }
                    if (Designation == "UCCO")
                    {
                        if (registeredcount.Count == totalcount[0].UCCO)
                            return false;
                        else
                            return true;
                    }
                    if (Designation == "UCPO")
                    {
                        if (registeredcount.Count == totalcount[0].UCPO)
                            return false;
                        else
                            return true;
                    }
                    if (Designation == "AIC")
                    {
                        if (registeredcount.Count == totalcount[0].AIC)
                            return false;
                        else
                            return true;
                    }
                    if (Designation == "SM")
                    {
                        if (registeredcount.Count == totalcount[0].SM)
                            return false;
                        else
                            return true;
                    }
                    if (Designation == "UCSP")
                    {
                        if (registeredcount.Count == totalcount[0].UCSP)
                            return false;
                        else
                            return true;
                    }

                    return false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region CheckUserDesignaionLimit
        public bool CheckUserDesignaionLimit(string GEOLVL, string OrganizationId, string UserLVL,string Designation)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var registeredcount = db.AspNetUsers.Where(x => x.GEOLVL == GEOLVL && x.OrganizationId.ToString()==OrganizationId && x.UserLVL==UserLVL && x.Designation==Designation && x.CampaignId==GetCurrentCampaignId() && x.RecordStatus==true).ToList();

                    var totalcount = db.Designation.Where(x => x.Code == GEOLVL && x.OrganizationId.ToString() == OrganizationId && x.DesignationLvl == UserLVL && x.Designation1 == Designation && x.RecordStatus == true).ToList();


                    if (registeredcount.Count == totalcount[0].NoOfStaff)
                        return false;
                    else
                        return true;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetCurrentCampaignId
        public int GetCurrentCampaignId()
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    return db.PMSEvents.Where(x => x.Status == true).Select(x => x.Id).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Assignuserdistrict
        public Assignuserdistrict AssignUserdistrict(Assignuserdistrict assignuserdistrict)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    if(assignuserdistrict.Id==0)
                    {
                        ////Mapping Values
                        var newuserlocations = this._mapper.Map<UserLocations>(assignuserdistrict);
                        newuserlocations.RecordStatus = true;
                        //////
                        newuserlocations.RecordStatus = true;
                        db.UserLocations.Add(newuserlocations);

                        db.SaveChanges();

                        assignuserdistrict.Id = newuserlocations.Id;

                        return assignuserdistrict;
                    }
                    else
                    {
                        var userlocation = db.UserLocations.Where(x => x.Id == assignuserdistrict.Id).FirstOrDefault();
                        if(userlocation!=null)
                        {
                            userlocation.Geolvl = assignuserdistrict.Geolvl;
                            userlocation.RecordStatus = true;
                            db.SaveChanges();
                            return assignuserdistrict;
                        }
                    }

                    
                }
                return assignuserdistrict;
            }
            

            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetUserassigneddistrictList
        public List<UserAssignedDistricts> AssignUserdistrict(string userid)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    
                    var userlocation = db.UserAssignedDistricts.Where(x => x.UserId.ToString() == userid && x.RecordStatus==true).ToList();
                    return userlocation;

                }

                return  new List<UserAssignedDistricts>();
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteDistricts
        public UserLocations DeleteDistricts(Assignuserdistrict assignuserdistrict)
        {
            try
            {
                using var db = new PMSDbContext();

                var assignedDistrict = db.UserLocations.FirstOrDefault(x => x.UserId == assignuserdistrict.UserId && x.Geolvl == assignuserdistrict.Geolvl && x.RecordStatus == true);
                assignedDistrict.RecordStatus = false;
                db.SaveChanges();

                return assignedDistrict;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetCampaigns
        public List<DDLCampaigns> GetCampaigns()
        {
            try
            {
                using (var db = new PMSDbContext())
                {


                    return db.PMSEvents.Select(x => new DDLCampaigns { Id = x.Id, Name = x.Name ,CurrentCampaignId=GetCurrentCampaignId() }).ToList();

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
