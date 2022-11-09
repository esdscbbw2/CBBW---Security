using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel
{
    public class LocalVehicleTripScheduleVM
    {
        public DateTime SCHFromDate { get; set; }
        public DateTime SCHToDate { get; set; }
        public string VehicleNo { get; set; }
        public string DriverCodenName { get; set; }
        public List<LocVehSchFromMat> LVSchDtl { get; set; }
        public string CallBackUrl { get; set; }
        public int IsSaveVisible { get; set; }
    }
}