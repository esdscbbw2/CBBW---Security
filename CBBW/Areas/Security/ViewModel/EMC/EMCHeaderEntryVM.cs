using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMC;

namespace CBBW.Areas.Security.ViewModel.EMC
{
    public class EMCHeaderEntryVM
    {

        public EMCHeaderEntryVM()
        {
            emnHeader = new EMCHeader();
        }
        public string NoteNumber { get; set; }
        public string CenterCodeName { get; set; }
        public string AttachFile { get; set; }
        public int Btnsubmit { get; set; }
        public EMCHeader emnHeader { get; set; }
        public int CenterCode { get; set; }
        public string CenterCodetxt { get; set; }
        public int IsEPTour { get; set; }
        public List<EMCTravellingPerson> PersonDtls { get; set; }
        public IEnumerable<CustomComboOptions> ListofCenterCode { get; set; }
        public int CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
    }
}