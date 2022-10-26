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
        bool RemoveNote(string NoteNumber, ref string pMsg);
        bool CheckScheduleDateAvailibility(string VehicleNo, DateTime ScheduleDate, ref string pMsg);
        IEnumerable<LocVehSchFromMat> getLocalVehicleSChedules(string VehicleNo, DateTime FromDate, DateTime ToDate, ref string pMsg);
        DateTime getSchToDate(DateTime FromSchDt, int FromLocation, int ToLocationType, int ToLocation,int IsCalculateHourly, ref string pMsg);
        bool UpdateOthTripSchDtl(string Notenumber, List<OthTripTemp> dtldata, ref string pMsg);
        CTVHdrDtl getSchDetailsFromNote(string NoteNumber, ref string pMsg);

    }
}
