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




    }
}
