using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class MLPersonsInfo
    {
        public List<MainLocationPersons> PersonInfo { get; set; }
        public List<EmpDate> EmpDatesForPunching { get; set; }
        public List<EmpDate> EmpDatesForReq { get; set; }
    }
}
