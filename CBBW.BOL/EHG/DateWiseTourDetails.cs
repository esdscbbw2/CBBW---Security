using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EHG
{
    public class DateWiseTourDetails: EHGNote
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string TourCatCodes { get; set; }
        public string TourCatText { get; set; }
        public string CenterCodes { get; set; }
        public string CenterNames { get; set; }
    }
}
