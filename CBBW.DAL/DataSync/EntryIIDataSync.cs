using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DataSync
{
    public class EntryIIDataSync
    {
        public DataTable GetEntryIINotes(int CentreCode,bool IsMainLocation, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@IsMainLocation", SqlDbType.Bit);
                para[paracount++].Value = IsMainLocation;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIINotes]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEntryIINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[7];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 250);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@IsMainLocation", SqlDbType.Int);
                para[paracount++].Value = IsMainLocation;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIINoteList]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEntryIITravellingDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar,25);
                para[paracount++].Value = NoteNumber;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIITravellingDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEntryIIVehicleAllotmentDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIIVehicleAllotmentDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }




    }
}
