using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMC;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public  class EMCEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        EMCDataSync _datasync;
        EMCDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public EMCEntities()
        {
            _datasync = new EMCDataSync();
            _DBMapper = new EMCDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public List<EMCNoteList> GetEMCNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<EMCNoteList> result = new List<EMCNoteList>();
            try
            {
                dt = _datasync.GetEMCNZBDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EMCNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetEMCTravellingPerson(string NoteNumber, int CenterCode, string CenterCodeName, List<EMCTravellingPerson> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMCTravellingPerson(NoteNumber, CenterCode, CenterCodeName, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool setEMCTravDetailsNTourDetails(string NoteNumber, List<EMCTravellingDetails> TDdata, List<EMCDateWiseTour> DWTdata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setEMCTravDetailsNTourDetails(NoteNumber, TDdata, DWTdata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<EMCTravellingPerson> GetEMCTravellingPerson(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            List<EMCTravellingPerson> result = new List<EMCTravellingPerson>();
            try
            {
                dt = _datasync.GetEMCTravellingPerson(Notenumber, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EMCTravellingPerson(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetEMCDetailsFinalSubmit(EMCHeader hdrmodel, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMCDetailsFinalSubmit(hdrmodel, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public EMCHeader GetEMCHdrEntry(string Notenumber, ref string pMsg)
        {
            EMCHeader result = new EMCHeader();
            try
            {
                dt = _datasync.GetEMCHdrEntry(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_EMCHeader(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public EMCTravellingDetails GetEMCTravellingDetails(string Notenumber, ref string pMsg)
        {
            EMCTravellingDetails result = new EMCTravellingDetails();
            try
            {
                dt = _datasync.GetEMCTravellingDetails(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_EMCTravellingDetails(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EMCDateWiseTour> GetEMCDateWiseTour(string Notenumber, ref string pMsg)
        {
            List<EMCDateWiseTour> result = new List<EMCDateWiseTour>();
            try
            {
                dt = _datasync.GetEMCDateWiseTour(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EMCDateWiseTour(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool RemoveEMCNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveEMCNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<EMCNote> GetEMCNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            List<EMCNote> result = new List<EMCNote>();
            try
            {
                dt = _datasync.GetEMCNoteListToBeApproved(CentreCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EMCNote x = new EMCNote();
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
        public bool SetEMCApprovalData(EMCApproveTravDetails model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMCApprovalData(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
       

       
        public IEnumerable<TPEPNote> GetEPTourNoteNumber(int EmployeeNumber, int CentreCode, ref string pMsg)
        {
            List<TPEPNote> tadaRules = new List<TPEPNote>();
            try
            {
                dt = _datasync.GetEPTourNoteNumber(EmployeeNumber, CentreCode,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tadaRules.Add(_DBMapper.Map_TPEPNote(dt.Rows[i]));
                    }
                }
            }
            catch(Exception ex) { ex.ToString(); }
            return tadaRules;
        }

    }
}
