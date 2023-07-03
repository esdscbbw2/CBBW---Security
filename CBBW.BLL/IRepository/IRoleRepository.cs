using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Role;
using CBBW.BOL.CustomModels;
using CBBW.BOL;

namespace CBBW.BLL.IRepository
{
    public interface IRoleRepository
    {
        RBACRoles getNewNoteNumber(ref string pMsg);
        IEnumerable<CustomComboOptions> GetModuleList(int ID, ref string pMsg);
        IEnumerable<CustomComboOptions> GetSubModuleList(int ModuleId, ref string pMsg);
        IEnumerable<CustomComboOptions> GetNavigationList(int SubModuleId, ref string pMsg);
        IEnumerable<CustomComboOptions> GetTaskList(int Id, ref string pMsg);
        IEnumerable<CustomComboOptions> GetActionList(int Id, ref string pMsg);
        TaskControl GetTaskDetails(int NavigationId, ref string pMsg);
         bool SetRoleModule(string RoleId, string RoleName, int NavigationId, int status, int UserId, List<Actions> dtldata, ref string pMsg);
        List<RoleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        Header GetRoleDetails(string RoleId, ref string pMsg);
        Header GetRoleDetailsForView(string RoleId, ref string pMsg);

        bool SetDeleteRoleModule(string RoleId, int NavigationId, int TaskId, ref string pMsg);
        bool SetRBACMVC(List<PageInformation> pagesinfo,ref string pMsg);

    }
}
