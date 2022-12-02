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
   
}
