using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Modules;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class ModuleEntities
    {

        DataTable dt = null;
        DataSet ds = null;
        ModuleDataSync _datasync;
        ModuleDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public ModuleEntities()
        {
            _datasync = new ModuleDataSync();
            _DBMapper = new ModuleDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }

        public List<ModuleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<ModuleList> result = new List<ModuleList>();
            try
            {
                dt = _datasync.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_IndexList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public bool SetAddModule(int status, List<Module> dtldata,int UserId, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetAddModule(status,dtldata, UserId, ref pMsg), ref pMsg, ref result);
            return result;
        }

        public Module GetModuleDetails(int ID, ref string pMsg)
        {
            Module result = new Module();
            try
            {
                dt = _datasync.GetModuleDetails(ID, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _DBMapper.Map_ModuleData(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
    }
}
