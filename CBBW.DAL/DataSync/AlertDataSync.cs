using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.Alerts;

namespace CBBW.DAL.DataSync
{
    public class AlertDataSync
    {
        public DataTable GetAlertMasterDetails(int UserId,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetAlertMasterDetails](" + UserId + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable GetAlertDetails(int ID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetAlertDetails](" + ID + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
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
