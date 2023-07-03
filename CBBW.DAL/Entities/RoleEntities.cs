using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.BIL;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Role;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public class RoleEntities
    {

        DataTable dt = null;
        DataSet ds = null;
        RoleDataSync _datasync;
        RoleDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public RoleEntities()
        {
            _datasync = new RoleDataSync();
            _DBMapper = new RoleDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public IEnumerable<CustomComboOptions> GetModuleList(int ID, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetModuleList(ID, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptionsWithoutID(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> GetSubModuleList(int ModuleId, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetSubModuleList(ModuleId, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptionsWithoutID(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> GetNavigationList(int SubModuleId, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetNavigationList(SubModuleId, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptionsWithoutID(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> GetTaskList(int Id, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetTaskList(Id, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptionsWithoutID(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> GetActionList(int Id, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetActionList(Id, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptionsWithoutID(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public TaskControl GetTaskDetails(int NavigationId, ref string pMsg)
        {
            TaskControl result = new TaskControl();
            try
            {
                ds= _datasync.GetTaskDetails(NavigationId, ref pMsg);
                if (ds != null)
                {
                    List<TaskNames> TSK = new List<TaskNames>();
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TSK.Add(_DBMapper.Map_TaskName(dt.Rows[i]));
                        }
                    }
                    result.taskname = TSK;
                    List<Actions> DWT = new List<Actions>();
                    dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DWT.Add(_DBMapper.Map_Actions(dt.Rows[i]));
                        }
                    }
                    result.actions = DWT;
                }
            }
            catch { }
            return result;
        }
        public bool SetRoleModule(string RoleId, string RoleName, int NavigationId, int status, int UserId, List<Actions> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetRoleModule( RoleId,  RoleName,  NavigationId,  status,  UserId, dtldata, ref  pMsg), ref pMsg, ref result);
            return result;
        }
        public List<RoleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<RoleList> result = new List<RoleList>();
            try
            {
                dt = _datasync.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_IndexList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<RBACRoles> GetRoleDetails( string  RoleId, ref string pMsg)
        {
            List<RBACRoles> result = new List<RBACRoles>();
            try
            {
                dt = _datasync.GetRoleDetails(RoleId, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_Roles(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<RBACRoles> GetRoleDetailsForView(string RoleId, ref string pMsg)
        {
            List<RBACRoles> result = new List<RBACRoles>();
            try
            {
                dt = _datasync.GetRoleDetailsForView(RoleId, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_Roles(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetDeleteRoleModule(string RoleId, int NavigationId, int TaskId, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetRoleTaskDelete(RoleId,NavigationId, TaskId, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetRBACMVC(List<PageInformation> pagesinfo, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetRBACMVC(pagesinfo,ref pMsg), ref pMsg, ref result);
            return result;
        }

    }
}
