using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.RBACUsers;

namespace CBBW.BLL.IRepository
{
    public interface IUserRepository
    {
        bool LogIn(string UserName, ref string pMsg);
        UserInfo getLoggedInUser();
        void LogOut();

        void RecordCallBack(string url);
        string GetCallBackUrl();
        void ClearCallBackRecording();
        List<UserMenu> GetUserMenu(int EmployeeNumber, int CentreCode, ref string pMsg);

    }
}
