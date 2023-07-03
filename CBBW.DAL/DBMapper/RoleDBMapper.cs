using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Role;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
   public class RoleDBMapper
    {
        public RoleList Map_IndexList(DataRow dr)
        {
            RoleList result = new RoleList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["RoleId"]))
                        result.RoleId = dr["RoleId"].ToString();
                    if (!DBNull.Value.Equals(dr["RoleName"]))
                        result.RoleName = dr["RoleName"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    result.EntryDateStr = MyDBLogic.ConvertDateToString(result.EntryDate).ToString();
                    result.CanDelete = true;
                }
            }
            catch { }
            return result;
        }
        public TaskNames Map_TaskName(DataRow dr)
        {
            TaskNames result = new TaskNames();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["TaskName"]))
                        result.TaskName = dr["TaskName"].ToString();
                }
            }
            catch { }
            return result;
        }
        public Actions Map_Actions(DataRow dr)
        {
            Actions result = new Actions();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["TaskName"]))
                        result.TaskName = dr["TaskName"].ToString();
                    if (!DBNull.Value.Equals(dr["ActionID"]))
                        result.ActionID = int.Parse(dr["ActionID"].ToString());
                    if (!DBNull.Value.Equals(dr["ActionName"]))
                        result.ActionName = dr["ActionName"].ToString();
                    if (!DBNull.Value.Equals(dr["TaskId"]))
                        result.TaskId = int.Parse(dr["TaskId"].ToString());
                }
            }
            catch { }
            return result;
        }

        public RBACRoles Map_Roles(DataRow dr)
        {
            RBACRoles result = new RBACRoles();
            try
            {
                if (dr != null)
                {
                    //if (!DBNull.Value.Equals(dr["ID"]))
                    //    result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleName"]))
                        result.ModuleName = dr["ModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["SubModuleName"]))
                        result.SubModuleName = dr["SubModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["NavigationName"]))
                        result.NavigationName = dr["NavigationName"].ToString();
                    if (!DBNull.Value.Equals(dr["TaskName"]))
                        result.TaskName = dr["TaskName"].ToString();
                    if (!DBNull.Value.Equals(dr["TaskId"]))
                        result.TaskId = int.Parse(dr["TaskId"].ToString());
                    if (!DBNull.Value.Equals(dr["ActionIds"]))
                        result.ActionIds = dr["ActionIds"].ToString();
                    if (!DBNull.Value.Equals(dr["RoleId"]))
                        result.RoleId = dr["RoleId"].ToString();
                    if (!DBNull.Value.Equals(dr["RoleName"]))
                        result.RoleName = dr["RoleName"].ToString();
                    if (!DBNull.Value.Equals(dr["NavigationId"]))
                        result.NavigationId = int.Parse(dr["NavigationId"].ToString());
                 
                }
            }
            catch { }
            return result;
        }
    }
}
