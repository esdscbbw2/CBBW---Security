using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV2
{
    public class CTVOtherTrip
    {
        public string NoteNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string TripPurpose { get; set; }
        public bool IsOtherPlaceStatement { get; set; }
        public List<CTVOtherTripDtls> TripDetails { get; set; }
    }
    public class CTVOtherTripDtls 
    {        
        public string FromDate { get; set; }
        public string FromTime { get; set; }
        public string ToDate { get; set; }
        public int FromLocationTypeCode { get; set; }
        public string FromLocationTypeText { get; set; }
        public int FromLocationCode { get; set; }
        public string FromLocationText { get; set; }
        public string ToLocationTypeCodes { get; set; }
        public string ToLocationTypeText { get; set; }
        public string ToLocationCodes { get; set; }
        public string ToLocationText { get; set; }
        public string FromDateDisplay { get; set; }
        public string ToDateDisplay { get; set; }
    }
}
