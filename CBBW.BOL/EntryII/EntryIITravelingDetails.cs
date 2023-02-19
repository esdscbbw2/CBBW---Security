using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class EntryIITravelingDetails
    {
        public string NoteNumber { get; set; }
        public bool PublicTransport { get; set; }
        public int VehicleType { get; set; }
        public string ReasonVehicleReq { get; set; }
        public int VehicleTypeProvided { get; set; }
        public string ReasonVehicleProvided { get; set; }
        public DateTime SchFromDate { get; set; }
        public string SchFromTime { get; set; }
        public DateTime SchTourToDate { get; set; }
        public string PurposeOfVisit { get; set; }
        public string EmpNoName { get; set; }
        public bool IsActive { get; set; }
        public string VehicleTypeText { get; set; }
        public string VehicleTypeProvidedText { get; set; }
        public string SchFromDateDisplay { get; set; }
        public string SchTourToDateDisplay { get; set; }

    }
}
