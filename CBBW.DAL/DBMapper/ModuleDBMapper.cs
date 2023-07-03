using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Modules;
using CBBW.DAL.DBLogic;


namespace CBBW.DAL.DBMapper
{
    public class ModuleDBMapper
    {
        public ModuleList Map_IndexList(DataRow dr)
        {
            ModuleList result = new ModuleList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleName"]))
                        result.ModuleName = dr["ModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
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
        public Module Map_ModuleData(DataRow dr)
        {
            Module result = new Module();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["ModuleName"]))
                        result.ModuleName = dr["ModuleName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["UserId"]))
                        result.UserId = int.Parse(dr["UserId"].ToString());
                    if (!DBNull.Value.Equals(dr["IsSubmit"]))
                        result.IsSubmit = bool.Parse(dr["IsSubmit"].ToString());
                   if (result.EntryDate == DateTime.Today)
                    { result.CanDelete = true; }
                   
                }
            }
            catch { }
            return result;
        }
    }
}
