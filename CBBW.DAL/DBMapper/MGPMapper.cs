using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.MGP;

namespace CBBW.DAL.DBMapper
{
    public class MGPMapper
    {
        public MGPNotes Map_MGPNotes(DataRow dr) 
        {
            MGPNotes result = new MGPNotes();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNumber = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CreateEmployeeNo"]))
                        result.CreateEmployeeNo = int.Parse(dr["CreateEmployeeNo"].ToString());
                }
            }
            catch { }
            return result;
        }
    }
}
