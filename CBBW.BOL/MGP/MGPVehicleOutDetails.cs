using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPVehicleOutDetails
    {

        public string NoteNumber { get; set; }
        public int DriverNo { get; set; }
        public string Drivername { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationName { get; set; }
        public int TripType { get; set; }
        public string TripTypeStr { get; set; }
        public string ToLocationCodeName { get; set; }

        public bool CarryingOutMat { get; set; }
        public int LoadPercentage { get; set; }
        public DateTime SchFromDate { get; set; }
        public int KMOUT { get; set; }
        public string VehicleNumber { get; set; }
        public string RFIDCard { get; set; }
        public DateTime ActualTripOutDate { get; set; }
        public string ActualTripOutTime { get; set; }
        public string OutRemarks { get; set; }
        public int ToLocationCode { get; set; }
        public string SchFromDatestr { get; set; }
        //public string VehicleNumber { get; set; }
        //public int FromCentreCode { get; set; }
        //public string FromCenterName { get; set; }
        //public string ToCentreCodeName { get; set; }
        //public int Distance { get; set; }
        //public DateTime SCHFromDate { get; set; }
        //public DateTime SCHTodate { get; set; }
        //public bool CarryingOutMaterialStat { get; set; }
        //public int LoadPercentage { get; set; }
        //public int KMOut { get; set; }
        //public int EditDriverNo { get; set; }
        //public string EditDriverName { get; set; }


    }
    public class MGPOutSave
    {
        public string NoteNumber { get; set; }
        public int DriverNo { get; set; }
        public string Drivername { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationName { get; set; }
        public int TripType { get; set; }
        public string TripTypeStr { get; set; }
        public string ToLocationCodeName { get; set; }
        public bool CarryingOutMat { get; set; }
        public int LoadPercentage { get; set; }
        public DateTime SchFromDate { get; set; }
        public int KMOUT { get; set; }
        public string VehicleNumber { get; set; }
        public string RFIDCard { get; set; }
        public DateTime ActualTripOutDate { get; set; }
        public string ActualTripOutTime { get; set; }
        public string OutRemarks { get; set; }
    }


    public class MGPInSave
    {
        public long ID { get; set; }
        public string NoteNumber { get; set; }
        public string RFIDCardIn { get; set; }
        public int FromLocationType { get; set; }
        public int FromLocationCode { get; set; }
        public string FromLocationName { get; set; }
        public bool CarryingInMaterial { get; set; }
        public float LoadPercentageIn { get; set; }
        public DateTime ActualTripInDate { get; set; }
        public string ActualTripInTime { get; set; }
        public long RequiredKmIn { get; set; }
        public long ActualKmIn { get; set; }
        public long KMRunInTrip { get; set; }

        public string RemarkIn { get; set; }
    }
   
}
