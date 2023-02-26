using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TFD
{
    public class TFDTourFeedBackDetails
    {
        public int ID { get; set; }
        public int IDs { get; set; }
      public string NoteNumber{get;set;}
      public string RefNoteNumber{get;set;}
      public string TourCategoryText { get;set;}
      public string TourCategory{get;set;}
      public string CenterCodeName { get;set;}
      public string CenterCodeNametxt { get;set;}
      public string TourFeedBack{get;set;}
      public bool ActionTakens{get;set;}
      public int ActionTaken { get; set; }
      public int ConcernDeptCode{get;set;}
      public string ConcernDeptName{get;set;}
    }
    public class TFDTourFBApproval
    {
        public int ConcernDeptCode { get; set; }
        public string ConcernDeptName { get; set; }
        public int ID { get; set; }
        public int IDs { get; set; }
        public string NoteNumber { get; set; }

    }
}
