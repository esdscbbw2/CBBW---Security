using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.BIL
{
   public class TADARuleData
    {
       
		
		public string NoteNumber { get; set; }
        public int EmployeeNumber { get; set; }
		public int CentreCode { get; set; }
		public int ZoneCode { get; set; }
		public int MinHoursForHalfDA { get; set; }
		public int MinKmsForDA { get; set; }
		public bool LodgingExpOnCompAcco { get; set; }
		public bool LocalConvEligibility { get; set; }
		public bool DepuStaffDAEligibility { get; set; }
		public bool ExtraDAApplicability { get; set; }
		public bool IsDAActualSpend { get; set; }
		public bool IsLodgingAllowed { get; set; }
		public int DAPerDay { get; set; }
		public int MaxLodgingExp { get; set; }
		public int MaxLocalConv { get; set; }
		public DateTime ActualTourInDate { get; set; }
		public DateTime ActualTourOutDate { get; set; }
		public string ActualTourOutTime { get; set; }
		public string ActualTourInTime { get; set; }
		public string ActualTourInDatestr { get; set; }
		public string ActualTourOutDatestr { get; set; }
		public int TotalNoOfDays { get; set; }
	    public int DAAmount { get; set; }
		public int DADeducted{ get; set; }
		public int EAmount { get; set; }
		public string DesginationCodeName { get; set; }
		public string PersonType { get; set; }
		public string PurposeOfVisit { get; set; }
		public int status { get; set; }

		public float TAAmount { get; set; }
		public float LocalConveyance { get; set; }
		public float Lodging { get; set; }
		public float TotalExpenses { get; set; }
		public bool IsVehicleProvided { get; set; }
		public bool TADADenied { get; set; }
		
		public string TourFromDateNTime { get; set; }
		public string TourToDateNTime { get; set; }



	}
}
