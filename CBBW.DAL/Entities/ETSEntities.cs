using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;
using CBBW.BOL.ETS;

namespace CBBW.DAL.Entities
{
    public class ETSEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        ETSDataSync _datasync;
       ETSDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public ETSEntities()
        {
            _datasync = new ETSDataSync();
            _DBMapper = new ETSDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public bool SetETSTravellingPerson(string NoteNumber, List<ETSTravellingPerson> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetETSTravellingPerson(NoteNumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool setETSTravDetailsNTourDetails(string NoteNumber, List<ETSTravellingDetails> TDdata, List<ETSDateWiseTour> DWTdata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setETSTravDetailsNTourDetails(NoteNumber, TDdata, DWTdata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<ETSTravellingPerson> GetETSTravellingPerson(string Notenumber, ref string pMsg)
        {
            List<ETSTravellingPerson> result = new List<ETSTravellingPerson>();
            try
            {
                dt = _datasync.GetETSTravellingPerson(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_ETSTravellingPerson(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetETSDetailsFinalSubmit(ETSHeader hdrmodel, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetETSDetailsFinalSubmit(hdrmodel, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<ETSNoteList> GetETSNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<ETSNoteList> result = new List<ETSNoteList>();
            try
            {
                dt = _datasync.GetETSNZBDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_ETSNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public ETSHeader GetETSHdrEntry(string Notenumber, ref string pMsg)
        {
            ETSHeader result = new ETSHeader();
            try
            {
                dt = _datasync.GetETSHdrEntry(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_ETSHeader(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public ETSTravellingDetails GetETSTravellingDetails(string Notenumber, ref string pMsg)
        {
            ETSTravellingDetails result = new ETSTravellingDetails();
            try
            {
                dt = _datasync.GetETSTravellingDetails(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_ETSTravellingDetails(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<ETSDateWiseTour> GetETSDateWiseTour(string Notenumber, ref string pMsg)
        {
            List<ETSDateWiseTour> result = new List<ETSDateWiseTour>();
            try
            {
                dt = _datasync.GetETSDateWiseTour(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_ETSDateWiseTour(dt.Rows[i]));

                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool RemoveETSNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveETSNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<ETSNote> GetETSNoteListToBeApproved(int CentreCode,int status, ref string pMsg)
        {
            List<ETSNote> result = new List<ETSNote>();
            try
            {
                dt = _datasync.GetETSNoteListToBeApproved(CentreCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ETSNote x = new ETSNote();
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

        public bool SetETSApprovalData(ETSApproveTravDetails model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetETSApprovalData(model, ref pMsg), ref pMsg, ref result);
            return result;
        }

        public bool SetETSRatifiedData(ETSRatified model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetETSRatifiedData(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
    }
}
