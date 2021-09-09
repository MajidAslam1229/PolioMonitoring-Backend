using AutoMapper;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models;
using PolioMonitoringSystem.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace PolioMonitoringSystem.Services
{
    public class QuickMessageService
    {


        #region Constructors
        public QuickMessageService()
        {
        }
        #endregion
        public LogsSM SendSMSTelenor(SMS sms)
        {
            string userName = "923464950571";
            string password = "Hisdu%40012_p%26shd-Hisdu%2Fp%26shd";

            QuickMessage obj = new QuickMessage(userName, password);
            LogsSMSSession session = obj.getSessionId(sms.UserId);
            if (session != null)
            {
                sms.Mask = sms.Mask == null ? "PSHD" : sms.Mask;
                LogsSM message = obj.sendQuickMessage(session, sms);
                return message;
            }
            return null;
        }


    }
}

