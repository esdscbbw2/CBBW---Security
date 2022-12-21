using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EHG
{
    public class VehicleAllotmentDetails: EHGNote
    {
        public int AuthorisedEmpNumber { get; set; }
        public string AuthorisedEmpName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationText { get; set; }
        public int MaterialStatus { get; set; }
        public int VehicleBelongsTo { get; set; }
        public string VehicleType { get; set; }
        public string VehicleNumber { get; set; }
        public string ModelName { get; set; }
        public int DriverNumber { get; set; }
        public string DriverName { get; set; }
        public int KMOut { get; set; }
        public int KMIn { get; set; }
        public string OtherVehicleNumber { get; set; }
        public string OtherVehicleModelName { get; set; }
        public bool IsActive { get; set; }
        public string VehicleBelongsToText { get; set; }
    }
}
