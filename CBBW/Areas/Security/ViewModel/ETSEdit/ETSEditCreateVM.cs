using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.ViewModel.ETSEdit
{
    public class ETSEditCreateVM
    {
        public ETSEditCreateVM()
        {
            EHGMaster master = EHGMaster.GetInstance;
            TourCategoryForEdit = master.TourCategoryForEdit;
            EditTags = master.EditTag;
            IndividualEditTags = master.IndividualEditTag;
        }  
        public string NoteNumber { get; set; }
        public int EditTag { get; set; }
        public string PurposeOfEdit { get; set; }
        public IEnumerable<EditNoteNumber> ToBeEditNoteList { get; set; }
        public IEnumerable<EditTPDetails> TravelingPersonDetails { get; set; }
        public List<CustomComboOptions> TourCategoryForEdit { get; set; }
        public List<CustomComboOptions> EditTags { get; set; }
        public List<CustomComboOptions> IndividualEditTags { get; set; }
        public List<EditDWTDetails> DWTDetailsHistory { get; set; }
        public List<EditDWTDetails> DWTDetailsCurrent { get; set; }
        public List<int> EditSequence { get; set; }
        public int btnTourEdit { get; set; }
        public int btnIndividualEdit { get; set; }
        public DateTime ExtensionFromDate { get; set; }
        public int IsExtensionAllowed { get; set; }
        public int MaxSourceID { get; set; }
        public int backbtnactive { get; set; }
        public int SelectedPersonType { get; set; }
        public int SelectedPersonID { get; set; }
        public string SelectedPersonName { get; set; }
    }
}