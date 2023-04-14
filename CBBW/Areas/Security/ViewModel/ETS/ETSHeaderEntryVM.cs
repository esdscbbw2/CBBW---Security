using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.ETS;

namespace CBBW.Areas.Security.ViewModel.ETS
{
    public class ETSHeaderEntryVM
    {

        public ETSHeaderEntryVM()
        {
            etsHeader = new ETSHeader();
        }
        public string NoteNumber { get; set; }
        public string CenterCodeName { get; set; }
        public string AttachFile { get; set; }
        public int Btnsubmit { get; set; }
        public ETSHeader etsHeader { get; set; }
        public List<ETSTravellingPerson> PersonDtls { get; set; }

        public int CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
        public bool TourCatstatus { get; set; }
    }
}