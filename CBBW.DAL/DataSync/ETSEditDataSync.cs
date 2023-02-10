using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;

namespace CBBW.DAL.DataSync
{
    public class ETSEditDataSync
    {
        public DataTable GetETSEditNoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, int IsApprovedList, ref string pMsg)
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
                para[paracount] = new SqlParameter("@IsApprovedList", SqlDbType.Int);
                para[paracount++].Value = IsApprovedList;
                using (SQLHelper sql = new SQLHelper("[ETS].[GetETSEditNoteList]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetEntryINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode,ref string pMsg)
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
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 250);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                using (SQLHelper sql = new SQLHelper("[ETS].[GetEntryINoteList]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getEditSL(string NoteNumber, ref string pMsg)
        {
            try
            {                
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;                
                using (SQLHelper sql = new SQLHelper("[ETS].[EditSL]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getETSEditHdr(string NoteNumber,int LockStatus, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@LockStatus", SqlDbType.Int);
                para[paracount++].Value = LockStatus;
                using (SQLHelper sql = new SQLHelper("[ETS].[getETSEditHdr]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetNoteHdrForEntryI(string NoteNumber, int LockStatus, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@LockStatus", SqlDbType.Int);
                para[paracount++].Value = LockStatus;
                para[paracount] = new SqlParameter("@EntryDateTag", SqlDbType.Int);
                para[paracount++].Value = 2;
                using (SQLHelper sql = new SQLHelper("[ETS].[getETSEditHdr]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getETSNoteListToBeEdited(int CentreCode,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getETSNoteListToBeEdited]("+ CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetNoteListForEntryI(int CentreCode, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getNoteListForEntryI](" + CentreCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getETSEditNoteListForDropDown(int CentreCode,int mStatus, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getETSEditNoteListForDropDown](" + CentreCode + ","+ mStatus + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getEditNoteHdr(string NoteNumber, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getEditNoteHdr]('" + NoteNumber + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getEditTPDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getEditTPDetails]('" + NoteNumber + "')", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getCurrentDateWiseTour(string NoteNumber,int FieldTag, 
            int PersonType,int PersonID,string PersonName, ref string pMsg,bool IsActive)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[6];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@FieldTag", SqlDbType.Int);
                para[paracount++].Value = FieldTag;
                para[paracount] = new SqlParameter("@PersonType", SqlDbType.Int);
                para[paracount++].Value = PersonType;
                para[paracount] = new SqlParameter("@PersonID", SqlDbType.Int);
                para[paracount++].Value = PersonID;
                para[paracount] = new SqlParameter("@PersonName", SqlDbType.NVarChar,100);
                para[paracount++].Value = PersonName;
                para[paracount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[paracount++].Value = true;
                if (IsActive) 
                {
                    using (SQLHelper sql = new SQLHelper("[ETS].[getCurrentDateWiseTourV3]", CommandType.StoredProcedure))
                    {
                        return sql.GetDataSet(para, ref pMsg);
                    }
                } 
                else 
                {
                    using (SQLHelper sql = new SQLHelper("[ETS].[getCurrentDateWiseTour]", CommandType.StoredProcedure))
                    {
                        return sql.GetDataSet(para, ref pMsg);
                    }
                }                
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetETSTourEdit(DWTTourDetailsForDB obj,int CentreCode,string CentreName, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(obj.DWTDetails);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[12];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = obj.NoteNumber;
                para[paracount] = new SqlParameter("@EditUserID", SqlDbType.Int);
                para[paracount++].Value = obj.UserID;
                para[paracount] = new SqlParameter("@EditUserName", SqlDbType.NVarChar, 100);
                para[paracount++].Value = obj.UserName;
                para[paracount] = new SqlParameter("@EditTag", SqlDbType.Int);
                para[paracount++].Value = obj.EditTag;
                para[paracount] = new SqlParameter("@IsIndividualEdit", SqlDbType.Bit);
                para[paracount++].Value = obj.IsIndividualEdit;
                para[paracount] = new SqlParameter("@ReasonForEdit", SqlDbType.NVarChar);
                para[paracount++].Value = obj.ReasonForEdit;
                para[paracount] = new SqlParameter("@PersonType", SqlDbType.Int);
                para[paracount++].Value = obj.PersonType;
                para[paracount] = new SqlParameter("@PersonID", SqlDbType.Int);
                para[paracount++].Value = obj.PersonID;
                para[paracount] = new SqlParameter("@PersonIDText", SqlDbType.NVarChar);
                para[paracount++].Value = obj.PersonName;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value =CentreCode;
                para[paracount] = new SqlParameter("@CentreCodeName", SqlDbType.NVarChar,100);
                para[paracount++].Value = CentreName;
                para[paracount] = new SqlParameter("@DateWiseTourDtls", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[ETS].[SetETSTourEdit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable UpdateETSTourEdit(string NoteNumber,ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;                
                using (SQLHelper sql = new SQLHelper("[ETS].[UpdateETSTourEdit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveETSEditNote(string NoteNumber,int ActiveTag, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@ActiveTag", SqlDbType.Int);
                para[paracount++].Value = ActiveTag;
                using (SQLHelper sql = new SQLHelper("[ETS].[RemoveETSEditNote]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetETSEditRatificationStatus(string NoteNumber, bool IsApproved,string ReasonForDisApproval,int ApproverID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@IsApproved", SqlDbType.Bit);
                para[paracount++].Value = IsApproved;
                para[paracount] = new SqlParameter("@ReasonForDisApproval", SqlDbType.NVarChar);
                para[paracount++].Value = string.IsNullOrEmpty(ReasonForDisApproval) ? " " : ReasonForDisApproval;
                para[paracount] = new SqlParameter("@ApproverID", SqlDbType.Int);
                para[paracount++].Value = ApproverID;
                using (SQLHelper sql = new SQLHelper("[ETS].[SetETSEditRatificationStatus]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetETSEditAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval, int ApproverID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[4];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@IsApproved", SqlDbType.Bit);
                para[paracount++].Value = IsApproved;
                para[paracount] = new SqlParameter("@ReasonForDisApproval", SqlDbType.NVarChar);
                para[paracount++].Value = string.IsNullOrEmpty(ReasonForDisApproval)?" ": ReasonForDisApproval;
                para[paracount] = new SqlParameter("@ApproverID", SqlDbType.Int);
                para[paracount++].Value = ApproverID;
                using (SQLHelper sql = new SQLHelper("[ETS].[SetETSEditAppStatus]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetETSVehicleAllotmentDetails(VehicleAllotmentDetails mData,int CentreCode,string CentreName, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[16];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = mData.NoteNumber;
                para[paracount] = new SqlParameter("@AuthorisedEmpNumber", SqlDbType.Int);
                para[paracount++].Value = mData.AuthorisedEmpNumber;
                para[paracount] = new SqlParameter("@AuthorisedEmpName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = mData.AuthorisedEmpName;
                para[paracount] = new SqlParameter("@DesignationCode", SqlDbType.Int);
                para[paracount++].Value = mData.DesignationCode;
                para[paracount] = new SqlParameter("@DesignationText", SqlDbType.NVarChar, 50);
                para[paracount++].Value = mData.DesignationText;
                para[paracount] = new SqlParameter("@MaterialStatus", SqlDbType.Bit);
                para[paracount++].Value = mData.MaterialStatus;
                para[paracount] = new SqlParameter("@VehicleBelongsTo", SqlDbType.Int);
                para[paracount++].Value = mData.VehicleBelongsTo;
                para[paracount] = new SqlParameter("@VehicleType", SqlDbType.NVarChar, 50);
                para[paracount++].Value = mData.VehicleType;
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.NVarChar, 20);
                para[paracount++].Value = mData.VehicleNumber;
                para[paracount] = new SqlParameter("@ModelName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = mData.ModelName;
                para[paracount] = new SqlParameter("@DriverNumber", SqlDbType.Int);
                para[paracount++].Value = mData.DriverNumber;
                para[paracount] = new SqlParameter("@DriverName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = mData.DriverName;
                para[paracount] = new SqlParameter("@KMOut", SqlDbType.Int);
                para[paracount++].Value = mData.KMOut;
                para[paracount] = new SqlParameter("@KMIn", SqlDbType.Int);
                para[paracount++].Value = mData.KMIn;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@CentreName", SqlDbType.NVarChar,100);
                para[paracount++].Value = CentreName;
                using (SQLHelper sql = new SQLHelper("[ETS].[SetETSVehicleAllotmentDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [ETS].[getVehicleAllotmentDetails]('" + Notenumber + "'," + IsActive + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet GetEntryITourData(string Notenumber, int IsActive, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = Notenumber;                
                para[paracount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[paracount++].Value = IsActive;
                using (SQLHelper sql = new SQLHelper("[ETS].[getEntryITourData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveEntryINote(string NoteNumber, bool ActiveTag, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@ActiveTag", SqlDbType.Bit);
                para[paracount++].Value = ActiveTag;
                using (SQLHelper sql = new SQLHelper("[ETS].[RemoveEntryINote]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable UpdateETSVehicleAllotmentDetails(string NoteNumber, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                using (SQLHelper sql = new SQLHelper("[ETS].[UpdateETSVehicleAllotmentDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
