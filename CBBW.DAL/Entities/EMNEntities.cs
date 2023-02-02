using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMN;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public class EMNEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        EMNDataSync _datasync;
        EMNDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public EMNEntities()
        {
            _datasync = new EMNDataSync();
            _DBMapper = new EMNDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public List<EMNNoteList> GetEMNNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<EMNNoteList> result = new List<EMNNoteList>();
            try
            {
                dt = _datasync.GetEMNNZBDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EMNNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getCenterCodeList(int center, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getCenterCodeList(center, ref pMsg);
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
        public IEnumerable<CustomComboOptions> getCenterCodeListFromTravellingPerson(string NoteNumber,int status, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getCenterCodeListFromTravellingPerson( NoteNumber,status, ref pMsg);
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
        public bool SetEMNTravellingPerson(string NoteNumber, int CenterCode, string CenterCodeName, List<EMNTravellingPerson> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMNTravellingPerson(NoteNumber, CenterCode, CenterCodeName, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool setEMNTravDetailsNTourDetails(string NoteNumber, List<EMNTravellingDetails> TDdata, List<EMNDateWiseTour> DWTdata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setEMNTravDetailsNTourDetails(NoteNumber, TDdata, DWTdata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<EMNTravellingPerson> GetEMNTravellingPerson(string Notenumber,int CenterCode, ref string pMsg)
        {
            List<EMNTravellingPerson> result = new List<EMNTravellingPerson>();
            try
            {
                dt = _datasync.GetEMNTravellingPerson(Notenumber, CenterCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EMNTravellingPerson(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetEMNDetailsFinalSubmit(EMNHeader hdrmodel, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMNDetailsFinalSubmit(hdrmodel, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public EMNHeader GetEMNHdrEntry(string Notenumber, ref string pMsg)
        {
            EMNHeader result = new EMNHeader();
            try
            {
                dt = _datasync.GetEMNHdrEntry(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_EMNHeader(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public EMNTravellingDetails GetEMNTravellingDetails(string Notenumber, ref string pMsg)
        {
            EMNTravellingDetails result = new EMNTravellingDetails();
            try
            {
                dt = _datasync.GetEMNTravellingDetails(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_EMNTravellingDetails(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EMNDateWiseTour> GetEMNDateWiseTour(string Notenumber, ref string pMsg)
        {
            List<EMNDateWiseTour> result = new List<EMNDateWiseTour>();
            try
            {
                dt = _datasync.GetEMNDateWiseTour(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EMNDateWiseTour(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool RemoveEMNNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveEMNNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<EMNNote> GetEMNNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            List<EMNNote> result = new List<EMNNote>();
            try
            {
                dt = _datasync.GetEMNNoteListToBeApproved(CentreCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EMNNote x = new EMNNote();
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
        public bool SetEMNApprovalData(EMNApproveTravDetails model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMNApprovalData(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetEMNRatifiedData(EMNRatified model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEMNRatifiedData(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
    }
}
