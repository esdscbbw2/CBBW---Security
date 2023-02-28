using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class SaveTPDWDetails 
    {
        public int PersonID { get; set; }
        public string DWTourCategoryIds { get; set; }
        public string DWTourCategoryNames { get; set; }
        public string DWTourCenterCodeIds { get; set; }
        public string DWTourCenterNames { get; set; }
        public string DWBranchCodes { get; set; }
        public string DWBranchNames { get; set; }
        public DateTime DWFromDate { get; set; }
        public DateTime DWToDate { get; set; }
        public DateTime RequiredTourInDate { get; set; }
        public DateTime RequiredTourInTime { get; set; }
        public DateTime ActualTourInDate { get; set; }
        public DateTime ActualTourInTime { get; set; }
        public DateTime ActualTourOutDate { get; set; }
        public DateTime ActualTourOutTime { get; set; }
        public int TourStatus { get; set; }
        public string LNPunchStatus { get; set; }
        public string EMPunchStatus { get; set; }
        public DateTime SchFromTime { get; set; }
        public DateTime DWLNPunch { get; set; }
        public DateTime DWEMPunch { get; set; }
    }
    public class SaveTPDetails
    {
        public int PersonType { get; set; }
        public string PersonTypeText { get; set; }
        public int PersonID { get; set; }
        public string PersonIdnName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationCodenName { get; set; }
        public int PersonCenterCode { get; set; }
        public string PersonCenterName { get; set; }
        public int AuthorisedEmpNo { get; set; }
        public string AuthorisedEmpNoName { get; set; }
        public bool IsVehicleProvided { get; set; }
        public bool TADADenied { get; set; }
        public int Isdriver { get; set; }             
        public string TourCategoryText { get; set; }
    }
}
