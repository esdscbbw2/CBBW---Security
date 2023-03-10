using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TFD;

namespace CBBW.Areas.Security.ViewModel.TFD
{
    public class TourFeedBackDetailsVM
    {
        public string NoteNumber { get; set; }
        public string RefNoteNumber { get; set; }
        public string ConcDept { get; set; }
        public int submitcount { get; set; }
        public int EmployeeNo { get; set; }
        public string ApprovalTime { get; set; }
        public IEnumerable<CustomComboOptions> ConDeptList { get; set; }
        public List<TFDTourFeedBackDetails> tfdfbdetails { get; set; }
        public List<TFDTourFBApproval> tfdfApprovalbdetails { get; set; }
    }
}