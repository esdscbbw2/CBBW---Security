using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EntryII;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class DCDetailsVM
    {
        public IEnumerable<EntryIINote> DCNoteList { get; set; }
        public List<MGPReferenceDCDetails> DCNoteDetails { get; set; }
        public List<EntryIIDCDetails> DCNoteDetailsFromEntryII { get; set; }
    }
}