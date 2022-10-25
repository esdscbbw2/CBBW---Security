using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;

namespace CBBW.DAL.DataSync
{
    public class CTVDataSync
    {
        public DataTable getNewCTVNoteNo(string CTVPattern, ref string pMsg)
        {
            try
            {                
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNoPattern", SqlDbType.NChar,20);
                para[paracount++].Value = CTVPattern;
                
                using (SQLHelper sql = new SQLHelper("[CTV].[NewVehicleTripSchedule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getVehicleInfo(string VehicleNo, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.VarChar,20);
                para[paracount++].Value = VehicleNo;

                using (SQLHelper sql = new SQLHelper("[CTV].[getVehicleValData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getLCVMCVVehicles(ref string pMsg) 
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getListofVehicles]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getUserInfo(string UserName, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                para[paracount++].Value = UserName;

                using (SQLHelper sql = new SQLHelper("[CTV].[getLogInUserInfo]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getLVTSFromMat(string VehicleNo,DateTime FromDate,DateTime ToDate, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.VarChar, 20);
                para[paracount++].Value = VehicleNo;
                para[paracount] = new SqlParameter("@FromDate", SqlDbType.Date);
                para[paracount++].Value = FromDate;
                para[paracount] = new SqlParameter("@ToDate", SqlDbType.Date);
                para[paracount++].Value = ToDate;

                using (SQLHelper sql = new SQLHelper("[CTV].[LocalVehicleSchedule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable setCTVHeader(TripScheduleHdr model,ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[14];
                para[paracount] = new SqlParameter("@NoteNo", SqlDbType.NChar, 25);
                para[paracount++].Value = model.NoteNo;
                para[paracount] = new SqlParameter("@EntryDate", SqlDbType.Date);
                para[paracount++].Value = model.EntryDate;
                para[paracount] = new SqlParameter("@EntryTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = model.EntryTime;
                para[paracount] = new SqlParameter("@CenterCode", SqlDbType.Int);
                para[paracount++].Value = model.CenterCode;
                para[paracount] = new SqlParameter("@CenterName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = model.CenterName;
                para[paracount] = new SqlParameter("@FortheMonth", SqlDbType.Int);
                para[paracount++].Value = model.FortheMonth;
                para[paracount] = new SqlParameter("@FortheYear", SqlDbType.Int);
                para[paracount++].Value = model.FortheYear;
                para[paracount] = new SqlParameter("@FromDate", SqlDbType.Date);
                para[paracount++].Value = model.FromDate;
                para[paracount] = new SqlParameter("@ToDate", SqlDbType.Date);
                para[paracount++].Value = model.ToDate;
                para[paracount] = new SqlParameter("@Vehicleno", SqlDbType.NVarChar, 20);
                para[paracount++].Value = model.Vehicleno;
                para[paracount] = new SqlParameter("@VehicleType", SqlDbType.NVarChar, 20);
                para[paracount++].Value = model.VehicleType;
                para[paracount] = new SqlParameter("@ModelName", SqlDbType.NVarChar, 20);
                para[paracount++].Value = model.ModelName;
                para[paracount] = new SqlParameter("@DriverNo", SqlDbType.Int);
                para[paracount++].Value = model.DriverNo;
                para[paracount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[paracount++].Value = model.IsActive;

                using (SQLHelper sql = new SQLHelper("[CTV].[SetCTVHdr]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveNote(string NoteNumber, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[CTV].[RemoveCTVNote]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable CheckAvailibiltyofSchDate(string VehicleNo,DateTime ScheduleDate,ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.VarChar, 20);
                para[paracount++].Value = VehicleNo;
                para[paracount] = new SqlParameter("@SchDate", SqlDbType.DateTime);
                para[paracount++].Value = ScheduleDate;
                using (SQLHelper sql = new SQLHelper("[CTV].[getVehicleSchDateValData]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
