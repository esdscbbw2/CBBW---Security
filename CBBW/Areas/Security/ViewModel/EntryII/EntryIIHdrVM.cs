using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class EntryIIHdrVM
    {
        public string NoteNumber { get; set; }
        public EditNoteDetails NoteDetails { get; set; }
        public List<EntryIINote> EntryIINotes { get; set; }
        public int IsBackButtonActive { get; set; }
        public int IsMainLocationEntered { get; set; }
    }
}