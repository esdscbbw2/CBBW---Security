using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.GVMR
{
   public class GVMRDetails
    {
        public long Gvmrid { get; set; }
        public long MGPId { get; set; }
        public string NoteNo { get; set; }
        public DateTime SchFromDate { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public string MonthYear { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public int LocationType { get; set; }
        public int LocationCode { get; set; }
        public string LocationName { get; set; }
        public string ActualInRFIDCard { get; set; }
        public DateTime ActualTripInDate { get; set; }
        public string ActualTripInTime{ get; set; }
        public long ActualTripInKM { get; set; }
        public string ActualOutRFIDCard { get; set; }
        public DateTime ActualTripOutDate { get; set; }
        public string ActualTripOutTime { get; set; }
        public long ActualTripOutKM { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public DateTime SchToDate { get; set; }
        public string ActualTripInDateDisplay { get; set; }
        public string ActualTripOutDateDisplay { get; set; }
      
        public string ToDateVal { get; set; }
        public string SchFromDateDisplay { get; set; }
    }
    public class GVMRDataSave
    {
        public long Gvmrid { get; set; }
        public string NoteNo { get; set; }
        public string ActualInRFIDCard { get; set; }
        public DateTime ActualTripInDate { get; set; }
        public string ActualTripInTime { get; set; }
        public long ActualTripInKM { get; set; }
        public string ActualOutRFIDCard { get; set; }
        public DateTime ActualTripOutDate { get; set; }
        public string ActualTripOutTime { get; set; }
        public long ActualTripOutKM { get; set; }
        public string Remark { get; set; }
    }
}
