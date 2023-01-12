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
        IEnumerable<ServiceType> getAllServiceTypes(ref string pMsg);
        ServiceType getServiceType(int ID,ref string pMsg);
        IEnumerable<CustomComboOptions> getEmployeeList(int centreCode, int functionalDesg, int isOtherStaff, ref string pMsg);
        IEnumerable<CustomComboOptions> getDriverList(ref string pMsg);
        string GetDesgCodenName(int empID, int empType);
        List<VehicleNo> getVehicleList(string VehicleType,int wheeltype, ref string pMsg);
        VehicleBasicInfo getVehicleBasicInfo(string VehicleNumber, ref string pMsg);
        CompanyTransportType getVehicleEligibility(int EmployeeNumber, ref string pMsg);
        IEnumerable<CustomComboOptions> getBranchType(int CentreId, ref string pMsg);
    }
}
