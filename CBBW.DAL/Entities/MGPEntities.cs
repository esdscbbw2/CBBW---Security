using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.MGP;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class MGPEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        MGPDataSync _datasync;
        MGPMapper _datamapper;
        public MGPEntities()
        {
            _datasync = new MGPDataSync();
            _datamapper = new MGPMapper();
        }
        public List<MGPNotes> getApprovedNoteNumbers(int Centercode, ref string pMsg) 
        {
            List<MGPNotes> result = new List<MGPNotes>();
            dt= _datasync.getNoteNumbersForMatGatePass(Centercode, ref pMsg);
            if (dt != null && dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    result.Add(_datamapper.Map_MGPNotes(dt.Rows[i]));
                }
            }
            return result;
        }
        public List<MGPMatOut> getMGPOutDetails(string NoteNumber, ref string pMsg) 
        {
            List<MGPMatOut> result = new List<MGPMatOut>();
            try
            {
                ds = _datasync.getMGPOutDetails(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPMatOut(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<RFID> getRFIDCards(ref string pMsg) 
        {
            List<RFID> result = new List<RFID>();
            try
            {
                dt = _datasync.getRFIDCards(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RFID obj = new RFID();
                        if (!DBNull.Value.Equals(dt.Rows[i]["RFIDNumber"]))
                            obj.RFIDCardNo = dt.Rows[i]["RFIDNumber"].ToString();
                        result.Add(obj);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
    }
}
