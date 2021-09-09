using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Helpers;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services
{
    public class MapService
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion
        
        #region Constructors
        public MapService(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region GetFormsIndicatorsList
        public IList<MapDTO> GetFormsIndicatorsList(string Code,int Dayofwork)
        {
            try
            {
                List<MapDTO> mapDTOList = new List<MapDTO>();

                using var db = new PMSDbContext();


                SqlConnection conn = (SqlConnection)db.Database.GetDbConnection();

                using (SqlCommand cmd = new SqlCommand("SP_Mapmarker", conn))
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
                        MapDTO mapDTO = new MapDTO();

                        mapDTO.Id = Convert.ToInt32(reader["Id"]);
                        mapDTO.IndicatorName= reader["IndicatorName"].ToString();
                        mapDTO.DivisionName = reader["DivisionName"].ToString();
                        mapDTO.DistrictName = reader["DistrictName"].ToString();
                        mapDTO.TehsilName = reader["TehsilName"].ToString();
                        mapDTO.UCNumber = reader["UCNumber"].ToString();
                        mapDTO.Dayofwork = reader["Dayofwork"].ToString();
                        mapDTO.Lat= reader["Lat"].ToString() ;
                        mapDTO.Long= reader["Long"].ToString() ;
                        mapDTO.Icon = AssignColorIcon(reader["IndicatorName"].ToString());

                        mapDTOList.Add(mapDTO);
                    }
                    conn.Close();
                }

               //(IList<MapDTO>, long, long) pageData = CommonMethods.GetPagedData(mapDTOList.AsQueryable(), paginatedFilterDTO.Size, paginatedFilterDTO.PageNumber);

                return mapDTOList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DashboardStat

        public DashboardStat GetDashboardStat(string user)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    DateTime now = DateTime.Now;

                    var dateAndTime = DateTime.Now.Date;

                    var date = dateAndTime;

                    var dashboarstat = db.FormsIndicatorMaster.Where(x => x.CreatedBy == user).ToList();


                    var TodayCount = dashboarstat.Where(x => x.CreatedDate?.Date == date).ToList();

                    DashboardStat dashboardStat = new DashboardStat();

                    dashboardStat.TotalCount = dashboarstat.Count();
                    dashboardStat.TodayCount = TodayCount.Count();

                    return dashboardStat;


                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        #region HelperMethods
        private string AssignColorIcon(string FormType)
        {
            string icon = "";
            switch (FormType)
            {
                case "Team did not visit the house":
                    icon = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
                    break;
                case "Team visited but missed vaccinating the child":
                    icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
                    break;  
            }
            return icon;

        }
        #endregion
    }
}
