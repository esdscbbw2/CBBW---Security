using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EntryII;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class MLInnerPageVM
    {
        public string NoteNumber { get; set; }
        public int DefaultPersonID { get; set; }
        public List<MainLocationPersons> TPDetails { get; set; }
    }
}