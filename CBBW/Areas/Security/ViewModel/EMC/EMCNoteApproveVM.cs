using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EMC;
using CBBW.BOL.ETS;

namespace CBBW.Areas.Security.ViewModel.EMC
{
    public class EMCNoteApproveVM
    {

        public EMCNoteApproveVM()
        {
            travdetails = new EMCApproveTravDetails();
            ratified = new EMCRatified();
        }
        public string NoteNumber { get; set; }
        public List<EMCNote> Notelist { get; set; }
        public EMCApproveTravDetails travdetails { get; set; }
        public EMCRatified ratified { get; set; }
        public int IsApprove { get; set; }
        public string ApproveReason { get; set; }
        public int IsRatified { get; set; }
        public string RatifiedReason { get; set; }
        public int btnDisplay { get; set; }

    }
}