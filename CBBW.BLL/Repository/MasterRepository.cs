using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public partial class MasterRepository : IMasterRepository
    {
        MasterEntities _entities;
        public MasterRepository()
        {
            _entities = new MasterEntities();
        }
        public IEnumerable<ServiceType> getAllServiceTypes(ref string pMsg)
        {
            return _entities.getServiceTypes(0,ref pMsg);
        }
        //public IEnumerable<CustomComboOptions> getBranchType(int CentreId, ref string pMsg)
        //{
        //    return _entities.getBranchType(CentreId, ref pMsg);
        //}
        public string GetDesgCodenName(int empID, int empType)
        {
            string msg = "";
            string result = "";
            if (empType == 1 || empType == 2)
                result = _entities.GetDesignation(empID, 1, ref msg);
            else if (empType == 3)
                result = "0 / Others";
            else if (empType == 4)
                result = "0 / Management";

            if (string.IsNullOrEmpty(result)) { result = "0 / NA"; }
            return result;
        }
        public IEnumerable<CustomComboOptions> getDriverList(ref string pMsg, int CentreCode = 13, string ExDrivername = "#")
        {
           return _entities.getDriverList(ref pMsg,CentreCode,ExDrivername);
        }
        public IEnumerable<CustomComboOptions> getEmployeeList(int centreCode, int functionalDesg, int isOtherStaff, ref string pMsg)
        {
           return _entities.getEmployeeList(centreCode, functionalDesg, isOtherStaff, ref pMsg);
        }
        public ServiceType getServiceType(int ID, ref string pMsg)
        {
            return _entities.getServiceTypes(ID, ref pMsg).FirstOrDefault();
        }
        public VehicleBasicInfo getVehicleBasicInfo(string VehicleNumber, ref string pMsg)
        {
            return _entities.getVehicleBasicInfo(VehicleNumber, ref pMsg);
        }
        //public CompanyTransportType getVehicleEligibility(int EmployeeNumber, ref string pMsg)
        //{
        //    CompanyTransportType result = new CompanyTransportType();
        //    result.ID = 3;result.DisplayText = "LV";
        //    return result;
        //}
        public List<VehicleNo> getVehicleList(string VehicleType, int wheeltype, ref string pMsg, int CentreCode = 0)
        {
            return _entities.getVehicleList(VehicleType, wheeltype, ref pMsg,CentreCode);
        }
        public IEnumerable<LocationMaster> GetCentresFromTourCategory(string TourCatIDs, ref string pMsg)
        {
            List<int> listOfIntegers = new List<int>();
            List<LocationMaster> locations = new List<LocationMaster>();
            MasterData masterdata = MasterData.GetInstance;
            if (masterdata.AllLocations != null && masterdata.AllLocations.Count > 0)
            {
                if (TourCatIDs.IndexOf(',') < 0) { TourCatIDs = TourCatIDs + ","; }
                string[] CatTypes = TourCatIDs.Split(',');
                foreach (var obj in CatTypes)
                {
                    int intValue;
                    if (int.TryParse(obj, out intValue))
                    {
                        if (intValue == 1 || intValue == 4) { intValue = 2; }
                        else if (intValue == 3) { intValue = 99; }
                        listOfIntegers.Add(intValue);                        
                    }
                }
                locations.AddRange(masterdata.AllLocations.Where(o => listOfIntegers.Contains(o.TypeID)).ToList());
            }
            return locations;
        }
        public IEnumerable<LocationMaster> GetBranchOfaCentre(int CentreCode, ref string pMsg)
        {
            List<LocationMaster> locations = new List<LocationMaster>();
            MasterData masterdata = MasterData.GetInstance;
            if (masterdata.AllLocations != null && masterdata.AllLocations.Count > 0)
            {
                locations.AddRange(masterdata.AllLocations.Where(o => o.TypeID == 1 && o.CentreCode == CentreCode).ToList());
            }
            return locations;
        }
        public bool GetHGOpenOrNot(int CentreCode, ref string pMsg)
        {
            return _entities.GetHGOpenOrNot(CentreCode, ref pMsg);
        }
        public bool SetPunchIN(int CentreCode, int EmployeeNumber, DateTime PunchDate, string PunchTime, ref string pMsg) 
        {
            return _entities.SetPunchIN(CentreCode,EmployeeNumber,PunchDate,PunchTime,ref pMsg);
        }
        public string GetNewNoteNumber(string NotePattern,ref string pMsg) 
        {
            return _entities.getNewNoteNumber(NotePattern, ref pMsg);
        }
        public bool GetEmployeeValidationForTour(int CentreCode, string EmployeeNumbers, DateTime FromDate, DateTime ToDate, ref string pMsg)
        {
            return _entities.GetEmployeeValidationForTour(CentreCode, EmployeeNumbers,FromDate,ToDate, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> GetEmployeeListV2(int centreCode, ref string pMsg)
        {
            return _entities.getEmployeeListV2(centreCode, ref pMsg);
        }
    }
}
