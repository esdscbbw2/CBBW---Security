using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.RBACUsers;

namespace CBBW.BOL.CTV
{
    public class UserInfo
    {
        public int CentreCode { get; set; }
        public string CentreName { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public bool IsOffline { get; set; }
        public string IPAddress { get; set; }
        public string ComputerName { get; set; }
        public List<UserModule> Modules { get; set; }
        public UserInfo()
        {

        }
        public UserInfo(bool defaultuser)
        {
            CentreCode = 13;
            CentreName = "NIZAMABAD";
            EmployeeNumber = 2002563;
            EmployeeName = "P PRAVEEN KUMAR";
            UserName = "praveen";
        }
    }
}
