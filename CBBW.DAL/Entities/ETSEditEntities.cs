using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{    
    public class ETSEditEntities
    {
        DataTable dt;
        DataSet ds;
        ETSEditDataSync _ETSEditDataSync;
        ETSEditDBMapper _ETSEditDBMapper;
        DBResponseMapper _DBResponseMapper;
        public ETSEditEntities()
        {
            _ETSEditDataSync = new ETSEditDataSync();
            _ETSEditDBMapper = new ETSEditDBMapper();
            _DBResponseMapper = new DBResponseMapper();
        }
        public int getEditSL(string NoteNumber, ref string pMsg) 
        {
            int result = 0;
            try
            {
                dt = _ETSEditDataSync.getEditSL(NoteNumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (!DBNull.Value.Equals(dt.Rows[0]["SL"]))
                            result =int.Parse(dt.Rows[0]["SL"].ToString());
                    }
                }                
            }
            catch (Exception ex) { pMsg = ex.Message;return 0; }
            return result;
        }
        public List<EditNoteNumber> GetNoteListForEntryI(int CentreCode, ref string pMsg) 
        {
            List<EditNoteNumber> result = new List<EditNoteNumber>();
            try
            {
                dt = _ETSEditDataSync.GetNoteListForEntryI(CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_ETSEditDBMapper.Map_EditNoteNumber(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EditNoteNumber> getETSNoteListToBeEdited(int CentreCode, ref string pMsg)
        {
            List<EditNoteNumber> result = new List<EditNoteNumber>();
            try
            {
                dt = _ETSEditDataSync.getETSNoteListToBeEdited(CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_ETSEditDBMapper.Map_EditNoteNumber(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EditNoteNumber> getETSEditNoteListForDropDown(int CentreCode, int mStatus, ref string pMsg) 
        {
            List<EditNoteNumber> result = new List<EditNoteNumber>();
            try
            {
                dt = _ETSEditDataSync.getETSEditNoteListForDropDown(CentreCode, mStatus, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_ETSEditDBMapper.Map_EditNoteNumber(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public EditNoteDetails getEditNoteHdr(string NoteNumber, ref string pMsg) 
        {
            try
            {
                dt = _ETSEditDataSync.getEditNoteHdr(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _ETSEditDBMapper.Map_EditNoteDetails(dt.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg =ex.Message; return null; }
        }
        public EditNoteDetails getETSEditHdr(string NoteNumber, int LockStatus, ref string pMsg)
        {
            try
            {
                dt = _ETSEditDataSync.getETSEditHdr(NoteNumber, LockStatus, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _ETSEditDBMapper.Map_EditNoteDetails(dt.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }
        public EditNoteDetails GetNoteHdrForEntryI(string NoteNumber, int LockStatus, ref string pMsg) 
        {
            try
            {
                dt = _ETSEditDataSync.GetNoteHdrForEntryI(NoteNumber, LockStatus, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _ETSEditDBMapper.Map_EditNoteDetails(dt.Rows[0],1);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }
        public List<EditTPDetails> getEditTPDetails(string NoteNumber, ref string pMsg) 
        {
            List<EditTPDetails> result = new List<EditTPDetails>();
            try
            {
                dt = _ETSEditDataSync.getEditTPDetails(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_ETSEditDBMapper.Map_EditTPDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EditDWTDetails> getCurrentDateWiseTour(string NoteNumber, int FieldTag,
            int PersonType, int PersonID, string PersonName, ref string pMsg, bool IsActive)
        {
            List<EditDWTDetails> result = new List<EditDWTDetails>();
            try
            {
                ds = _ETSEditDataSync.getCurrentDateWiseTour(NoteNumber, FieldTag,PersonType,PersonID,PersonName, ref pMsg,IsActive);
                if (ds != null) 
                {
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            result.Add(_ETSEditDBMapper.Map_EditDWTDetails(dt.Rows[i]));
                        }
                    }
                }                
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetETSTourEdit(DWTTourDetailsForDB obj, int CentreCode, string CentreName, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.SetETSTourEdit(obj, CentreCode, CentreName, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool UpdateETSTourEdit(string NoteNumber, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.UpdateETSTourEdit(NoteNumber, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool RemoveETSEditNote(string NoteNumber, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.RemoveETSEditNote(NoteNumber, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<EditNoteList> GetETSEditNoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, int IsApprovedList, ref string pMsg)
        {
            List<EditNoteList> result = new List<EditNoteList>();
            try
            {
                dt = _ETSEditDataSync.GetETSEditNoteList(DisplayLength,DisplayStart,
                    SortColumn,SortDirection,SearchText,CentreCode,IsApprovedList, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_ETSEditDBMapper.Map_EditNoteList(dt.Rows[i]));
                    }
                }                
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EntryINoteList> GetEntryINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, ref string pMsg)
        {
            List<EntryINoteList> result = new List<EntryINoteList>();
            try
            {
                dt = _ETSEditDataSync.GetEntryINoteList(DisplayLength, DisplayStart,
                    SortColumn, SortDirection, SearchText, CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_ETSEditDBMapper.Map_EntryINoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetETSEditRatificationStatus(string NoteNumber, bool IsRatified, string RatReason, int ApproverID, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.SetETSEditRatificationStatus(NoteNumber, IsRatified, RatReason, ApproverID, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetETSEditAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval, int ApproverID, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.SetETSEditAppStatus(NoteNumber, IsApproved, ReasonForDisApproval, ApproverID, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetETSVehicleAllotmentDetails(VehicleAllotmentDetails mData, int CentreCode, string CentreName, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.SetETSVehicleAllotmentDetails(mData, CentreCode, CentreName, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public VehicleAllotmentDetails GetVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg) 
        {
            try
            {
                dt = _ETSEditDataSync.GetVehicleAllotmentDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    EHGDBMapper _EHGDBMapper = new EHGDBMapper();
                    return _EHGDBMapper.Map_VehicleAllotmentDetails(dt.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }
        public EntryITourDetails GetEntryITourData(string Notenumber, int IsActive, ref string pMsg) 
        {
            EntryITourDetails result = new EntryITourDetails();
            result.NoteNumber = Notenumber;
            try
            {
                ds = _ETSEditDataSync.GetEntryITourData(Notenumber, IsActive, ref pMsg);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result.TravellingDetails = _ETSEditDBMapper.Map_EntryITDetails(dt.Rows[0]);
                    }
                    List<EntryIDWDetails> DWT = new List<EntryIDWDetails>();
                    dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DWT.Add(_ETSEditDBMapper.Map_EntryIDWDetails(dt.Rows[i]));
                        }
                    }
                    result.DateWiseDetails = DWT;
                }
            }
            catch { }
            return result;
        }
        public bool RemoveEntryINote(string NoteNumber, bool ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.RemoveEntryINote(NoteNumber, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public IEnumerable<NoteDriver> GETDriverList(string NoteNumber, ref string pMsg)
        {
            dt = _ETSEditDataSync.getEditTPDetails(NoteNumber, ref pMsg);
            return _ETSEditDBMapper.Map_DriverDetails(dt);
        }
        public bool UpdateETSVehicleAllotmentDetails(string NoteNumber, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.UpdateETSVehicleAllotmentDetails(NoteNumber, ref pMsg), ref pMsg, ref result);
            return result;
        }

    }
}
