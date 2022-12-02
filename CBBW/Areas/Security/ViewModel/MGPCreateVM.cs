using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.ViewModel
{
    public class MGPCreateVM
    {
        public string NoteNo { get; set; }
        public int CenterCode { get; set; }
        public string FortheMonthnYear { get; set; }
        public IEnumerable<MGPNote> ListofNotes { get; set; }
    }
}