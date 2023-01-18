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
        ETSEditDataSync _ETSEditDataSync;
        ETSEditDBMapper _ETSEditDBMapper;
        public ETSEditEntities()
        {
            _ETSEditDataSync = new ETSEditDataSync();
            _ETSEditDBMapper = new ETSEditDBMapper();
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
    }
}
