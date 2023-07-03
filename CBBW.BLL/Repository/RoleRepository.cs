using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Role;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class RoleRepository: IRoleRepository
    {
        RoleEntities _RoleEntities;
        MasterEntities _MasterEntities;
        string NotePattern = "200001-RID-" + DateTime.Today.ToString("yyyyMMdd") + "-";
        UserRepository _user;
        UserInfo user;
        public RoleRepository()
        {
            _RoleEntities = new RoleEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }

        public IEnumerable<CustomComboOptions> GetModuleList(int ID, ref string pMsg)
        {
            return _RoleEntities.GetModuleList(ID, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> GetSubModuleList(int ModuleId, ref string pMsg)
        {
            return _RoleEntities.GetSubModuleList(ModuleId, ref pMsg);
        }

        public IEnumerable<CustomComboOptions> GetNavigationList(int SubModuleId, ref string pMsg)
        {
            return _RoleEntities.GetNavigationList(SubModuleId, ref pMsg);
        }

        public RBACRoles getNewNoteNumber(ref string pMsg)
        {
            RBACRoles obj = new RBACRoles();
            obj.RoleId = _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterC = user.CentreCode;
            return obj;
        }

        public TaskControl GetTaskDetails(int NavigationId, ref string pMsg)
        {
            return _RoleEntities.GetTaskDetails(NavigationId, ref pMsg);
        }

        public bool SetRoleModule(string RoleId, string RoleName, int NavigationId, int status, int UserId, List<Actions> dtldata, ref string pMsg)
        {
            return _RoleEntities.SetRoleModule(RoleId, RoleName, NavigationId, status, UserId, dtldata, ref pMsg);
        }

        public List<RoleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _RoleEntities.GetIndexListPage(DisplayLength,DisplayStart,SortColumn, SortDirection,SearchText,CenterCode,status, ref  pMsg);
        }

        public Header GetRoleDetails(string RoleId, ref string pMsg)
        {
            Header obj = new Header();
            obj.rolelist = _RoleEntities.GetRoleDetails(RoleId, ref pMsg);
            if (obj.rolelist != null)
            {
                foreach (var item in obj.rolelist.Select(x => new { x.RoleId, x.RoleName }).Distinct().ToList())
                {
                    obj.RoleId = item.RoleId;
                    obj.RoleName = item.RoleName;
                }
            }
            return obj;
        }

        public IEnumerable<CustomComboOptions> GetTaskList(int Id, ref string pMsg)
        {
            return _RoleEntities.GetTaskList(Id, ref pMsg);
        }

        public IEnumerable<CustomComboOptions> GetActionList(int Id, ref string pMsg)
        {
            return _RoleEntities.GetActionList(Id, ref pMsg);
        }

        public Header GetRoleDetailsForView(string RoleId, ref string pMsg)
        {
            Header obj = new Header();
            obj.rolelist = _RoleEntities.GetRoleDetailsForView(RoleId, ref pMsg);
            if (obj.rolelist != null)
            {
                foreach (var item in obj.rolelist.Select(x => new { x.RoleId, x.RoleName }).Distinct().ToList())
                {
                    obj.RoleId = item.RoleId;
                    obj.RoleName = item.RoleName;
                }
            }
            return obj;
        }

        public bool SetDeleteRoleModule(string RoleId, int NavigationId, int TaskId, ref string pMsg)
        {
            return _RoleEntities.SetDeleteRoleModule(RoleId, NavigationId, TaskId, ref pMsg);
        }

        public bool SetRBACMVC(List<PageInformation> pagesinfo,ref string pMsg)
        {
            return _RoleEntities.SetRBACMVC(pagesinfo,ref pMsg);
        }
    }
}
