using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TFD;

namespace CBBW.Areas.Security.ViewModel.TFD
{

    public class TFDHdrVM
    {
        public TFDHdrVM()
        {
            tfdHdr = new TFDHdr();
        }
        public string NoteNumber { get; set; }
        public int EmployeeNo { get; set; }
        public int submitcount { get; set; }
        public List<TFDNote> Notelist { get; set; }
        public List<CustomComboOptions> AuthEmp { get; set; }
        public TFDHdr tfdHdr { get; set; }
        public List<TFDTravellingPerson> TfdTP { get; set; }
        public int CanDelete { get; set; }
        public int CBUID { get; set; }
        public bool IsApprove { get; set; }
        public int IsApproves { get; set; }
        public string ApproveReason { get; set; }
        public List<TFDDateWiseTourData> TFdDateWise { get; set; }

    }
}