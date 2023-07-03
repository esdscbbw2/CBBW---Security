using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.SubModules;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public  class SubModuleEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        SubModuleDataSync _datasync;
        SubModuleDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public SubModuleEntities()
        {
            _datasync = new SubModuleDataSync();
            _DBMapper = new SubModuleDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }

        public List<SubModuleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<SubModuleList> result = new List<SubModuleList>();
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
        public bool SetAddSubModule(int status, int UserId, int ModuleId, List<SubModule> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetAddSubModule(status, UserId, ModuleId, dtldata,ref pMsg), ref pMsg, ref result);
            return result;
        }

        public List<SubModule> GetSubModuleDetails(int ID, ref string pMsg)
        {
            List<SubModule> result = new List<SubModule>();
            try
            {

                ds = _datasync.GetSubModuleDetails(ID, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_DBMapper.Map_SubModuleData(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

    }
}
