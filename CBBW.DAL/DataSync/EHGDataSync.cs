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
                SqlParameter[] para = new SqlParameter[21];
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
                using (SQLHelper sql = new SQLHelper("[EHG].[SetEHGHdrForManagement]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable SetEHGTravellingPersonDetails(string NoteNumber, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
            try
            {
                CommonTable dtl = new CommonTable(dtldata);
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
    }
}
