using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPHistoryDCDetails
    {
        public string NoteNumber { get; set; }
	    public DateTime NoteDate { get; set; }
		public int FromLocationCode { get; set; }
		public string FromLocationText { get; set; }
		public int ToLocationCode { get; set; }
		public string ToLocationText { get; set; }
		public string VehicleNo { get; set; }
		public string CheckFound { get; set; }
		public string NoteDatestr { get; set; }


	}
}
