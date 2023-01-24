using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Master
{
    public class VTStatement
    {
        public int EligibleVT { get; set; }
        public int ProvidedVT { get; set; }
        public string CStatement { get; set; }
        public bool AuthEmp { get; set; }
    }
}
