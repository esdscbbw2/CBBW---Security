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
        public DataSet getMGPHistoryDCDetails(long ID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@ID", SqlDbType.BigInt);
                para[paracount++].Value = @ID;

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

        public DataTable spUpdateOutDetailsflag(string NoteNumber, long ID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@ID", SqlDbType.BigInt);
                para[paracount++].Value = ID;

                using (SQLHelper sql = new SQLHelper("[MGP].[spUpdateOutDetailsflag]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
