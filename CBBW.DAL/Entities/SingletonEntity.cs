using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class SingletonEntity
    {
        DataTable dt;
        MasterDBMapper _mapper;
        MasterDataSync _datasync;
        public SingletonEntity()
        {
            _mapper = new MasterDBMapper();
            _datasync = new MasterDataSync();
        }
        public List<LocationMaster> GetLocationsFromCommaSeparatedTypes(string LocationTypeID)
        {
            string pMsg = "";
            List<LocationMaster> locations = new List<LocationMaster>();
            try
            {
                dt = _datasync.GetLocationsFromCommaSeparatedTypes(LocationTypeID, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        locations.Add(_mapper.Map_LocationMaster(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return locations;
        }
    }
}
