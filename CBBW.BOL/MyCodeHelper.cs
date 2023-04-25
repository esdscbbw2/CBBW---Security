using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
