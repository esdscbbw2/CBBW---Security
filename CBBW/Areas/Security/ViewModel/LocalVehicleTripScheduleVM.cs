using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;

namespace CBBW.Areas.Security.ViewModel
{
    public class LocalVehicleTripScheduleVM
    {
        public LocalVehicleTripScheduleVM()
        {
            this.SCHFromDate = MyCodeHelper.FirstDayOfTheFortNight();
            this.SCHToDate = MyCodeHelper.LastDayOfTheFortNight();
        }
        public DateTime SCHFromDate { get; set; }
        public DateTime SCHToDate { get; set; }
        public string VehicleNo { get; set; }
        public string DriverCodenName { get; set; }
        public List<LocVehSchFromMat> LVSchDtl { get; set; }
        public string CallBackUrl { get; set; }
        public int IsSaveVisible { get; set; }
        public IEnumerable<CustomComboOptions> ListofDrivers { get; set; }
        public string NoteNo { get; set; }
        public int IsBackDenied { get; set; }
    }
    public class LocalVehicleTripScheduleEditVM 
    {
        public string NoteNo { get; set; }
        public List<LTSDriVerChange> DriverList { get; set; }
    }
}