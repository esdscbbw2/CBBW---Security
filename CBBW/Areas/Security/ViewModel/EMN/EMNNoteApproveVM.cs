using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EMN;
using CBBW.BOL.ETS;

namespace CBBW.Areas.Security.ViewModel.EMN
{
    public class EMNNoteApproveVM
    {

        public EMNNoteApproveVM()
        {
            travdetails = new EMNApproveTravDetails();
            ratified = new EMNRatified();
        }
        public string NoteNumber { get; set; }
        public List<EMNNote> Notelist { get; set; }
        public EMNApproveTravDetails travdetails { get; set; }
        public EMNRatified ratified { get; set; }
        public int IsApprove { get; set; }
        public string ApproveReason { get; set; }
        public int IsRatified { get; set; }
        public string RatifiedReason { get; set; }
        public int btnDisplay { get; set; }

    }
}