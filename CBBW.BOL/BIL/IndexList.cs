using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.BIL
{
    public class IndexList
    {
        public string NoteNumber { get; set; }
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string RefNoteNumber { get; set; }
        public DateTime RefEntryDate { get; set; }
        public string EmployeeCodeName { get; set; }
        public DateTime TourFromDate { get; set; }
        public DateTime TourToDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime PaymentEntryDate { get; set; }
        public string RefEntryDateDisplay { get; set; }
        public string TourFromDateDisplay { get; set; }
        public string TourToDateDisplay { get; set; }
        public string EntryDateDisplay { get; set; }
        public string ApprovalDateDisplay { get; set; }
        public string PaymentEntryDateDisplay { get; set; }
        public bool? IsApproved { get; set; }
        public string IsApproveds { get; set; }
        public bool CanDelete { get; set; }

    }
}
