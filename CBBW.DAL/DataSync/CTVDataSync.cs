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
                para[paracount] = new SqlParameter("@NoteNoPattern", SqlDbType.NChar,20);
                para[paracount++].Value = CTVPattern;
                
                using (SQLHelper sql = new SQLHelper("[CTV].[NewVehicleTripSchedule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getVehicleInfo(string VehicleNo, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.VarChar,20);
                para[paracount++].Value = VehicleNo;

                using (SQLHelper sql = new SQLHelper("[CTV].[getVehicleValData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getLCVMCVVehicles(ref string pMsg) 
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getListofVehicles]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        
    }
}
