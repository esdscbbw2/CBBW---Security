using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPOutInDetails: MGPNote
    {
        public long ID { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public string VehicleNo { get; set; }
        public long DriverNo { get; set; }
        public string DriverName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationText { get; set; }
        public int TripType { get; set; }
        public string ToLocationCodenName { get; set; }
        public bool CarryingOutMaterial { get; set; }
        public float LoadPercentage { get; set; }
        public string RFIDOut { get; set; }
        public DateTime SchFromDate { get; set; }
        public DateTime ActualTripOutDate { get; set; }
        public string ActualTripOutTime { get; set; }
        public long KMSOut { get; set; }
        public string OutRemarks { get; set; }
        public int CreateID { get; set; }
        public string CallBackUrl { get; set; }
        public IEnumerable<RFID> ListofRFID { get; set; }
        public string FromDate { get; set; }
        public string ATripOutDate { get; set; }

        public bool OutActive { get; set; }
        public bool InActive { get; set; }
        //public DateTime EntryInDate { get; set; }
        //public string EntryInTime { get; set; }
        //public string RFIDCardIn { get; set; }
        //public DateTime ActualTripInDate { get; set; }
        //public string ActualTripInTime { get; set; }
        //public int RequiredKmIn { get; set; }
        //public int ActualKmIn { get; set; }
        //public int KMRunInTrip { get; set; }
        //public string RemarkIn { get; set; }


    }
}
