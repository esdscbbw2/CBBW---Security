using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EHG
{
    public class EHGTravelingPersondtls: EHGNote
    {
        public int ID { get; set; }
        public int PersonType { get; set; }
        public int EmployeeNo { get; set; }
        public int DesignationCode { get; set; }
        public string EmployeeNonName { get; set; }
        public string EmployeeNonNamecmb { get; set; }
        public string DesignationCodenName { get; set; }
        public DateTime FromDate { get; set; }
        public string FromDateStr { get; set; }
        public string FromDateStrDisplay { get; set; }
        public string FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public string ToDateStr { get; set; }
        public string ToDateStrDisplay { get; set; }
        public string PurposeOfVisit { get; set; }
        public bool TADADenied { get; set; }
        public bool IsActive { get; set; }
        public bool IsAuthorised { get; set; }
    }
    public class EHGTravelingPersondtlsForManagement : EHGTravelingPersondtls 
    {
        public DateTime ActualTourOutDate { get; set; }
        public string ActualTourOutTime { get; set; }
        public DateTime RequiredTourInDate { get; set; }
        public string RequiredTourInTime { get; set; }
        public DateTime ActualTourInDate { get; set; }
        public string ActualTourInTime { get; set; }
        public int TourStatus { get; set; }
        public string TourStatusText { get; set; }
    }
}
