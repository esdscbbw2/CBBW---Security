using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.BIL;

namespace CBBW.DAL.DataSync
{
   public class BILDataSync
    {
        public DataTable GetNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [BIL].[getAllNoteList](" + CentreCode + "," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetBILNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [BIL].[getBILNoteNumberList](" + CentreCode + "," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTAdARuleData(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar,25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[BIL].[getTADACalculation]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[BIL].[getBILforListPage]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEmployeeList(string NoteNumber, int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [BIL].[getAllEmployeeList]('" + NoteNumber + "'," + CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetNoteHdr(string NoteNumber, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [BIL].[getNoteHdr]('" + NoteNumber + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetDADetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[BIL].[getTADACalculationDateWise]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable SetSetTADABillGeneration (TADABillGeneration hdrmodel, ref string pMsg)
        {
            try
            {

                int paracount = 0;
                SqlParameter[] para = new SqlParameter[23];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = hdrmodel.NoteNumber;
                para[paracount] = new SqlParameter("@RefNoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = hdrmodel.RefNoteNumber;
                para[paracount] = new SqlParameter("@EmployeeCode", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.EmployeeNo;
                para[paracount] = new SqlParameter("@EmployeeCodeName", SqlDbType.NVarChar,100);
                para[paracount++].Value = hdrmodel.EmployeeCodeName;
                para[paracount] = new SqlParameter("@RefEntryDate", SqlDbType.DateTime);
                if (hdrmodel.RefEntryDate.Year != 0001)
                {
                    para[paracount++].Value = hdrmodel.RefEntryDate;
                }
                //para[paracount++].Value = hdrmodel.RefEntryDate;
                para[paracount] = new SqlParameter("@RefEntryTime", SqlDbType.NVarChar,15);
                para[paracount++].Value = hdrmodel.RefEntryTime;
                para[paracount] = new SqlParameter("@PersonType", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.PersonType;
                para[paracount] = new SqlParameter("@PersonTypetxt", SqlDbType.NVarChar,100);
                para[paracount++].Value = hdrmodel.PersonTypetxt;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.CenterCode;
                para[paracount] = new SqlParameter("@CenterCodeName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = hdrmodel.CenterCodeName;
                para[paracount] = new SqlParameter("@DesigCode", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.DesigCode;
                para[paracount] = new SqlParameter("@DesigCodeName", SqlDbType.NVarChar,100);
                para[paracount++].Value = hdrmodel.DesigCodeName;
                para[paracount] = new SqlParameter("@DAAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.DAAmount;
                para[paracount] = new SqlParameter("@DADeducted", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.DADeducted;
                para[paracount] = new SqlParameter("@EDAllowance", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.EDAllowance;

                para[paracount] = new SqlParameter("@TAAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.TAAmount;
                para[paracount] = new SqlParameter("@LocalConveyance", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.LocalConveyance;
                para[paracount] = new SqlParameter("@Lodging", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.Lodging; 
                para[paracount] = new SqlParameter("@TotalExpenses", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.TotalExpenses;
                para[paracount] = new SqlParameter("@TourFromDate", SqlDbType.DateTime);
                if (hdrmodel.TourFromDate.Year != 0001)
                {
                    para[paracount++].Value = hdrmodel.TourFromDate;
                }
                //para[paracount++].Value = hdrmodel.TourFromDate;
                para[paracount] = new SqlParameter("@TourToDate", SqlDbType.DateTime);
                if (hdrmodel.TourToDate.Year != 0001)
                {
                    para[paracount++].Value = hdrmodel.TourToDate;
                }
                //para[paracount++].Value = hdrmodel.TourToDate;
                para[paracount] = new SqlParameter("@NoOfDays", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.NoOfDays;
                para[paracount] = new SqlParameter("@PurposeOfVisit", SqlDbType.NVarChar,100);
                para[paracount++].Value = hdrmodel.PurposeOfVisit;

               using (SQLHelper sql = new SQLHelper("[BIL].[SetTADABillGeneration]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable SetBillGenerationFinalSubmit(string NoteNumber, int status, ref string pMsg)
        {
            try
            {

                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;
                using (SQLHelper sql = new SQLHelper("[BIL].[SetBillGenerationFinalSubmit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTADABillGenerationData(string NoteNumber,string RefNoteNumber ,int EmployeeNo,int status, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@RefNoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = RefNoteNumber;
                para[paracount] = new SqlParameter("@EmployeeNo", SqlDbType.Int);
                para[paracount++].Value = EmployeeNo;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;

                using (SQLHelper sql = new SQLHelper("[BIL].[getTADABillGenerationData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable RemoveBILNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[BIL].[RemoveBILNoteNumber]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable SetApprovalTADABillGeneration(TADABillGeneration hdrmodel, ref string pMsg)
        {
            try
            {

                int paracount = 0;
                SqlParameter[] para = new SqlParameter[16];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = hdrmodel.NoteNumber;
                para[paracount] = new SqlParameter("@IsApproved", SqlDbType.Bit);
                para[paracount++].Value = hdrmodel.IsApproved;
                para[paracount] = new SqlParameter("@ApprovalReason", SqlDbType.NVarChar,100);
                para[paracount++].Value = hdrmodel.ApprovalReason = hdrmodel.ApprovalReason != null ? hdrmodel.ApprovalReason : "NA";
                para[paracount] = new SqlParameter("@AEDAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.AEDAmount;
                para[paracount] = new SqlParameter("@ATAAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ATAAmount;
                para[paracount] = new SqlParameter("@ALocAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ALocAmount;
                para[paracount] = new SqlParameter("@ALodAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ALodAmount;
                para[paracount] = new SqlParameter("@ATotalAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ATotalAmount;
                para[paracount] = new SqlParameter("@AReamrk", SqlDbType.NVarChar,100);
                para[paracount++].Value = hdrmodel.AReamrk != null ? hdrmodel.AReamrk : "NA";
                para[paracount] = new SqlParameter("@EEDAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.EEDAmount;
                para[paracount] = new SqlParameter("@ETAAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ETAAmount;
                para[paracount] = new SqlParameter("@ELocAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ELocAmount;
                para[paracount] = new SqlParameter("@ELodAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ELodAmount;
                para[paracount] = new SqlParameter("@ETotalAmount", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.ETotalAmount;
                para[paracount] = new SqlParameter("@EReamrk", SqlDbType.NVarChar, 100);
                para[paracount++].Value = hdrmodel.EReamrk = hdrmodel.EReamrk != null ? hdrmodel.EReamrk : "NA";
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.status;
                //para[paracount] = new SqlParameter("@DeptCode", SqlDbType.Int);
                //para[paracount++].Value = hdrmodel.DeptCode!=0? hdrmodel.DeptCode:0;
                //para[paracount] = new SqlParameter("@DeptName", SqlDbType.NVarChar, 50);
                //para[paracount++].Value = hdrmodel.DeptName!=null? hdrmodel.DeptName:"NA";
                //para[paracount] = new SqlParameter("@RequisitionDate", SqlDbType.DateTime);
                //if (hdrmodel.RequisitionDate.Year != 0001) { 
                //para[paracount++].Value = hdrmodel.RequisitionDate;
                //}
                //para[paracount] = new SqlParameter("@RequisitionNo", SqlDbType.Int);
                //para[paracount++].Value = hdrmodel.RequisitionNo!=0 ? hdrmodel.RequisitionNo:0;
                //para[paracount] = new SqlParameter("@PreparedEmpNo", SqlDbType.Int);
                //para[paracount++].Value = hdrmodel.PreparedEmpNo!=0? hdrmodel.PreparedEmpNo:0;
                //para[paracount] = new SqlParameter("@PreparedEmpName", SqlDbType.NVarChar, 50);
                //para[paracount++].Value = hdrmodel.PreparedEmpName!=null? hdrmodel.PreparedEmpName:"NA";
                //para[paracount] = new SqlParameter("@RequisitionAmt", SqlDbType.Float);
                //para[paracount++].Value = hdrmodel.RequisitionAmt!=0? hdrmodel.RequisitionAmt:0;
                //para[paracount] = new SqlParameter("@Remark", SqlDbType.NVarChar, 50);
                //para[paracount++].Value = hdrmodel.Remark!=null ? hdrmodel.Remark:"NA";

                using (SQLHelper sql = new SQLHelper("[BIL].[SetApprovalTADABillGeneration]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetAnPFinalSubmit( int status,List<ApprovalNoteNo> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = status;
                para[paracount] = new SqlParameter("@NoteList", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[BIL].[SetBILAnPFinalSubmit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetDeptWiseEmployeeList(int DeptId, int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [BIL].[getDeptWiseEmployee](" + DeptId + "," + CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTraveelingDetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[BIL].[getTravellingDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetDeductionFormDA(TADABillGeneration hdrmodel, ref string pMsg)
        {
            try
            {

                int paracount = 0;
                SqlParameter[] para = new SqlParameter[10];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = hdrmodel.NoteNumber;
                para[paracount] = new SqlParameter("@DeptCode", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.DeptCode != 0 ? hdrmodel.DeptCode : 0;
                para[paracount] = new SqlParameter("@DeptName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = hdrmodel.DeptName != null ? hdrmodel.DeptName : "NA";
                para[paracount] = new SqlParameter("@RequisitionDate", SqlDbType.DateTime);
                if (hdrmodel.RequisitionDate.Year != 0001)
                {
                    para[paracount++].Value = hdrmodel.RequisitionDate;
                }
                para[paracount] = new SqlParameter("@RequisitionNo", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.RequisitionNo != 0 ? hdrmodel.RequisitionNo : 0;
                para[paracount] = new SqlParameter("@PreparedEmpNo", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.PreparedEmpNo != 0 ? hdrmodel.PreparedEmpNo : 0;
                para[paracount] = new SqlParameter("@PreparedEmpName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = hdrmodel.PreparedEmpName != null ? hdrmodel.PreparedEmpName : "NA";
                para[paracount] = new SqlParameter("@RequisitionAmt", SqlDbType.Float);
                para[paracount++].Value = hdrmodel.RequisitionAmt != 0 ? hdrmodel.RequisitionAmt : 0;
                para[paracount] = new SqlParameter("@Remark", SqlDbType.NVarChar, 50);
                para[paracount++].Value = hdrmodel.Remark != null ? hdrmodel.Remark : "NA";
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.status;
                using (SQLHelper sql = new SQLHelper("[BIL].[SetDeductionFormDA]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable GetTADACalculationDateWiseForReport(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@EmployeeNumber", SqlDbType.Int);
                para[paracount++].Value = EmployeeNumber;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NVarChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[BIL].[getTADACalculationDateWiseForReport]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

    }
}
