using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV
{
    public class LocVehSchFromMat
    {
        public string VehicleNumber { get; set; }
        public DateTime FromDate { get; set; }
        public string FromTime { get; set; }
        public int FromCenterTypeCode { get; set; }
        public string FromCenterTypeName { get; set; }
        public int FromCentreCode { get; set; }
        public string FromCenterName { get; set; }
        public int ToCentreTypeCode { get; set; }
        public string ToCenterTypeName { get; set; }
        public int ToCentreCode { get; set; }
        public string ToCenterName { get; set; }
        public float Distance { get; set; }
        public DateTime ToDate { get; set; }
        public string DriverCodenName { get; set; }
        public string ToDateStr { get; set; }
        public string FromDateStr { get; set; }
    }
}
