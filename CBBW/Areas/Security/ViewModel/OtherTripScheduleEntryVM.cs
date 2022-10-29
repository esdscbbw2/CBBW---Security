using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel
{
    public class OtherTripScheduleEntryVM
    {
        public string VehicleNo { get; set; }
        public string TripPurpose { get; set; }
        public string NoteNumber { get; set; }
        public int DriverCode { get; set; }
        public string DriverName { get; set; }
        public List<OthTripTemp> OTSchList { get; set; }
    }
    
}