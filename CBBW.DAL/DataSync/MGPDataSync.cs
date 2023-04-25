using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.MGP;

namespace CBBW.DAL.DataSync
{
    public class MGPDataSync
    {
        #region For Listing Page (Index page)
        public DataTable getMGPDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
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
                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPDetailsforListPage]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        #endregion
        #region In/Out Button Active 
        public DataTable getMGPButtonStatus(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPButtonStatus]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        #endregion
        #region For Out Details
        public DataTable getNoteNumbersForMatGatePass(int CenterCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MGP].[getApprovedNoteNumbersfromCTV](" + CenterCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetNoteNumbersfromMGP(int CenterCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MGP].[GetNoteNumbersfromMGP](" + CenterCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getRFIDCards(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MGP].[getRFIDCardNos]()", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getMGPOutDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPOutDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        // Getting data for Out details in Item Wise Details using NoteNo(For New Data insert)
        public DataSet getItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[getItemWiseDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        // Getting data for Out details in Reference DC Details using NoteNo(For New Data insert)
        public DataSet getReferenceDCDetails(string VehicleNo,DateTime FromDT,DateTime ToDT, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@VehicleNo", SqlDbType.VarChar, 20);
                para[paracount++].Value = VehicleNo;
                para[paracount] = new SqlParameter("@FromDT", SqlDbType.Date);
                para[paracount++].Value = FromDT;
                para[paracount] = new SqlParameter("@ToDT", SqlDbType.Date);
                para[paracount++].Value = ToDT;

                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPDCDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getSchDtlsForMGP(string NoteNumber, ref string pMsg)
        {
            try
            {
                
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPCurrentVehicleOut]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getMGPHistoryDCDetails(long ID,int status, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@ID", SqlDbType.BigInt);
                para[paracount++].Value = @ID;
                para[paracount] = new SqlParameter("@status", SqlDbType.BigInt);
                para[paracount++].Value = status;


                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPHistoryDCDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable setMGPOutDetails(MGPOutSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails,ref string pMsg)
        {
            try
            {
                CommonTable schdtlData = new CommonTable(mgprefdcdetails);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[18];

                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar,25);
                para[paracount++].Value = mgpouthdr.NoteNumber;
                para[paracount] = new SqlParameter("@DriverNo", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.DriverNo;
                para[paracount] = new SqlParameter("@Drivername", SqlDbType.NVarChar);
                para[paracount++].Value = mgpouthdr.Drivername;
                para[paracount] = new SqlParameter("@DesignationCode", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.DesignationCode;
                para[paracount] = new SqlParameter("@DesignationName", SqlDbType.NVarChar,50);
                para[paracount++].Value = mgpouthdr.DesignationName;
                para[paracount] = new SqlParameter("@TripType", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.TripType;
                para[paracount] = new SqlParameter("@TripTypeStr", SqlDbType.VarChar,10);
                para[paracount++].Value = mgpouthdr.TripTypeStr;
                para[paracount] = new SqlParameter("@ToLocationCodeName", SqlDbType.NVarChar);
                para[paracount++].Value = mgpouthdr.ToLocationCodeName;
                para[paracount] = new SqlParameter("@CarryingOutMat", SqlDbType.Bit);
                para[paracount++].Value = mgpouthdr.CarryingOutMat;
                para[paracount] = new SqlParameter("@LoadPercentage", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.LoadPercentage;
                para[paracount] = new SqlParameter("@SchFromDate", SqlDbType.DateTime);
                para[paracount++].Value =mgpouthdr.SchFromDate;
                para[paracount] = new SqlParameter("@KMOUT", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.KMOUT;
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.NVarChar,20);
                para[paracount++].Value = mgpouthdr.VehicleNumber;
                para[paracount] = new SqlParameter("@RFIDCard", SqlDbType.NVarChar,50);
                para[paracount++].Value = mgpouthdr.RFIDCard;
                para[paracount] = new SqlParameter("@ActualTripOutDate", SqlDbType.DateTime);
                para[paracount++].Value = mgpouthdr.ActualTripOutDate;
                para[paracount] = new SqlParameter("@ActualTripOutTime", SqlDbType.NVarChar,15);
                para[paracount++].Value = mgpouthdr.ActualTripOutTime;
                para[paracount] = new SqlParameter("@OutRemarks", SqlDbType.NVarChar,300);
                para[paracount++].Value = mgpouthdr.OutRemarks;
                
                para[paracount] = new SqlParameter("@DCDetails", SqlDbType.Structured);
                para[paracount++].Value = schdtlData.UDTable;
               

                using (SQLHelper sql = new SQLHelper("[MGP].[SetMGPOutDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable spUpdateOutDetailsflag(string NoteNumber, long ID,int status, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@ID", SqlDbType.BigInt);
                para[paracount++].Value = ID;
                para[paracount] = new SqlParameter("@status", SqlDbType.BigInt);
                para[paracount++].Value = status; 
                

                using (SQLHelper sql = new SQLHelper("[MGP].[spUpdateOutDetailsflag]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        #endregion
        #region For In Details
        public DataSet getMGPCurrentOutDetailsForIn(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPCurrentOutDetailsForIn]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getReferenceInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@VehicleNo", SqlDbType.VarChar, 20);
                para[paracount++].Value = VehicleNo;
                para[paracount] = new SqlParameter("@FromDT", SqlDbType.Date);
                para[paracount++].Value = FromDT;
                para[paracount] = new SqlParameter("@ToDT", SqlDbType.Date);
                para[paracount++].Value = ToDT;

                using (SQLHelper sql = new SQLHelper("[MGP].[getMGPINDCDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getItemWiseInDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[getItemWiseInDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable setMGPInDetails(MGPInSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg)
        {
            try
            {
                CommonTable schdtlData = new CommonTable(mgprefdcdetails);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[15];
                para[paracount] = new SqlParameter("@ID", SqlDbType.BigInt);
                para[paracount++].Value = mgpouthdr.ID;
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = mgpouthdr.NoteNumber;
                para[paracount] = new SqlParameter("@RFIDCardIn", SqlDbType.NVarChar,50);
                para[paracount++].Value = mgpouthdr.RFIDCardIn;
                para[paracount] = new SqlParameter("@FromLocationType", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.FromLocationType;
                para[paracount] = new SqlParameter("@FromLocationCode", SqlDbType.Int);
                para[paracount++].Value = mgpouthdr.FromLocationCode;
                para[paracount] = new SqlParameter("@FromLocationName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = mgpouthdr.FromLocationName;
                para[paracount] = new SqlParameter("@CarryingInMaterial", SqlDbType.Bit);
                para[paracount++].Value = mgpouthdr.CarryingInMaterial;
                para[paracount] = new SqlParameter("@LoadPercentageIn", SqlDbType.Float);
                para[paracount++].Value = mgpouthdr.LoadPercentageIn;
                para[paracount] = new SqlParameter("@ActualTripInDate", SqlDbType.Date);
                para[paracount++].Value = mgpouthdr.ActualTripInDate;
                para[paracount] = new SqlParameter("@ActualTripInTime", SqlDbType.NVarChar,15);
                para[paracount++].Value = mgpouthdr.ActualTripInTime;
                para[paracount] = new SqlParameter("@RequiredKmIn", SqlDbType.BigInt);
                para[paracount++].Value = mgpouthdr.RequiredKmIn;
                para[paracount] = new SqlParameter("@ActualKmIn", SqlDbType.BigInt);
                para[paracount++].Value = mgpouthdr.ActualKmIn;
                para[paracount] = new SqlParameter("@KMRunInTrip", SqlDbType.BigInt);
                para[paracount++].Value = mgpouthdr.KMRunInTrip;
                para[paracount] = new SqlParameter("@RemarkIn", SqlDbType.NVarChar,300);
                para[paracount++].Value = mgpouthdr.RemarkIn;
                para[paracount] = new SqlParameter("@DCDetails", SqlDbType.Structured);
                para[paracount++].Value = schdtlData.UDTable;


                using (SQLHelper sql = new SQLHelper("[MGP].[SetMGPInDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        #endregion
        #region For Report
        public DataSet GetMGPDetailsForPrint(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MGP].[GetMGPDetailsForPrint]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet GetMGPDetailsForPrintV2(string NoteNumber,DateTime SchFromDate, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@SchFromDate", SqlDbType.Date);
                para[paracount++].Value = SchFromDate;

                using (SQLHelper sql = new SQLHelper("[MGP].[GetMGPDetailsForPrintReport]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        #endregion

    }
}
