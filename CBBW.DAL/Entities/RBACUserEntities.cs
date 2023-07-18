using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class RBACUserEntities
    {
        DataTable dt;
        DataSet ds;
        RBACUserDBMapper _RBACUserDBMapper;
        RBACUserDataSync _RBACUserDataSync;
        DBResponseMapper _DBResponseMapper;
        public RBACUserEntities()
        {
            _RBACUserDBMapper = new RBACUserDBMapper();
            _RBACUserDataSync = new RBACUserDataSync();
            _DBResponseMapper = new DBResponseMapper();
        }
        public List<Employee> GetListOfActiveEmployees(ref string pMsg)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                dt = _RBACUserDataSync.GetListOfActiveEmployees(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_RBACUserDBMapper.Map_Employee(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; 
            }
            return result;
        }
        public bool ValidateUserName(string UserName, ref string pMsg) 
        {
            return _RBACUserDataSync.ValidateUserName(UserName,ref pMsg);
        }
        public List<CustomComboOptions> GetCentreList(ref string pMsg) 
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _RBACUserDataSync.GetCentreList(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptions(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) 
            { 
                pMsg = ex.Message;
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public List<MyRole> GetListOfRoles(ref string pMsg)
        {
            List<MyRole> result = new List<MyRole>();
            try
            {
                dt = _RBACUserDataSync.GetListOfRoles(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_RBACUserDBMapper.Map_MyRole(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) 
            {
                pMsg = ex.Message;
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public bool SetUserData(UpdateUser data, ref string pMsg, bool IsEdit = false) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_RBACUserDataSync.SetUserData(data, ref pMsg,IsEdit), ref pMsg, ref result);
            return result;
        }
        public List<UserList> GetUserList(int DisplayLength, int DisplayStart,
            int SortColumn, string SortDirection, string SearchText, ref string pMsg)
        {
            List<UserList> result = new List<UserList>();
            try
            {
                dt = _RBACUserDataSync.GetUserList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_RBACUserDBMapper.Map_UserList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
            }
            return result;
        }
        public List<ViewUserData> GetUserRoles(int EmployeeNumber, ref string pMsg) 
        {
            List<ViewUserData> result = new List<ViewUserData>();
            try
            {
                dt = _RBACUserDataSync.GetUserRoles(EmployeeNumber,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_RBACUserDBMapper.Map_ViewUserData(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public bool DeleteUserRole(int EmployeeNumber, string RoleIDs, ref string pMsg,ref int MStat)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_RBACUserDataSync.DeleteUserRole(EmployeeNumber, RoleIDs,ref pMsg),ref pMsg, ref result, ref MStat);
            return result;
        }
        public bool UpdatePassword(UpdatePassword data, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_RBACUserDataSync.UpdatePassword(data, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<UserMenu> GetUserMenu(int EmployeeNumber, int CentreCode,int ModuleID, ref string pMsg) 
        {
            List<UserMenu> result = new List<UserMenu>();
            List<UserMenuRaw> obj = new List<UserMenuRaw>();
            try
            {
                dt = _RBACUserDataSync.GetUserMenu(EmployeeNumber, CentreCode, ModuleID, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        obj.Add(_RBACUserDBMapper.Map_UserMenuRaw(dt.Rows[i]));
                    }
                }
                if(obj!=null && obj.Count > 0) 
                {
                    foreach(var item in obj.Select(o=>new {o.ModuleID,o.ModuleName }).Distinct().ToList()) 
                    {
                        UserMenu m1 = new UserMenu(){ModuleID=item.ModuleID,ModuleName=item.ModuleName };
                        List<UserSubModule> x1 =new List<UserSubModule>();
                        foreach (var smitem in obj.Where(o=>o.ModuleID==item.ModuleID)
                            .Select(o=>new { o.SubModuleID,o.SubModuleName })
                            .Distinct().ToList()) 
                        {
                            UserSubModule m2 = new UserSubModule() {SubModuleID= smitem.SubModuleID,SubModuleName= smitem.SubModuleName };
                            List<UserNavigation> x2 = new List<UserNavigation>();
                            foreach(var navitem in obj.Where(o =>o.ModuleID == item.ModuleID && o.SubModuleID == smitem.SubModuleID)
                                .Select(o=>new { o.NavigationID,o.NavigationName})
                                .Distinct().ToList()) 
                            {
                                UserNavigation m3 = new UserNavigation() {NavigationID=navitem.NavigationID,NavigationName=navitem.NavigationName };
                                List<UserTask> x3 = new List<UserTask>();
                                foreach (var taskitem in obj.Where(o => o.ModuleID == item.ModuleID && o.SubModuleID == smitem.SubModuleID && o.NavigationID==navitem.NavigationID)
                                    .Select(o=>new {o.TaskName }).Distinct().ToList())
                                {
                                    UserTask m4 = new UserTask() {TaskName=taskitem.TaskName};
                                    List<UserSubTask> x4 = new List<UserSubTask>();
                                    foreach(var subtaskitem in obj.Where(o => o.ModuleID == item.ModuleID && o.SubModuleID == smitem.SubModuleID && o.NavigationID == navitem.NavigationID && o.TaskName==taskitem.TaskName)
                                        .Select(o=>new {o.TaskID,o.URL,o.SubTaskName })
                                        .Distinct().ToList()) 
                                    {
                                        UserSubTask m5 = new UserSubTask() {TaskID=subtaskitem.TaskID,URL=MyCodeHelper.BaseUrl+"/"+subtaskitem.URL,SubTaskName=subtaskitem.SubTaskName };
                                        x4.Add(m5);
                                    }
                                    m4.SubTasks = x4;
                                    if (x4!=null && string.IsNullOrEmpty(x4.FirstOrDefault().SubTaskName)) 
                                    {
                                        m4.URL = x4.FirstOrDefault().URL;
                                    }
                                    x3.Add(m4);
                                }
                                m3.Tasks = x3;
                                x2.Add(m3);
                            }                            
                            m2.Navigations = x2;
                            x1.Add(m2);
                        }
                        m1.SubModules = x1;
                        result.Add(m1);
                    }
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
            }
            return result;
        }
        public List<UserModule> GetUserModule(int EmployeeNumber, int CentreCode, ref string pMsg) 
        {
            List<UserModule> result = new List<UserModule>();
            try
            {
                dt = _RBACUserDataSync.GetUserModule(EmployeeNumber, CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_RBACUserDBMapper.Map_UserModule(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }




    }
}
