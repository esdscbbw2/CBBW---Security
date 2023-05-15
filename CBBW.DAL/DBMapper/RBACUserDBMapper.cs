using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            catch (Exception ex) { ex.ToString(); }
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
                        result.RoleId = int.Parse(dr["RoleId"].ToString());
                    if (!DBNull.Value.Equals(dr["RoleName"]))
                        result.RoleName = dr["RoleName"].ToString();                    
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }





    }
}
