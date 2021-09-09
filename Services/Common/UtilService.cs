//using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Data.Database.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.Common
{
    public class UtilService
    {
        public static Response<T> GetResponse<T>(T data, string messages = null) where T : class
        {
            return new Response<T>() { IsException = false, Messages = messages ?? string.Empty, Data = data };
        }

        public static Response<T> GetExResponse<T>(Exception ex) where T : class
        {
            return new Response<T>() { IsException = true, Messages = GetExMessage(ex), Data = null };
        }

        public static Response<T> GetExResponse<T>(string message) where T : class
        {
            return new Response<T>() { IsException = true, Messages = message, Data = null };
        }

        public static DateTime GetPkCurrentDateTime()
        {
            return DateTime.UtcNow.AddHours(5);
        }

        public static int ToInt32(string value)
        {
            if (value == null)
                return 0;
            return int.Parse(value, CultureInfo.CurrentCulture);
        }



        public static Guid GenereateGuid()
        {
            try
            {
                Guid guid = System.Guid.NewGuid();
                return guid;
            }
            catch
            {
                throw;
            }
        }

        public static PMSDbContext Instance()
        {
            try
            {
                return new PMSDbContext();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        //public static string GetFirstCharacterOfEveryword(string str)
        //{
        //    string[] strSplit = str.Split();
        //    foreach (string res in strSplit)
        //    {

        //    }
        //    return sb.ToString();
        //}
        //public static string GenerateBarcodeString(string message)
        //{
        //    return ConvertImageToBase64(GenerateBarcode(message));
        //}

        //public static string GenerateQRCodeString(string message)
        //{
        //    return ConvertImageToBase64(GenerateQrCode(message));
        //}


        //public static Image GenerateBarcode(string message)
        //{
        //    Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
        //    return barcode.Draw(message, 50);
        //}
        //public static Image GenerateQrCode(string message)
        //{
        //    //BarcodeSettings settings = new BarcodeSettings();
        //    //settings.Type = BarCodeType.QRCode;
        //    //settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;
        //    //settings.Data = message;
        //    //settings.Data2D = message;
        //    //settings.X = 1.0f;
        //    //settings.QRCodeECL = QRCodeECL.H;

        //    //BarCodeGenerator generator = new BarCodeGenerator(settings);
        //    //return generator.GenerateImage();
        //    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
        //    return qrcode.Draw(message, 50);
        //    //return BarcodeWriter.CreateBarcode(message, BarcodeWriterEncoding.QRCode).ToBitmap();
        //}

        //public static string ConvertImageToBase64(Image img)
        //{
        //    try
        //    {
        //        using (img)
        //        {
        //            using (MemoryStream m = new MemoryStream())
        //            {
        //                img.Save(m, ImageFormat.Bmp);
        //                byte[] imageBytes = m.ToArray();

        //                // Convert byte[] to Base64 String
        //                string base64String = Convert.ToBase64String(imageBytes);
        //                return base64String;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        public static string GetExMessage(Exception message)
        {
            var msg = string.Empty;
            if (message == null) return msg;
            msg += message.Message;
            if (message.InnerException != null)
            {
                msg += "\r\nInnerException: " + GetExMessage(message.InnerException);
            }
            return msg;
        }


        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #region GetCurrentCampaignId
        public static int GetCurrentCampaignId()
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
    }
}
