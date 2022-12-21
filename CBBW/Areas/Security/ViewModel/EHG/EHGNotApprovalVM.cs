using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EHG;

namespace CBBW.Areas.Security.ViewModel.EHG
{
    public class EHGNotApprovalVM
    {
        public string NoteNumber { get; set; }
        public List<EHGNote> NoteList { get; set; }
        public int AppStatus { get; set; }
        public string ReasonForDisApproval { get; set; }

    }
}