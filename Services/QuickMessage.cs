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
    public class QuickMessage
    {
        private string MSISDN;
        private string PASSWORD;
        private string abc;

        public QuickMessage(string msisdn, string password)
        {
            MSISDN = msisdn;
            this.PASSWORD = password;
        }
        public LogsSMSSession getSessionId(string userId)
        {
            try
            {
                using (var db = new PMSDbContext())
                {

                    //}
                    string url = "https://telenorcsms.com.pk:27677/corporate_sms2/api/auth.jsp?msisdn=" + MSISDN + "&password=" + PASSWORD;
                    string sessionId = SendRequest(url);
                    LogsSMSSession sMS_Session = new LogsSMSSession();
                    sMS_Session.SessionId = sessionId;
                    sMS_Session.RequestDateTime = DateTime.UtcNow.AddHours(5);
                    sMS_Session.LastActivityTime = sMS_Session.RequestDateTime;
                    sMS_Session.RequestedBy = userId;
                    //db.SMS_Session.Add(sMS_Session);
                    //db.SaveChanges();
                    return sMS_Session;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public LogsSM sendQuickMessage(LogsSMSSession session, SMS sms)
        {

            try
            {
                using (var db = new PMSDbContext())
                {
                    string url = "https://telenorcsms.com.pk:27677/corporate_sms2/api/sendsms.jsp?session_id=" + session.SessionId + "&text=" + sms.Message + "&to=" + sms.MobileNumber;
                    if (sms.Mask != null)
                    {
                        url = url += "&mask=" + sms.Mask;
                    }
                    LogsSM sMS_Log = new LogsSM();
                    sMS_Log.SMS_Session_Id = session.Id;
                    sMS_Log.Message = sms.Message;
                    sMS_Log.Number = sms.MobileNumber;
                    sMS_Log.Mask = sms.Mask;
                    sMS_Log.CreatedBy = sms.UserId;
                    sMS_Log.CreateDate = DateTime.UtcNow.AddHours(5);
                    sMS_Log.FKId = sms.FKId;
                    sMS_Log.Remarks = sms.Remarks;
                    sMS_Log.MessageId = SendRequest(url);
                    db.LogsSM.Add(sMS_Log);
                    db.SaveChanges();
                    return sMS_Log;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public string ping(string sessionId)
        {
            string url =
           "https://telenorcsms.com.pk:27677/corporate_sms2/api/ping.jsp?session_id=" + sessionId;
            return SendRequest(url);
        }
        public string status(string messageId, string sessionId)
        {
            string url =
           "https://telenorcsms.com.pk:27677/corporate_sms2/api/ping.jsp?session_id=" + sessionId;
            return SendRequest(url);
        }
        public string SendRequest(string url)
        {
            string response = null;
            try
            {
                var client = new WebClient();
                response = client.DownloadString(url);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response);
                XmlNodeList responseType = xmldoc.GetElementsByTagName("response");
                XmlNodeList data = xmldoc.GetElementsByTagName("data");
                if (responseType.Equals("Error"))
                {
                    return null;
                }
                response = data[0].InnerText;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
    
}

