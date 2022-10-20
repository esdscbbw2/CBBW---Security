using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV
{
    public class VehicleInfo
    {
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public string VehicleStatus { get; set; }
        public int ServiceDuaration { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
    }
}
