using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV
{
    public class TripScheduleHdr : TripSchedule
    {
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public int FortheMonth { get; set; }
        public int FortheYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime ToDate { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public bool IsActive { get; set; }
        public string TripPurpose { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public string ReasonForDisApproval { get; set; }
        public IEnumerable<VehicleNo> ListofVehicles { get; set; }
        public string CentreCodenName { get; set; }
        public string FortheMonthnYear { get; set; }
        public string DriverNonName { get; set; }
        public int IsOTSActivated { get; set; }
        public int IsSubmitted { get; set; }
        public int IsOTSSaved { get; set; }

    }    
}
