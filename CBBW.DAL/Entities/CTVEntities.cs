using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class CTVEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        CTVDataSync _datasync;
        CTVDBMapper _CTVDBMapper;
        DBResponseMapper _DBResponseMapper;
        public CTVEntities()
        {
            _datasync = new CTVDataSync();
            _CTVDBMapper = new CTVDBMapper();
            _DBResponseMapper = new DBResponseMapper();
        }
        public string getNewCTVNoteNo(string SchPattern, ref string pMsg) 
        {
            string noteno = string.Empty;
            try
            {
                dt = _datasync.getNewCTVNoteNo(SchPattern, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["NewNoteNo"]))
                        noteno = dt.Rows[0]["NewNoteNo"].ToString();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return noteno;
        }
        public VehicleInfo getVehicleInfo(string VehicleNo, ref string pMsg)
        {
            VehicleInfo result = new VehicleInfo();
            try
            {
                dt = _datasync.getVehicleInfo(VehicleNo,ref pMsg);
                if (dt != null && dt.Rows.Count>0)
                {
                    result = _CTVDBMapper.Map_VehicleInfo(dt.Rows[0]);
                }
            }
            catch(Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<VehicleNo> getLCVMCVVehicleList(ref string pMsg)
        {
            List<VehicleNo> result = new List<VehicleNo>();
            try
            {
                dt = _datasync.getLCVMCVVehicles(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        VehicleNo x = new VehicleNo();
                        if (!DBNull.Value.Equals(dt.Rows[i]["VehicleNumber"])) 
                        {
                            x.VehicleNumber = dt.Rows[i]["VehicleNumber"].ToString();
                            //x.VehicleID = dt.Rows[i]["VehicleNumber"].ToString();
                        }                            
                        result.Add(x);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public UserInfo getLogInUserInfo(string UserName, ref string pMsg) 
        {
            UserInfo result = new UserInfo();
            try
            {
                dt = _datasync.getUserInfo(UserName,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _CTVDBMapper.Map_UserInfo(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool CreateCTVHdr(TripScheduleHdr model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setCTVHeader(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool InsertOthTripSchDtl(string Notenumber, string TripPurpose, List<OthTripTemp> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setOthTripSchDtls(Notenumber, TripPurpose, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool RemoveNote(string NoteNumber,int OnlyDtl, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveNote(NoteNumber, OnlyDtl, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<LocVehSchFromMat> getLocalVehicleSchedule(string VehicleNo, DateTime FromDate, DateTime ToDate, ref string pMsg) 
        {
            List<LocVehSchFromMat> result = new List<LocVehSchFromMat>();
            try
            {
                dt = _datasync.getLVTSFromMat(VehicleNo, FromDate, ToDate, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!DBNull.Value.Equals(dt.Rows[i]["VehicleNumber"]))
                        {
                            result.Add(_CTVDBMapper.Map_LocVehSchFromMat(dt.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool CheckAvailibiltyofSchDate(string VehicleNo, DateTime ScheduleDate, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.CheckAvailibiltyofSchDate(VehicleNo, ScheduleDate, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public CTVHdrDtl getCTVSchDetailsFromNote(string NoteNumber, ref string pMsg) 
        {
            CTVHdrDtl result = new CTVHdrDtl();
            try
            {
                ds = _datasync.getCTVSchDetailsFromNote(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null; DataRow hdr = null;
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        dtl = ds.Tables[1];
                    }
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        hdr = ds.Tables[0].Rows[0];
                    }
                    return _CTVDBMapper.Map_CTVHdrDtl(hdr, dtl);
                }
            }
            catch (Exception ex){pMsg = ex.Message;}

            return result;
        }
        public VehicleAvblInfo getVehicleSlot(string VehicleNo, int IncludeOTVSch, ref string pMsg) 
        {
            VehicleAvblInfo result = new VehicleAvblInfo();
            try
            {
                ds = _datasync.getVehicleSlotVacency(VehicleNo, IncludeOTVSch, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null; DataRow hdr = null;
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        dtl = ds.Tables[1];
                    }
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        hdr = ds.Tables[0].Rows[0];
                    }
                    return _CTVDBMapper.Map_VehicleAvblInfo(hdr, dtl);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }

            return result;
        }
    }
}
