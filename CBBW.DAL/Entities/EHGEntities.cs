using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{    
    public class EHGEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        EHGDataSync _datasync;
        EHGDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public EHGEntities()
        {
            _datasync = new EHGDataSync();
            _DBMapper = new EHGDBMapper();
            _DBResponseMapper = new DBResponseMapper();
        }
        public bool SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtlsForManagement dtl, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGHdrForManagement(header, dtl, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetEHGTravellingPersonDetails(string NoteNumber, string AuthEmp, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGTravellingPersonDetails(NoteNumber,AuthEmp, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetDateWiseTourDetails(NoteNumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetEHGVehicleAllotmentDetails(VehicleAllotmentDetails mData, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGVehicleAllotmentDetails(mData, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool UpdateEHGHdr(EHGHeader header, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.UpdateEHGHdr(header, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<DateWiseTourDetails> getDateWiseTourDetails(string Notenumber, int IsActive, ref string pMsg) 
        {
            List<DateWiseTourDetails> result = new List<DateWiseTourDetails>();
            try
            {
                dt = _datasync.getDateWiseTourDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_DateWiseTourDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EHGTravelingPersondtlsForManagement> getTravelingPersonDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            List<EHGTravelingPersondtlsForManagement> result = new List<EHGTravelingPersondtlsForManagement>();
            try
            {
                dt = _datasync.getTravelingPersonDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EHGTravelingPersondtlsForManagement(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public VehicleAllotmentDetails getVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            VehicleAllotmentDetails result = new VehicleAllotmentDetails();
            try
            {
                dt = _datasync.getVehicleAllotmentDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_VehicleAllotmentDetails(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public EHGHeader getEHGNoteHdr(string Notenumber, ref string pMsg,int isLocked=0)
        {
            EHGHeader result = new EHGHeader();
            try
            {
                dt = _datasync.getEHGNoteHdr(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_EHGHeader(dt.Rows[0]);
                    }
                }
                if (isLocked == 1) { _datasync.LockEHGHdr(Notenumber, ref pMsg); }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool RemoveEHGNote(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveEHGNote(NoteNumber, RemoveTag, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<EHGNoteList> GetEHGNoteList(int DisplayLength, int DisplayStart, 
            int SortColumn, string SortDirection, string SearchText, int CentreCode,
            bool IsApprovedList,ref string pMsg)
        {
            List<EHGNoteList> result = new List<EHGNoteList>();
            try
            {
                dt = _datasync.GetEHGNoteList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText,CentreCode, IsApprovedList,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EHGNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EHGNote> getNoteListToBeApproved(int CentreCode,ref string pMsg)
        {
            List<EHGNote> result = new List<EHGNote>();
            try
            {
                dt = _datasync.getNoteListToBeApproved(CentreCode,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EHGNote x = new EHGNote();
                        if (!DBNull.Value.Equals(dt.Rows[i]["NoteNumber"]))
                        {
                            x.NoteNumber = dt.Rows[i]["NoteNumber"].ToString();
                        }
                        result.Add(x);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetEHGHdrAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval,
            int ApproverID, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGHdrAppStatus(NoteNumber,IsApproved,ReasonForDisApproval,ApproverID, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<CustomComboOptions> getDriverListForOfficeWork(string Notenumber, ref string pMsg) 
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getDriverListForOfficeWork(Notenumber,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptionsForDrivers(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
    }
}
