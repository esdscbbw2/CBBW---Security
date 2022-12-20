using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.Master;

namespace CBBW.DAL.DBMapper
{
    public class MasterDBMapper
    {
        public VehicleBasicInfo Map_VehicleBasicInfo(DataRow dr) 
        {
            VehicleBasicInfo result = new VehicleBasicInfo();
            if (dr != null)
            {
                if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                    result.VehicleNumber = dr["VehicleNumber"].ToString();
                if (!DBNull.Value.Equals(dr["Nature"]))
                    result.VehicleNature = dr["Nature"].ToString();
                if (!DBNull.Value.Equals(dr["VehicleType"]))
                    result.VehicleType = dr["VehicleType"].ToString();
                if (!DBNull.Value.Equals(dr["ModelName"]))
                    result.ModelName = dr["ModelName"].ToString();
                if (!DBNull.Value.Equals(dr["VehicleStatus"]))
                    result.VehicleStatus = dr["VehicleStatus"].ToString();
                if (!DBNull.Value.Equals(dr["ServiceDuration"]))
                    result.ServiceDuaration =int.Parse(dr["ServiceDuration"].ToString());
                if (!DBNull.Value.Equals(dr["IsActive"]))
                    result.IsActive =bool.Parse(dr["IsActive"].ToString());
            }
            return result;
        }
        public ServiceType Map_ServiceType(DataRow dr) 
        {
            ServiceType result = new ServiceType();
            if (dr != null) 
            {
                if (!DBNull.Value.Equals(dr["ID"]))
                    result.ID = int.Parse(dr["ID"].ToString());
                if (!DBNull.Value.Equals(dr["Description"]))
                    result.Description = dr["Description"].ToString();
            }
            return result;
        }
        public PublicTransportType Map_PublicTransportType(DataRow dr)
        {
            PublicTransportType result = new PublicTransportType();
            if (dr != null)
            {
                if (!DBNull.Value.Equals(dr["ID"]))
                    result.ID = int.Parse(dr["ID"].ToString());
                if (!DBNull.Value.Equals(dr["DisplayText"]))
                    result.DisplayText= dr["DisplayText"].ToString();
            }
            return result;
        }
    }
}
