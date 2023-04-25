using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class ReportDCDetails
    {
		public long MGPOutInDetailsId { get; set; }

		public int IsOutorIn { get; set; }
	    public string NoteNo { get; set; }
		public string VehicleNo { get; set; }
		public DateTime NoteDate { get; set; }
		public string NoteDateStr { get; set; }
		public int FromLocationCode { get; set; }
		public int FromLocationType { get; set; }
		public string FromLocationName { get; set; }
		public int ToLocationCode { get; set; }
		public int ToLocationType { get; set; }
		public string ToLocationName { get; set; }
		public string CheckFound { get; set; }


	}
}
