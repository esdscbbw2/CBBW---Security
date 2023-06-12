using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETS
{
    public class ETSTravellingDetails
    {
        public string NoteNumber { get; set; }
        public int PublicTransport { get; set; }
        public bool PublicTransports { get; set; }
        public int VehicleType { get; set; }
        public string ReasonVehicleReq { get; set; }
        public int VehicleTypeProvided { get; set; }
        public string VehicleTypeProvidedtxt { get; set; }
        public string ReasonVehicleProvided { get; set; }
        public DateTime SchFromDate { get; set; }
        public string SchFromTime { get; set; }
        public DateTime SchTourToDate { get; set; }
        public string PurposeOfVisit { get; set; }
        public string EmpNoName { get; set; }
        public bool IsActive { get; set; }
        public string SchFromDateDisplay { get; set; }
        public string SchTourToDateDisplay { get; set; }
        public string SchFromDateStr { get; set; }
        public string SchTourToDateStr { get; set; }
    }
    public class Employee
    {
        public string EmployeeNonName { get; set; }
    }


    public class ETSApproveTravDetails
    {
        public string NoteNumber { get; set; }
        public string EmployeeNonName { get; set; }
        public int VehicleTypeProvided { get; set; }
        public string ReasonVehicleProvided { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedReason { get; set; }
        public int status { get; set; }
    }
    public class ETSRatified
    {
        public string NoteNumber { get; set; }
        public bool IsRatified { get; set; }
        public string RatifiedReason { get; set; }
        public int status { get; set; }
    }
}
