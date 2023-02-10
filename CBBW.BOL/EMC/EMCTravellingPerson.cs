using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EMC
{
   public class EMCTravellingPerson: TPEPNote
	{
		public string NoteNumber { get; set; }
		public int CenterCode { get; set; }
		public string CenterCodeName { get; set; }
		public int PersonType { get; set; }
		public string PersonTypeName { get; set; }
		public int EmployeeNo { get; set; }
		public string EmployeeNonNamecmb { get; set; }
		public string EmployeeNonName { get; set; }
		public int DesignationCode { get; set; }
		public string DesignationCodenName { get; set; }
		public int EligibleVehicleType { get; set; }
		public string EligibleVehicleTypeName { get; set; }
		//public string EPNoteNumber { get; set; }
		//public DateTime NoteDate { get; set; }
		public int TADADenied { get; set; }
		public bool TADADenieds { get; set; }
		public bool IsActive { get; set; }
		public int BtnSubmit { get; set; }
	}
	public class TPEPNote
	{
		public string EPNoteNumber { get; set; }
		public DateTime NoteDate { get; set; }
		public string NoteDatestr { get; set; }
	}
	}
