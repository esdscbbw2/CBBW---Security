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
        public bool SetEHGTravellingPersonDetails(string NoteNumber, string AuthEmp, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetEHGTravellingPersonDetails(NoteNumber,AuthEmp, dtldata, ref pMsg), ref pMsg, ref result);
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
        public bool UpdateEHGHdr(string NoteNumber, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.UpdateEHGHdr(NoteNumber, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<DateWiseTourDetails> getDateWiseTourDetails(string Notenumber, int IsActive, ref string pMsg) 
        {
            List<DateWiseTourDetails> result = new List<DateWiseTourDetails>();
            try
            {
                dt = _datasync.getDateWiseTourDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_DateWiseTourDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EHGTravelingPersondtlsForManagement> getTravelingPersonDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            List<EHGTravelingPersondtlsForManagement> result = new List<EHGTravelingPersondtlsForManagement>();
            try
            {
                dt = _datasync.getTravelingPersonDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_EHGTravelingPersondtlsForManagement(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public VehicleAllotmentDetails getVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            VehicleAllotmentDetails result = new VehicleAllotmentDetails();
            try
            {
                dt = _datasync.getVehicleAllotmentDetails(Notenumber, IsActive, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_VehicleAllotmentDetails(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public EHGHeader getEHGNoteHdr(string Notenumber, ref string pMsg)
        {
            EHGHeader result = new EHGHeader();
            try
            {
                dt = _datasync.getEHGNoteHdr(Notenumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_EHGHeader(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
    }
}
