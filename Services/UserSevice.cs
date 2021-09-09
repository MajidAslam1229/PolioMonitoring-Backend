using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services
{
    public class UserSevice
    {

        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public UserSevice(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

    }
}
