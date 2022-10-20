using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;

namespace CBBW.DAL.DBMapper
{
    public class CTVDBMapper
    {
        public VehicleInfo Map_VehicleInfo(DataRow dr)
        {
            VehicleInfo result = new VehicleInfo();
            try
            {
                if (dr != null)
                {                    
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["Nature"]))
                        result.VehicleType = dr["Nature"].ToString();
                    if (!DBNull.Value.Equals(dr["Manufacturer"]))
                        result.ModelName = dr["Manufacturer"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo =int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleStatus"]))
                        result.VehicleStatus = dr["VehicleStatus"].ToString();
                    if (!DBNull.Value.Equals(dr["ServiceDuaration"]))
                        result.ServiceDuaration = int.Parse(dr["ServiceDuaration"].ToString());
                    if (!DBNull.Value.Equals(dr["IsSuccess"]))
                        result.IsSuccess = bool.Parse(dr["IsSuccess"].ToString());
                    if (!DBNull.Value.Equals(dr["Msg"]))
                        result.Msg = dr["Msg"].ToString();
                    result.IsActive = result.VehicleStatus == "ACTIVE" ? true : false;
                }
            }
            catch { }
            return result;
        }
    }
}
