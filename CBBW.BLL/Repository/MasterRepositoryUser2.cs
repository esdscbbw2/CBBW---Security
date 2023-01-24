using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.Master;

namespace CBBW.BLL.Repository
{
    public partial class MasterRepository : IMasterRepository
    {
        public CustomComboOptions getVehicleEligibility(int EmployeeNumber, ref string pMsg)
        {
            int vt=_entities.getEligibleVehicleType(EmployeeNumber, ref pMsg);
            EHGMaster master = EHGMaster.GetInstance;
            return master.VehicleTypes.Where(o => o.ID == vt).FirstOrDefault();
            
        }
        public IEnumerable<CustomComboOptions> getBranchType(int CentreId, ref string pMsg)
        {
            return _entities.getBranchType(CentreId, ref pMsg);
        }

        public VTStatement getVehicleEligibilityStatement(int EligibleVT, int ProvidedVT, ref string pMsg)
        {
            return _entities.getVehicleEligibilityStatement(EligibleVT, ProvidedVT,ref pMsg);
        }
    }
}
