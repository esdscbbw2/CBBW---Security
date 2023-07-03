using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;
using CBBW.BOL.Task;

namespace CBBW.DAL.Entities
{
    public class TaskEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        TaskDataSync _datasync;
        TaskDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public TaskEntities()
        {
            _datasync = new TaskDataSync();
            _DBMapper = new TaskDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public List<TaskList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<TaskList> result = new List<TaskList>();
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

        public IEnumerable<CustomCheckBoxOption> GetTaskNameList(int Id, ref string pMsg)
        {
            List<CustomCheckBoxOption> result = new List<CustomCheckBoxOption>();
            try
            {
                dt = _datasync.GetTaskNameList(Id, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomCheckBoxOption(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }


        public bool SetAddTaskModule(int status, int UserId,int NavigationId, List<TaskMaster> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetAddTaskModule(status, UserId, NavigationId, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }

        public List<TaskMaster> GetTaskMasterDetails(int ID, ref string pMsg)
        {
            List<TaskMaster> result = new List<TaskMaster>();
            try
            {

                ds = _datasync.GetTaskMasterDetails(ID, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_DBMapper.Map_TaskMasterDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

    }

}
