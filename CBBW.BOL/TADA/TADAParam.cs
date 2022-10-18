using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TADA
{
    public class TADAParam
    {
        public int ID { get; set; }
        public string ConnectingID { get; set; }
        public bool IsDAActualSpend { get; set; }
        public bool IsLodgingAllowed { get; set; }
        public int Metro_DAPerDay { get; set; }
        public int City_DAPerDay { get; set; }
        public int Town_DAPerDay { get; set; }
        public int Metro_MaxLodgingExp { get; set; }
        public int City_MaxLodgingExp { get; set; }
        public int Town_MaxLodgingExp { get; set; }
        public int Metro_MaxLocalConv { get; set; }
        public int City_MaxLocalConv { get; set; }
        public int Town_MaxLocalConv { get; set; }
        public bool IsLocalConvAllowed { get; set; }
        public bool mDelete { get; set; }
    }
}
