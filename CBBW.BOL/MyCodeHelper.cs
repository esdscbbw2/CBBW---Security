using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CBBW.BOL
{
    public static class MyCodeHelper
    {
        public static string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        //public static string BaseUrl = "https://localhost:44396/Security";
        public static int GetEmpNoFromString(string EmpNonName) 
        {
            int result = 0;
            try
            {
                if(EmpNonName!=null && EmpNonName.IndexOf("/")>=0)
                    result = int.Parse(EmpNonName.Split('/')[0]);
            }
            catch { }
            return result;
        }
        public static string GetNotePattern(string ModuleName) 
        {
            ModuleName = ModuleName.ToUpper();
            string result = "";
            switch (ModuleName)
            {
                case "CTV":
                    result= "200001-CTV-" + DateTime.Today.ToString("yyyyMMdd") + "-";
                    break;
                default:
                    break;
            }
            return result;
        }
        public static DateTime FirstDayOfTheFortNight() 
        {
            DateTime now = DateTime.Now;
            if (now.Day <= 15) 
            {
                return new DateTime(now.Year, now.Month, 1);
            } 
            else 
            {
                return new DateTime(now.Year, now.Month, 16);
            }
        }
        public static DateTime LastDayOfTheFortNight()
        {
            DateTime now = DateTime.Now;
            if (now.Day <= 15)
            {
                return new DateTime(now.Year, now.Month, 15);
            }
            else
            {
                return new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            }
        }
        public static DateTime ConvertStringToDate(string dateString) 
        {
            return DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
        }
        public static string GetIPAddress() 
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            return ipAddress;
        }
        public static string GetComputerName()
        {
            string ComName=Dns.GetHostName();
            //string ComName = HttpContext.Current.Request.UserHostName;
            return ComName;
        }
    }
}
