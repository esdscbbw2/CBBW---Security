using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV
{
    public class OthTripTemp
    {
        public string FromDate { get; set; }
        public string FromTime { get; set; }
        public int FromCentreTypeCode { get; set; }
        public int FromCentreCode { get; set; }
        public int ToCentreTypeCode { get; set; }
        public int ToCentreCode { get; set; }
        public string ToDate { get; set; }
    }
}
