using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class MasterEntities
    {        
        DataTable dt;
        DataSet ds;
        MasterDBMapper _mapper;
        DBResponseMapper _dbresmapper;
        MasterDataSync _datasync;
        public MasterEntities()
        {  
            _mapper = new MasterDBMapper();
            _datasync = new MasterDataSync();
            _dbresmapper = new DBResponseMapper();
        }
        public IEnumerable<ServiceType> getServiceTypes(int ID, ref string pMsg) 
        {
            List<ServiceType> servicetypes = new List<ServiceType>();
            try
            {
                dt = _datasync.getServiceTypes(ID, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        servicetypes.Add(_mapper.Map_ServiceType(dt.Rows[i]));
                    }
                }                
            }
            catch (Exception ex){pMsg = ex.Message;}
            return servicetypes;
        }
        public IEnumerable<PublicTransportType> getPublicTransportTypes(ref string pMsg) 
        {
            List<PublicTransportType> result = new List<PublicTransportType>();
            try
            {
                dt = _datasync.getPublicTransType(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_mapper.Map_PublicTransportType(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<TADAPubTransOption> getPublicTransportClassTypes(int ID,ref string pMsg)
        {
            List<TADAPubTransOption> result = new List<TADAPubTransOption>();
            try
            {
                dt = _datasync.getPubTransClassType(ID,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_dbresmapper.Map_TADAPubTransOption(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getLocationTypes(ref string pMsg) 
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getLocationTypes(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_dbresmapper.Map_CustomComboOptions(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getLocationsFromType(int LocationTypeID,ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getLocationsFromType(LocationTypeID,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_dbresmapper.Map_CustomComboOptions(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public float GetDistance(int FromLocation, int ToLocationType, int ToLocation, ref string pMsg) 
        {
            return _datasync.GetDistance(FromLocation, ToLocationType, ToLocation, ref pMsg);
        }
        public DateTime GetToSchDate(DateTime FromDate, int FromLocation, int ToLocationType,
            int ToLocation, int IsCalculateHourly, ref string pMsg)
        {
            return _datasync.GetToSchDate(FromDate,FromLocation,ToLocationType,ToLocation,IsCalculateHourly,ref pMsg);
        }
    }
}
