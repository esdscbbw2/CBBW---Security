using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CTV2;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
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
        #region For CTV2
        public List<CTVNoteList4DT> GetNoteListForDataTable(int DisplayLength, int DisplayStart, int SortCol, string SortDirection,
            string SearchText, int CentreCode, bool IsApproved, ref string pMsg)
        {
            List<CTVNoteList4DT> result = new List<CTVNoteList4DT>();
            try
            {
                dt = _datasync.GetNoteListForDataTable(DisplayLength,DisplayStart,SortCol,SortDirection,SearchText,CentreCode,IsApproved, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_CTVDBMapper.Map_CTVNoteList4DT(dt.Rows[i]));                        
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public CTVSlots GetSlots(string VehicleNo, int IncludeOTVSch, ref string pMsg) 
        {
            CTVSlots result = new CTVSlots();
            try
            {
                ds = _datasync.getVehicleSlotVacency(VehicleNo, IncludeOTVSch, ref pMsg);
                if (ds != null)
                {
                    DataTable bookedslots = null; DataTable avblslots = null; DataRow hdr = null;
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        avblslots = ds.Tables[2];
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        bookedslots = ds.Tables[1];
                    }                    
                    return _CTVDBMapper.Map_CTVSlots(bookedslots, avblslots);
                }
            }
            catch (Exception ex) { pMsg = ex.Message;return null; }
            return result;
        }
        public DateTime GetToDate(DateTime FromDate, int FromCentre, string ToCentre, ref string pMsg) 
        {
            return _datasync.GetToDate(FromDate, FromCentre, ToCentre, ref pMsg);
        }
        public bool SetCTVOtherTrip(CTVOtherTrip data, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetCTVOtherTrip(data, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public CTVOtherTrip GetOthTripSchEntryData(string NoteNumber,ref string pMsg) 
        {
            CTVOtherTrip result = new CTVOtherTrip();
            List<CTVOtherTripDtls> data = new List<CTVOtherTripDtls>();
            try
            {
                dt = _datasync.GetOthTripSchEntryData(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        data.Add(_CTVDBMapper.Map_CTVOtherTripDtls(dt.Rows[i]));
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["TripPurpose"]))
                        result.TripPurpose = dt.Rows[0]["TripPurpose"].ToString();
                    if (!DBNull.Value.Equals(dt.Rows[0]["IsOtherPlaceStatement"]))
                        result.IsOtherPlaceStatement =bool.Parse(dt.Rows[0]["IsOtherPlaceStatement"].ToString());
                }
                result.NoteNumber = NoteNumber;
                result.TripDetails = data;                
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }



        #endregion For CTV2
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
        public List<VehicleNo> getLCVMCVVehicleList(int CentreCode,ref string pMsg)
        {
            List<VehicleNo> result = new List<VehicleNo>();
            try
            {
                dt = _datasync.getLCVMCVVehicles(CentreCode,ref pMsg);
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
        public bool setCTVApproval(string Notenumber, int EmployeeNumber, bool Isapproved,
            DateTime ApprovalDatetime, string DisApprovalReason, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setCTVApproval(Notenumber,EmployeeNumber,
                Isapproved,ApprovalDatetime,DisApprovalReason, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetCTVEditHdr(string Notenumber, int EmployeeNumber, 
            int ApprovalFor, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetCTVEditHdr(Notenumber,EmployeeNumber,ApprovalFor,ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool InsertOthTripSchDtl(string Notenumber, string TripPurpose, List<OthTripTemp> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setOthTripSchDtls(Notenumber, TripPurpose, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool EditOthTripSchDtl(string Notenumber, string TripPurpose, List<OthTripTemp> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setEditOtherTripSchDtls(Notenumber, TripPurpose, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool setLocalTripSchDtls(string Notenumber, List<LocVehSchFromMat> dtldata, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setLocalTripSchDtls(Notenumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool setLocalTripSchDriver(string Notenumber, List<LTSDriVerChange> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setLocalTripSchDriver(Notenumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool RemoveNote(string NoteNumber,int OnlyDtl, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveNote(NoteNumber, OnlyDtl, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<LocVehSchFromMat>getLocalVehicleSchedule(string VehicleNo, DateTime FromDate, DateTime ToDate, ref string pMsg) 
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
        //public bool CheckAvailibiltyofSchDate(string VehicleNo, DateTime ScheduleDate, ref string pMsg) 
        //{
        //    bool result = false;
        //    _DBResponseMapper.Map_DBResponse(_datasync.CheckAvailibiltyofSchDate(VehicleNo, ScheduleDate, ref pMsg), ref pMsg, ref result);
        //    return result;
        //}
        public CTVHdrDtl getCTVSchDetailsFromNote(string NoteNumber, ref string pMsg) 
        {
            CTVHdrDtl result = new CTVHdrDtl();
            try
            {
                ds = _datasync.getCTVSchDetailsFromNote(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable hdrdtl = null; DataTable dtl = null; DataRow hdr = null;
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        hdrdtl = ds.Tables[2];
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        dtl = ds.Tables[1];
                    }
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        hdr = ds.Tables[0].Rows[0];
                    }
                    return _CTVDBMapper.Map_CTVHdrDtl(hdr, dtl, hdrdtl);
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
                    DataTable bookedslots = null; DataTable avblslots = null; DataRow hdr = null;
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        avblslots = ds.Tables[2];
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        bookedslots = ds.Tables[1];
                    }
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        hdr = ds.Tables[0].Rows[0];
                    }
                    return _CTVDBMapper.Map_VehicleAvblInfo(hdr, bookedslots, avblslots);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<NoteNumber> getNotenumbersTobeApproved(int EmpNo,int CenterCode,ref string pMsg) 
        {
            List<NoteNumber> result = new List<NoteNumber>();
            try
            {
                dt = _datasync.getNoteNumbersTobeApproved(CenterCode,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!DBNull.Value.Equals(dt.Rows[i]["CreateEmployeeNo"])) 
                        {
                            if (EmpNo != int.Parse(dt.Rows[i]["CreateEmployeeNo"].ToString())) 
                            {
                                NoteNumber x = new NoteNumber();
                                if (!DBNull.Value.Equals(dt.Rows[i]["NoteNo"]))
                                {
                                    x.NoteNo = dt.Rows[i]["NoteNo"].ToString();
                                    //x.VehicleID = dt.Rows[i]["VehicleNumber"].ToString();
                                }
                                result.Add(x);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<TripScheduleHdr> getCtvSchedule(int PageSize, int PageNumber, int SortCol, string SortDirection,
            string SearchText, int centercode, ref string pMsg)
        {
            List<TripScheduleHdr> tripSchedule = new List<TripScheduleHdr>();
            try
            {
                dt = _datasync.getCtvSchedule(PageSize, PageNumber, SortCol, SortDirection, SearchText,
                    centercode,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tripSchedule.Add(_CTVDBMapper.Map_CtvSchedule(dt.Rows[i], i + 1));
                    }
                }
            }
            catch(Exception ex) { pMsg = ex.Message; }
            return tripSchedule;
        }
        public IEnumerable<CustomComboOptions> getDriverList(string ExpDriverName,ref string pMsg,int CentreCode=13)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getDriverList(CentreCode,ExpDriverName,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptions(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        
    }
}
