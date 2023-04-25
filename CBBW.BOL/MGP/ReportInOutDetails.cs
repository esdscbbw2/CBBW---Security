using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class ReportInOutDetails
    {
		public long ID { get; set; }
		public string NoteNo { get; set; }
		public int DriverNo { get; set; }
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
		public bool OutActive { get; set; }
		public DateTime EntryInDate { get; set; }
		public string EntryInTime { get; set; }
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
		public bool InActive { get; set; }
		public bool IsVehicleOut { get; set; }

		public string SchFromDateDisplay { get; set; }
		public string ActualTripOutDateDisplay { get; set; }
		public string EntryInDateDisplay { get; set; }
		public string ActualTripInDateDisplay { get; set; }
	}
}
