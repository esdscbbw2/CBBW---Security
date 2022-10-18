using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.DAL.DBMapper
{
    public class DBResponseMapper
    {
        public void Map_DBResponse(DataTable dt,ref string pMsg,ref bool IsSuccess) 
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!DBNull.Value.Equals(dt.Rows[0]["IsSuccess"]))
                    IsSuccess = bool.Parse(dt.Rows[0]["IsSuccess"].ToString());
                if (!DBNull.Value.Equals(dt.Rows[0]["Msg"]))
                    pMsg = dt.Rows[0]["Msg"].ToString();
            }
        }
        public CustomCheckBoxOption Map_CustomCheckBoxOption(DataRow dr) 
        {
            CustomCheckBoxOption result = new CustomCheckBoxOption();
            if (dr != null) 
            {                
                if (!DBNull.Value.Equals(dr["ID"]))
                    result.ID = int.Parse(dr["ID"].ToString());
                if (!DBNull.Value.Equals(dr["DisplayText"]))
                    result.DisplayText = dr["DisplayText"].ToString();
                if (!DBNull.Value.Equals(dr["IsSelected"]))
                    result.IsSelected = bool.Parse(dr["IsSelected"].ToString());
            }
            return result;
        }
        public TADAPubTransOption Map_TADAPubTransOption(DataRow dr)
        {
            TADAPubTransOption result = new TADAPubTransOption();
            if (dr != null)
            {
                if (!DBNull.Value.Equals(dr["TransTypeID"]))
                    result.TransTypeID = int.Parse(dr["TransTypeID"].ToString());
                if (!DBNull.Value.Equals(dr["ID"]))
                    result.ID = int.Parse(dr["ID"].ToString());
                if (!DBNull.Value.Equals(dr["DisplayText"]))
                    result.DisplayText = dr["DisplayText"].ToString();
                if (!DBNull.Value.Equals(dr["IsSelected"]))
                    result.IsSelected = bool.Parse(dr["IsSelected"].ToString());
            }
            return result;
        }
    }
}
