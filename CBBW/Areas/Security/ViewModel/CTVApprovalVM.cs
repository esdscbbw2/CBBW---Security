using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel
{
    public class CTVApprovalVM
    {
        public string NoteNo { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateTimeofApproval { get; set; }
        public string DisapprovalReason { get; set; }
        public List<NoteNumber> ListofNoteNumbers { get; set; }
    }
}