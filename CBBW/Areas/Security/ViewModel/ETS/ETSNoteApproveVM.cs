using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.ETS;

namespace CBBW.Areas.Security.ViewModel.ETS
{
    public class ETSNoteApproveVM
    {

        public ETSNoteApproveVM()
        {
            travdetails = new ETSApproveTravDetails();
            ratified = new ETSRatified();
        }
        public string NoteNumber { get; set; }
        public List<ETSNote> Notelist { get; set; }
        public ETSApproveTravDetails travdetails { get; set; }
        public ETSRatified ratified { get; set; }
        public int IsApprove { get; set; }
        public string ApproveReason { get; set; }
        public int IsRatified { get; set; }
        public string RatifiedReason { get; set; }
        public int btnDisplay { get; set; }

    }
}