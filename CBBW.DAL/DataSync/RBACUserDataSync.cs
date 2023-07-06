using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.RBACUsers;

namespace CBBW.DAL.DataSync
{
    public class RBACUserDataSync
    {
        public DataTable GetListOfActiveEmployees(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetListOfActiveEmployees]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null; 
            }
        }
        public bool ValidateUserName(string UserName, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [RBAC].[ValidateUserName]('" + UserName + "')", CommandType.Text))
                {
                    return bool.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                }

            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return false;
            }
        }
        public DataTable GetCentreList(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MTR].[GetCentreList](1)", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null; 
            }
        }
        public DataTable GetListOfRoles(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetListOfRoles]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) 
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null; 
            }
        }
        public DataTable SetUserData(UpdateUser data, ref string pMsg,bool IsEdit=false)
        {
            try
            {
                CommonTable srcdtl = new CommonTable(data.UserRoleList);
                CommonTable dtl = new CommonTable(data.UserRoles);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[9];                
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = data.EmployeeNumber;
                para[paracount] = new SqlParameter("@EmployeeName", SqlDbType.NVarChar,150);
                para[paracount++].Value = data.EmployeeName;
                para[paracount] = new SqlParameter("@UserName", SqlDbType.NVarChar,50);
                para[paracount++].Value = data.UserName;
                para[paracount] = new SqlParameter("@Password", SqlDbType.NVarChar);
                para[paracount++].Value = data.Password;
                para[paracount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[paracount++].Value = data.IsActive;
                para[paracount] = new SqlParameter("@AdminID", SqlDbType.Int);
                para[paracount++].Value = data.AdminID;
                para[paracount] = new SqlParameter("@IsEdit", SqlDbType.Bit);
                para[paracount++].Value = IsEdit;
                para[paracount] = new SqlParameter("@UserRoles", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                para[paracount] = new SqlParameter("@SrcUserRoles", SqlDbType.Structured);
                para[paracount++].Value = srcdtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[RBAC].[SetUserData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }
        public DataTable GetUserList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 250);
                para[paracount++].Value = SearchText;                
                using (SQLHelper sql = new SQLHelper("[RBAC].[GetUserList]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }
        public DataTable GetUserRoles(int EmployeeNumber,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetUserRoleDetails]("+ EmployeeNumber + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }
        public DataTable DeleteUserRole(int EmployeeNumber,string RoleIDs, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@RoleIds", SqlDbType.NVarChar);
                para[paracount++].Value = RoleIDs;
                using (SQLHelper sql = new SQLHelper("[RBAC].[DeleteUserRole]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }
        public DataTable UpdatePassword(UpdatePassword data, ref string pMsg)
        {
            try
            {                
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = data.EmployeeNumber;                
                para[paracount] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = data.UserName;
                para[paracount] = new SqlParameter("@Password", SqlDbType.NVarChar);
                para[paracount++].Value = data.Password;
                using (SQLHelper sql = new SQLHelper("[RBAC].[UpdatePassword]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }

    }
}
