using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EMC;

namespace CBBW.DAL.DataSync
{
    public class EMCDataSync
    {
        public DataTable GetEMCNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
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
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 255);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;
                using (SQLHelper sql = new SQLHelper("[MFG].[getMFGNZBDetailsforListPage]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEMCTravellingPerson(string NoteNumber,int CenterCode,string CenterCodeName, List<EMCTravellingPerson> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;
                para[paracount] = new SqlParameter("@CenterCodeName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = CenterCodeName;
                para[paracount] = new SqlParameter("@TravellingPersonDtl", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[MFG].[SetMFGTravellingPerson]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable setEMCTravDetailsNTourDetails(string NoteNumber, List<EMCTravellingDetails> TDdata, List<EMCDateWiseTour> DWTdata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(TDdata);
                CommonTable dt2 = new CommonTable(DWTdata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@TravellingDetailsDtl", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                para[paracount] = new SqlParameter("@TourWiseDetailsDtl", SqlDbType.Structured);
                para[paracount++].Value = dt2.UDTable;
                using (SQLHelper sql = new SQLHelper("[MFG].[SetMFGTravDetailsNTourDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEMCTravellingPerson(string NoteNumber,int CenterCode,int status, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = CenterCode;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;

                using (SQLHelper sql = new SQLHelper("[MFG].[getMFGTravellingPerson]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEMCDetailsFinalSubmit(EMCHeader hdrmodel, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = hdrmodel.NoteNumber;
                para[paracount] = new SqlParameter("@CenterCodeName", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.CenterCodeName;
                para[paracount] = new SqlParameter("@AttachFile", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.AttachFile;
                para[paracount] = new SqlParameter("@IsEPTour", SqlDbType.Bit);
                para[paracount++].Value = hdrmodel.IsEPTour;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.Status;

                using (SQLHelper sql = new SQLHelper("[MFG].[SetMFGDetailsFinalSubmit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEMCHdrEntry(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MFG].[getMFGHdrEntry]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveEMCNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@RemoveTag", SqlDbType.Int);
                para[paracount++].Value = RemoveTag;
                para[paracount] = new SqlParameter("@ActiveTag", SqlDbType.Int);
                para[paracount++].Value = ActiveTag;
                using (SQLHelper sql = new SQLHelper("[MFG].[RemoveMFGNoteNumber]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEMCTravellingDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MFG].[getMFGTravellingDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEMCDateWiseTour(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[MFG].[getMFGDateWiseTour]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEMCNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MFG].[getMFGNoteListToBeApproved](" + CentreCode + "," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEMCApprovalData(EMCApproveTravDetails model, ref string pMsg)
        {
            try
            {

                int paracount = 0;
                SqlParameter[] para = new SqlParameter[7];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = model.NoteNumber;
                para[paracount] = new SqlParameter("@VehicleTypeProvided", SqlDbType.Int);
                para[paracount++].Value = model.VehicleTypeProvided;
                para[paracount] = new SqlParameter("@ReasonVehicleProvided", SqlDbType.NChar, 100);
                para[paracount++].Value = model.ReasonVehicleProvided;
                para[paracount] = new SqlParameter("@EmployeeNonName", SqlDbType.NChar, 150);
                para[paracount++].Value = model.EmployeeNonName;
                para[paracount] = new SqlParameter("@IsApproved", SqlDbType.Bit);
                para[paracount++].Value = model.IsApproved;
                para[paracount] = new SqlParameter("@ApprovedReason", SqlDbType.NChar, 250);
                para[paracount++].Value = model.ApprovedReason;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = model.status;

                using (SQLHelper sql = new SQLHelper("[MFG].[SetMFGApprovalData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        
        public DataTable GetEPTourNoteNumber(int EmployeeNumber, int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MFG].[getEPTourNoteNumber](" + EmployeeNumber + "," + CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }


    }
}
