using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.BIL
{
    public class TADAReport
    {
        public DateTime ActualTourInDate { get; set; }
		public string ActualTourInDatestr { get; set; }
		public DateTime ActualTourOutDate { get; set; }
		public string ActualTourOutDatestr { get; set; }
		public string TourCategoryCodesName { get; set; }
		public string LocationCodeName { get; set; }
		public string ZoneCodeName { get; set; }
		public int DAPerDay { get; set; }
		public int LocalConvAmount { get; set; }
		public int LodgingAmount { get; set; }
		public int TourCategoryCode { get; set; }
		public string TourFeedBack { get; set; }
		public bool TourActions { get; set; }
	}
}
