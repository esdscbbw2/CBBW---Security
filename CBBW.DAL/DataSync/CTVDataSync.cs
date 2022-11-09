﻿using System;
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
        public DataSet getVehicleSlotVacency(string VehicleNo,int IncludeOTVSch, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@VehicleNumber", SqlDbType.VarChar, 20);
                para[paracount++].Value = VehicleNo;
                para[paracount] = new SqlParameter("@IncludeOTVSch", SqlDbType.Int);
                para[paracount++].Value = IncludeOTVSch;

                using (SQLHelper sql = new SQLHelper("[CTV].[getSlotVacency]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
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

                using (SQLHelper sql = new SQLHelper("[CTV].[LocalVehicleScheduleV2]", CommandType.StoredProcedure))
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
                //para[paracount] = new SqlParameter("@VehicleType", SqlDbType.NVarChar, 20);
                //para[paracount++].Value = model.VehicleType;
                //para[paracount] = new SqlParameter("@ModelName", SqlDbType.NVarChar, 20);
                //para[paracount++].Value = model.ModelName;
                para[paracount] = new SqlParameter("@DriverNo", SqlDbType.Int);
                para[paracount++].Value = model.DriverNo;
                para[paracount] = new SqlParameter("@DriverName", SqlDbType.NVarChar, 50);
                para[paracount++].Value = model.VehicleType;
                para[paracount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[paracount++].Value = model.IsActive;
                para[paracount] = new SqlParameter("@Employeenumber", SqlDbType.Int);
                para[paracount++].Value = model.EmployeeNumber;
                using (SQLHelper sql = new SQLHelper("[CTV].[SetCTVHdr]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable setOthTripSchDtls(string Notenumber,string TripPurpose, List<OthTripTemp> dtldata, ref string pMsg) 
        {
            try
            {
                CommonTable schdtlData = new CommonTable(dtldata);                
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@NoteNo", SqlDbType.NChar,25);
                para[paracount++].Value = Notenumber;
                para[paracount] = new SqlParameter("@TripPurpose", SqlDbType.NVarChar);
                para[paracount++].Value = TripPurpose;
                para[paracount] = new SqlParameter("@TripDtl", SqlDbType.Structured);
                para[paracount++].Value = schdtlData.UDTable;
                
                using (SQLHelper sql = new SQLHelper("[CTV].[SetOtherTripSch]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveNote(string NoteNumber,int OnlyDtl, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.VarChar, 25);
                para[paracount++].Value = NoteNumber;
                para[paracount] = new SqlParameter("@OnlyDtl", SqlDbType.Int);
                para[paracount++].Value = OnlyDtl;

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
        public DataSet getCTVSchDetailsFromNote(string NoteNumber, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[CTV].[getOVTSchDetails]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getNoteNumbersTobeApproved(int CenterCode,ref string pMsg) 
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [CTV].[getNoteNumbersTobeApproved]("+ CenterCode + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getCTVSchHdrFromNote(string NoteNumber, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@NoteNumber", SqlDbType.NChar, 25);
                para[paracount++].Value = NoteNumber;

                using (SQLHelper sql = new SQLHelper("[CTV].[getOVTSchHdrFromNote]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        ///From punu's project
        public DataTable getCtvSchedule(int centercode, ref string pMsg)
        {

            TripScheduleHdr tr = new TripScheduleHdr();
            // IEnumerable<TripScheduleHdr> model = _ctvRepo.getCtvSchedule(x, ref pMsg);
            // int x = tr.TotalCount;

            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[6];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = 100000;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = 0;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = 3;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar);
                para[paracount++].Value = "dsc";
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar);
                para[paracount++].Value = "";
                para[paracount] = new SqlParameter("@centrecode", SqlDbType.NVarChar);
                para[paracount++].Value = centercode;
                using (SQLHelper sql = new SQLHelper("[CTV].[spGetVehicleTripSchedule1]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
                //using (SQLHelper sql = new SQLHelper("Select * from [CTV].[getSchedules]()", CommandType.Text))
                //{
                //   return sql.GetDataTable( ref pMsg);
                //}

            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        ///From Punus project - end
    }
}
