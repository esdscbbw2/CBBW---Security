using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV
{
    public class CTVHdrDtl
    {
        public TripScheduleHdr SchHdrData { get; set; }
        public IEnumerable<LocVehSchFromMat> SchDetailList { get; set; }
    }
}
