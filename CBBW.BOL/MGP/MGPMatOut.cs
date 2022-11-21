using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPMatOut: MGPNote
    {
        public int ID { get; set; }
        public string VehicleNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationText { get; set; }
        public int TripType { get; set; }
        public string ToLocationCodenName { get; set; }
        public bool CarryingOutMaterial { get; set; }
        public float LoadPercentage { get; set; }
        public string RFID { get; set; }
        public DateTime SchFromDate { get; set; }
        public DateTime ActualTripOutDate { get; set; }
        public DateTime ActualTripOutTime { get; set; }
        public int KMSOut { get; set; }
        public string Remarks { get; set; }
        public int CreateID { get; set; }
    }
}
