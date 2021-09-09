using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Helpers;
using PolioMonitoringSystem.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static PolioMonitoringSystem.Models.DTO_s.DashboardFiltersDTO;
using static PolioMonitoringSystem.Models.DTO_s.ReportsDTO;

namespace PolioMonitoringSystem.Services
{
    public class DashboardService
    {
        public DashboardService()
        {

        }
        
        #region GetIndicatorCount
        public List<FixedSiteIndicatorAnswer> GetIndicatorCount(int IndicatorId=2)
        {
            try
            {
                List<FixedSiteIndicatorAnswer> Counts = new List<FixedSiteIndicatorAnswer>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_IndicatorCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        IndicatorId = 2;
                        if (IndicatorId != 0)
                        {
                          cmd.Parameters.AddWithValue("@IndicatorId", IndicatorId);
                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            FixedSiteIndicatorAnswer DTO = new FixedSiteIndicatorAnswer();

                            DTO.AnswerDesc = reader["AnswerDesc"].ToString();
                            DTO.AnswerCount = Convert.ToInt32(reader["AnswerCount"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                   return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetFormFilledCount
        public List<FormFilledCount> GetFormFilledCount(string Code)
        {
            try
            {
                List<FormFilledCount> Counts = new List<FormFilledCount>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Form_Complaince", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Code", Code);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FormFilledCount DTO = new FormFilledCount();

                            DTO.Name = reader["Name"].ToString();
                            DTO.value = Convert.ToInt32(reader["total"].ToString());
                            DTO.Code = (reader["Code"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetTotalFormFilledCount
        public List<TotalFormFilledCount> GetTotalFormFilledCount(string Code,int Dayofwork)
        {
            try
            {
                List<TotalFormFilledCount> Counts = new List<TotalFormFilledCount>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FormFilledCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);

                        }
                    
                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (Dayofwork != null && Dayofwork != 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }
                       


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TotalFormFilledCount DTO = new TotalFormFilledCount();

                            DTO.FormName = reader["FormName"].ToString();
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());
                         

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetFormFilledCategoryWiseCount
        public List<FormFilledCategoryWiseCount> GetFormFilledCategoryWiseCount(string DisCode,string Code, int Dayofwork)
        {
            try
            {
                List<FormFilledCategoryWiseCount> Counts = new List<FormFilledCategoryWiseCount>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Form_Complaince", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (DisCode != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", DisCode);

                        }

                        cmd.Parameters.AddWithValue("@FormName", Code);

                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (Dayofwork != null && Dayofwork != 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FormFilledCategoryWiseCount DTO = new FormFilledCategoryWiseCount();

                            DTO.FormName = reader["FormName"].ToString();
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());
                            DTO.Name = (reader["Name"].ToString());
                            DTO.Code = (reader["Code"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetMobileTeamMonitoringCount
        public List<MobileTeamMonitoring> GetMobileTeamMonitoringCount(string Code,int Dayofwork)
        {
            try
            {
                List<MobileTeamMonitoring> Counts = new List<MobileTeamMonitoring>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoring", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);

                        }
                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", '1');
                        }

                        if (Dayofwork != null && Dayofwork != 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoring DTO = new MobileTeamMonitoring();

                            DTO.Name = reader["Name"].ToString();
                            DTO.AIClessthan4 = Convert.ToInt32(reader["AIC < 4"].ToString());
                            DTO.AICgreaterthanequal4 = Convert.ToInt32(reader["AIC >= 4"].ToString());
                            DTO.UCMOlessthan4 = Convert.ToInt32(reader["UCMO < 4"].ToString());
                            DTO.UCMOgreaterthan4 = Convert.ToInt32(reader["UCMO >=4"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetHouseHoldClusterCount
        public List<HouseholdClusterCount> GetHouseHoldClusterCount(string Code,int Dayofwork)
        {
            try
            {
                List<HouseholdClusterCount> Counts = new List<HouseholdClusterCount>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldCluster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);

                        }

                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", '1');
                        }

                        if (Dayofwork != null && Dayofwork != 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseholdClusterCount DTO = new HouseholdClusterCount();

                            DTO.Name = reader["Name"].ToString();
                            DTO.AIClessthan3 = Convert.ToInt32(reader["AIC < 3"].ToString());
                            DTO.AICgreaterthanequal3 = Convert.ToInt32(reader["AIC >= 3"].ToString());
                            DTO.UCMOlessthan3 = Convert.ToInt32(reader["UCMO < 3"].ToString());
                            DTO.UCMOgreaterthan3 = Convert.ToInt32(reader["UCMO >=3"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetFormPopupTotalFormsFilledDistrict
        public List<HouseholdClusterCount> GetFormPopupTotalFormsFilledDistrict()
        {
            try
            {
                List<HouseholdClusterCount> Counts = new List<HouseholdClusterCount>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldCluster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseholdClusterCount DTO = new HouseholdClusterCount();

                            DTO.Name = reader["Name"].ToString();
                            DTO.AIClessthan3 = Convert.ToInt32(reader["AIC < 3"].ToString());
                            DTO.AICgreaterthanequal3 = Convert.ToInt32(reader["AIC >= 3"].ToString());
                            DTO.UCMOlessthan3 = Convert.ToInt32(reader["UCMO < 3"].ToString());
                            DTO.UCMOgreaterthan3 = Convert.ToInt32(reader["UCMO >=3"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetFormFilledDesignationWise
        public List<FormFilledDesignationWiseCount> GetFormFilledDesignationWise(string Discode,string Code,int Dayofwork)
        {
            try
            {
                List<FormFilledDesignationWiseCount> Counts = new List<FormFilledDesignationWiseCount>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Form_Popup_DesignationWisePopupCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FormName", Code);

                        if (Discode != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", Discode);

                        }

                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (Dayofwork != null && Dayofwork != 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FormFilledDesignationWiseCount DTO = new FormFilledDesignationWiseCount();

                            DTO.FormName = reader["FormName"].ToString();
                            DTO.Designation = (reader["Designation"].ToString());
                            DTO.Total = (reader["Total"].ToString());
                            DTO.Name = (reader["Name"].ToString());
                            DTO.Code = (reader["Code"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetVaccinateandUnVaccinatedChild
        public List<VaccinatedandUnVaccinatedChildren> GetVaccinateandUnVaccinatedChild(string Code,int Dayofwork)
        {
            try
            {
                List<VaccinatedandUnVaccinatedChildren> Counts = new List<VaccinatedandUnVaccinatedChildren>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterVaccinatedandUnVaccinateChild", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);

                        }


                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (Dayofwork != null && Dayofwork != 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            VaccinatedandUnVaccinatedChildren DTO = new VaccinatedandUnVaccinatedChildren();

                            DTO.Name = reader["Name"].ToString();

                            DTO.zeroto11MChecked = Convert.ToInt32(reader["0-11M checked"].ToString());
                            DTO.zeroto11Mvaccinated = Convert.ToInt32(reader["0-11M Vaccinated"].ToString());
                            DTO.zeroto11Munvaccinated = Convert.ToInt32(reader["0-11M UnVaccinated"].ToString());
                           
                            DTO.eleventto59MChecked = Convert.ToInt32(reader["12-59M checked"].ToString());
                            DTO.eleventto59vaccinated = Convert.ToInt32(reader["12-59M Vaccinated"].ToString());
                            DTO.eleventto59unvaccinated = Convert.ToInt32(reader["12-59M UnVaccinated"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GeReasonForMissedChild
        public List<MissedChild> GeReasonForMissedChild(string Code,int Dayofwork)
        {
            try
            {
                List<MissedChild> Counts = new List<MissedChild>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterMissedChild", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);

                        }

                        if (Dayofwork == 0)
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (Dayofwork != null && Dayofwork != 0 )
                        {
                            cmd.Parameters.AddWithValue("@Day", Dayofwork.ToString());
                        }
                        

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MissedChild DTO = new MissedChild();

                            DTO.Name = reader["Name"].ToString();

                            DTO.TeamMissedthehouse = Convert.ToInt32(reader["Team Missed the house"].ToString());
                            DTO.TVBMC = Convert.ToInt32(reader["TVBMC"].ToString());
                            DTO.NA = Convert.ToInt32(reader["NA"].ToString());
                            DTO.Ref = Convert.ToInt32(reader["Ref"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetTeamMonitoringReport
        public List<MobilieTeammonitoringReport> GetTeamMonitoringReport(PaginatedFilterDTO paginatedFilterDTO)
        {
            try
            {
                List<MobilieTeammonitoringReport> Counts = new List<MobilieTeammonitoringReport>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoring_Report", conn))
                    {
                        string Code = "0";
                       
                        //int pageSize = 0;
                        //int currentPage = 0;

                        //pageSize = pageSize == 0 ? 50 : pageSize;
                        //currentPage = currentPage > 0 ? currentPage - 1 : 0;
                        //var startIndex = pageSize * currentPage;
                        //var pagedData = data.ToList();

                        //var selectedData = pagedData
                        //    .Skip(startIndex)
                        //    .Take(pageSize)
                        //    .ToList();

                        //var pagesCount = Math.Ceiling(Convert.ToDecimal(pagedData.Count)) / Convert.ToDecimal(pageSize);

                        //return (selectedData, (long)pagesCount, (long)pagedData.Count);

                        cmd.CommandType = CommandType.StoredProcedure;

                        if (paginatedFilterDTO.division != null && paginatedFilterDTO.division.ToString() != "")
                        {
                            Code = paginatedFilterDTO.division.ToString();

                        }

                        if (paginatedFilterDTO.district != null && paginatedFilterDTO.district.ToString() != "")
                        {
                            Code = paginatedFilterDTO.district.ToString();
                        }

                        if (paginatedFilterDTO.tehsil != null && paginatedFilterDTO.tehsil.ToString() != "")
                        {
                            Code = paginatedFilterDTO.tehsil.ToString();
                        }

                        if (paginatedFilterDTO.uc != null && paginatedFilterDTO.uc.ToString() != "")
                        {
                            Code = paginatedFilterDTO.uc.ToString();
                        }


                        if (Code == "")
                        {
                            Code = "0";
                            cmd.Parameters.AddWithValue("@Code", Code.ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);
                        }

                       
                     

                        if (paginatedFilterDTO.day == "")
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (paginatedFilterDTO.day != null && paginatedFilterDTO.day != "")
                        {
                            cmd.Parameters.AddWithValue("@Day", paginatedFilterDTO.day.ToString());
                        }


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobilieTeammonitoringReport DTO = new MobilieTeammonitoringReport();

                            DTO.FirstName = reader["FirstName"].ToString();
                            DTO.LastName = (reader["LastName"].ToString());
                            DTO.PhoneNumber = (reader["PhoneNumber"].ToString());
                            DTO.DistrictName = (reader["DistrictName"].ToString());
                            DTO.TehsilName = (reader["TehsilName"].ToString());
                            DTO.UCNumber = (reader["UCNumber"].ToString());
                            DTO.Designation = (reader["Designation"].ToString());
                            DTO.Count = (reader["Counter"].ToString());

                            Counts.Add(DTO);

                            //(IList<MapDTO>, long, long) pageData = CommonMethods.GetPagedData(Counts.AsQueryable(), paginatedFilterDTO.Size, paginatedFilterDTO.PageNumber);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetHouseHoldReport
        public List<MobilieTeammonitoringReport> GetHouseHoldReport(PaginatedFilterDTO paginatedFilterDTO)
        {
            try
            {
                List<MobilieTeammonitoringReport> Counts = new List<MobilieTeammonitoringReport>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldCluster_Report", conn))
                    {
                        string Code = "0";

                        //int pageSize = 0;
                        //int currentPage = 0;

                        //pageSize = pageSize == 0 ? 50 : pageSize;
                        //currentPage = currentPage > 0 ? currentPage - 1 : 0;
                        //var startIndex = pageSize * currentPage;
                        //var pagedData = data.ToList();

                        //var selectedData = pagedData
                        //    .Skip(startIndex)
                        //    .Take(pageSize)
                        //    .ToList();

                        //var pagesCount = Math.Ceiling(Convert.ToDecimal(pagedData.Count)) / Convert.ToDecimal(pageSize);

                        //return (selectedData, (long)pagesCount, (long)pagedData.Count);

                        cmd.CommandType = CommandType.StoredProcedure;

                        if (paginatedFilterDTO.division != null)
                        {
                            Code = paginatedFilterDTO.division.ToString();

                        }

                        if (paginatedFilterDTO.district != null)
                        {
                            Code = paginatedFilterDTO.district.ToString();
                        }

                        if (paginatedFilterDTO.tehsil != null && paginatedFilterDTO.tehsil.ToString() != "")
                        {
                            Code = paginatedFilterDTO.tehsil.ToString();
                        }

                        if (paginatedFilterDTO.uc != null && paginatedFilterDTO.uc.ToString() != "")
                        {
                            Code = paginatedFilterDTO.uc.ToString();
                        }

                        if(Code=="")
                        {
                            Code = "0";
                            cmd.Parameters.AddWithValue("@Code",Code.ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Code", Code);
                        }
                        


                        if (paginatedFilterDTO.day == "")
                        {
                            cmd.Parameters.AddWithValue("@Day", null);
                        }

                        if (paginatedFilterDTO.day != null && paginatedFilterDTO.day != "")
                        {
                            cmd.Parameters.AddWithValue("@Day", paginatedFilterDTO.day.ToString());
                        }


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobilieTeammonitoringReport DTO = new MobilieTeammonitoringReport();

                            DTO.FirstName = reader["FirstName"].ToString();

                            DTO.LastName = (reader["LastName"].ToString());
                            DTO.PhoneNumber = (reader["PhoneNumber"].ToString());
                            DTO.DistrictName = (reader["DistrictName"].ToString());
                            DTO.TehsilName = (reader["TehsilName"].ToString());
                            DTO.UCNumber = (reader["UCNumber"].ToString());
                            DTO.Designation = (reader["Designation"].ToString());
                            DTO.Count = (reader["Counter"].ToString());



                            Counts.Add(DTO);

                            //(IList<MapDTO>, long, long) pageData = CommonMethods.GetPagedData(Counts.AsQueryable(), paginatedFilterDTO.Size, paginatedFilterDTO.PageNumber);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetTeamMonitoringExcelReport
        public byte[] GetTeamMonitoringExcelReport(PaginatedFilterDTO paginatedFilterDTO)
        {
            try
            {
                //paginatedFilterDTO.FromDate = (paginatedFilterDTO.FromDate == null ? DateTime.Now.Date : (DateTime)paginatedFilterDTO.FromDate);
                //paginatedFilterDTO.ToDate = (paginatedFilterDTO.ToDate == null ? DateTime.Now.Date : ((DateTime)paginatedFilterDTO.ToDate).AddHours(23).AddMinutes(59).AddSeconds(59));

                using var _db = new PMSDbContext();

              
                    var adminResult = GetTeamMonitoringReport(paginatedFilterDTO);
;
                    DataTable dataTable = new DataTable("Team Monitioring");

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });

                    using XLWorkbook xlWorkbook = new XLWorkbook();

                    xlWorkbook.Worksheets.Add(dataTable);

                    using MemoryStream memoryStream = new MemoryStream();

                    xlWorkbook.SaveAs(memoryStream);

                    return memoryStream.ToArray();

               
                

                return null;
               
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw ex;
            }
        }
        #endregion

        #region GetHouseHoldExcelReport
        public byte[] GetHouseHoldExcelReport(PaginatedFilterDTO paginatedFilterDTO)
        {
            try
            {
                //paginatedFilterDTO.FromDate = (paginatedFilterDTO.FromDate == null ? DateTime.Now.Date : (DateTime)paginatedFilterDTO.FromDate);
                //paginatedFilterDTO.ToDate = (paginatedFilterDTO.ToDate == null ? DateTime.Now.Date : ((DateTime)paginatedFilterDTO.ToDate).AddHours(23).AddMinutes(59).AddSeconds(59));

                using var _db = new PMSDbContext();


                var adminResult = GetHouseHoldReport(paginatedFilterDTO);
                ;
                DataTable dataTable = new DataTable("HouseHold Cluster");

                dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                adminResult.ForEach(ar =>
                {
                    DataRow dataRow = dataTable.NewRow();

                    ar.GetType().GetProperties().ToList().ForEach(a =>
                    {
                        dataRow[a.Name] = a.GetValue(ar);
                    });

                    dataTable.Rows.Add(dataRow);
                });

                using XLWorkbook xlWorkbook = new XLWorkbook();

                xlWorkbook.Worksheets.Add(dataTable);

                using MemoryStream memoryStream = new MemoryStream();

                xlWorkbook.SaveAs(memoryStream);

                return memoryStream.ToArray();




                return null;

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw ex;
            }
        }
        #endregion



        //////////////////////////////////////////////////////New Dashboard /////////////////////////////////////////

        #region Home Tab
        #region RegistrationCompliance
        public List<RegistrationCompaliance> RegistrationCompliance(FilterDTO homeTabFilter)
        {
            try
            {
                List<RegistrationCompaliance> Counts = new List<RegistrationCompaliance>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplaince", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }


                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationCompaliance DTO = new RegistrationCompaliance();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Designation = (reader["Designation"].ToString());
                            DTO.Total = Convert.ToInt32(reader["Total"]);
                            DTO.TotalRegistered = Convert.ToInt32(reader["TotalRegistered"]);


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Organizationwiseregistration 
        public List<Organizationwiseregistration> Organizationwiseregistration(FilterDTO homeTabFilter)
        {
            try
            {
                List<Organizationwiseregistration> Counts = new List<Organizationwiseregistration>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceOrganizationWiseregistration", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }


                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Organizationwiseregistration DTO = new Organizationwiseregistration();

                            DTO.OrganizationName = reader["OrganizationName"].ToString();
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Administrativecompliance
        public List<AdministrativeComplaince> Administrativecompliance(FilterDTO homeTabFilter)
        {
            try
            {
                List<AdministrativeComplaince> Counts = new List<AdministrativeComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceAdministrative", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }


                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AdministrativeComplaince DTO = new AdministrativeComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.TotalRegistered = Convert.ToInt32(reader["TotalRegistered"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringTotalFormFilled
        public List<FixedSiteMonitoringTotalFormFilled> FixedSiteMonitoringTotalFormFilled(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringTotalFormFilled> Counts = new List<FixedSiteMonitoringTotalFormFilled>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringTotalFormFilled", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringTotalFormFilled DTO = new FixedSiteMonitoringTotalFormFilled();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringTotalFormFilledDesigantionWise
        public List<FixedSiteMonitoringTotalFormFilledDesigantionWise> FixedSiteMonitoringTotalFormFilledDesigantionWise(FilterDTO homeTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringTotalFormFilledDesigantionWise> Counts = new List<FixedSiteMonitoringTotalFormFilledDesigantionWise>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringTotalFormFilledDesigantionWise", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }


                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringTotalFormFilledDesigantionWise DTO = new FixedSiteMonitoringTotalFormFilledDesigantionWise();

                            DTO.Designation = reader["Designation"].ToString();
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringFunctionalSIAFixedSite
        public List<FixedSiteMonitoringFunctionalSIAFixedSite> FixedSiteMonitoringFunctionalSIAFixedSite(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringFunctionalSIAFixedSite> Counts = new List<FixedSiteMonitoringFunctionalSIAFixedSite>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringFunctionalSIAFixedSite", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringFunctionalSIAFixedSite DTO = new FixedSiteMonitoringFunctionalSIAFixedSite();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Functional = Convert.ToInt32(reader["Functional"].ToString());
                            DTO.NonFunctional = Convert.ToInt32(reader["NonFunctional"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SuperVisorTrainedUnTrainedStaff
        public List<SuperVisorTrainedUnTrainedStaff> SuperVisorTrainedUnTrainedStaff(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<SuperVisorTrainedUnTrainedStaff> Counts = new List<SuperVisorTrainedUnTrainedStaff>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SuperVisorTrainedUnTrainedStaff", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SuperVisorTrainedUnTrainedStaff DTO = new SuperVisorTrainedUnTrainedStaff();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Trained = Convert.ToInt32(reader["Trained"].ToString());
                            DTO.UnTrained = Convert.ToInt32(reader["UnTrained"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region UCMOAICHHCluster
        public List<UCMOAICHHCluster> UCMOAICHHCluster(FilterDTO homeTabFilter)
        {
            try
            {
                List<UCMOAICHHCluster> Counts = new List<UCMOAICHHCluster>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_UCMOAICHHCluster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICHHCluster DTO = new UCMOAICHHCluster();

                            DTO.Name = reader["Name"].ToString();
                            DTO.AIClessthan3 = Convert.ToInt32(reader["AIC < 3"].ToString());
                            DTO.AICgreaterthanequal3 = Convert.ToInt32(reader["AIC >= 3"].ToString());
                            DTO.UCMOlessthan3 = Convert.ToInt32(reader["UCMO < 3"].ToString());
                            DTO.UCMOgreaterthan3 = Convert.ToInt32(reader["UCMO >=3"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region UCMOAICSupervisorCluster
        public List<UCMOAICSupervisorCluster> UCMOAICSupervisorCluster(FilterDTO homeTabFilter)
        {
            try
            {
                List<UCMOAICSupervisorCluster> Counts = new List<UCMOAICSupervisorCluster>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_UCMOAICSupervisorCluster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICSupervisorCluster DTO = new UCMOAICSupervisorCluster();

                            DTO.Name = reader["Name"].ToString();
                            DTO.AIClessthan4 = Convert.ToInt32(reader["AIC < 4"].ToString());
                            DTO.AICgreaterthanequal4 = Convert.ToInt32(reader["AIC >= 4"].ToString());
                            DTO.UCMOlessthan4 = Convert.ToInt32(reader["UCMO < 4"].ToString());
                            DTO.UCMOgreaterthan4 = Convert.ToInt32(reader["UCMO >=4"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringNeapComposition
        public List<MobileTeamMonitoringNeapComposition> MobileTeamMonitoringNeapComposition(FilterDTO homeTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringNeapComposition> Counts = new List<MobileTeamMonitoringNeapComposition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringNeapComposition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringNeapComposition DTO = new MobileTeamMonitoringNeapComposition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.BothAdult = Convert.ToInt32(reader["BothAdult"].ToString());
                            DTO.Local = Convert.ToInt32(reader["Local"].ToString());
                            DTO.Government = Convert.ToInt32(reader["Government"].ToString());
                            DTO.Female = Convert.ToInt32(reader["Femalemember"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringTrainedUnTrainedStaff
        public List<MobileTeamMonitoringTrainedUnTrainedStaff> MobileTeamMonitoringTrainedUnTrainedStaff(FilterDTO homeTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringTrainedUnTrainedStaff> Counts = new List<MobileTeamMonitoringTrainedUnTrainedStaff>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringTrainedUnTrainedStaff", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringTrainedUnTrainedStaff DTO = new MobileTeamMonitoringTrainedUnTrainedStaff();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Trained = Convert.ToInt32(reader["Trained"].ToString());
                            DTO.UnTrained = Convert.ToInt32(reader["UnTrained"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringVaccineCondition
        public List<MobileTeamMonitoringVaccineCondition> MobileTeamMonitoringVaccineCondition(FilterDTO homeTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringVaccineCondition> Counts = new List<MobileTeamMonitoringVaccineCondition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringVaccineCondition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringVaccineCondition DTO = new MobileTeamMonitoringVaccineCondition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Dry = Convert.ToInt32(reader["Dry"].ToString());
                            DTO.Cool = Convert.ToInt32(reader["Cool"].ToString());
                            DTO.ValidVVM = Convert.ToInt32(reader["Valid VVM"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TransitTeamMonitoringNeapComposition
        public List<TransitTeamMonitoringNeapComposition> TransitTeamMonitoringNeapComposition(FilterDTO homeTabFilter)
        {
            try
            {
                List<TransitTeamMonitoringNeapComposition> Counts = new List<TransitTeamMonitoringNeapComposition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitTeamMonitoringNeapComposition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitTeamMonitoringNeapComposition DTO = new TransitTeamMonitoringNeapComposition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Local = Convert.ToInt32(reader["Local"].ToString());
                            DTO.Trained = Convert.ToInt32(reader["Trained"].ToString());
                            DTO.Adult = Convert.ToInt32(reader["Adult"].ToString());
                            DTO.Others = Convert.ToInt32(reader["Others"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TransitTeamMonitoringLogisticsForEachMember
        public List<TransitTeamMonitoringLogisticsForEachMember> TransitTeamMonitoringLogisticsForEachMember(FilterDTO homeTabFilter)
        {
            try
            {
                List<TransitTeamMonitoringLogisticsForEachMember> Counts = new List<TransitTeamMonitoringLogisticsForEachMember>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitTeamMonitoringLogisticsForEachMember", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitTeamMonitoringLogisticsForEachMember DTO = new TransitTeamMonitoringLogisticsForEachMember();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.VaccinceCarrier = Convert.ToInt32(reader["VaccinceCarrier"].ToString());
                            DTO.FM = Convert.ToInt32(reader["FM"].ToString());
                            DTO.TallySheet = Convert.ToInt32(reader["TallySheet"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TransitTeamMonitoringVaccineCondition
        public List<TransitTeamMonitoringVaccineCondition> TransitTeamMonitoringVaccineCondition(FilterDTO homeTabFilter)
        {
            try
            {
                List<TransitTeamMonitoringVaccineCondition> Counts = new List<TransitTeamMonitoringVaccineCondition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitTeamMonitoringVaccineCondition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitTeamMonitoringVaccineCondition DTO = new TransitTeamMonitoringVaccineCondition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Dry = Convert.ToInt32(reader["Dry"].ToString());
                            DTO.Cool = Convert.ToInt32(reader["Cool"].ToString());
                            DTO.ValidVVM = Convert.ToInt32(reader["Valid VVM"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TransitTeamMonitoringLEAsSupport
        public List<TransitTeamMonitoringLEAsSupport> TransitTeamMonitoringLEAsSupport(FilterDTO homeTabFilter)
        {
            try
            {
                List<TransitTeamMonitoringLEAsSupport> Counts = new List<TransitTeamMonitoringLEAsSupport>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitTeamMonitoringLEAsSupport", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitTeamMonitoringLEAsSupport DTO = new TransitTeamMonitoringLEAsSupport();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Present = Convert.ToInt32(reader["Present"].ToString());
                            DTO.Helping = Convert.ToInt32(reader["Helping"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());



                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldClusterPopulationType
        public List<HouseHoldClusterPopulationType> HouseHoldClusterPopulationType(FilterDTO homeTabFilter)
        {
            try
            {
                List<HouseHoldClusterPopulationType> Counts = new List<HouseHoldClusterPopulationType>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterPopulationType", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterPopulationType DTO = new HouseHoldClusterPopulationType();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.PopulationType = (reader["PopulationType"].ToString());
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());



                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldClusterVaccinatedandUnVaccinatedAgeWise
        public List<HouseHoldClusterVaccinatedandUnVaccinatedAgeWise> HouseHoldClusterVaccinatedandUnVaccinatedAgeWise(FilterDTO homeTabFilter)
        {
            try
            {
                List<HouseHoldClusterVaccinatedandUnVaccinatedAgeWise> Counts = new List<HouseHoldClusterVaccinatedandUnVaccinatedAgeWise>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterVaccinatedandUnVaccinatedAgeWise", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        if (homeTabFilter.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", homeTabFilter.PopulationType);

                        }



                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterVaccinatedandUnVaccinatedAgeWise DTO = new HouseHoldClusterVaccinatedandUnVaccinatedAgeWise();

                            DTO.Name = reader["Name"].ToString();
                            DTO.zeroto11MChecked = Convert.ToInt32(reader["0-11M checked"].ToString());
                            DTO.zeroto11Mvaccinated = Convert.ToInt32(reader["0-11M Vaccinated"].ToString());
                            DTO.zeroto11Munvaccinated = Convert.ToInt32(reader["0-11M UnVaccinated"].ToString());
                            DTO.eleventto59MChecked = Convert.ToInt32(reader["12-59M checked"].ToString());
                            DTO.eleventto59vaccinated = Convert.ToInt32(reader["12-59M Vaccinated"].ToString());
                            DTO.eleventto59unvaccinated = Convert.ToInt32(reader["12-59M UnVaccinated"].ToString());



                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldClusterreasonformissed
        public List<HouseHoldClusterreasonformissed> HouseHoldClusterreasonformissed(FilterDTO homeTabFilter)
        {
            try
            {
                List<HouseHoldClusterreasonformissed> Counts = new List<HouseHoldClusterreasonformissed>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HHVHHCShit", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        if (homeTabFilter.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", homeTabFilter.PopulationType);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterreasonformissed DTO = new HouseHoldClusterreasonformissed();

                            DTO.Name = reader["Name"].ToString();
                            DTO.TeamMissedthehouse = Convert.ToInt32(reader["TeamMissedthehouse"].ToString());
                            DTO.TVBMC = Convert.ToInt32(reader["TVBMC"].ToString());
                            DTO.NA = Convert.ToInt32(reader["NA"].ToString());
                            DTO.Ref = Convert.ToInt32(reader["Ref"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());




                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldClusterZeroDoseEIChildren
        public List<HouseHoldClusterZeroDoseEIChildren> HouseHoldClusterZeroDoseEIChildren(FilterDTO homeTabFilter)
        {
            try
            {
                List<HouseHoldClusterZeroDoseEIChildren> Counts = new List<HouseHoldClusterZeroDoseEIChildren>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterZeroDoseEIChildren", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterZeroDoseEIChildren DTO = new HouseHoldClusterZeroDoseEIChildren();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());



                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region CatchUpHouseHoldClusterPopulationType
        public List<CatchUpHouseHoldClusterPopulationType> CatchUpHouseHoldClusterPopulationType(FilterDTO homeTabFilter)
        {
            try
            {
                List<CatchUpHouseHoldClusterPopulationType> Counts = new List<CatchUpHouseHoldClusterPopulationType>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_CatchUpHouseHoldClusterPopulationType", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", homeTabFilter.Designation);

                        }
                        if (homeTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", homeTabFilter.Organization);

                        }
                        if (homeTabFilter.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", homeTabFilter.PopulationType);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CatchUpHouseHoldClusterPopulationType DTO = new CatchUpHouseHoldClusterPopulationType();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Total = Convert.ToInt32(reader["Total"].ToString());
                            DTO.PopulationType = (reader["PopulationType"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region CatchupHouseHoldChecked
        public List<CatchupHouseHoldChecked> CatchupHouseHoldChecked(FilterDTO homeTabFilter)
        {
            try
            {
                List<CatchupHouseHoldChecked> Counts = new List<CatchupHouseHoldChecked>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_CatchupHouseHoldChecked", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CatchupHouseHoldChecked DTO = new CatchupHouseHoldChecked();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.zeroByzero = Convert.ToInt32(reader["zeroByzero"].ToString());
                            DTO.Lock = Convert.ToInt32(reader["Lock"].ToString());
                            DTO.Ref = Convert.ToInt32(reader["Ref"].ToString());
                            DTO.NA = Convert.ToInt32(reader["NA"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Complaince

        #region RegistrationCompliance
        public List<RegistrationComplianceUClevel> RegistrationComplianceUClevel(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<RegistrationComplianceUClevel> Counts = new List<RegistrationComplianceUClevel>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceUClevel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }


                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationComplianceUClevel DTO = new RegistrationComplianceUClevel();

                            DTO.Name = reader["Name"].ToString();
                            DTO.UCMO = Convert.ToInt32(reader["UCMO"].ToString());
                            DTO.AIC = Convert.ToInt32(reader["AIC"].ToString());
                            DTO.UCPO = Convert.ToInt32(reader["UCPO"].ToString());
                            DTO.UCSP = Convert.ToInt32(reader["UCSP"].ToString());
                            DTO.UCCO = Convert.ToInt32(reader["UCCO"].ToString());
                            DTO.SM = Convert.ToInt32(reader["SM"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<RegistrationComplianceTehsillevel> RegistrationComplianceTehsillevel(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<RegistrationComplianceTehsillevel> Counts = new List<RegistrationComplianceTehsillevel>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceTehsillevel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }


                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationComplianceTehsillevel DTO = new RegistrationComplianceTehsillevel();

                            DTO.Name = reader["Name"].ToString();
                            DTO.TPO = Convert.ToInt32(reader["TPO"].ToString());
                            DTO.TCO = Convert.ToInt32(reader["TCO"].ToString());
                            DTO.TSP = Convert.ToInt32(reader["TSP"].ToString());
                            DTO.DDHO = Convert.ToInt32(reader["DDHO"].ToString());
                            DTO.Other= Convert.ToInt32(reader["Other"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RegistrationComplianceDistrictlevel> RegistrationComplianceDistrictlevel(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<RegistrationComplianceDistrictlevel> Counts = new List<RegistrationComplianceDistrictlevel>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceDistrictlevel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }


                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationComplianceDistrictlevel DTO = new RegistrationComplianceDistrictlevel();

                            DTO.Name = reader["Name"].ToString();
                            DTO.IO = Convert.ToInt32(reader["IO"].ToString());
                            DTO.DHCSO = Convert.ToInt32(reader["DHCSO"].ToString());
                            DTO.DSO = Convert.ToInt32(reader["DSO"].ToString());
                            DTO.DHO = Convert.ToInt32(reader["DHO"].ToString());
                            DTO.CEO = Convert.ToInt32(reader["CEO"].ToString());



                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RegistrationComplianceDivisionallevel> RegistrationComplianceDivisionlevel(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<RegistrationComplianceDivisionallevel> Counts = new List<RegistrationComplianceDivisionallevel>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceDivisionallevel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }


                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationComplianceDivisionallevel DTO = new RegistrationComplianceDivisionallevel();

                            DTO.Name = reader["Name"].ToString();
                            DTO.AreaCoordinator = Convert.ToInt32(reader["AreaCoordinator"].ToString());
                            DTO.DCO = Convert.ToInt32(reader["DCO"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RegistrationComplianceProvincelevel> RegistrationComplianceProvincelevel(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<RegistrationComplianceProvincelevel> Counts = new List<RegistrationComplianceProvincelevel>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrationComplainceProvincelevel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }


                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationComplianceProvincelevel DTO = new RegistrationComplianceProvincelevel();

                            DTO.Name = reader["Name"].ToString();
                            DTO.PEIRISynergyCoordinator = Convert.ToInt32(reader["PEIRISynergyCoordinator"].ToString());
                            DTO.ProvincialCampagnSupportOfficer = Convert.ToInt32(reader["ProvincialCampagnSupportOfficer"].ToString());
                            DTO.ProvincialMonitoringandEvaluationOfficer = Convert.ToInt32(reader["ProvincialMonitoringandEvaluationOfficer"].ToString());
                            DTO.ProvincialPolioEradicationOfficer = Convert.ToInt32(reader["ProvincialPolioEradicationOfficer"].ToString());
                            DTO.ProvincialTrainingOfficer = Convert.ToInt32(reader["ProvincialTrainingOfficer"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region FixedSiteUCMOandAICComplaince
        public List<UCMOAICComplaince> FixedSiteUCMOAICComplaince(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<UCMOAICComplaince> Counts = new List<UCMOAICComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteUCMOandAICCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICComplaince DTO = new UCMOAICComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.UCMO = Convert.ToInt32(reader["UCMO"].ToString());
                            DTO.AIC = Convert.ToInt32(reader["AIC"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteOrganizationComplaince
        public List<OrganizationComplaince> FixedSiteOrganizationComplaince(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<OrganizationComplaince> Counts = new List<OrganizationComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteOrganizationCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrganizationComplaince DTO = new OrganizationComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Government = Convert.ToInt32(reader["Government"].ToString());
                            DTO.WHO = Convert.ToInt32(reader["WHO"].ToString());
                            DTO.UNICEF = Convert.ToInt32(reader["UNICEF"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SupervisormonitoringUCMOandAICComplaince
        public List<UCMOAICComplaince> SupervisormonitoringUCMOandAICComplaince(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<UCMOAICComplaince> Counts = new List<UCMOAICComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SupervisormonitoringUCMOandAICCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICComplaince DTO = new UCMOAICComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.UCMO = Convert.ToInt32(reader["UCMO"].ToString());
                            DTO.AIC = Convert.ToInt32(reader["AIC"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SupervisormonitoringOrganizationComplaince
        public List<OrganizationComplaince> SupervisormonitoringOrganizationComplaince(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<OrganizationComplaince> Counts = new List<OrganizationComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SupervisormonitoringOrganizationCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrganizationComplaince DTO = new OrganizationComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Government = Convert.ToInt32(reader["Government"].ToString());
                            DTO.WHO = Convert.ToInt32(reader["WHO"].ToString());
                            DTO.UNICEF = Convert.ToInt32(reader["UNICEF"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TeammonitoringUCMOandAICComplaince
        public List<UCMOAICComplaince> TeammonitoringUCMOandAICComplaince(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<UCMOAICComplaince> Counts = new List<UCMOAICComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TeamMonitoringUCMOandAICCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICComplaince DTO = new UCMOAICComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.UCMO = Convert.ToInt32(reader["UCMO"].ToString());
                            DTO.AIC = Convert.ToInt32(reader["AIC"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TeammonitoringOrganizationComplaince
        public List<OrganizationComplaince> TeammonitoringOrganizationComplaince(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<OrganizationComplaince> Counts = new List<OrganizationComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TeammonitoringOrganizationCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrganizationComplaince DTO = new OrganizationComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Government = Convert.ToInt32(reader["Government"].ToString());
                            DTO.WHO = Convert.ToInt32(reader["WHO"].ToString());
                            DTO.UNICEF = Convert.ToInt32(reader["UNICEF"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringUCMOandAICCompliance
        public List<UCMOAICComplaince> MobileTeamMonitoringUCMOandAICCompliance(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<UCMOAICComplaince> Counts = new List<UCMOAICComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringUCMOandAICCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICComplaince DTO = new UCMOAICComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.UCMO = Convert.ToInt32(reader["UCMO"].ToString());
                            DTO.AIC = Convert.ToInt32(reader["AIC"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamOrganizationCompliance
        public List<OrganizationComplaince> MobileTeamOrganizationCompliance(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<OrganizationComplaince> Counts = new List<OrganizationComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamOrganizationCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrganizationComplaince DTO = new OrganizationComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Government = Convert.ToInt32(reader["Government"].ToString());
                            DTO.WHO = Convert.ToInt32(reader["WHO"].ToString());
                            DTO.UNICEF = Convert.ToInt32(reader["UNICEF"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldUCMOandAICCompliance
        public List<UCMOAICComplaince> HouseHoldUCMOandAICCompliance(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<UCMOAICComplaince> Counts = new List<UCMOAICComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldUCMOandAICCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UCMOAICComplaince DTO = new UCMOAICComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.UCMO = Convert.ToInt32(reader["UCMO"].ToString());
                            DTO.AIC = Convert.ToInt32(reader["AIC"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldOrganizationCompliance
        public List<OrganizationComplaince> HouseHoldOrganizationCompliance(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<OrganizationComplaince> Counts = new List<OrganizationComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldOrganizationCompliance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrganizationComplaince DTO = new OrganizationComplaince();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Government = Convert.ToInt32(reader["Government"].ToString());
                            DTO.WHO = Convert.ToInt32(reader["WHO"].ToString());
                            DTO.UNICEF = Convert.ToInt32(reader["UNICEF"].ToString());
                            DTO.Other = Convert.ToInt32(reader["Other"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Fixed Site

        #region FixedSiteMonitoringNeap
        public List<FixedSiteMonitoringNeap> FixedSiteMonitoringNeap(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringNeap> Counts = new List<FixedSiteMonitoringNeap>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringNeap", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringNeap DTO = new FixedSiteMonitoringNeap();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Local= Convert.ToInt32(reader["Local"].ToString());
                            DTO.Adult = Convert.ToInt32(reader["Adult"].ToString());
                            DTO.Trained = Convert.ToInt32(reader["Trained"].ToString());
                            DTO.GovernmentAccountable = Convert.ToInt32(reader["GovernmentAccountable"].ToString());
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringVaccineCondition
        public List<FixedSiteMonitoringVaccineCondition> FixedSiteMonitoringVaccineCondition(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringVaccineCondition> Counts = new List<FixedSiteMonitoringVaccineCondition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringVaccineCondition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringVaccineCondition DTO = new FixedSiteMonitoringVaccineCondition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Dry = Convert.ToInt32(reader["Dry"].ToString());
                            DTO.Cool = Convert.ToInt32(reader[" Cool"].ToString());
                            DTO.ValidVVM = Convert.ToInt32(reader["Valid VVM"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringTemperatureCondition
        public List<FixedSiteMonitoringTemperatureCondition> FixedSiteMonitoringTemperatureCondition(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringTemperatureCondition> Counts = new List<FixedSiteMonitoringTemperatureCondition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringTemperatureCondition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringTemperatureCondition DTO = new FixedSiteMonitoringTemperatureCondition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.NotMaintained = Convert.ToInt32(reader["NotMaintained"].ToString());
                            DTO.Maintained = Convert.ToInt32(reader["Maintained"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringEssentialImmunizationduringSIA
        public List<FixedSiteMonitoringEssentialImmunizationduringSIA> FixedSiteMonitoringEssentialImmunizationduringSIA(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringEssentialImmunizationduringSIA> Counts = new List<FixedSiteMonitoringEssentialImmunizationduringSIA>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringEssentialImmunizationduringSIAs", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringEssentialImmunizationduringSIA DTO = new FixedSiteMonitoringEssentialImmunizationduringSIA();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Given = Convert.ToInt32(reader["Given"].ToString());
                            DTO.NotGiven = Convert.ToInt32(reader["NotGiven"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringZeroDoseReferral
        public List<FixedSiteMonitoringZeroDoseReferral> FixedSiteMonitoringZeroDoseReferral(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringZeroDoseReferral> Counts = new List<FixedSiteMonitoringZeroDoseReferral>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringZeroDoseReferral", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringZeroDoseReferral DTO = new FixedSiteMonitoringZeroDoseReferral();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Referred = Convert.ToInt32(reader["Referred"].ToString());
                            DTO.NotReferred = Convert.ToInt32(reader["NotReferred"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region FixedSiteMonitoringAFPCaseDefinition
        public List<FixedSiteMonitoringAFPCaseDefinition> FixedSiteMonitoringAFPCaseDefinition(FilterDTO fixedSiteTabFilter)
        {
            try
            {
                List<FixedSiteMonitoringAFPCaseDefinition> Counts = new List<FixedSiteMonitoringAFPCaseDefinition>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSiteMonitoringAFPCaseDefinition", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (fixedSiteTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", fixedSiteTabFilter.FilterLvl);

                        }

                        if (fixedSiteTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", fixedSiteTabFilter.Code);

                        }

                        if (fixedSiteTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", fixedSiteTabFilter.campaignId);

                        }

                        if (fixedSiteTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", fixedSiteTabFilter.day);

                        }

                        if (fixedSiteTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", fixedSiteTabFilter.Designation);

                        }
                        if (fixedSiteTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", fixedSiteTabFilter.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteMonitoringAFPCaseDefinition DTO = new FixedSiteMonitoringAFPCaseDefinition();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Available = Convert.ToInt32(reader["Available"]);
                            DTO.NotAvailable = Convert.ToInt32(reader["NotAvailable"]);
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region SuperVisorMonitoring

        #region SuperVisorNecessaryitems
        public List<SuperVisorNecessaryitems> SuperVisorNecessaryitems(FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                List<SuperVisorNecessaryitems> Counts = new List<SuperVisorNecessaryitems>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SuperVisorNecessaryitems", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (superVisorMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", superVisorMonitoringTabFilter.FilterLvl);

                        }

                        if (superVisorMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", superVisorMonitoringTabFilter.Code);

                        }

                        if (superVisorMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", superVisorMonitoringTabFilter.campaignId);

                        }

                        if (superVisorMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", superVisorMonitoringTabFilter.day);

                        }

                        if (superVisorMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", superVisorMonitoringTabFilter.Designation);

                        }
                        if (superVisorMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", superVisorMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SuperVisorNecessaryitems DTO = new SuperVisorNecessaryitems();

                            DTO.Name = reader["Name"].ToString();
                            DTO.OPVVial = Convert.ToInt32(reader["OPVVial"].ToString());
                            DTO.FM = Convert.ToInt32(reader["FM"].ToString());
                            DTO.Chalks = Convert.ToInt32(reader["Chalks"].ToString());
                            DTO.OpsPlan = Convert.ToInt32(reader["OpsPlan"].ToString());
                            DTO.MClist = Convert.ToInt32(reader["MClist"].ToString());
                            DTO.MonitoringPlan = Convert.ToInt32(reader["MonitoringPlan"].ToString());
                            DTO.HRMPlist = Convert.ToInt32(reader["HRMPlist"].ToString());
                            DTO.Transport = Convert.ToInt32(reader["Transport"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SuperVisorTeamMonitornigchecklist
        public List<SuperVisorTeamMonitornigchecklist> SuperVisorTeamMonitornigchecklist(FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                List<SuperVisorTeamMonitornigchecklist> Counts = new List<SuperVisorTeamMonitornigchecklist>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SuperVisorTeamMonitornigchecklist", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (superVisorMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", superVisorMonitoringTabFilter.FilterLvl);

                        }

                        if (superVisorMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", superVisorMonitoringTabFilter.Code);

                        }

                        if (superVisorMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", superVisorMonitoringTabFilter.campaignId);

                        }

                        if (superVisorMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", superVisorMonitoringTabFilter.day);

                        }

                        if (superVisorMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", superVisorMonitoringTabFilter.Designation);

                        }
                        if (superVisorMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", superVisorMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SuperVisorTeamMonitornigchecklist DTO = new SuperVisorTeamMonitornigchecklist();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Correctlyfilled = Convert.ToInt32(reader["Correctlyfilled"].ToString());
                            DTO.Incorrect = Convert.ToInt32(reader["Incorrect"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SuperVisorHRMPVisits
        public List<SuperVisorHRMPVisits> SuperVisorHRMPVisits(FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                List<SuperVisorHRMPVisits> Counts = new List<SuperVisorHRMPVisits>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SuperVisorHRMPVisits", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (superVisorMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", superVisorMonitoringTabFilter.FilterLvl);

                        }

                        if (superVisorMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", superVisorMonitoringTabFilter.Code);

                        }

                        if (superVisorMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", superVisorMonitoringTabFilter.campaignId);

                        }

                        if (superVisorMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", superVisorMonitoringTabFilter.day);

                        }

                        if (superVisorMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", superVisorMonitoringTabFilter.Designation);

                        }
                        if (superVisorMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", superVisorMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SuperVisorHRMPVisits DTO = new SuperVisorHRMPVisits();

                            DTO.Name = reader["Name"].ToString();
                            DTO.yes = Convert.ToInt32(reader["yes"].ToString());
                            DTO.No = Convert.ToInt32(reader["No"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SuperVisorHHClustertaken
        public List<SuperVisorHHClustertaken> SuperVisorHHClustertaken(FilterDTO superVisorMonitoringTabFilter)
        {
            try
            {
                List<SuperVisorHHClustertaken> Counts = new List<SuperVisorHHClustertaken>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_SuperVisorHHClustertaken", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (superVisorMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", superVisorMonitoringTabFilter.FilterLvl);

                        }

                        if (superVisorMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", superVisorMonitoringTabFilter.Code);

                        }

                        if (superVisorMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", superVisorMonitoringTabFilter.campaignId);

                        }

                        if (superVisorMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", superVisorMonitoringTabFilter.day);

                        }

                        if (superVisorMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", superVisorMonitoringTabFilter.Designation);

                        }
                        if (superVisorMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", superVisorMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SuperVisorHHClustertaken DTO = new SuperVisorHHClustertaken();

                            DTO.Name = reader["Name"].ToString();
                            DTO.yes = Convert.ToInt32(reader["yes"].ToString());
                            DTO.No = Convert.ToInt32(reader["No"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #endregion

        #region TeamMonitoring

        #region MobileTeamMonitoringDataRecording
        public List<MobileTeamMonitoringDataRecording> MobileTeamMonitoringDataRecording(FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringDataRecording> Counts = new List<MobileTeamMonitoringDataRecording>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringDataRecording", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (teamMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", teamMonitoringTabFilter.FilterLvl);

                        }

                        if (teamMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", teamMonitoringTabFilter.Code);

                        }

                        if (teamMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", teamMonitoringTabFilter.campaignId);

                        }

                        if (teamMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", teamMonitoringTabFilter.day);

                        }

                        if (teamMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", teamMonitoringTabFilter.Designation);

                        }
                        if (teamMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", teamMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringDataRecording DTO = new MobileTeamMonitoringDataRecording();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.yes = Convert.ToInt32(reader["yes"].ToString());
                            DTO.No = Convert.ToInt32(reader["No"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringRouteMaps
        public List<MobileTeamMonitoringRouteMaps> MobileTeamMonitoringRouteMaps(FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringRouteMaps> Counts = new List<MobileTeamMonitoringRouteMaps>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringRouteMaps", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (teamMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", teamMonitoringTabFilter.FilterLvl);

                        }

                        if (teamMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", teamMonitoringTabFilter.Code);

                        }

                        if (teamMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", teamMonitoringTabFilter.campaignId);

                        }

                        if (teamMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", teamMonitoringTabFilter.day);

                        }

                        if (teamMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", teamMonitoringTabFilter.Designation);

                        }
                        if (teamMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", teamMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringRouteMaps DTO = new MobileTeamMonitoringRouteMaps();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Avaiable = Convert.ToInt32(reader["Available"].ToString());
                            DTO.NotAvailable = Convert.ToInt32(reader["NotAvailable"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringIPCquestions
        public List<MobileTeamMonitoringIPCquestions> MobileTeamMonitoringIPCquestions(FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringIPCquestions> Counts = new List<MobileTeamMonitoringIPCquestions>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringIPCquestions", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (teamMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", teamMonitoringTabFilter.FilterLvl);

                        }

                        if (teamMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", teamMonitoringTabFilter.Code);

                        }

                        if (teamMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", teamMonitoringTabFilter.campaignId);

                        }

                        if (teamMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", teamMonitoringTabFilter.day);

                        }

                        if (teamMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", teamMonitoringTabFilter.Designation);

                        }
                        if (teamMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", teamMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringIPCquestions DTO = new MobileTeamMonitoringIPCquestions();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.GoodIPC = Convert.ToInt32(reader["GoodIPC"].ToString());
                            DTO.PoorIPC = Convert.ToInt32(reader["PoorIPC"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MobileTeamMonitoringZeroDoserecording
        public List<MobileTeamMonitoringZeroDoserecording> MobileTeamMonitoringZeroDoserecording(FilterDTO teamMonitoringTabFilter)
        {
            try
            {
                List<MobileTeamMonitoringZeroDoserecording> Counts = new List<MobileTeamMonitoringZeroDoserecording>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_MobileTeamMonitoringZeroDoserecording", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (teamMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", teamMonitoringTabFilter.FilterLvl);

                        }

                        if (teamMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", teamMonitoringTabFilter.Code);

                        }

                        if (teamMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", teamMonitoringTabFilter.campaignId);

                        }

                        if (teamMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", teamMonitoringTabFilter.day);

                        }

                        if (teamMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", teamMonitoringTabFilter.Designation);

                        }
                        if (teamMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", teamMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MobileTeamMonitoringZeroDoserecording DTO = new MobileTeamMonitoringZeroDoserecording();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Recording = Convert.ToInt32(reader["Recording"].ToString());
                            DTO.NotRecording = Convert.ToInt32(reader["NotRecording"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region TransitTeamMonitoring

        #region TransitTeamMonitoringCNIC
        public List<TransitTeamMonitoringCNIC> TransitTeamMonitoringCNIC(FilterDTO transitTeamMonitoringTabFilter)
        {
            try
            {
                List<TransitTeamMonitoringCNIC> Counts = new List<TransitTeamMonitoringCNIC>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitTeamMonitoringCNIC", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (transitTeamMonitoringTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", transitTeamMonitoringTabFilter.FilterLvl);

                        }

                        if (transitTeamMonitoringTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", transitTeamMonitoringTabFilter.Code);

                        }

                        if (transitTeamMonitoringTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", transitTeamMonitoringTabFilter.campaignId);

                        }

                        if (transitTeamMonitoringTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", transitTeamMonitoringTabFilter.day);

                        }

                        if (transitTeamMonitoringTabFilter.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", transitTeamMonitoringTabFilter.Designation);

                        }
                        if (transitTeamMonitoringTabFilter.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", transitTeamMonitoringTabFilter.Organization);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitTeamMonitoringCNIC DTO = new TransitTeamMonitoringCNIC();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Valid = Convert.ToInt32(reader["Valid"].ToString());
                            DTO.NotValid = Convert.ToInt32(reader["NotValid"].ToString());
                            DTO.Denominator = Convert.ToInt32(reader["Denominator"].ToString());


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region HouseHoldCluster

        #region HouseHoldClusterGuestChildrenVaccination
        public List<HouseHoldClusterGuestChildrenVaccination> HouseHoldClusterGuestChildrenVaccination(FilterDTO filterDTO)
        {
            try
            {
                List<HouseHoldClusterGuestChildrenVaccination> Counts = new List<HouseHoldClusterGuestChildrenVaccination>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterGuestChildrenVaccination", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (filterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", filterDTO.FilterLvl);

                        }

                        if (filterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", filterDTO.Code);

                        }

                        if (filterDTO.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", filterDTO.campaignId);

                        }

                        if (filterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", filterDTO.day);

                        }

                        if (filterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", filterDTO.Designation);

                        }
                        if (filterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", filterDTO.Organization);

                        }
                        if (filterDTO.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", filterDTO.PopulationType);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterGuestChildrenVaccination DTO = new HouseHoldClusterGuestChildrenVaccination();

                            DTO.Name = reader["Name"].ToString();
                            DTO.TotalGuestChildrens = Convert.ToInt32(reader["TotalGuestChildrens"].ToString());
                            DTO.VaccinatedChildrens = Convert.ToInt32(reader["VaccinatedChildrens"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldClusterFingermarkingstatus
        public List<HouseHoldClusterFingermarkingstatus> HouseHoldClusterFingermarkingstatus(FilterDTO filterDTO)
        {
            try
            {
                List<HouseHoldClusterFingermarkingstatus> Counts = new List<HouseHoldClusterFingermarkingstatus>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterFingermarkingstatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (filterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", filterDTO.FilterLvl);

                        }

                        if (filterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", filterDTO.Code);

                        }

                        if (filterDTO.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", filterDTO.campaignId);

                        }

                        if (filterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", filterDTO.day);

                        }

                        if (filterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", filterDTO.Designation);

                        }
                        if (filterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", filterDTO.Organization);

                        }
                        if (filterDTO.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", filterDTO.PopulationType);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterFingermarkingstatus DTO = new HouseHoldClusterFingermarkingstatus();

                            DTO.Name = reader["Name"].ToString();
                            DTO.TotalChildrens = Convert.ToInt32(reader["TotalChildrens"].ToString());
                            DTO.FMChildrens = Convert.ToInt32(reader["FMChildrens"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region HouseHoldClusterEIVaccinationlessthantwoyears
        public List<HouseHoldClusterEIVaccinationlessthantwoyears> HouseHoldClusterEIVaccinationlessthantwoyears(FilterDTO filterDTO)
        {
            try
            {
                List<HouseHoldClusterEIVaccinationlessthantwoyears> Counts = new List<HouseHoldClusterEIVaccinationlessthantwoyears>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterEIVaccinationlessthantwoyears", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (filterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", filterDTO.FilterLvl);

                        }

                        if (filterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", filterDTO.Code);

                        }

                        if (filterDTO.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", filterDTO.campaignId);

                        }

                        if (filterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", filterDTO.day);

                        }

                        if (filterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", filterDTO.Designation);

                        }
                        if (filterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", filterDTO.Organization);

                        }
                        if (filterDTO.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", filterDTO.PopulationType);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterEIVaccinationlessthantwoyears DTO = new HouseHoldClusterEIVaccinationlessthantwoyears();

                            DTO.Name = reader["Name"].ToString();
                            DTO.TotalChildrenslessthantwoyears = Convert.ToInt32(reader["TotalChildrenslessthantwoyears"].ToString());
                            DTO.ZeroDoselessthentwoyears = Convert.ToInt32(reader["ZeroDoselessthentwoyears"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region CatchUpdaysHouseHoldCluster

        #region CatchupHouseHoldFindings
        public List<CatchupHouseHoldFindings> CatchupHouseHoldFindings(FilterDTO homeTabFilter)
        {
            try
            {
                List<CatchupHouseHoldFindings> Counts = new List<CatchupHouseHoldFindings>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_CatchupHouseHoldFindings", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CatchupHouseHoldFindings DTO = new CatchupHouseHoldFindings();

                            DTO.Name = reader["Name"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Match = Convert.ToInt32(reader["Match"]);
                            DTO.NotMatch = Convert.ToInt32(reader["NotMatch"]);

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Maps
        #region FixedSitePinDrops
        public List<FixedSitePinDrops> FixedSitePinDrops(FilterDTO homeTabFilter)
        {
            try
            {
                List<FixedSitePinDrops> Counts = new List<FixedSitePinDrops>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSitePinDrops", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", homeTabFilter.PopulationType);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSitePinDrops DTO = new FixedSitePinDrops();

                            DTO.FormName= reader["FormName"].ToString();
                            DTO.Division = reader["Division"].ToString();
                            DTO.District = reader["District"].ToString();
                            DTO.Tehsil = reader["Tehsil"].ToString();
                            DTO.UC = reader["UC"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Lat = Convert.ToDouble(reader["Lat"]);
                            DTO.Long = Convert.ToDouble(reader["Long"]);
                            DTO.TeamNo= reader["TeamNo"].ToString();
                            DTO.status = reader["Status"].ToString();
                            DTO.Icon = AssignColorIcon(reader["Status"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TransitPinDrops
        public List<TransitPinDrops> TransitPinDrops(FilterDTO homeTabFilter)
        {
            try
            {
                List<TransitPinDrops> Counts = new List<TransitPinDrops>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitPinDrops", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }
                        
                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", homeTabFilter.PopulationType);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitPinDrops DTO = new TransitPinDrops();
                            DTO.FormName = reader["FormName"].ToString();
                            DTO.Division = reader["Division"].ToString();
                            DTO.District = reader["District"].ToString();
                            DTO.Tehsil = reader["Tehsil"].ToString();
                            DTO.UC = reader["UC"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Lat = Convert.ToDouble(reader["Lat"]);
                            DTO.Long = Convert.ToDouble(reader["Long"]);
                            DTO.Icon = AssignColorIcon(reader["Status"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region ReasonFormissedMarkers
        public List<ReasonForMissed> ReasonForMissedMarkers(FilterDTO homeTabFilter)
        {
            try
            {
                List<ReasonForMissed> Counts = new List<ReasonForMissed>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                  

                    using (SqlCommand cmd = new SqlCommand(GetStoreProcedureName(homeTabFilter.ReasonForMissedType), conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (homeTabFilter.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", homeTabFilter.FilterLvl);

                        }

                        if (homeTabFilter.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", homeTabFilter.Code);

                        }

                        if (homeTabFilter.campaignId != null)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", homeTabFilter.campaignId);

                        }

                        if (homeTabFilter.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", homeTabFilter.day);

                        }

                        if (homeTabFilter.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", homeTabFilter.PopulationType);

                        }
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReasonForMissed DTO = new ReasonForMissed();

                            DTO.FormName = reader["FormName"].ToString();
                            DTO.Division = reader["Division"].ToString();
                            DTO.District = reader["District"].ToString();
                            DTO.Tehsil = reader["Tehsil"].ToString();
                            DTO.UC = reader["UC"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.Code = Convert.ToInt32(reader["Code"].ToString());
                            DTO.Lat = Convert.ToDouble(reader["Lat"]);
                            DTO.Long = Convert.ToDouble(reader["Long"]);
                            DTO.Icon = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Reports
        #region FixedSiteReports
        public List<FixedSiteReport> FixedSiteReports(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<FixedSiteReport> Counts = new List<FixedSiteReport>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_FixedSite_Report", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.IndicatorId != null )
                        {
                            cmd.Parameters.AddWithValue("@IndicatorId", reportFilterDTO.IndicatorId);

                        }
                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FixedSiteReport DTO = new FixedSiteReport();

                            DTO.Province = reader["Province"].ToString();
                            DTO.Division = reader["DivisionName"].ToString();
                            DTO.District = reader["DistrictName"].ToString();
                            DTO.Tehsil = reader["TehsilName"].ToString();
                            DTO.UC = reader["UCNumber"].ToString();
                            DTO.AIC = reader["AICName"].ToString();
                            DTO.Surveyor = reader["SurveryorName"].ToString();
                            DTO.Designation= reader["Designation"].ToString();
                            DTO.TeamNo = reader["TeamNo"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.CreationDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            DTO.IndicatorName = reader["IndicatorName"].ToString();
                            DTO.Answer = reader["AnswerDesc"].ToString();

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region SupervisormonitoringReports
        public List<Supervisormonitoring> SupervisormonitoringReports(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<Supervisormonitoring> Counts = new List<Supervisormonitoring>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Supervisormonitoring_Report", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.IndicatorId != null)
                        {
                            cmd.Parameters.AddWithValue("@IndicatorId", reportFilterDTO.IndicatorId);

                        }
                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Supervisormonitoring DTO = new Supervisormonitoring();

                            DTO.Province = reader["Province"].ToString();
                            DTO.Division = reader["DivisionName"].ToString();
                            DTO.District = reader["DistrictName"].ToString();
                            DTO.Tehsil = reader["TehsilName"].ToString();
                            DTO.UC = reader["UCNumber"].ToString();
                            DTO.SuperVisorName = reader["SupervisorName"].ToString();
                            DTO.Surveyor = reader["SurveryorName"].ToString();
                            DTO.Designation = reader["Designation"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.CreationDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            DTO.IndicatorName = reader["IndicatorName"].ToString();
                            DTO.Answer = reader["AnswerDesc"].ToString();

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TeammonitoringReports
        public List<TeammonitoringReports> TeammonitoringReports(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<TeammonitoringReports> Counts = new List<TeammonitoringReports>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Teammonitoring_Report", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.IndicatorId != null)
                        {
                            cmd.Parameters.AddWithValue("@IndicatorId", reportFilterDTO.IndicatorId);

                        }
                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TeammonitoringReports DTO = new TeammonitoringReports();

                            DTO.Province = reader["Province"].ToString();
                            DTO.Division = reader["DivisionName"].ToString();
                            DTO.District = reader["DistrictName"].ToString();
                            DTO.Tehsil = reader["TehsilName"].ToString();
                            DTO.UC = reader["UCNumber"].ToString();
                            DTO.Surveyor = reader["SurveryorName"].ToString();
                            DTO.Designation = reader["Designation"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.CreationDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            DTO.IndicatorName = reader["IndicatorName"].ToString();
                            DTO.Answer = reader["AnswerDesc"].ToString();

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TransitTeammonitoringReports
        public List<TransitTeammonitoringReports> TransitTeammonitoringReports(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<TransitTeammonitoringReports> Counts = new List<TransitTeammonitoringReports>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_TransitTeammonitoring_Report", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.IndicatorId != null)
                        {
                            cmd.Parameters.AddWithValue("@IndicatorId", reportFilterDTO.IndicatorId);

                        }
                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TransitTeammonitoringReports DTO = new TransitTeammonitoringReports();

                            DTO.Province = reader["Province"].ToString();
                            DTO.Division = reader["DivisionName"].ToString();
                            DTO.District = reader["DistrictName"].ToString();
                            DTO.Tehsil = reader["TehsilName"].ToString();
                            DTO.UC = reader["UCNumber"].ToString();
                            DTO.Surveyor = reader["SurveryorName"].ToString();
                            DTO.Designation = reader["Designation"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.CreationDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            DTO.IndicatorName = reader["IndicatorName"].ToString();
                            DTO.Answer = reader["AnswerDesc"].ToString();

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region HouseHoldClusterReport
        public List<HouseHoldCluster> HouseHoldClusterReport(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<HouseHoldCluster> Counts = new List<HouseHoldCluster>();

                string StoreProcedure = "";

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();
     

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldCluster_Reports", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.IndicatorId != null)
                        {
                            cmd.Parameters.AddWithValue("@IndicatorId", reportFilterDTO.IndicatorId);

                        }
                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldCluster DTO = new HouseHoldCluster();

                            DTO.Province = reader["Province"].ToString();
                            DTO.Division = reader["DivisionName"].ToString();
                            DTO.District = reader["DistrictName"].ToString();
                            DTO.Tehsil = reader["TehsilName"].ToString();
                            DTO.UC = reader["UCNumber"].ToString();
                            DTO.Surveyor = reader["SurveryorName"].ToString();
                            DTO.Designation = reader["Designation"].ToString();
                            DTO.Dayofwork = reader["Dayofwork"].ToString();
                            DTO.CreationDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            DTO.zerotoelevenMChecked = Convert.ToInt32(reader["0-11M Checked"].ToString());
                            DTO.zerotoelevenMvaccinated = Convert.ToInt32(reader["0-11M Vaccinated"].ToString());
                            DTO.twelvetofiftynineMChecked = Convert.ToInt32(reader["12-59M Checked"].ToString());
                            DTO.twelvetofiftynineMvaccinated = Convert.ToInt32(reader["12-59M Vaccinated"].ToString());
                            DTO.Teammissedthehouse = Convert.ToInt32(reader["Team missed the house"].ToString());
                            DTO.TVBMC = Convert.ToInt32(reader["TVBMC"].ToString());
                            DTO.NA = Convert.ToInt32(reader["NA"].ToString());
                            DTO.Refusals = Convert.ToInt32(reader["Refusals"].ToString());
                            DTO.Others = Convert.ToInt32(reader["Others"].ToString());
                            DTO.TotalGuestChildren = Convert.ToInt32(reader["Total Guest Children"].ToString());
                            DTO.VaccinatedGuestChildren = Convert.ToInt32(reader["Vaccinated Guest Children"].ToString());
                            DTO.ChildrenUnderTwoYears = Convert.ToInt32(reader["Children Under Two Years"].ToString());
                            //DTO.AgeMatchedChildren = Convert.ToInt32(reader["Age Matched Children"].ToString());
                            DTO.noofchildrenseenzerotofiftynincemonths = Convert.ToInt32(reader["no.of children seen 0-59 months"].ToString());
                            DTO.zerotofiftynincemonthschildrenfingermarked = Convert.ToInt32(reader["0-59childrenfingermarked"].ToString());
                            DTO.ZeroDose = Convert.ToInt32(reader["ZeroDose"].ToString());
                            //DTO.AFPCase = Convert.ToInt32(reader["AFPCase"].ToString());

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<AdministrativeHouseHoldCluster> AdministrativeHouseHoldClusterReport(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<AdministrativeHouseHoldCluster> Counts = new List<AdministrativeHouseHoldCluster>();


                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();


                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldCluster_Report_Administrative", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.IndicatorId != null)
                        {
                            cmd.Parameters.AddWithValue("@IndicatorId", reportFilterDTO.IndicatorId);

                        }
                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AdministrativeHouseHoldCluster DTO = new AdministrativeHouseHoldCluster();

                            DTO.Province = reader["Province"].ToString();
                            DTO.Division = reader["DivisionName"].ToString();
                            DTO.District = reader["DistrictName"].ToString();
                            DTO.Tehsil = reader["TehsilName"].ToString();
                            DTO.UC = reader["UCNumber"].ToString();
                            //DTO.Surveyor = reader["SurveryorName"].ToString();
                            //DTO.Designation = reader["Designation"].ToString();
                            //DTO.Dayofwork = reader["Dayofwork"].ToString();
                            //DTO.CreationDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            DTO.TotalChildrenLessthanFiveYears = Convert.ToInt32(reader["Total <5 years Children seen ?"].ToString());
                            DTO.NoofChildrenFoundFingerMarked = Convert.ToInt32(reader["No of Children found Finger Marked ?"].ToString());
                            DTO.NoofChildrenNotAvailable = Convert.ToInt32(reader["No of Children not available (NA) if any ?"].ToString());
                            DTO.NoofChildrenRefused = Convert.ToInt32(reader["No of Children refused if any ?"].ToString());
                            

                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region MonitoringComplaince
        public List<MonitoringComplaince> MonitoringComplaince(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<MonitoringComplaince> Counts = new List<MonitoringComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Monitoring_Complaince", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MonitoringComplaince DTO = new MonitoringComplaince();

                            DTO.Division = reader["Division"].ToString();
                            DTO.District = reader["District"].ToString();
                            DTO.Tehsil = reader["Tehsil"].ToString();
                            DTO.UC = reader["UC"].ToString();
                            DTO.FirstName = reader["FirstName"].ToString();
                            DTO.LastName = reader["LastName"].ToString();
                            DTO.Designation = reader["Designation"].ToString();
                            DTO.PhoneNumber = reader["PhoneNumber"].ToString();
                            DTO.UserLevel = (reader["UserLVL"].ToString());
                            DTO.DayofWork = (reader["DayofWork"].ToString());
                            DTO.FixedSite = Convert.ToInt32(reader["FixedSiteForm"]);
                            DTO.SupervisorMonitoring = Convert.ToInt32(reader["SupervisorMonitoringForm"]);
                            DTO.TeamMonitoring = Convert.ToInt32(reader["TeamMonitoringForm"]);
                            DTO.TransitTeamMonitoring = Convert.ToInt32(reader["SIATransitSiteMonitoringChecklist"]);
                            DTO.HouseHoldCluster = Convert.ToInt32(reader["HouseHoldForm"]);
                            DTO.CatchupCluster = Convert.ToInt32(reader["CatchUpCluster"]);


                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region RegistrationComplainceUserWise

        public List<RegistrationComplaince> RegistrationComplainceUserWise(ReportFilterDTO reportFilterDTO)
        {
            try
            {
                List<RegistrationComplaince> Counts = new List<RegistrationComplaince>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_Registration_Complaince", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.OffSet != null)
                        {
                            cmd.Parameters.AddWithValue("@OffSet", reportFilterDTO.OffSet);

                        }
                        if (reportFilterDTO.RowLimit != null)
                        {
                            cmd.Parameters.AddWithValue("@RowLimit", reportFilterDTO.RowLimit);

                        }

                        if (reportFilterDTO.Code == "1" || reportFilterDTO.Code==null )
                        {
                            cmd.Parameters.AddWithValue("@Code", null);
                            reportFilterDTO.Code = null;

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }

                        if (reportFilterDTO.UCCode == "1" || reportFilterDTO.UCCode == null)
                        {
                            cmd.Parameters.AddWithValue("@UCCode", null);
                            reportFilterDTO.UCCode = null;

                        }

                        if (reportFilterDTO.UCCode != null)
                        {
                            cmd.Parameters.AddWithValue("@UCCode", reportFilterDTO.UCCode);

                        }

                        if (reportFilterDTO.DivisionCode == "1" || reportFilterDTO.DivisionCode == null)
                        {
                            cmd.Parameters.AddWithValue("@DivisionCode", null);
                            reportFilterDTO.DivisionCode = null;

                        }

                        if (reportFilterDTO.DivisionCode != null)
                        {
                            cmd.Parameters.AddWithValue("@DivisionCode", reportFilterDTO.DivisionCode);

                        }

                        if (reportFilterDTO.DistrictCode == "1" || reportFilterDTO.DistrictCode == null)
                        {
                            cmd.Parameters.AddWithValue("@DistrictCode", null);
                            reportFilterDTO.DistrictCode = null;

                        }

                        if (reportFilterDTO.DistrictCode != null)
                        {
                            cmd.Parameters.AddWithValue("@DistrictCode", reportFilterDTO.DistrictCode);

                        }

                        if (reportFilterDTO.TehsilCode == "1" || reportFilterDTO.TehsilCode == null)
                        {
                            cmd.Parameters.AddWithValue("@TehsilCode", null);
                            reportFilterDTO.TehsilCode = null;

                        }

                        if (reportFilterDTO.TehsilCode != null)
                        {
                            cmd.Parameters.AddWithValue("@TehsilCode", reportFilterDTO.TehsilCode);

                        }

                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }

                        if (reportFilterDTO.Designation != null)
                        {
                            cmd.Parameters.AddWithValue("@Designation", reportFilterDTO.Designation);

                        }

                        if (reportFilterDTO.Organization != null)
                        {
                            cmd.Parameters.AddWithValue("@Organization", reportFilterDTO.Organization);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RegistrationComplaince DTO = new RegistrationComplaince();

                            if(reportFilterDTO.FilterLvl=="Province")
                            {
                                DTO.Province = reader["Province"].ToString();
                                DTO.TotalRegistered = Convert.ToInt32(reader["Total Registered"]);
                            }
                            if (reportFilterDTO.FilterLvl == "Division")
                            {
                                DTO.Province = reader["Province"].ToString();
                                DTO.Division = reader["Division"].ToString();
                                DTO.TotalRegistered = Convert.ToInt32(reader["Total Registered"]);
                            }
                            if (reportFilterDTO.FilterLvl == "District")
                            {
                                DTO.Province = reader["Province"].ToString();
                                DTO.Division = reader["Division"].ToString();
                                DTO.District = reader["District"].ToString();
                                DTO.TotalRegistered = Convert.ToInt32(reader["Total Registered"]);
                            }
                            if (reportFilterDTO.FilterLvl == "Tehsil")
                            {
                                DTO.Province = reader["Province"].ToString();
                                DTO.Division = reader["Division"].ToString();
                                DTO.District = reader["District"].ToString();
                                DTO.Tehsil = reader["Tehsil"].ToString();
                                DTO.TotalRegistered = Convert.ToInt32(reader["Total Registered"]);
                            }
                            if (reportFilterDTO.FilterLvl == "UC")
                            {
                                DTO.Province = reader["Province"].ToString();
                                DTO.Division = reader["Division"].ToString();
                                DTO.District = reader["District"].ToString();
                                DTO.Tehsil = reader["Tehsil"].ToString();
                                DTO.UC = reader["UC"].ToString();
                                DTO.Total = Convert.ToInt32(reader["Total"]);
                                DTO.TotalRegistered = Convert.ToInt32(reader["Total Registered"]);
                            } 
                            DTO.Designation = reader["Designation"].ToString();
                            DTO.UserLevel = (reader["UserLVL"].ToString());
                            Counts.Add(DTO);
                        }
                        conn.Close();
                    }
                    return Counts;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Polygon

        public List<HouseHoldClusterMarkers> HouseHoldClusterMarkers(FilterDTO reportFilterDTO)
        {
            try
            {
                List<HouseHoldClusterMarkers> Counts = new List<HouseHoldClusterMarkers>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClustermarkers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }


                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }


                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }


                        if (reportFilterDTO.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", reportFilterDTO.PopulationType);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterMarkers DTO = new HouseHoldClusterMarkers();

                            DTO.Division = reader["Division"].ToString();
                            DTO.District = reader["District"].ToString();
                            DTO.Tehsil = reader["Tehsil"].ToString();
                            DTO.UC = reader["UC"].ToString();
                            DTO.Lat = reader["Lat"].ToString();
                            DTO.Long = reader["Long"].ToString();
                            Counts.Add(DTO);

                         
                        }
                        conn.Close();
                        return Counts;

                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public List<HouseHoldClusterMissedarea> HouseholdClusterMissedArea(FilterDTO reportFilterDTO)
        {
            try
            {
                List<HouseHoldClusterMissedarea> Counts = new List<HouseHoldClusterMissedarea>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClustermissedareas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }


                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }


                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }


                        if (reportFilterDTO.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", reportFilterDTO.PopulationType);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterMissedarea DTO = new HouseHoldClusterMissedarea();

                            if(reader["Status"].ToString() == "Missed Area" /*|| reader["Status"].ToString() == "Not Missed Area"*/)
                            {
                                DTO.Lat = Convert.ToDecimal(reader["Lat"]);
                                DTO.Long = Convert.ToDecimal(reader["Long"]);

                                Counts.Add(DTO);
                            }
    
                        }
                        conn.Close();
                        return Counts;
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public List<HouseHoldClusterPoorlyCoveredArea> HouseholdClusterPoorlyCoveredArea(FilterDTO reportFilterDTO)
        {
            try
            {
                List<HouseHoldClusterPoorlyCoveredArea> Counts = new List<HouseHoldClusterPoorlyCoveredArea>();

                using (var db = new PMSDbContext())
                {
                    SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                    using (SqlCommand cmd = new SqlCommand("SP_HouseHoldClusterpoorlycoveredarea", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (reportFilterDTO.FilterLvl != null)
                        {
                            cmd.Parameters.AddWithValue("@FilterLvl", reportFilterDTO.FilterLvl);

                        }

                        if (reportFilterDTO.Code != null)
                        {
                            cmd.Parameters.AddWithValue("@Code", reportFilterDTO.Code);

                        }


                        if (reportFilterDTO.campaignId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CampaignId", reportFilterDTO.campaignId);

                        }


                        if (reportFilterDTO.day != null)
                        {
                            cmd.Parameters.AddWithValue("@Day", reportFilterDTO.day);

                        }


                        if (reportFilterDTO.PopulationType != null)
                        {
                            cmd.Parameters.AddWithValue("@PopulationType", reportFilterDTO.PopulationType);

                        }

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HouseHoldClusterPoorlyCoveredArea DTO = new HouseHoldClusterPoorlyCoveredArea();

                            if (reader["Status"].ToString() == "poorly Covered area")
                            {
                                DTO.Lat = Convert.ToDecimal(reader["Lat"]);
                                DTO.Long = Convert.ToDecimal(reader["Long"]);

                                Counts.Add(DTO);
                            }

                        }
                        conn.Close();
                        return Counts;
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region GetExcelReport
        public byte[] GetExcelReport(ReportFilterDTO reportFilterDTO )
        {
            try
            {
                //paginatedFilterDTO.FromDate = (paginatedFilterDTO.FromDate == null ? DateTime.Now.Date : (DateTime)paginatedFilterDTO.FromDate);
                //paginatedFilterDTO.ToDate = (paginatedFilterDTO.ToDate == null ? DateTime.Now.Date : ((DateTime)paginatedFilterDTO.ToDate).AddHours(23).AddMinutes(59).AddSeconds(59));

                using var _db = new PMSDbContext();

                DataTable dataTable = new DataTable("Report");

                if (reportFilterDTO.FormId==1)
                {
                    var adminResult = FixedSiteReports(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }


                if (reportFilterDTO.FormId == 2)
                {
                    var adminResult = SupervisormonitoringReports(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }


                if (reportFilterDTO.FormId == 3)
                {
                    var adminResult = TeammonitoringReports(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }



                if (reportFilterDTO.FormId == 4)
                {
                    var adminResult = TransitTeammonitoringReports(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }


                if (reportFilterDTO.FormId == 5 && reportFilterDTO.UserType=="Technical")
                {
                    var adminResult = HouseHoldClusterReport(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }

                if (reportFilterDTO.FormId == 5 && reportFilterDTO.UserType == "Administrative")
                {
                    var adminResult = AdministrativeHouseHoldClusterReport(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }

                if (reportFilterDTO.FormId == 0 && reportFilterDTO.ReportType==2)
                {
                    var adminResult = MonitoringComplaince(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }
                if (reportFilterDTO.FormId == 0 && reportFilterDTO.ReportType ==1)
                {
                    var adminResult = RegistrationComplainceUserWise(reportFilterDTO);

                    dataTable.Columns.AddRange(adminResult.FirstOrDefault().GetType().GetProperties().Select(a => new DataColumn(a.Name)).ToArray());

                    adminResult.ForEach(ar =>
                    {
                        DataRow dataRow = dataTable.NewRow();

                        ar.GetType().GetProperties().ToList().ForEach(a =>
                        {
                            dataRow[a.Name] = a.GetValue(ar);
                        });

                        dataTable.Rows.Add(dataRow);
                    });
                }

                using XLWorkbook xlWorkbook = new XLWorkbook();

                xlWorkbook.Worksheets.Add(dataTable);

                using MemoryStream memoryStream = new MemoryStream();

                xlWorkbook.SaveAs(memoryStream);

                return memoryStream.ToArray();

                return null;

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw ex;
            }
        }

        #endregion

        #endregion

        #region HelperMethods
        private string AssignColorIcon(string FormType)
        {
            string icon = "";
            switch (FormType)
            {
                case "Functional":
                    icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
                    break;
                case "NonFunctional":
                    icon = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
                    break;
            }
            return icon;

        }

        private string GetStoreProcedureName(string ReasonformissedType)
        {
            string storeProcedurename = "";
            switch (ReasonformissedType)
            {
                case "1":

                    storeProcedurename = "SP_HouseHoldClusterTeamdidnotvisitthehousemarkers";
                    break;
                case "2":
                    storeProcedurename = "SP_HouseHoldClusterTVBMCmarkers";
                    break;
                case "3":
                    storeProcedurename = "SP_HouseHoldClusterNAmarkers";
                    break;
                case "4":
                    storeProcedurename = "SP_HouseHoldClusterRefusalsmarkers";
                    break;
                case "5":
                    storeProcedurename = "SP_HouseHoldClusterOthermarkers";
                    break;
            }
            return storeProcedurename;

        }

        
        #endregion

    }
}

