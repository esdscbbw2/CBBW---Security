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
    }
}
