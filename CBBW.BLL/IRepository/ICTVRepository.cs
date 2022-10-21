using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;

namespace CBBW.BLL.IRepository
{
    public interface ICTVRepository
    {
        string getNewTripScheduleNo(string SchPattern, ref string pMsg);
        TripScheduleHdr NewTripScheduleNo(string SchPattern, ref string pMsg);
        IEnumerable<CustomComboOptions> getLocationTypes(ref string pMsg);
        IEnumerable<CustomComboOptions> getLocationsFromType(int LocationTypeID, ref string pMsg);
        List<VehicleNo> getLCVMCVVehicleList(ref string pMsg);
        VehicleInfo getVehicleInfo(string VehicleNo, ref string pMsg);
        UserInfo getUserInfo(string UserName, ref string pMsg);
        bool CreateNewCTVHdr(TripScheduleHdr model, ref string pMsg);
    }
}
