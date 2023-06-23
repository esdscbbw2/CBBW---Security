using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV
{
    public class VehicleBasicInfo : VehicleNo
    {
        public string VehicleNature { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public string VehicleStatus { get; set; }
        public int ServiceDuaration { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public int KMIn { get; set; }
        public int KMOut { get; set; }
    }
    public class VehicleInfo : VehicleBasicInfo
    {        
        public int DriverNo { get; set; }
        public string DriverName { get; set; }       
        
        public int LocalTripRecords { get; set; }        
        public string DriverNonName { get; set; }
        public bool IsSlotAvbl { get; set; }
    }
    public class VehicleNo
    {
        public string VehicleNumber { get; set; }
    }
}
