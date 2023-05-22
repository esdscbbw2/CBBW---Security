using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;

namespace CBBW.Areas.Security.ViewModel.CTV
{
    public class AddOtherTripVM
    {
        public string NoteNumber { get; set; }
        public string VehicleNumber { get; set; }
        public int IsBackDenied { get; set; }
        public IEnumerable<CustomComboOptions> LocationTypes { get; set; }
    }
}