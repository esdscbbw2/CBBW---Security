using System;
using System.Collections.Generic;
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
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
    }
}
