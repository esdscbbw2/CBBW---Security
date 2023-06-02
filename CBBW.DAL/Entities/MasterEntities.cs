using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
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
        //DataSet ds;
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
                //result.Add(new CustomComboOptions {ID=99,DisplayText="99 / Others" });
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getLocationsFromType(string LocationTypeID, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getLocationsFromType(LocationTypeID, ref pMsg);
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
        public DateTime GetToSchDateFromMultiLocation(DateTime FromDate, int FromLocation, string ToLocationType,
            string ToLocation, ref string pMsg)
        {
            return _datasync.GetToSchDateForMultiLocation(FromDate, FromLocation, ToLocationType, ToLocation, ref pMsg);
            
        }
        public DateTime getEffectedRuleID(int RuleType, ref string pMsg) 
        {
            return _datasync.GetEffectedRuleID(RuleType, ref pMsg);
        } 
        public string getNewNoteNumber(string numberPattern, ref string pMsg)
        {
            string noteno = string.Empty;
            try
            {
                dt = _datasync.getNewNoteNumber(numberPattern, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["NewNoteNo"]))
                        noteno = dt.Rows[0]["NewNoteNo"].ToString();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return noteno;
        }
        public IEnumerable<CustomComboOptions> getEmployeeList(int centreCode, int functionalDesg,int isOtherStaff, ref string pMsg) 
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getEmployeeList(centreCode,functionalDesg, isOtherStaff, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_dbresmapper.Map_CustomComboOptionsForEmployees(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getDriverList(ref string pMsg,int CentreCode=13,string ExDriverName="#")
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getDriverList(CentreCode,ExDriverName, ref pMsg);
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
        public IEnumerable<CustomComboOptions> getCenterWiseDriverList(int CentreCode, ref string pMsg) 
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getCenterWiseDriverList(CentreCode,ref pMsg);
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
        public List<VehicleNo> getVehicleList(string VehicleType,int wheeltype,ref string pMsg)
        {
            List<VehicleNo> result = new List<VehicleNo>();
            try
            {
                dt = _datasync.getVehicleList(VehicleType, wheeltype,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        VehicleNo x = new VehicleNo();
                        if (!DBNull.Value.Equals(dt.Rows[i]["VehicleNumber"]))
                        {
                            x.VehicleNumber = dt.Rows[i]["VehicleNumber"].ToString();
                            //x.VehicleID = dt.Rows[i]["VehicleNumber"].ToString();
                        }
                        result.Add(x);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public VehicleBasicInfo getVehicleBasicInfo(string VehicleNumber, ref string pMsg) 
        {
            VehicleBasicInfo result = new VehicleBasicInfo();
            try
            {
                dt = _datasync.getVehicleBasicInfo(VehicleNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result=_mapper.Map_VehicleBasicInfo(dt.Rows[0]);                    
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getBranchType(int CenterId, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.getBranchType(CenterId, ref pMsg);
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
        public int getEligibleVehicleType(int EmployeeNumber, ref string pMsg) 
        {
            return _datasync.getEligibleVehicleType(EmployeeNumber, ref pMsg);
        }
        public VTStatement getVehicleEligibilityStatement(int EligibleVT, int ProvidedVT, ref string pMsg)
        {
            VTStatement result = new VTStatement();
            try
            {
                dt = _datasync.getVehicleEligibilityStatement(EligibleVT, ProvidedVT, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _mapper.Map_VTStatement(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public string GetDesignation(int PersonID, int PersonType, ref string pMsg)
        {
            return _datasync.GetDesignation(PersonID, PersonType,ref pMsg);
        }
        public bool GetHGOpenOrNot(int CentreCode, ref string pMsg) 
        {
            return _datasync.GetHGOpenOrNot(CentreCode, ref pMsg);
        }
        public bool SetPunchIN(int CentreCode, int EmployeeNumber, DateTime PunchDate, string PunchTime, ref string pMsg) 
        {
            bool result = false;
            _dbresmapper.Map_DBResponse(_datasync.SetPunchIN(CentreCode,EmployeeNumber,PunchDate,PunchTime, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public int GetCentreCodeFromLocation(int LocationTypeCode, int LocationCode, ref string pMsg) 
        {
            return _datasync.GetCentreCodeFromLocation(LocationTypeCode, LocationCode, ref pMsg);
        }


    }
}
