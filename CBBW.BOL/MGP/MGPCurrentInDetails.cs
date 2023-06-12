using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPCurrentInDetails
    {
		public long ID { get; set; }
	    public string NoteNo { get; set; }
		public string VehicleNo { get; set; }
		public long DriverNo { get; set; }
		public string DriverName { get; set; }
		public int DesignationCode { get; set; }
		public string DesignationText { get; set; }
		public int TripType { get; set; }
		public string TripTypeStr { get; set; }
		public int FromLocationType { get; set; }
		public int FromLocationCode { get; set; }
		public string FromLocationName { get; set; }
		public bool CarryingInMaterial { get; set; }
		public int LoadPercentageIn { get; set; }
		public DateTime SchFromDate { get; set; }
		public bool OutActive { get; set; }
		public bool InActive { get; set; }
		public int KMSOut { get; set; }
		//only for reference
		public string FromschDates { get; set; }
		public string RFIDCard { get; set; }
		public DateTime ActualTripInDate { get; set; }
		public string ActualTripInTime { get; set; }
		public long ActualKmIn { get; set; }
		public long KMRunInTrip { get; set; }
		public string InRemark { get; set; }
		public int RequiredKmIn { get; set; }
		public int ActKmIn { get; set; }
		public int RunningKm { get; set; }
		
	}

	public class Percentage
	{
		public float CapacityPercentage { get; set; }
	}
}
