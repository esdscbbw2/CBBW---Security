using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.GVMR;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class GVMREntities
    {
        DataTable dt = null;
        DataSet ds = null;
        GVMRDataSync _datasync;
        GVMRDBMapper _datamapper;
        DBResponseMapper _DBResponseMapper;

        public GVMREntities()
        {
            _datasync = new GVMRDataSync();
            _datamapper = new GVMRDBMapper();
            _DBResponseMapper = new DBResponseMapper();
        }



        public IEnumerable<GVMRNoteNumber> GetNoteNumbers(int CenterCode, int status, ref string pMsg)
        {
            List<GVMRNoteNumber> result = new List<GVMRNoteNumber>();
            try
            {
                dt = _datasync.GetNoteNumbers(CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GVMRNoteNumber x = new GVMRNoteNumber();
                        if (!DBNull.Value.Equals(dt.Rows[i]["NoteNo"]))
                        {
                            x.NoteNo = dt.Rows[i]["NoteNo"].ToString();

                        }
                        result.Add(x);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public List<GVMRDetails> GetGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg)
        {
            List<GVMRDetails> result = new List<GVMRDetails>();
            try
            {
                ds = _datasync.GetGVMRDetails(NoteNumber, CenterCode, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_GVMRDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public List<PunchingDetails> GetPunchingDetails(string CentreCode, DateTime FromDate, DateTime ToDate, int UserID, ref string pMsg)
        {
            List<PunchingDetails> result = new List<PunchingDetails>();
            try
            {
                ds = _datasync.GetPunchingDetails( CentreCode,  FromDate,  ToDate,  UserID, ref  pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_PunchingDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool setGVMRDetails(GVMRDataSave gvmrdata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setGVMRDetails(gvmrdata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<GVMRNoteList> getGVMRDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, ref string pMsg)
        {
            List<GVMRNoteList> result = new List<GVMRNoteList>();
            try
            {
                dt = _datasync.getGVMRDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_datamapper.Map_GVMRNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<GVMRDetails> getGVMRDetailsForView(string NoteNumber, int CenterCode, ref string pMsg)
        {
            List<GVMRDetails> result = new List<GVMRDetails>();
            try
            {
                ds = _datasync.getGVMRDetailsForView(NoteNumber, CenterCode, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_GVMRDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public bool SetGVMRDetailsV2(List<GVMRDataSave> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetGVMRDetailsV2(dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        // Not in Use
        public GetGVMRDetailsWithPunching GetGVMRDetailsWithPunchingDetails(string NoteNumber, int CenterCode, ref string pMsg)
        {
            try
            {
                ds = _datasync.GetGVMRDetailsWithPunchingDetails(NoteNumber, CenterCode, ref pMsg);

                if (ds != null)
                {
                    DataTable dt0 = null; DataTable dt1 = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) { dt0 = ds.Tables[0]; }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0) { dt1 = ds.Tables[1]; }
                    return _datamapper.Map_GetGVMRDetailsWithPunching(dt0, dt1);
                }
                else { return null; }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
    }
}
