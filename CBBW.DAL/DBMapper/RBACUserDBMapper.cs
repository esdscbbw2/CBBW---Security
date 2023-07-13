using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.RBACUsers;


namespace CBBW.DAL.DBMapper
{
    public class RBACUserDBMapper
    {
        public Employee Map_Employee(DataRow dr)
        {
            Employee result = new Employee();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeName"]))
                        result.EmployeeName = dr["EmployeeName"].ToString();
                    if (!DBNull.Value.Equals(dr["Designation"]))
                        result.Designation = dr["Designation"].ToString();
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public MyRole Map_MyRole(DataRow dr)
        {
            MyRole result = new MyRole();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RoleId"]))
                        result.RoleId = dr["RoleId"].ToString();
                    if (!DBNull.Value.Equals(dr["RoleName"]))
                        result.RoleName = dr["RoleName"].ToString();                    
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public UserList Map_UserList(DataRow dr) 
        {
            UserList result = new UserList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeName"]))
                        result.EmployeeName = dr["EmployeeName"].ToString();
                    if (!DBNull.Value.Equals(dr["UserName"]))
                        result.UserName = dr["UserName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmpStatus"]))
                        result.IsActivestr = dr["EmpStatus"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public ViewUserData Map_ViewUserData(DataRow dr) 
        {
            ViewUserData result = new ViewUserData();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeName"]))
                        result.EmployeeName = dr["EmployeeName"].ToString();
                    if (!DBNull.Value.Equals(dr["UserName"]))
                        result.UserName = dr["UserName"].ToString();
                    if (!DBNull.Value.Equals(dr["Designation"]))
                        result.Designation = dr["Designation"].ToString();
                    if (!DBNull.Value.Equals(dr["RoleIds"]))
                        result.RoleIDs = dr["RoleIds"].ToString();
                    if (!DBNull.Value.Equals(dr["Roles"]))
                        result.RoleName = dr["Roles"].ToString();
                    if (!DBNull.Value.Equals(dr["LocationTypes"]))
                        result.LocationTypeCodeDesc = dr["LocationTypes"].ToString();
                    if (!DBNull.Value.Equals(dr["Locations"]))
                        result.LocationCodeDesc =dr["Locations"].ToString();
                    if (!DBNull.Value.Equals(dr["EffectiveFromDate"]))
                        result.EffectiveFromDate = DateTime.Parse(dr["EffectiveFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EffectiveToDate"]))
                        result.EffectiveToDate = DateTime.Parse(dr["EffectiveToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationTypeCodes"]))
                        result.LocationTypeCodes = dr["LocationTypeCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["LocationCodes"]))
                        result.LocationCodes = dr["LocationCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    result.EffectiveFromDateStr = result.EffectiveFromDate.ToString("yyyy-MM-dd");
                    //result.EffectiveToDateStr = result.EffectiveToDate.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public UserMenuRaw Map_UserMenuRaw(DataRow dr) 
        {
            UserMenuRaw result = new UserMenuRaw();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ModuleID"]))
                        result.ModuleID = int.Parse(dr["ModuleID"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleName"]))
                        result.ModuleName = dr["ModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["SubModuleID"]))
                        result.SubModuleID = int.Parse(dr["SubModuleID"].ToString());
                    if (!DBNull.Value.Equals(dr["SubModuleName"]))
                        result.SubModuleName = dr["SubModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["NavigationID"]))
                        result.NavigationID = int.Parse(dr["NavigationID"].ToString());
                    if (!DBNull.Value.Equals(dr["NavigationName"]))
                        result.NavigationName = dr["NavigationName"].ToString();
                    if (!DBNull.Value.Equals(dr["TaskID"]))
                        result.TaskID = int.Parse(dr["TaskID"].ToString());
                    if (!DBNull.Value.Equals(dr["TaskName"]))
                        result.TaskName = dr["TaskName"].ToString();
                    if (!DBNull.Value.Equals(dr["URL"]))
                        result.URL = dr["URL"].ToString();
                    if (!DBNull.Value.Equals(dr["IsSubTask"]))
                        result.IsSubTask =bool.Parse(dr["IsSubTask"].ToString());
                    if (!DBNull.Value.Equals(dr["SubTaskName"]))
                        result.SubTaskName = dr["SubTaskName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }


    }
}
