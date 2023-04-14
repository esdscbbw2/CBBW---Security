using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;
namespace CBBW.DAL.Entities
{
    public class EntryIIEntities
    {
        EntryIIDataSync _EntryIIDataSync;
        EntryIIDBMapper _EntryIIDBMapper;
        DBResponseMapper _DBResponseMapper;
        DataTable dt;
        DataSet ds;
        public EntryIIEntities()
        {
            _EntryIIDataSync = new EntryIIDataSync();
            _EntryIIDBMapper = new EntryIIDBMapper();
            _DBResponseMapper = new DBResponseMapper();
        }
        public List<EntryIINote> GetEntryIINotes(int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            List<EntryIINote> result = new List<EntryIINote>();
            try
            {
                dt = _EntryIIDataSync.GetEntryIINotes(CentreCode, IsMainLocation, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_EntryIINote(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EntryIIList> GetEntryIINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            List<EntryIIList> result = new List<EntryIIList>();
            try
            {
                dt = _EntryIIDataSync.GetEntryIINoteList(DisplayLength,DisplayStart,SortColumn,SortDirection,SearchText, CentreCode, IsMainLocation, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_EntryIIList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EntryIITravelingDetails> GetEntryIITravellingDetails(string NoteNumber, ref string pMsg)
        {
            List<EntryIITravelingDetails> result = new List<EntryIITravelingDetails>();
            try
            {
                dt = _EntryIIDataSync.GetEntryIITravellingDetails(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_EntryIITravelingDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string Notenumber, ref string pMsg)
        {
            try
            {
                dt = _EntryIIDataSync.GetEntryIIVehicleAllotmentDetails(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    EHGDBMapper _EHGDBMapper = new EHGDBMapper();
                    return _EHGDBMapper.Map_VehicleAllotmentDetails(dt.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }
        public VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string NoteNumber, DateTime FromDate, DateTime ToDate, int CentreCode, bool IsMainLocation, ref string pMsg) 
        {
            try
            {
                dt = _EntryIIDataSync.GetEntryIIVehicleAllotmentDetails(NoteNumber,FromDate,ToDate,CentreCode,IsMainLocation, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    EHGDBMapper _EHGDBMapper = new EHGDBMapper();
                    return _EHGDBMapper.Map_VehicleAllotmentDetails(dt.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }
        public List<PunchInDetails> GetLastPunchingOfaPerson(int EmployeeNumber, DateTime PunchDate, ref string pMsg) 
        {
            List<PunchInDetails> result = new List<PunchInDetails>();
            try
            {
                dt = _EntryIIDataSync.GetLastPunchingOfaPerson(EmployeeNumber, PunchDate, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_PunchInDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public LastCentrePunchOut GetLastPunchingCentreOfaPerson(int EmployeeNumber, DateTime PunchDate, int CurrentCentreCode, ref string pMsg)
        {
            LastCentrePunchOut result = new LastCentrePunchOut();
            try
            {
                dt = _EntryIIDataSync.GetLastPunchingCentreOfaPerson(EmployeeNumber, PunchDate, CurrentCentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _EntryIIDBMapper.Map_LastCentrePunchOut(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public PunchInDetails GetPunchingDetails(int EmployeeNumber, DateTime PunchDate, int CentreCode, string RFIDNumber, ref string pMsg) 
        {
            PunchInDetails result = new PunchInDetails();
            try
            {
                dt = _EntryIIDataSync.GetPunchingDetails(EmployeeNumber, PunchDate, CentreCode, RFIDNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _EntryIIDBMapper.Map_PunchInDetails(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public int GetRequiredTimeInMinutesForEmployee(int EmployeeNumber, bool IsVehicleProvided, int FromCentreCode, int ToCentreCode, ref string pMsg) 
        {
            int result = 0;
            try
            {
                dt = _EntryIIDataSync.GetRequiredTimeInMinutesForEmployee(EmployeeNumber,IsVehicleProvided,FromCentreCode,ToCentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["RequiredTimeInMinutes"]))
                        result = int.Parse(dt.Rows[0]["RequiredTimeInMinutes"].ToString());
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<PunchInDetails> GetPunchingsV2(DateTime PunchDate, int CentreCode, string EmployeeIDs, ref string pMsg)
        {
            List<PunchInDetails> result = new List<PunchInDetails>();
            try
            {
                dt = _EntryIIDataSync.GetPunchingsV2(PunchDate,CentreCode,EmployeeIDs, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_PunchInDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<LastCentrePunchOutWithEmpNo> GetLastPunchingCentresV2(DateTime PunchDate, int CentreCode, string EmployeeIDs, ref string pMsg)
        {
            List<LastCentrePunchOutWithEmpNo> result = new List<LastCentrePunchOutWithEmpNo>();
            try
            {
                dt = _EntryIIDataSync.GetLastPunchingCentresV2(PunchDate, CentreCode, EmployeeIDs, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_LastCentrePunchOutWithEmpNo(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<PunchInDetails> GetPunchingsV3(int CentreCode, List<EmpDate> dtldata, ref string pMsg) 
        {
            List<PunchInDetails> result = new List<PunchInDetails>();
            try
            {
                dt = _EntryIIDataSync.GetPunchingsV3(CentreCode, dtldata, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_PunchInDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<PunchInDetails> GetPunchingsV4(int CentreCode, bool IsMainLocation, DateTime SchFromDate, string SchFromTime, List<EmpDate> dtldata, ref string pMsg) 
        {
            List<PunchInDetails> result = new List<PunchInDetails>();
            try
            {
                dt = _EntryIIDataSync.GetPunchingsV4(CentreCode, IsMainLocation, SchFromDate,SchFromTime, dtldata, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_PunchInDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<LastCentrePunchOutWithDistance> GetLastPunchingCentresV3(int CentreCode, List<EmpDate> dtldata, ref string pMsg)
        {
            List<LastCentrePunchOutWithDistance> result = new List<LastCentrePunchOutWithDistance>();
            try
            {
                dt = _EntryIIDataSync.GetLastPunchingCentresV3(CentreCode, dtldata, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_LastCentrePunchOutWithDistance(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public MLPersonsInfo GetMainLocationTPs(string NoteNumber, ref string pMsg)
        {
            MLPersonsInfo robj = new MLPersonsInfo();
            List<EmpDate> forPunching = new List<EmpDate>();
            List<EmpDate> forReq = new List<EmpDate>();
            List<MainLocationPersons> result = new List<MainLocationPersons>();
            try
            {
                ds = _EntryIIDataSync.GetEntryIITPOutInDetails(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MainLocationPersons personinfo = _EntryIIDBMapper.Map_MainLocationPersons(dt.Rows[i]);
                            EmpDate forobj1 = new EmpDate() { PunchDate=personinfo.SchFromDate, EmpNumber= personinfo.PersonID };
                            EmpDate forobj2 = new EmpDate() { PunchDate = personinfo.SchToDate, EmpNumber = personinfo.PersonID };
                            result.Add(personinfo);
                            forReq.Add(forobj2);
                            forPunching.Add(forobj1);
                            forPunching.Add(forobj2);
                        }
                    }
                }
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
            //forReq = forReq.Distinct().ToList();
            forPunching = forPunching.Distinct().ToList();
            robj.EmpDatesForPunching = forPunching;
            robj.EmpDatesForReq = forReq;
            robj.PersonInfo = result;
            return robj;
        }
        public LocationWiseTPDetails GetLocationWiseTPs(string NoteNumber,int CentreCode, ref string pMsg)
        {
            try
            {
                return _EntryIIDBMapper.Map_LocationWiseTPDetails(_EntryIIDataSync.GetEntryIITPOutInDetailsLW(NoteNumber, CentreCode, ref pMsg));                
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }
        public bool SetEntryIIData(string NoteNumber, bool IsMainLocation,
            int CentreCode,bool IsOffline, List<SaveTPDetails> Persons, List<SaveTPDWDetails> DWTour,
            List<SaveVehicleDetails> VAData, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_EntryIIDataSync.SetEntryIIData(NoteNumber, IsMainLocation, CentreCode, IsOffline, Persons, DWTour,VAData, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool UpdateEntryIIData(string NoteNumber, int CentreCode, string CentreName, bool IsEPTour,
            bool IsMainLocation, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_EntryIIDataSync.UpdateEntryIIData(NoteNumber, CentreCode,CentreName,IsEPTour,IsMainLocation, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public int GetTravelKmsOfANote(string NoteNumber, DateTime TillDate, int FromLocation, ref string pMsg)
        {
            return _EntryIIDataSync.GetTravelKmsOfANote(NoteNumber, TillDate, FromLocation,ref pMsg);
        }
        public EntryIIInnerView GetEntryIIData(string NoteNumber, int CentreCode, bool IsMainlocation, ref string pMsg) 
        {
            EntryIIInnerView result = new EntryIIInnerView();
            try
            {
                return _EntryIIDBMapper.Map_EntryIIInnerView(_EntryIIDataSync.GetEntryIIData(NoteNumber, CentreCode, IsMainlocation, ref pMsg));
            }
            catch (Exception ex)
            { pMsg = ex.Message; }
            return result;
        }
        public int IsMainLocationEntered(string NoteNumber, ref string pMsg) 
        {
            return _EntryIIDataSync.IsMainLocationEntered(NoteNumber, ref pMsg);
        }
        public NoteStatus GetEntryIINoteStatus(string NoteNumber, int CentreCode, ref string pMsg) 
        {
            NoteStatus result = new NoteStatus();
            try
            {
                dt = _EntryIIDataSync.GetEntryIINoteStatus(NoteNumber, CentreCode, ref pMsg);
                if (dt != null) 
                {
                    return _EntryIIDBMapper.Map_NoteStatus(dt.Rows[0]);
                }                
            }
            catch (Exception ex)
            { pMsg = ex.Message; }
            return result;
        }



    }
}
