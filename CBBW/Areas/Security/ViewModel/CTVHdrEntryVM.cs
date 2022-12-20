using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel
{
    public class CTVHdrEntryVM 
    {
        public TripScheduleHdr TripScheduleHdr { get; set; }
        public IEnumerable<string> ListOfVehicles { get; set; }

    }
}