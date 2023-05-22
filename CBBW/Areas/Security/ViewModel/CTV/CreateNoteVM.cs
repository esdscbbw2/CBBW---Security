using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel.CTV
{
    public class CreateNoteVM
    {
        public CreateNoteVM()
        {
            this.ForTheMonthYear = DateTime.Today.ToString("MMM yyyy");
            this.FromDate = MyCodeHelper.FirstDayOfTheFortNight();
            this.ToDate = MyCodeHelper.LastDayOfTheFortNight();
        }
        public string NoteNumber { get; set; }
        public string CentreCodeName { get; set; }
        public string ForTheMonthYear { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string VehicleNumber { get; set; }
        public IEnumerable<VehicleNo> ListofVehicles { get; set; }
        public int IsBackDenied { get; set; }
    }
}