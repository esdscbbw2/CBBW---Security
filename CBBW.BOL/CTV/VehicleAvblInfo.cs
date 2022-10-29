using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;

namespace CBBW.BOL.CTV
{
    public class VehicleAvblInfo
    {
        public bool IsSlotAvbl { get; set; }
        public string Msg { get; set; }
        public IEnumerable<CustomDateRange> SlotsBooked { get; set; }
        
    }
}
