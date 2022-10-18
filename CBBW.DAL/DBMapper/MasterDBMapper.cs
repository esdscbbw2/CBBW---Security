using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;

namespace CBBW.DAL.DBMapper
{
    public class MasterDBMapper
    {
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
