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
        IEnumerable<CustomComboOptions> getLocationsFromType(string LocationTypeIDs, ref string pMsg);
        IEnumerable<CustomComboOptions> getLocationsFromType(int LocationTypeID, ref string pMsg);
        List<VehicleNo> getLCVMCVVehicleList(ref string pMsg);
        VehicleInfo getVehicleInfo(string VehicleNo, ref string pMsg);
        UserInfo getUserInfo(string UserName, ref string pMsg);
        bool CreateNewCTVHdr(TripScheduleHdr model, ref string pMsg);
        bool RemoveNote(string NoteNumber,int OnlyDtl, ref string pMsg);
        bool CheckScheduleDateAvailibility(string VehicleNo, DateTime ScheduleDate, ref string pMsg);
        List<LocVehSchFromMat> getLocalVehicleSChedules(string VehicleNo, DateTime FromDate, DateTime ToDate, ref string pMsg);
        DateTime getSchToDate(string VehicleNo,DateTime FromSchDt, int FromLocation, int ToLocationType, int ToLocation,int IsCalculateHourly, ref string pMsg);
        bool UpdateOthTripSchDtl(string Notenumber,string TripPurpose, List<OthTripTemp> dtldata, ref string pMsg);
        CTVHdrDtl getSchDetailsFromNote(string NoteNumber, ref string pMsg);
        List<NoteNumber> GetNoteNumbersTobeApproved(int EmpNo, int CenterCode,ref string pMsg);
        VehicleAvblInfo getVehicleDateSlots(string VehicleNo, int IncludeOTVSch, ref string pMsg);
        DateTime getSchToDateFromMultiLocation(string VehicleNo, DateTime FromSchDt,
            int FromLocation, string ToLocationType, string ToLocation, ref string pMsg);
        IEnumerable<TripScheduleHdr> getCtvSchedule(int PageSize, int PageNumber, int SortCol, string SortDirection,
            string SearchText, int centercode, ref string pMsg);
        bool setCTVApproval(string Notenumber, int EmployeeNumber, bool Isapproved,
            DateTime ApprovalDatetime, string DisApprovalReason, ref string pMsg);
        IEnumerable<TripScheduleHdr> getApprovedCtvSchedule(int PageSize, int PageNumber, int SortCol, string SortDirection,
            string SearchText, int centercode, ref string pMsg);
        IEnumerable<CustomComboOptions> getDriverList(string ExpDriverName, ref string pMsg);
        bool setLocalTripSchDriver(string Notenumber, List<LTSDriVerChange> dtldata, ref string pMsg);
    }
}
