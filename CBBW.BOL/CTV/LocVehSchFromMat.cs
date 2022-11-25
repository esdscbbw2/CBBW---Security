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
        public string ToCentreTypeCodes { get; set; }
        public string ToCenterTypeName { get; set; }
        public int ToCentreCode { get; set; }
        public string ToCentreCodes { get; set; }
        public string ToCenterName { get; set; }
        public float Distance { get; set; }
        public DateTime ToDate { get; set; }
        public string DriverCodenName { get; set; }
        public string ToDateStr { get; set; }
        public string FromDateStr { get; set; }
        public string FromDateStrYMD { get; set; }
        public bool CanSchedule { get; set; }
        public float CalcDays { get; set; }
        public string TripPurpose { get; set; }
        public bool bOtherPlace { get; set; }
        public int IsActivetoEdit { get; set; }
        public int EditDriverNo { get; set; }
        public string EditDriverName { get; set; }
        public int CurrentDriverCode { get; set; }
        public string CurrentDriverName { get; set; }
    }
}
