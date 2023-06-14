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
        public string NoteNumber2 { get; set; }
        public List<EHGNote> NoteList { get; set; }
        public int AppStatus { get; set; }
        public string ReasonForDisApproval { get; set; }
        public EHGHeader Header { get; set; }
        public List<EHGTravelingPersondtlsForManagement> TPDetails { get; set; }
        public int BackBtnActive { get; set; }
        public int DWTActive { get; set; }
        public int VAActive { get; set; }
        public string DocFileName { get; set; }
    }
}