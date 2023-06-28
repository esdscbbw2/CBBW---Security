using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;

namespace CBBW.BLL.IRepository
{
    public interface IMasterRepository
    {
        bool IsVehicleExist(string VehicleNumber, ref string pMsg);
        IEnumerable<ServiceType> getAllServiceTypes(ref string pMsg);
        ServiceType getServiceType(int ID,ref string pMsg);
        IEnumerable<CustomComboOptions> getEmployeeList(int centreCode, int functionalDesg, int isOtherStaff, ref string pMsg);
        IEnumerable<CustomComboOptions> getDriverList(ref string pMsg,int CentreCode=13,string ExDrivername="#");
        string GetDesgCodenName(int empID, int empType);
        List<VehicleNo> getVehicleList(string VehicleType,int wheeltype, ref string pMsg, int CentreCode = 0);
        VehicleBasicInfo getVehicleBasicInfo(string VehicleNumber, ref string pMsg);
        CustomComboOptions getVehicleEligibility(int EmployeeNumber, ref string pMsg);
        IEnumerable<CustomComboOptions> getBranchType(int CentreId, ref string pMsg);
        VTStatement getVehicleEligibilityStatement(int EligibleVT, int ProvidedVT, ref string pMsg);
        IEnumerable<LocationMaster> GetCentresFromTourCategory(string TourCatIDs, ref string pMsg);
        IEnumerable<LocationMaster> GetBranchOfaCentre(int CentreCode, ref string pMsg);
        bool GetHGOpenOrNot(int CentreCode, ref string pMsg);
        bool SetPunchIN(int CentreCode, int EmployeeNumber, DateTime PunchDate, string PunchTime, ref string pMsg);
        string GetNewNoteNumber(string NotePattern, ref string pMsg);
        bool GetEmployeeValidationForTour(int CentreCode, string EmployeeNumbers, DateTime FromDate, DateTime ToDate, ref string pMsg);
        IEnumerable<CustomComboOptions> GetEmployeeListV2(int centreCode, ref string pMsg);
        bool VehicleAvailableValidation(string VehicleNumber, int CentreCode, DateTime FromDate, DateTime ToDate, int KMLimit, ref string pMsg);
    }
}
