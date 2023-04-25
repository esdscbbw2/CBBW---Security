using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class PrintHeader
    {
        public ReportHdr ReportHdr { get; set; }
        public List<ReportInOutDetails> reportInOutdetails { get; set; }
        public List<ReportDCDetails> reportDCdetails { get; set; }
        public List<MGPItemWiseDetails> reportItemWisedetails { get; set; }
    }
    public class ReportHdr
    {
        public string MonthName { get; set; }
        public string NoteNumber { get; set; }
        public  DateTime EntryDate { get; set; }
        public string EntryDateStr { get; set; }
        public string EntryTime { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public int FortheMonth { get; set; }
        public int FortheYear { get; set; }
        public string Vehicleno { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public int   DriverNo { get; set; }
        public string  DriverName { get; set; }
        public string  TripType { get; set; }
        public DateTime  ActualTripOutDate { get; set; }
        public string ActualTripOutDateStr { get; set; }
        public string  ActualTripOutTime { get; set; }
        public int KMSOut { get; set; }
        public bool IsMatOutActive { get; set; }
        public bool IsMatInActive { get; set; }
       
    }
}
