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


    }
}
