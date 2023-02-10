using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EMC
{
    public class EMCNote
    {
        public string NoteNumber { get; set; }
    }

    public class EMCNoteList : EMCNote
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string CenterCodeName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public bool? IsApproved { get; set; }
        public string IsApproveds { get; set; }
        public bool CanDelete { get; set; }

    }
    public class EMCHeader : EMCNoteList
    {
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string EntryTime { get; set; }
        public string AttachFile { get; set; }
        public bool IsEPTour { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public string ApprovedReason { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ApproveDatestr { get; set; }
        public string ApproveTime { get; set; }
        public bool? IsRatified { get; set; }
        public string IsRatifieds { get; set; }
        public string RatifiedReason { get; set; }
        public DateTime RatifiedDate { get; set; }
        public string RatifiedDatestr { get; set; }
        public string RatifiedTime { get; set; }


    }
}
