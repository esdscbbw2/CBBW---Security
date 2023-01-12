using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EHG;

namespace CBBW.Areas.Security.ViewModel.EHG
{
    public class EHGHeaderDisplayVM
    {
        public string NoteNumber { get; set; }
        public EHGHeader HeaderData { get; set; }
        public List<EHGTravelingPersondtlsForManagement> TPDetails { get; set; }
        public bool CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
        public int DeleteBtn { get; set; }
    }
}