using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.ETSEdit;

namespace CBBW.DAL.DataSync
{
    public class ETSEditDataSync
    {
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
        public DataSet getCurrentDateWiseTour(string NoteNumber,int FieldTag, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@FieldTag", SqlDbType.Int);
                para[paracount++].Value = FieldTag;
                using (SQLHelper sql = new SQLHelper("[ETS].[getCurrentDateWiseTour]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetETSTourEdit(DWTTourDetailsForDB obj, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(obj.DWTDetails);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[10];
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
                para[paracount] = new SqlParameter("@DateWiseTourDtls", SqlDbType.Structured);
                para[paracount++].Value = dtl.UDTable;
                using (SQLHelper sql = new SQLHelper("[ETS].[SetETSTourEdit]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

    }
}
