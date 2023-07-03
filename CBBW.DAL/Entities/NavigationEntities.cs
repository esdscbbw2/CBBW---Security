using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Navigation;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class NavigationEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        NavigationDataSync _datasync;
        NavigationDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public NavigationEntities()
        {
            _datasync = new NavigationDataSync();
            _DBMapper = new NavigationDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public List<NavigationList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<NavigationList> result = new List<NavigationList>();
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

        public bool SetAddNavigationModule(int status, int UserId, int ModuleId, int SubModuleId, List<Navigations> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetAddNavigationModule(status, UserId, ModuleId, SubModuleId, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }

        public List<Navigations> GetNavigationDetails(int ID, ref string pMsg)
        {
            List<Navigations> result = new List<Navigations>();
            try
            {

                ds = _datasync.GetNavigationDetails(ID, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_DBMapper.Map_NaviModuleData(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

    }

}
