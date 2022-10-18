using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DataSync
{
    public class CTVDataSync
    {
        public DataTable getNewCTVNoteNo(string CTVPattern, ref string pMsg)
        {
            try
            {
                
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNoPattern", SqlDbType.Date);
                para[paracount++].Value = CTVPattern;
                
                using (SQLHelper sql = new SQLHelper("[CTV].[NewVehicleTripSchedule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
