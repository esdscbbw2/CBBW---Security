using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EMN
{
   public class EMNTravellingPerson
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
		public int TADADenied { get; set; }
		public bool TADADenieds { get; set; }
		public bool IsActive { get; set; }
		public int BtnSubmit { get; set; }
	}
}
