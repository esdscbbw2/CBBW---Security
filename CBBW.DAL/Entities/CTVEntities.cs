using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class CTVEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        CTVDataSync _datasync;
        CTVDBMapper _CTVDBMapper;
        //DBResponseMapper _DBResponseMapper;
        public CTVEntities()
        {
            _datasync = new CTVDataSync();
            _CTVDBMapper = new CTVDBMapper();
            //_DBResponseMapper = new DBResponseMapper();
        }
        public string getNewCTVNoteNo(string SchPattern, ref string pMsg) 
        {
            string noteno = string.Empty;
            try
            {
                dt = _datasync.getNewCTVNoteNo(SchPattern, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["NewNoteNo"]))
                        noteno = dt.Rows[0]["NewNoteNo"].ToString();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return noteno;
        }
        public VehicleInfo getVehicleInfo(string VehicleNo, ref string pMsg)
        {
            VehicleInfo result = new VehicleInfo();
            try
            {
                dt = _datasync.getVehicleInfo(VehicleNo,ref pMsg);
                if (dt != null && dt.Rows.Count>0)
                {
                    result = _CTVDBMapper.Map_VehicleInfo(dt.Rows[0]);
                }
            }
            catch { }
            return result;
        }
        public List<string> getLCVMCVVehicleList(ref string pMsg)
        {
            List<string> result = new List<string>();
            try
            {
                dt = _datasync.getLCVMCVVehicles(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!DBNull.Value.Equals(dt.Rows[i]["VehicleNumber"]))
                            result.Add(dt.Rows[i]["VehicleNumber"].ToString());
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        
    }
}
