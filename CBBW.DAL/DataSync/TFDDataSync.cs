using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.TFD;

namespace CBBW.DAL.DataSync
{
   public class TFDDataSync
    {
        public DataTable GetNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [TFD].[getENTNoteNumberList](" + CentreCode + "," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTFDHeaderData(string NoteNumber, int CenterCode, int status, ref string pMsg)
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

                using (SQLHelper sql = new SQLHelper("[TFD].[getTFDHeaderData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetENTTravellingPersonDetails(string NoteNumber, int CenterCode, int status, ref string pMsg)
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

                using (SQLHelper sql = new SQLHelper("[TFD].[getENTTravellingPersonDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetENTDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre, int status, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@PersonType", SqlDbType.Int);
                para[paracount++].Value = PersonType;
                para[paracount] = new SqlParameter("@EmployeeNo", SqlDbType.Int);
                para[paracount++].Value = EmployeeNo;
                para[paracount] = new SqlParameter("@PersonCentre", SqlDbType.Int);
                para[paracount++].Value = PersonCentre;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value =status;

                using (SQLHelper sql = new SQLHelper("[TFD].[getENTDateWiseTourData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetENTAuthEmployeeList(string NoteNumber, int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [TFD].[getENTAuthEmployeeList]('" + NoteNumber + "'," + CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetTFDFeedBackDetails(string NoteNumber, List<TFDTourFeedBackDetails> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@tfdfbdetails", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[TFD].[SetTFDFeedBackDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetTFDetailsFinalSubmit(TFDHdr hdrmodel, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[14];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = hdrmodel.NoteNumber;
                para[paracount] = new SqlParameter("@RefNoteNumber", SqlDbType.VarChar,25);
                para[paracount++].Value = hdrmodel.RefNoteNumber;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.CenterCode;
                para[paracount] = new SqlParameter("@CenterCodeName", SqlDbType.VarChar,100);
                para[paracount++].Value = hdrmodel.CenterCodeName;
                para[paracount] = new SqlParameter("@EntEntryDate", SqlDbType.DateTime);
                para[paracount++].Value = hdrmodel.EntEntryDate;
                para[paracount] = new SqlParameter("@EntEntryTime", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.EntEntryTime;
                para[paracount] = new SqlParameter("@TourFromDate", SqlDbType.DateTime);
                para[paracount++].Value = hdrmodel.TourFromDate;
                para[paracount] = new SqlParameter("@TourToDate", SqlDbType.DateTime);
                para[paracount++].Value = hdrmodel.TourToDate;
                para[paracount] = new SqlParameter("@PurposeOfVisit", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.PurposeOfVisit;
                para[paracount] = new SqlParameter("@AuthEmployeeCode", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.AuthEmployeeCode;
                para[paracount] = new SqlParameter("@AuthEmployeeName", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.AuthEmployeeName;
                para[paracount] = new SqlParameter("@UserID", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.UserID;
                para[paracount] = new SqlParameter("@UserName", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.UserName;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.status;

                using (SQLHelper sql = new SQLHelper("[TFD].[SetTFDFinalSubmit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTFDDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[TFD].[getTFDDetailsforListPage]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTFDTourFeedBackDetails(string NoteNumber, int CenterCode, int status, ref string pMsg)
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

                using (SQLHelper sql = new SQLHelper("[TFD].[getTFDTourFeedBackDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTFDHeaderDetails(string NoteNumber, int CenterCode, int status, ref string pMsg)
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

                using (SQLHelper sql = new SQLHelper("[TFD].[getTFDHeaderDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveTFDNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[TFD].[RemoveTFDNoteNumber]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetENTConcernDeptList(string NoteNumber, int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [TFD].[getENTConcernDeptList]('" + NoteNumber + "'," + CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable SetTFDFeedBackApproval(string NoteNumber, List<TFDTourFBApproval> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@tfdfApprovalbdetails", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[TFD].[SetTFDFeedBackApproval]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable SetTFDDateWiseTourData(string NoteNumber,bool IsApprove,string ApproveReason, List<TFDDateWiseTourData> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@IsApprove", SqlDbType.Bit);
                para[paracount++].Value = IsApprove;
                para[paracount] = new SqlParameter("@ApproveReason", SqlDbType.NChar, 100);
                para[paracount++].Value = ApproveReason;
                para[paracount] = new SqlParameter("@TFdDateWise", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[TFD].[SetTFDDateWiseTourData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTFDDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre, int status, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@PersonType", SqlDbType.Int);
                para[paracount++].Value = PersonType;
                para[paracount] = new SqlParameter("@EmployeeNo", SqlDbType.Int);
                para[paracount++].Value = EmployeeNo;
                para[paracount] = new SqlParameter("@PersonCentre", SqlDbType.Int);
                para[paracount++].Value = PersonCentre;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;

                using (SQLHelper sql = new SQLHelper("[TFD].[getTFDDateWiseTourData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
