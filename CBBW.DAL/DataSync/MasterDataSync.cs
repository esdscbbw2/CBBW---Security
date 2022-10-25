using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DataSync
{
    public class MasterDataSync
    {
        public DataTable getServiceTypes(int ID,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [MTR].[master_GetServiceTypes](" + ID + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message;return null; }
        }
        public DataTable getPublicTransType(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [RUL].[getPublicTransType]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getPubTransClassType(int ID,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [RUL].[getPublicTransClassType]("+ ID + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getLocationTypes(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getLocationTypes]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getLocationsFromType(int LocationTypeID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getLocationsFromType](" + LocationTypeID + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public float GetDistance(int FromLocation, int ToLocationType, int ToLocation, ref string pMsg)
        {
            float distance = 0;
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [CTV].[GetDistanceinKm](" + FromLocation + "," + ToLocationType + "," + ToLocation + ")", CommandType.Text))
                {
                    DataTable dt = sql.GetDataTable(ref pMsg);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (!DBNull.Value.Equals(dt.Rows[0]["Distance"]))
                            distance = float.Parse(dt.Rows[0]["Distance"].ToString());
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return distance;
        }
        public DateTime GetToSchDate(DateTime FromDate,int FromLocation, int ToLocationType, 
            int ToLocation,int IsCalculateHourly, ref string pMsg)
        {
            DateTime ToDate =new DateTime();
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                para[paracount++].Value = FromDate;
                para[paracount] = new SqlParameter("@FromLocation", SqlDbType.SmallInt);
                para[paracount++].Value = FromLocation;
                para[paracount] = new SqlParameter("@ToLocationType", SqlDbType.SmallInt);
                para[paracount++].Value = ToLocationType;
                para[paracount] = new SqlParameter("@ToLocation", SqlDbType.SmallInt);
                para[paracount++].Value = ToLocation;
                para[paracount] = new SqlParameter("@CalculatedHourly", SqlDbType.Bit);
                para[paracount++].Value = IsCalculateHourly;

                using (SQLHelper sql = new SQLHelper("[CTV].[GetSchDateTo]", CommandType.StoredProcedure))
                {
                    DataTable dt = sql.GetDataTable(para,ref pMsg);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (!DBNull.Value.Equals(dt.Rows[0]["ToDate"]))
                            ToDate = DateTime.Parse(dt.Rows[0]["ToDate"].ToString());
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return ToDate;
        }
    }
}
