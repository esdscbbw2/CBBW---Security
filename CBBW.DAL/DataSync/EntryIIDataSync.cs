using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EntryII;

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
        public DataTable GetEntryIIVehicleAllotmentDetails(string NoteNumber,DateTime FromDate,DateTime ToDate,int CentreCode,bool IsMainLocation, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@SchFromDate", SqlDbType.Date);
                para[paracount++].Value = FromDate;
                para[paracount] = new SqlParameter("@SchToDate", SqlDbType.Date);
                para[paracount++].Value = ToDate;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@IsMainLocation", SqlDbType.Bit);
                para[paracount++].Value = IsMainLocation;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIIVehicleAllotmentDetailsV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetLastPunchingOfaPerson(int EmployeeNumber,DateTime PunchDate, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@PunchDate", SqlDbType.Date);
                para[paracount++].Value = PunchDate;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetLastPunchingOfaPerson]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetLastPunchingCentreOfaPerson(int EmployeeNumber, DateTime PunchDate,int CurrentCentreCode, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@PunchDate", SqlDbType.Date);
                para[paracount++].Value = PunchDate;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CurrentCentreCode;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetLastPunchingCentreOfaPerson]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetPunchingDetails(int EmployeeNumber, DateTime PunchDate, int CentreCode,string RFIDNumber, ref string pMsg) 
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
                para[paracount] = new SqlParameter("@RFIDNumber", SqlDbType.NVarChar);
                para[paracount++].Value = RFIDNumber;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetPunchingDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetRequiredTimeInMinutesForEmployee(int EmployeeNumber,bool IsVehicleProvided,int FromCentreCode,int ToCentreCode,ref string pMsg) 
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select RequiredTimeInMinutes from [ENT].[GetRequiredTimeInForEmployee](" + EmployeeNumber + ","+ IsVehicleProvided + ","+ FromCentreCode + ","+ ToCentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetPunchingsV2(DateTime PunchDate, int CentreCode,string EmployeeIDs,  ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@PunchDate", SqlDbType.Date);
                para[paracount++].Value = PunchDate;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@EmployeeIDs", SqlDbType.NVarChar);
                para[paracount++].Value = EmployeeIDs;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetPunchingsV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetLastPunchingCentresV2(DateTime PunchDate, int CentreCode, string EmployeeIDs, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@PunchDate", SqlDbType.Date);
                para[paracount++].Value = PunchDate;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@EmployeeNumbers", SqlDbType.NVarChar);
                para[paracount++].Value = EmployeeIDs;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetLastPunchingCentresV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetPunchingsV3(int CentreCode, List<EmpDate> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];                
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@PunchEmpDate", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetPunchingsV3]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetLastPunchingCentresV3(int CentreCode, List<EmpDate> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@PunchEmpDate", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetLastPunchingCentresV3]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet GetEntryIITPOutInDetails(string NoteNumber, ref string pMsg, int CentreCode = 0)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIITPOutInDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet GetEntryIITPOutInDetailsLW(string NoteNumber, int CentreCode, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                using (SQLHelper sql = new SQLHelper("[ENT].[GetEntryIITPOutInDetailsLW]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEntryIIData(string NoteNumber,bool IsMainLocation,
            int CentreCode, bool IsOffline, List<SaveTPDetails> Persons, List<SaveTPDWDetails> DWTour,
            List<SaveVehicleDetails> VAData,ref string pMsg) 
        {
            try
            {
                CommonTable TP = new CommonTable(Persons);
                CommonTable DWT = new CommonTable(DWTour);
                CommonTable VA = new CommonTable(VAData);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[7];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar,25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@IsMainLocation", SqlDbType.Bit);
                para[paracount++].Value = IsMainLocation;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@IsOffline", SqlDbType.Bit);
                para[paracount++].Value = IsOffline;
                para[paracount] = new SqlParameter("@EntryIIDateWise", SqlDbType.Structured);
                para[paracount++].Value = DWT.UDTable;
                para[paracount] = new SqlParameter("@EntryIITPersons", SqlDbType.Structured);
                para[paracount++].Value = TP.UDTable;
                para[paracount] = new SqlParameter("@EntryIIVADetails", SqlDbType.Structured);
                para[paracount++].Value = VA.UDTable;
                using (SQLHelper sql = new SQLHelper("[ENT].[SetEntryIIData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable UpdateEntryIIData(string NoteNumber,int CentreCode,string CentreName, bool IsEPTour,
            bool IsMainLocation,  ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@CentreCodeName", SqlDbType.NVarChar,50);
                para[paracount++].Value = CentreName;
                para[paracount] = new SqlParameter("@IsEPTour", SqlDbType.Bit);
                para[paracount++].Value = IsEPTour;
                para[paracount] = new SqlParameter("@IsMainLocation", SqlDbType.Bit);
                para[paracount++].Value = IsMainLocation;                
                using (SQLHelper sql = new SQLHelper("[ENT].[UpdateEntryIIData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public int GetTravelKmsOfANote(string NoteNumber,DateTime TillDate,int FromLocation,ref string pMsg) 
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [ENT].[GetTravelKmsOfANote]('" + NoteNumber + "','"+ TillDate.ToString("yyyy-MM-dd") + "',"+ FromLocation + ")", CommandType.Text))
                {
                    return int.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return 0; }
        }




    }
}
