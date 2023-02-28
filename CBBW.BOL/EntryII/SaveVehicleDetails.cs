using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class SaveVehicleDetails
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public string InRFIDCard { get; set; }
        public DateTime ActualTourInDate { get; set; }
        public DateTime ActualTourInTime { get; set; }
        public int KMIn { get; set; }
        public string OutRFIDCard { get; set; }
        public DateTime ActualTourOutDate { get; set; }
        public DateTime ActualTourOutTime { get; set; }
        public int KMOut { get; set; }
        public string VRemarks { get; set; }
        public int RequiredKMIn { get; set; }
        public bool IsCarryingMaterial { get; set; }

    }
}
