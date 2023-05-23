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
        public int GetCentreCodeFromLocation(int LocationTypeCode, int LocationCode, ref string pMsg)
        {
            try
            {
                if (LocationTypeCode == 2) { return LocationCode; }
                else 
                {
                    using (SQLHelper sql = new SQLHelper("SELECT [MTR].[GetCentreCodeFromLocation](" + LocationTypeCode + "," + LocationCode + ")", CommandType.Text))
                    {
                        return int.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                    }
                }
                
            }
            catch (Exception ex) { pMsg = ex.Message; return 0; }
        }
        public DataTable getVehicleEligibilityStatement(int EligibleVT,int ProvidedVT, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [MTR].[getVehicleEligibilityStatement](" + EligibleVT + ","+ ProvidedVT + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
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
        public DataTable getLocationsFromType(string LocationTypeID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getLocationsFromTypes]('" + LocationTypeID + "')", CommandType.Text))
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
        public DateTime GetToSchDateForMultiLocation(DateTime FromDate, int FromLocation, string ToLocationType,
            string ToLocation, ref string pMsg)
        {
            DateTime ToDate = new DateTime();
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                para[paracount++].Value = FromDate;
                para[paracount] = new SqlParameter("@FromLocation", SqlDbType.SmallInt);
                para[paracount++].Value = FromLocation;
                para[paracount] = new SqlParameter("@ToLocationType", SqlDbType.VarChar,100);
                para[paracount++].Value = ToLocationType;
                para[paracount] = new SqlParameter("@ToLocation", SqlDbType.VarChar,100);
                para[paracount++].Value = ToLocation;

                using (SQLHelper sql = new SQLHelper("[CTV].[GetSchDateToMulti]", CommandType.StoredProcedure))
                {
                    DataTable dt = sql.GetDataTable(para, ref pMsg);
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
        public DateTime GetEffectedRuleID(int RuleType, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [CTV].[GetEffectiveRuleID](" + RuleType + ")", CommandType.Text))
                {
                    return DateTime.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return DateTime.Today; }
        }
        public DataTable getNewNoteNumber(string CTVPattern, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNoPattern", SqlDbType.NChar, 20);
                para[paracount++].Value = CTVPattern;

                using (SQLHelper sql = new SQLHelper("[MTR].[NewNoteNumber]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getEmployeeList(int centreCode,int functionalDesg,int isOtherStaff,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EHG].[getEmployees]("+functionalDesg+","+centreCode+","+ isOtherStaff + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getDriverList(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MTR].[getDriverList]('#')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getCenterWiseDriverList(int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MTR].[getCenterWiseDriverList]("+ CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getVehicleList(string VehicleType,int wheeltype, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MTR].[getListofVehicles]('"+ VehicleType + "',"+ wheeltype + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getVehicleBasicInfo(string VehicleNumber, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MTR].[getVehicleInfo]('" + VehicleNumber + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getBranchType(int centerid, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getBranchesOfaCentre]('" + centerid + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public int getEligibleVehicleType(int EmployeeNumber, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [ETS].[GetEligibleVehicleType](" + EmployeeNumber + ")", CommandType.Text))
                {
                    return int.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return 0; }
        }
        public string GetDesignation(int PersonID,int PersonType, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [MTR].[GetDesignation](" + PersonID + ","+ PersonType + ")", CommandType.Text))
                {
                    return sql.ExecuteScaler(ref pMsg).ToString();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return ""; }
        }
        public DataTable GetLocationsFromCommaSeparatedTypes(string LocationTypeID, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[GetLocationsFromCommaSeparatedTypes]('" + LocationTypeID + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public bool GetHGOpenOrNot(int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [EHG].[GetHGOpenOrNot](" + CentreCode + ")", CommandType.Text))
                {
                    return bool.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return false; }
        }
        public DataTable SetPunchIN(int CentreCode,int EmployeeNumber,DateTime PunchDate,string PunchTime, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@PunchDate", SqlDbType.Date);
                para[paracount++].Value = PunchDate;
                para[paracount] = new SqlParameter("@PunchTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = PunchTime;

                using (SQLHelper sql = new SQLHelper("[MTR].[SetPunchIN]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);                    
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
