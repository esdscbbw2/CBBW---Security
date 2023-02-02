using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMN;

namespace CBBW.Areas.Security.ViewModel.EMN
{
    public class EMNHeaderEntryVM
    {

        public EMNHeaderEntryVM()
        {
            emnHeader = new EMNHeader();
        }
        public string NoteNumber { get; set; }
        public string CenterCodeName { get; set; }
        public string AttachFile { get; set; }
        public int Btnsubmit { get; set; }
        public EMNHeader emnHeader { get; set; }
        public int CenterCode { get; set; }
        public string CenterCodetxt { get; set; }
        public List<EMNTravellingPerson> PersonDtls { get; set; }
        public IEnumerable<CustomComboOptions> ListofCenterCode { get; set; }
        public int CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
    }
}