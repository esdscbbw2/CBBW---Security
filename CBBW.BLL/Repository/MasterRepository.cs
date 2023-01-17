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
            //empType : 2-driver, 1-Others
            string result = "";
            if (empType == 1)
                result = "4 / DIC";
            if (empType == 2)
                result = "0 / Senior Driver"; 
            else if (empType == 3)
                result = "0 / Others";
            else if (empType == 4)
                result = "0 / Management";
            return result;
        }
        public IEnumerable<CustomComboOptions> getDriverList(ref string pMsg)
        {
           return _entities.getDriverList(ref pMsg);
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

        public List<VehicleNo> getVehicleList(string VehicleType, int wheeltype, ref string pMsg)
        {
            return _entities.getVehicleList(VehicleType, wheeltype, ref pMsg);
        }
    }
}
