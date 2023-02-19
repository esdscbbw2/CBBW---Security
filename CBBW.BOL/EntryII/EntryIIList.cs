using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class EntryIIList
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecord { get; set; }
        public string NoteNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public int CentreCode { get; set; }
        public string CentreCodeName { get; set; }
        public string EmpNo { get; set; }
        public string VehicleNo { get; set; }


    }
}
