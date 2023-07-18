using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.Alerts;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public class AlertEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        AlertDataSync _datasync;
        AlertDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public AlertEntities()
        {
            _datasync = new AlertDataSync();
            _DBMapper = new AlertDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }

        public List<AlertMaster> GetAlertMasterDetails(int UserId, ref string pMsg)
        {
            List<AlertMaster> result = new List<AlertMaster>();
            try
            {
                dt = _datasync.GetAlertMasterDetails(UserId, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_AlertMaster(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
            return result;
        }
        public List<AlertDetail> GetAlertDetails(int ID, ref string pMsg)
        {
            List<AlertDetail> result = new List<AlertDetail>();
            try
            {
                dt = _datasync.GetAlertDetails(ID, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_AlertDetail(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
            return result;
        }
    }
}
