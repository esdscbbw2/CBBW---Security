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
        public List<EditDWTDetails> getCurrentDateWiseTour(string NoteNumber, int FieldTag, ref string pMsg) 
        {
            List<EditDWTDetails> result = new List<EditDWTDetails>();
            try
            {
                ds = _ETSEditDataSync.getCurrentDateWiseTour(NoteNumber, FieldTag, ref pMsg);
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
        public bool SetETSTourEdit(DWTTourDetailsForDB obj, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_ETSEditDataSync.SetETSTourEdit(obj, ref pMsg), ref pMsg, ref result);
            return result;
        }
    }
}
