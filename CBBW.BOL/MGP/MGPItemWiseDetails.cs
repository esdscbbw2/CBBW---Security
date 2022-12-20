using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    // Getting data for Out details in Item Wise Details using NoteNo(For New Data insert)
    public class MGPItemWiseDetails
    {
        public string NoteNumber { get; set; }
        public int ItemTypeCode { get; set; }
        public string ItemTypeText { get; set; }
        public int ItemCode { get; set; }
        public string ItemText { get; set; }
        public int UOMCode { get; set; }
        public string UOMText { get; set; }
        public int QuantityBag { get; set; }
        public int QuantityKg { get; set; }

    }
}
