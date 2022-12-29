using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.GVMR
{
    public class GVMRNoteNumber
    {
        public string NoteNo { get; set; }
    }

    public class GVMRNoteList
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string NoteNo { get; set; }
        public string LocationName { get; set; }
        public string VehicleNo { get; set; }
        public string MonthYear { get; set; }
    }
}
