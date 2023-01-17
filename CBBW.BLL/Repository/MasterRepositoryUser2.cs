using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;

namespace CBBW.BLL.Repository
{
    public partial class MasterRepository : IMasterRepository
    {
        public CompanyTransportType getVehicleEligibility(int EmployeeNumber, ref string pMsg)
        {
            CompanyTransportType result = new CompanyTransportType();
            if (EmployeeNumber == 0)
            {
                result.ID = 0; result.DisplayText = "NA";
            }
            else
            {
                result.ID = 3; result.DisplayText = "LV";
            }
            return result;
        }
        public IEnumerable<CustomComboOptions> getBranchType(int CentreId, ref string pMsg)
        {
            return _entities.getBranchType(CentreId, ref pMsg);
        }
    }
}
