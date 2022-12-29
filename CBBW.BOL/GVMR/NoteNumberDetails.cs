using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.GVMR
{
    public class NoteNumberDetails
    {

        public string NoteNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public int CenterCode { get; set; }
        public int FortheMonth { get; set; }
        public int FortheYear { get; set; }
        public DateTime FromDate { get; set; }
        public string Vehicleno { get; set; }
        public int DriverNo { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public bool OutActive { get; set; }
        public bool InActive { get; set; }
  
    }
}
