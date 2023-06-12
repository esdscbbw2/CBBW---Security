using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.GVMR
{

    public class GVMRHeader
    {
        public string NoteNo { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public string EntryTime { get; set; }
        public string MonthYear { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public string LocationName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LocationCodes { get; set; }
        public List<GVMRDetails> gvmrDetailslist { get; set; }
        public List<PunchingDetails> punchingdetails { get; set; }
        public bool NoData { get; set; }
    }
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
        public string EntryDateDisplay { get; set; }
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
        public string SchFromDateEntry { get; set; }
        public bool IsRFIDCentres { get; set; }
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
        public int CenterCode { get; set; }
        public string Remark { get; set; }
    }

    public class PunchingDetails
    {
        public int LocationCode { get; set; }
        public int LocationTypeCode { get; set; }
        public DateTime PunchIn { get; set; }
        public DateTime PunchOut { get; set; }
        public DateTime PunchInDate { get; set; }
        public string PunchinTime { get; set; }
        public DateTime PunchOutDate { get; set; }
        public string PunchOutTime { get; set; }
        public bool IsRFIDCentre { get; set; }
        public string PunchOutDatestr { get; set; }
        public string PunchInDatestr { get; set; }
    }

    public class GetGVMRDetailsWithPunching
    {
       
        public List<PunchingDetails> punchingdetails { get; set; }
        public List<GVMRDetails> gvmrdetails { get; set; }
    }
}
