using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TFD;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public  class TFDEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        TFDDataSync _datasync;
        TFDDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public TFDEntities()
        {
            _datasync = new TFDDataSync();
            _DBMapper = new TFDDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public List<TFDNote> GetNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            List<TFDNote> result = new List<TFDNote>();
            try
            {
                dt = _datasync.GetNoteNumberList(CentreCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TFDNote x = new TFDNote();
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
        public List<TFDTravellingPerson> GetENTTravellingPerson(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            List<TFDTravellingPerson> result = new List<TFDTravellingPerson>();
            try
            {
                dt = _datasync.GetENTTravellingPersonDetails(Notenumber, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_TFDTravellingPerson(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<TFDDateWiseTourData> GetENTDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre, int status, ref string pMsg)
        {
            List<TFDDateWiseTourData> result = new List<TFDDateWiseTourData>();
            try
            {
                dt = _datasync.GetENTDateWiseTourData(NoteNumber, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_TFDDateWiseTourData(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<CustomCheckBoxOption> GetENTAuthEmployeeList(string Notenumber, int CentreCode, ref string pMsg)
        {
            List<CustomCheckBoxOption> result = new List<CustomCheckBoxOption>();
            try
            {
                dt = _datasync.GetENTAuthEmployeeList(Notenumber, CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomCheckBoxOption(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public TFDHdr GetTFDHeaderData(string Notenumber,int CenterCode,int status, ref string pMsg)
        {
            TFDHdr result = new TFDHdr();
            try
            {
                dt = _datasync.GetTFDHeaderData(Notenumber, CenterCode, status, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_TFDHeader(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetTFDFeedBackDetails(string NoteNumber, List<TFDTourFeedBackDetails> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetTFDFeedBackDetails(NoteNumber,dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetTFDetailsFinalSubmit(TFDHdr hdrmodel, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetTFDetailsFinalSubmit(hdrmodel, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<TFDNoteList> GetTFDDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<TFDNoteList> result = new List<TFDNoteList>();
            try
            {
                dt = _datasync.GetTFDDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_TFDNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<TFDTourFeedBackDetails> GetTFDTourFeedBackDetails(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            List<TFDTourFeedBackDetails> result = new List<TFDTourFeedBackDetails>();
            try
            {
                dt = _datasync.GetTFDTourFeedBackDetails(Notenumber, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_TFDTourFeedBackDetails(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public TFDHdr GetTFDHeaderDetails(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            TFDHdr result = new TFDHdr();
            try
            {
                dt = _datasync.GetTFDHeaderDetails(Notenumber, CenterCode, status, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_TFDHeaderDetails(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool RemoveTFDNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveTFDNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public IEnumerable<CustomComboOptions> GetENTConcernDeptList(string Notenumber, int CentreCode, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetENTConcernDeptList(Notenumber, CentreCode, ref pMsg);
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
        public bool SetTFDFeedBackApproval(string NoteNumber, List<TFDTourFBApproval> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetTFDFeedBackApproval(NoteNumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetTFDDateWiseTourData(string NoteNumber, bool IsApprove, string ApproveReason, List<TFDDateWiseTourData> dtldata, ref string pMsg)
        {

            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetTFDDateWiseTourData(NoteNumber, IsApprove, ApproveReason, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<TFDDateWiseTourData> GetTFDDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre, int status, ref string pMsg)
        {
            List<TFDDateWiseTourData> result = new List<TFDDateWiseTourData>();
            try
            {
                dt = _datasync.GetTFDDateWiseTourData(NoteNumber, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_TFDDateWiseTourDataView(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public string GetENTTourCategroy(string NoteNumber, ref string pMsg)
        {
            string result = null;
            try
            {
                dt = _datasync.GetENTTourCategroy(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["TourCategoryCodes"]))
                        result = dt.Rows[0]["TourCategoryCodes"].ToString();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

    }
}
