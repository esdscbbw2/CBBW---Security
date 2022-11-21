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
        public int DriverCode { get; set; }
        public string DriverName { get; set; }
        public string ToCentreTypeCodes { get; set; }
        public string ToCentreCodes { get; set; }
        public string ToCentreTypeCodesStr { get; set; }
        public string ToCentreCodesStr { get; set; }
        public int IsActivetoEdit { get; set; }
        public int EditDriverNo { get; set; }
        public string EditDriverName { get; set; }
    }
}
