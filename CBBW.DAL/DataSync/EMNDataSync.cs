using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.EMN;

namespace CBBW.DAL.DataSync
{
    public class EMNDataSync
    {
        public DataTable GetEMNNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[EMN].[getEMNNZBDetailsforListPage]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable getCenterCodeList(int center, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EMN].[getCenterCodeList]('" + center + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable getCenterCodeListFromTravellingPerson(string NoteNumber,int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EMN].[getCenterCodeListFromTravellingPerson]('" + NoteNumber + "'," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable SetEMNTravellingPerson(string NoteNumber,int CenterCode,string CenterCodeName, List<EMNTravellingPerson> dtldata, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[EMN].[SetEMNTravellingPerson]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable setEMNTravDetailsNTourDetails(string NoteNumber, List<EMNTravellingDetails> TDdata, List<EMNDateWiseTour> DWTdata, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[EMN].[SetEMNTravDetailsNTourDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable GetEMNTravellingPerson(string NoteNumber,int CenterCode,int status, ref string pMsg)
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

                using (SQLHelper sql = new SQLHelper("[EMN].[getEMNTravellingPerson]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable SetEMNDetailsFinalSubmit(EMNHeader hdrmodel, ref string pMsg)
        {  
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = hdrmodel.NoteNumber;
                para[paracount] = new SqlParameter("@CenterCodeName", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.CenterCodeName;
                para[paracount] = new SqlParameter("@AttachFile", SqlDbType.VarChar);
                para[paracount++].Value = hdrmodel.AttachFile;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = hdrmodel.Status;
                using (SQLHelper sql = new SQLHelper("[EMN].[SetEMNDetailsFinalSubmit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable GetEMNHdrEntry(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                using (SQLHelper sql = new SQLHelper("[EMN].[getEMNHdrEntry]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable RemoveEMNNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[EMN].[RemoveEMNNoteNumber]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable GetEMNTravellingDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[EMN].[getEMNTravellingDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable GetEMNDateWiseTour(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[EMN].[getEMNDateWiseTour]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable GetEMNNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EMN].[getEMNNoteListToBeApproved](" + CentreCode + "," + status + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable SetEMNApprovalData(EMNApproveTravDetails model, ref string pMsg)
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

                using (SQLHelper sql = new SQLHelper("[EMN].[SetEMNApprovalData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
        public DataTable SetEMNRatifiedData(EMNRatified model, ref string pMsg)
        {
            try
            {

                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = model.NoteNumber;
                para[paracount] = new SqlParameter("@IsRatified", SqlDbType.Bit);
                para[paracount++].Value = model.IsRatified;
                para[paracount] = new SqlParameter("@RatifiedReason", SqlDbType.NChar, 250);
                para[paracount++].Value = model.RatifiedReason;
                para[paracount] = new SqlParameter("@status", SqlDbType.Int);
                para[paracount++].Value = model.status;

                using (SQLHelper sql = new SQLHelper("[EMN].[SetEMNRatifiedData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
                pMsg = ex.Message;
                return null;
            }
        }
    }
}
