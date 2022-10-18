using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.DAL.DataSync;

namespace CBBW.DAL.Entities
{
    public class CTVEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        CTVDataSync _datasync;
        public CTVEntities()
        {
            _datasync = new CTVDataSync();
        }
        public string getNewCTVNoteNo(string SchPattern, ref string pMsg) 
        {
            string noteno = string.Empty;
            dt = _datasync.getNewCTVNoteNo(SchPattern, ref pMsg);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!DBNull.Value.Equals(dt.Rows[0]["NewNoteNo"]))
                    noteno = dt.Rows[0]["NewNoteNo"].ToString();
            }
            return noteno;
        }

    }
}
