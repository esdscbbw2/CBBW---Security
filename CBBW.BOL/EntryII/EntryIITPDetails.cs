using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class EntryIIPersons: EntryIINote
    {
        public int MainLocationCode { get; set; }
        public int PersonType { get; set; }
        public string PersonTypeText { get; set; }
        public int PersonID { get; set; }
        public string PersonIdnName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationCodenName { get; set; }
        public int PersonCenterCode { get; set; }
        public string PersonCenterName { get; set; }
        public bool TADADenied { get; set; }
        public int Isdriver { get; set; }
        public bool IsVehicleProvided { get; set; }
        public string AuthorisedEmpNoName { get; set; }

    }
    public class EntryIITPDetails : EntryIIPersons
    {
        public DateTime SchFromDate { get; set; }
        public string SchFromTime { get; set; }
        public DateTime SchToDate { get; set; }
        public DateTime RequiredTourInDate { get; set; }
        public DateTime RequiredTourInTime { get; set; }
        public DateTime ActualTourInDate { get; set; }
        public DateTime ActualTourInTime { get; set; }
        public DateTime ActualTourOutDate { get; set; }
        public DateTime ActualTourOutTime { get; set; }
        public int TourStatus { get; set; }
        public int LNPunchRequired { get; set; }
        public int EMPunchRequired { get; set; }
        public DateTime LNPunchTime { get; set; }
        public DateTime EMPunchTime { get; set; }
        public DateTime MainLocationGenTimeIn { get; set; }
        public DateTime CentreTimeIn { get; set; }
        public int LastPunchOutLocationCode { get; set; }
        public DateTime LastLocationPunchOutTime { get; set; }
        public string EmployeeIDs { get; set; }
    }
    public class MainLocationPersons:EntryIITPDetails
    {
        public string TourCategoryText { get; set; }
        public string LastCentreCodes { get; set; }
        public int LastCentreDistance { get; set; }
        public int RequiredTimeinMinutes { get; set; }
    }
    public class LocationWisePersons  : EntryIITPDetails
    {
        public DateTime DWFromDate { get; set; }
        public DateTime DWToDate { get; set; }
        public string DWTourCategoryIds { get; set; }
        public string DWTourCategoryNames { get; set; }
        public string DWTourCenterCodeIds { get; set; }
        public string DWTourCenterNames { get; set; }
        public string DWBranchCodes { get; set; }
        public string DWBranchNames { get; set; }
        public DateTime MCurDate { get; set; }
    }
    public class LocationWiseTPDetails 
    {
        public List<EntryIIPersons> PersonDetails { get; set; }
        public List<LocationWisePersons> PersonDateWiseDetails { get; set; }
    }
    
}
