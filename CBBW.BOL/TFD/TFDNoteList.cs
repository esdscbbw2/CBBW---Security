using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TFD
{
    public class TFDNoteList : TFDNote
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string CenterCodeName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public bool? IsApproved { get; set; }
        public string IsApproveds { get; set; }
        public bool CanDelete { get; set; }
        public string RefNoteNumber { get; set; }
        public DateTime EntEntryDate { get; set; }
        public string  AuthEmployeeName { get; set; }
        public string EntEntryDateDisplay { get; set; }

    }
}
