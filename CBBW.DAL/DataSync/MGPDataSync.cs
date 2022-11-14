using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DataSync
{
    public class MGPDataSync
    {
        public DataTable getNoteNumbersForMatGatePass(int CenterCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getNoteNumbersForMatGatePass](" + CenterCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
