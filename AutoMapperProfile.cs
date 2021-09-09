using AutoMapper;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem
{
    public class AutoMapperProfile
    {
        public class UserProfile : Profile
        {
            public UserProfile()
            {

                #region UserListMapping

                CreateMap<V_Userlist, UserDto>();


                CreateMap<V_Userlist, MobRegistrationDTO>();

                CreateMap<Assignuserdistrict, UserLocations>();



                #endregion

                #region FormsIndicatorSettings
                CreateMap<FormsIndicatorSettingsDTO, FormsIndicator>();
                CreateMap<FormsIndicator, FormsIndicatorSettingsDTO>();


                #endregion


                #region FormIndicator

                CreateMap<FormsIndicatorDTO, FormsIndicatorMaster>();
                CreateMap<FormsIndicatorMaster, FormsIndicatorDTO>();
                CreateMap<FormsIndicatorMaster, GetFormIndicatorDTO>();

                #endregion

                #region OptionsIndicator

                CreateMap<FormsIndicatorSettingsDTO, IndicatorOptions>();
                CreateMap<FormsIndicatorMaster, FormsIndicatorDTO>();

                #endregion

                #region MapOptionListonGetOptionListDTO

                CreateMap<OptionList, GetOptionListDTO>();
               


                #endregion

                #region Map

                CreateMap<FormsIndicatorMaster, MapDTO>();

                #endregion

                #region Event

                CreateMap<EventDTO, PMSEvents>();
                CreateMap<PMSEvents, EventDTO>();

                #endregion

            }
        }
    }
}
