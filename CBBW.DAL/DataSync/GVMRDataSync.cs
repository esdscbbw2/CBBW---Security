using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.GVMR;

namespace CBBW.DAL.DataSync
{
    public class GVMRDataSync
    {

        public DataTable GetNoteNumbers(int CenterCode, int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [GVMR].[getNoteNumbersFromMGP](" + CenterCode + "," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet GetGVMRDetails(string NoteNumber,int CenterCode, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;
                using (SQLHelper sql = new SQLHelper("[GVMR].[getGVMRDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataSet GetPunchingDetails(string CentreCode, DateTime FromDate, DateTime ToDate,int UserID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.NVarChar);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                para[paracount++].Value = FromDate;
                para[paracount] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                para[paracount++].Value = ToDate;
                para[paracount] = new SqlParameter("@UserID", SqlDbType.Int);
                para[paracount++].Value = UserID;
                using (SQLHelper sql = new SQLHelper("[GVMR].[GetGVMRPunchingsNew]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable setGVMRDetails(GVMRDataSave gvmrdata, ref string pMsg)
        {
            try
            {
               
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[12];
                para[paracount] = new SqlParameter("@Gvmrid", SqlDbType.BigInt);
                para[paracount++].Value = gvmrdata.Gvmrid;
                para[paracount] = new SqlParameter("@NoteNo", SqlDbType.NVarChar, 25);
                para[paracount++].Value = gvmrdata.NoteNo;
                para[paracount] = new SqlParameter("@ActualInRFIDCard", SqlDbType.NVarChar, 50);
                para[paracount++].Value = gvmrdata.ActualInRFIDCard;
                para[paracount] = new SqlParameter("@ActualTripInDate", SqlDbType.DateTime);
                para[paracount++].Value = gvmrdata.ActualTripInDate;
                para[paracount] = new SqlParameter("@ActualTripInTime", SqlDbType.NVarChar,15);
                para[paracount++].Value = gvmrdata.ActualTripInTime;
                para[paracount] = new SqlParameter("@ActualTripInKM", SqlDbType.BigInt);
                para[paracount++].Value = gvmrdata.ActualTripInKM;
                para[paracount] = new SqlParameter("@ActualOutRFIDCard", SqlDbType.NVarChar,50);
                para[paracount++].Value = gvmrdata.ActualOutRFIDCard;
                para[paracount] = new SqlParameter("@ActualTripOutDate", SqlDbType.DateTime);
                para[paracount++].Value = gvmrdata.ActualTripOutDate;
                para[paracount] = new SqlParameter("@ActualTripOutTime", SqlDbType.NVarChar,15);
                para[paracount++].Value = gvmrdata.ActualTripOutTime;
                para[paracount] = new SqlParameter("@ActualTripOutKM", SqlDbType.BigInt);
                para[paracount++].Value = gvmrdata.ActualTripOutKM;
                para[paracount] = new SqlParameter("@Remark", SqlDbType.NVarChar,100);
                para[paracount++].Value = gvmrdata.Remark;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = gvmrdata.CenterCode;
                using (SQLHelper sql = new SQLHelper("[GVMR].[SetGVMRDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getGVMRDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[6];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 255);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;
                using (SQLHelper sql = new SQLHelper("[GVMR].[getGVMRDetailsforListPage]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getGVMRDetailsForView(string NoteNumber, int CenterCode, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;


                using (SQLHelper sql = new SQLHelper("[GVMR].[getGVMRDetailsForView]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        // Not in Use
        public DataSet GetGVMRDetailsWithPunchingDetails(string NoteNumber, int CenterCode, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;


                using (SQLHelper sql = new SQLHelper("[GVMR].[getGVMRDetailsWithPunchingDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetGVMRDetailsV2(List<GVMRDataSave> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@GVMRDetails", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[GVMR].[SetGVMRDetailsV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }

}
