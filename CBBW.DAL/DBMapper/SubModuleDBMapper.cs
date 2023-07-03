using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.SubModules;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
    public class SubModuleDBMapper
    {

        public SubModuleList Map_IndexList(DataRow dr)
        {
            SubModuleList result = new SubModuleList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleId"]))
                        result.ModuleId = int.Parse(dr["ModuleId"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleName"]))
                        result.ModuleName = dr["ModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["SubModuleName"]))
                        result.SubModuleName = dr["SubModuleName"].ToString();
       
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    //if (!result.IsApproved.HasValue && result.EntryDate == DateTime.Today)
                    //{ result.CanDelete = true; }
                    result.CanDelete = true;
                }
            }
            catch { }
            return result;
        }
        public SubModule Map_SubModuleData(DataRow dr)
        {
            SubModule result = new SubModule();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleId"]))
                        result.ModuleId = int.Parse(dr["ModuleId"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleName"]))
                        result.ModuleName = dr["ModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["SubModuleName"]))
                        result.SubModuleName = dr["SubModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["UserId"]))
                        result.UserId = int.Parse(dr["UserId"].ToString());
                    if (!DBNull.Value.Equals(dr["IsSubmit"]))
                        result.IsSubmit = bool.Parse(dr["IsSubmit"].ToString());
                    result.IsActiveInt = result.IsActive == true ? 1 : 0;
                    if (result.EntryDate == DateTime.Today)
                    { result.CanDelete = true; }

                }
            }
            catch { }
            return result;
        }
    }
}
