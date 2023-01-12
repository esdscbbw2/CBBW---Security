using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETS
{
    public class ETSNote
    {
        public string NoteNumber { get; set; }
    }
    public class ETSNoteList : ETSNote
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string CenterCodeName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public bool IsApproved { get; set; }
        public bool CanDelete { get; set; }

    }
    public class ETSHeader: ETSNoteList
    {
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string EntryTime { get; set; }
        public string AttachFile { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }

    }

    
}
