using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.ViewModel.ETSEdit
{
    public class ETSEditViewVM
    {
        public string NoteDescription { get; set; }
        public EditNoteDetails NoteDetails { get; set; }
        public int DeleteBtn { get; set; }
        public int CBUID { get; set; }
        public string NoteNumber { get; set; }
        public IEnumerable<EditTPDetails> TravelingPersonDetails { get; set; }
        public List<EditDWTDetails> DWTDetailsHistory { get; set; }
        public List<EditDWTDetails> BaseDWTDetailsHistory { get; set; }
        public List<int> EditSequence { get; set; }
        public int MaxRowID { get; set; }
        public int SelectedPersonType { get; set; }
        public int SelectedPersonID { get; set; }
        public string SelectedPersonName { get; set; }
    }
}