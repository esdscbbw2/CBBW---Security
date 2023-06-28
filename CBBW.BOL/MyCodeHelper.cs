using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web.Mvc;

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
        public static string GetCommaSeparatedString(List<int> ObjectList)
        {
            string Result = string.Empty;
            foreach (var item in ObjectList)
            {
                Result = Result + item.ToString() + ",";
            }
            Result = Result.Substring(0, Result.Length - 1);
            return Result;
        }
        public static bool WriteErrorLog(string ErrorPath,Exception ex)
        {
            //string url = HttpContext.Current.Request.Path;
            //url = url + HttpContext.Current.Request.QueryString.ToString();
            bool result = false;
            string virtualPath = "~/Logs/ERRLOG_"+DateTime.Today.ToString("yyyyMMdd")+".txt";
            var physicalPath = HttpContext.Current.Server.MapPath(virtualPath);
            using (StreamWriter writer = new StreamWriter(physicalPath,true))
            {
                writer.WriteLine(" ");
                writer.WriteLine("Called From: " + HttpContext.Current.Request.Path);
                writer.WriteLine("Time: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                writer.WriteLine("..................................................................");
                writer.WriteLine("Method: "+ ErrorPath);
                writer.WriteLine("Line No: " + GetErrorLine(ex));
                writer.WriteLine("Error Message: "+ ex.Message);
                //writer.WriteLine("Stack Trace: " + ex.StackTrace);
                writer.WriteLine("------------------------------------------------------------------");
            }
            return result;
        }
        static int GetErrorLine(Exception ex)
        {
            StackTrace stackTrace = new StackTrace(ex, true);
            StackFrame frame = stackTrace.GetFrame(0);
            int errorLine = frame.GetFileLineNumber();
            if (errorLine == 0) 
            {
                int l = ex.StackTrace.Length;
                int x = ex.StackTrace.IndexOf(":line");
                x = x > 0 ? x + 5 : 0;
                if (l>0 && x > 0) 
                {
                    string s = ex.StackTrace.Substring(x, l-x);
                    return GetFirstNumber(s);
                }                
            }
            return errorLine;
        }
        public static int GetFirstNumber(string input)
        {
            Match match = Regex.Match(input, @"\d+");
            if (match.Success)
            {
                if (int.TryParse(match.Value, out int number))
                {
                    return number;
                }
            }
            return -1;
        }
        public static MethodInformation GetMethodInfo()
        {
            StackTrace stackTrace = new StackTrace();
            // Get the calling method (skip 1 frame to exclude the GetCallingMethodInfo itself)
            StackFrame frame = stackTrace.GetFrame(1);
            MethodBase method = frame.GetMethod();
            Type declaringType = method.DeclaringType;
            string methodName = method.Name;
            ParameterInfo[] parameters = method.GetParameters();
            string allparams = " ";
            for (int i = 0; i < parameters.Length; i++)
            {
                allparams = allparams + parameters[i].ParameterType+" " + parameters[i].Name + ",";
            }
            allparams = "(" + allparams.Substring(0, allparams.Length-1) + ")";
            string methodPath = $"{declaringType.FullName}.{methodName}";
            MethodInformation methodInformation = new MethodInformation
            {
                MethodPath = methodPath,
                MethodParams = allparams,
                MethodName = methodName,
                MethodSignature= methodPath+ allparams
            };
            return methodInformation;
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
