using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EMC
{
    public class EMCDateWiseTour
    {
		public string NoteNumber { get; set; }
		public string DDSchFromDate { get; set; }
		public DateTime SchFromDate { get; set; }
		public DateTime SchToDate { get; set; }
		public string DDSchToDate { get; set; }
		public string TourCategory { get; set; }
		public string CenterName { get; set; }
		public string CenterCodeName { get; set; }
		public string BranchCodeName { get; set; }
		public string CenterCode { get; set; }
		public string TourCategoryId { get; set; }
		public string BranchCode { get; set; }
		public string TourCatText { get; set; }
		public string CenterCodeNametxt { get; set; }
		public string BranchCodeNametxt { get; set; }
		public bool IsActive { get; set; }
		
		public string SchToDatestr { get; set; }

	}
}
