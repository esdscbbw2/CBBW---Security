using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;

namespace CBBW.BOL.ETSEdit
{
    public class EHGEditDetails 
    {
        public List<EditDWTDetails> DateWiseTourHistory { get; set; }
        public List<EditDWTDetails> DateWiseTourCurrent { get; set; }
    }
    public class EditDWTDetails: EditNoteNumber
    {
        public DateTime SchFromDate { get; set; }
        public string SchFromDateDisplay { get; set; }
        public DateTime SchToDate { get; set; }
        public string SchToDateDisplay { get; set; }
        public DateTime EditedTourToDate { get; set; }
        public string EditedTourToDateDisplay { get; set; }
        public string EditedTourToDateStr { get; set; }
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
        public int EditSL { get; set; }
        public int EditTag { get; set; }
        public string EditTagText { get; set; }
        public string EditReason { get; set; }
        public List<CustomComboOptions> TourCategories { get; set; }
        public int IsEditable { get; set; }
    }
}
