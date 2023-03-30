using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel
{
    public class OtherTripSchDisplayVM
    {
        public string NoteNumber { get; set; }
        public string TripPurpose { get; set; }
        public IEnumerable<LocVehSchFromMat> SchDetailList { get; set; }
        public IEnumerable<LocVehSchFromMat> SchDetailEntryList { get; set; }
        public string CallBackUrl { get; set; }
    }
}