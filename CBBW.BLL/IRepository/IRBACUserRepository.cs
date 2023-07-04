using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;

namespace CBBW.BLL.IRepository
{
    public interface IRBACUserRepository
    {
        List<Employee> GetListOfActiveEmployees(ref string pMsg);
        bool ValidateUserName(string UserName, ref string pMsg);
        List<CustomComboOptions> GetCentreList(ref string pMsg);
        List<MyRole> GetListOfRoles(ref string pMsg);
        IEnumerable<CustomComboOptions> GetLocationTypes(ref string pMsg);
        bool SetUserData(UpdateUser data, ref string pMsg);
        List<UserList> GetUserList(int DisplayLength, int DisplayStart,
            int SortColumn, string SortDirection, string SearchText, ref string pMsg);
        


    }
}
