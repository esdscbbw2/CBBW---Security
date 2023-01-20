using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETSEdit
{    
    public class EditDWTDetails: EditNoteNumber
    {
        public DateTime SchFromDate { get; set; }
        public DateTime SchToDate { get; set; }
        public DateTime EditedTourToDate { get; set; }
        public string TourCategoryIds { get; set; }
        public string TourCategoryNames { get; set; }
        public string TourCenterCodeIds { get; set; }
        public string TourCenterNames { get; set; }
        public string BranchCodes { get; set; }
        public string BranchNames { get; set; }
        public int SourceID { get; set; }
        public int PersonType { get; set; }
        public int PersonID { get; set; }
        public string PersonIDnName { get; set; }
        public bool IsEdited { get; set; }
    }
}
