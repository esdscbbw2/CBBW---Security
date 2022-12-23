using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;

namespace CBBW.DAL.DataSync
{
    public class EHGDataSync
    {
        public DataTable SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtls dtl, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[24];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = header.NoteNumber;
                para[paracount] = new SqlParameter("@EntryDate", SqlDbType.Date);
                para[paracount++].Value = header.EntryDate;
                para[paracount] = new SqlParameter("@EntryTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = header.EntryTime;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = header.CenterCode;
                para[paracount] = new SqlParameter("@CenterName", SqlDbType.NVarChar,50);
                para[paracount++].Value = header.CenterName;
                para[paracount] = new SqlParameter("@VehicleType", SqlDbType.Int);
                para[paracount++].Value = header.VehicleType;
                para[paracount] = new SqlParameter("@MaterialStatus", SqlDbType.Bit);
                para[paracount++].Value = header.MaterialStatus;
                para[paracount] = new SqlParameter("@Initiator", SqlDbType.Int);
                para[paracount++].Value = header.Initiator;
                para[paracount] = new SqlParameter("@Instructor", SqlDbType.Int);
                para[paracount++].Value = header.Instructor;
                para[paracount] = new SqlParameter("@AuthorisedEmpNo", SqlDbType.Int);
                para[paracount++].Value = header.AuthorisedEmpNo;
                para[paracount] = new SqlParameter("@PurposeOfAllotment", SqlDbType.Int);
                para[paracount++].Value = header.PurposeOfAllotment;
                para[paracount] = new SqlParameter("@AuthorisedEmpName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = header.AuthorisedEmployeeName;

                para[paracount] = new SqlParameter("@EmployeeNo", SqlDbType.Int);
                para[paracount++].Value = dtl.EmployeeNo;
                para[paracount] = new SqlParameter("@EmployeeNonName", SqlDbType.NVarChar,150);
                para[paracount++].Value = dtl.EmployeeNonName;
                para[paracount] = new SqlParameter("@DesignationCode", SqlDbType.Int);
                para[paracount++].Value = dtl.DesignationCode;
                para[paracount] = new SqlParameter("@DesignationCodenName", SqlDbType.NVarChar, 150);
                para[paracount++].Value = dtl.DesignationCodenName;
                para[paracount] = new SqlParameter("@FromDate", SqlDbType.Date);
                para[paracount++].Value = dtl.FromDate;
                para[paracount] = new SqlParameter("@FromTime", SqlDbType.NVarChar,15);
                para[paracount++].Value = dtl.FromTime;
                para[paracount] = new SqlParameter("@ToDate", SqlDbType.Date);
                para[paracount++].Value = dtl.ToDate;
                para[paracount] = new SqlParameter("@PurposeOfVisit", SqlDbType.NVarChar);
                para[paracount++].Value = dtl.PurposeOfVisit;
                para[paracount] = new SqlParameter("@TADADenied", SqlDbType.Bit);
                para[paracount++].Value = dtl.TADADenied;

                para[paracount] = new SqlParameter("@InstructorName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = header.InstructorName;
                para[paracount] = new SqlParameter("@InitiatorName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = header.InitiatorCodenName;
                para[paracount] = new SqlParameter("@ImageFile", SqlDbType.NVarChar);
                para[paracount++].Value = string.IsNullOrEmpty(header.DocFileName)?" ": header.DocFileName;
                using (SQLHelper sql = new SQLHelper("[EHG].[SetEHGHdrForManagement]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEHGTravellingPersonDetails(string NoteNumber,string AuthEmp, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata, AuthEmp);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@TravellingPersonDtl", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[EHG].[SetEHGTravellingPersonDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@DateWiseTourDtl", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[EHG].[SetDateWiseTourDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEHGVehicleAllotmentDetails(VehicleAllotmentDetails mData, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[14];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = mData.NoteNumber;
                para[paracount] = new SqlParameter("@AuthorisedEmpNumber", SqlDbType.Int);
                para[paracount++].Value =mData.AuthorisedEmpNumber;
                para[paracount] = new SqlParameter("@AuthorisedEmpName", SqlDbType.NVarChar,50);
                para[paracount++].Value = mData.AuthorisedEmpName;
                para[paracount] = new SqlParameter("@DesignationCode", SqlDbType.Int);
                para[paracount++].Value = mData.DesignationCode;
                para[paracount] = new SqlParameter("@DesignationText", SqlDbType.NVarChar,50);
                para[paracount++].Value = mData.DesignationText;
                para[paracount] = new SqlParameter("@MaterialStatus", SqlDbType.Bit);
                para[paracount++].Value = mData.MaterialStatus;
                para[paracount] = new SqlParameter("@VehicleBelongsTo", SqlDbType.Int);
                para[paracount++].Value = mData.VehicleBelongsTo;
                para[paracount] = new SqlParameter("@VehicleType", SqlDbType.NVarChar,50);
                para[paracount++].Value = mData.VehicleType;
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.NVarChar,20);
                para[paracount++].Value = mData.VehicleNumber;
                para[paracount] = new SqlParameter("@ModelName", SqlDbType.NVarChar,50);
                para[paracount++].Value = mData.ModelName;
                para[paracount] = new SqlParameter("@DriverNumber", SqlDbType.Int);
                para[paracount++].Value = mData.DriverNumber;
                para[paracount] = new SqlParameter("@DriverName", SqlDbType.NVarChar,50);
                para[paracount++].Value = mData.DriverName;
                para[paracount] = new SqlParameter("@KMOut", SqlDbType.Int);
                para[paracount++].Value = mData.KMOut;
                para[paracount] = new SqlParameter("@KMIn", SqlDbType.Int);
                para[paracount++].Value = mData.KMIn;
                using (SQLHelper sql = new SQLHelper("[EHG].[SetEHGVehicleAllotmentDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable UpdateEHGHdr(EHGHeader header, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[15];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value =header.NoteNumber;
                para[paracount] = new SqlParameter("@EntryDate", SqlDbType.Date);
                para[paracount++].Value = header.EntryDate;
                para[paracount] = new SqlParameter("@EntryTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = header.EntryTime;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = header.CenterCode;
                para[paracount] = new SqlParameter("@CenterName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = header.CenterName;
                para[paracount] = new SqlParameter("@VehicleType", SqlDbType.Int);
                para[paracount++].Value = header.VehicleType;
                para[paracount] = new SqlParameter("@MaterialStatus", SqlDbType.Bit);
                para[paracount++].Value = header.MaterialStatus;
                para[paracount] = new SqlParameter("@Initiator", SqlDbType.Int);
                para[paracount++].Value = header.Initiator;
                para[paracount] = new SqlParameter("@Instructor", SqlDbType.Int);
                para[paracount++].Value = header.Instructor;
                para[paracount] = new SqlParameter("@AuthorisedEmpNo", SqlDbType.Int);
                para[paracount++].Value = header.AuthorisedEmpNo;
                para[paracount] = new SqlParameter("@PurposeOfAllotment", SqlDbType.Int);
                para[paracount++].Value = header.PurposeOfAllotment;
                para[paracount] = new SqlParameter("@AuthorisedEmpName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = header.AuthorisedEmployeeName;
                para[paracount] = new SqlParameter("@InstructorName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = header.InstructorName;
                para[paracount] = new SqlParameter("@InitiatorName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = header.InitiatorCodenName;
                para[paracount] = new SqlParameter("@ImageFile", SqlDbType.NVarChar);
                para[paracount++].Value = string.IsNullOrEmpty(header.DocFileName) ? " " : header.DocFileName;
                using (SQLHelper sql = new SQLHelper("[EHG].[UpdateEHGHdr]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveEHGNote(string NoteNumber,int RemoveTag,int ActiveTag, ref string pMsg)
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
                using (SQLHelper sql = new SQLHelper("[EHG].[RemoveEHGNote]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getDateWiseTourDetails(string Notenumber,int IsActive,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EHG].[getDateWiseTourDetails]('"+ Notenumber + "',"+IsActive+")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getEHGNoteHdr(string Notenumber, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EHG].[getEHGNoteHdr]('" + Notenumber + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getTravelingPersonDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EHG].[getTravelingPersonDetails]('" + Notenumber + "'," + IsActive + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EHG].[getVehicleAllotmentDetails]('" + Notenumber + "'," + IsActive + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEHGNoteList(int DisplayLength,int DisplayStart,int SortColumn,
            string SortDirection,string SearchText,int CentreCode,bool IsApprovedList, ref string pMsg) 
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
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar,1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar,250);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@IsApprovedList", SqlDbType.Bit);
                para[paracount++].Value = IsApprovedList;
                using (SQLHelper sql = new SQLHelper("[EHG].[GetEHGNoteList]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getNoteListToBeApproved(int CentreCode,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [EHG].[getNoteListToBeApproved]("+ CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEHGHdrAppStatus(string NoteNumber,bool IsApproved,string ReasonForDisApproval,
            int ApproverID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@EntryDateTime", SqlDbType.DateTime);
                para[paracount++].Value = DateTime.Now;
                para[paracount] = new SqlParameter("@IsApproved", SqlDbType.Bit);
                para[paracount++].Value = IsApproved;
                para[paracount] = new SqlParameter("@ReasonForDisApproval", SqlDbType.NVarChar);
                para[paracount++].Value = ReasonForDisApproval;
                para[paracount] = new SqlParameter("@ApproverID", SqlDbType.Int);
                para[paracount++].Value = ApproverID;
                using (SQLHelper sql = new SQLHelper("[EHG].[SetEHGHdrAppStatus]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
