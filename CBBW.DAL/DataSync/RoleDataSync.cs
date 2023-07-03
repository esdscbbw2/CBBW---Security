using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.Role;

namespace CBBW.DAL.DataSync
{
    public class RoleDataSync
    {
        public DataTable GetModuleList(int ID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[getModuleList](" + ID + ")", CommandType.Text))
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
        public DataTable GetSubModuleList(int ModuleId, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[getSubModuleList](" + ModuleId + ")", CommandType.Text))
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
        public DataTable GetNavigationList(int SubModuleId, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[getNavigationList](" + SubModuleId + ")", CommandType.Text))
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
        public DataTable GetTaskList(int ID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[getTaskListForRole](" + ID + ")", CommandType.Text))
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
        public DataTable GetActionList(int ID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[getActionsList](" + ID + ")", CommandType.Text))
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
        public DataSet GetTaskDetails(int NavigationId, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NavigationId", SqlDbType.Int);
                para[paracount++].Value = NavigationId;
                using (SQLHelper sql = new SQLHelper("[RBAC].[getTaskDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }
        public DataTable SetRoleModule(string RoleId,string RoleName, int NavigationId, int status, int UserId, List<Actions> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[6];
                para[paracount] = new SqlParameter("@RoleId", SqlDbType.NChar,25);
                para[paracount++].Value = RoleId;
                para[paracount] = new SqlParameter("@RoleName", SqlDbType.NVarChar);
                para[paracount++].Value = RoleName;
                para[paracount] = new SqlParameter("@NavigationId", SqlDbType.Int);
                para[paracount++].Value = NavigationId;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;
                para[paracount] = new SqlParameter("@UserId", SqlDbType.Int);
                para[paracount++].Value = UserId;
                para[paracount] = new SqlParameter("@RoleList", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[RBAC].[SetRoleModule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; return null;
            }
        }
        public DataTable GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[7];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 255);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;
                using (SQLHelper sql = new SQLHelper("[RBAC].[GetRoleLists]", CommandType.StoredProcedure))
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
        public DataTable GetRoleDetails(string RoleId, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@RoleId", SqlDbType.NVarChar, 25);
                para[paracount++].Value = RoleId;
                using (SQLHelper sql = new SQLHelper("[RBAC].[GetRoleDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message; 
                return null;
            }
        }
        public DataTable GetRoleDetailsForView(string RoleId, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@RoleId", SqlDbType.NVarChar, 25);
                para[paracount++].Value = RoleId;
                using (SQLHelper sql = new SQLHelper("[RBAC].[GetRoleDetailsForViewDelete]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable SetRoleTaskDelete(string RoleId,int NavigationId, int TaskId, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@RoleId", SqlDbType.NVarChar, 25);
                para[paracount++].Value = RoleId;
                para[paracount] = new SqlParameter("@NavigationId", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NavigationId;
                para[paracount] = new SqlParameter("@TaskId", SqlDbType.NVarChar, 25);
                para[paracount++].Value = TaskId;
                using (SQLHelper sql = new SQLHelper("[RBAC].[SetDeleteRoleModule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }

        public DataTable SetRBACMVC(List<PageInformation> pagesinfo, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(pagesinfo);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@MVCList", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[RBAC].[SetRBACMVC]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }

    }
}
