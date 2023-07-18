using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.Alerts;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
   public class AlertDBMapper
    {
        public string pMsg;
        public AlertMaster Map_AlertMaster(DataRow dr)
        {
            AlertMaster result = new AlertMaster();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["AlertName"]))
                        result.AlertName = dr["AlertName"].ToString();
                    if (!DBNull.Value.Equals(dr["TaskID"]))
                        result.TaskID = int.Parse(dr["TaskID"].ToString());
                    if (!DBNull.Value.Equals(dr["MTag"]))
                        result.MTag = dr["MTag"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    result.EffectiveDateStr = MyDBLogic.ConvertDateToString(result.EffectiveDate);
           

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
        public AlertDetail Map_AlertDetail(DataRow dr)
        {
            AlertDetail result = new AlertDetail();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["AlertID"]))
                        result.AlertID = int.Parse(dr["AlertID"].ToString());
                    if (!DBNull.Value.Equals(dr["AlertDetails"]))
                        result.AlertDetails = dr["AlertDetails"].ToString();
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
