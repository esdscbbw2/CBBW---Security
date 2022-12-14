using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{    
    public class EHGEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        EHGDataSync _datasync;
        EHGDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public EHGEntities()
        {
            _datasync = new EHGDataSync();
            _DBMapper = new EHGDBMapper();
            _DBResponseMapper = new DBResponseMapper();
        }
        public bool SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtls dtl, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGHdrForManagement(header, dtl, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetEHGTravellingPersonDetails(string NoteNumber, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGTravellingPersonDetails(NoteNumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetDateWiseTourDetails(NoteNumber, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetEHGVehicleAllotmentDetails(VehicleAllotmentDetails mData, ref string pMsg) 
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGVehicleAllotmentDetails(mData, ref pMsg), ref pMsg, ref result);
            return result;
        }
    }
}
