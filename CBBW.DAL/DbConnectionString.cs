using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL
{
    public static class DbConnectionString
    {
        public static string DBConRead = ConfigurationManager.AppSettings["DBConRead"].ToString().ToUpper();
        public static string AppEnvironment = ConfigurationManager.AppSettings["AppEnvironment"].ToString().ToUpper();

        public static string GetDBConnectionString() 
        {
            if (DBConRead == "WEBCONFIG")
                return ConfigurationManager.ConnectionStrings["CBBWConnectionString"].ConnectionString;
            else
                return Get_Environment_ConnectionString();
        }
        private static string Get_Environment_ConnectionString() 
        {
            switch (AppEnvironment)
            {
                case "QA":
                    return "";
                case "UAT":
                    return "";
                case "PRODUCTION":
                    return "";
                default:
                    return "Data Source=CBBW_MIGRATION;Initial Catalog=CBBW;Persist Security Info=True;User ID=db_cbbw;Password=ESDS@4321#";
            }
            
        }
    }
}
