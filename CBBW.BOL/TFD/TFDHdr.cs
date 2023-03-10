using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TFD
{
 
    public class TFDHdr
    {
        public string RefNoteNumber { get; set; }
        public string NoteNumber { get; set; }
        public string EntryTime { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDatestr { get; set; }
        public DateTime EntEntryDate { get; set; }
        public string EntEntryTime { get; set; }
        public DateTime TourFromDate { get; set; }
        public DateTime TourToDate { get; set; }
        public string PurposeOfVisit { get; set; }
        public int UserID { get; set; }
        public string CenterName { get; set; }
        public int CenterCode { get; set; }
        public string UserName { get; set; }
        public string CenterCodeName { get; set; }
        public string EntEntryDatestr { get; set; }
        public string TourFromDatestr { get; set; }
        public string TourToDatestr { get; set; }
        public int status { get; set; }
        public int AuthEmployeeCode { get; set; }
        public string AuthEmployeeName { get; set; }
        public string ApprovalDTstr { get; set; }
        public string ApprovalTime { get; set; }
        public DateTime ApprovalDT { get; set; }
        public bool IsApproved { get; set; }
        public string Remark { get; set; }

    }
    public class TFDNote
    {
        public string NoteNumber { get; set; }
    }

    public class TFDNoteList : TFDNote
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string CenterCodeName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public bool? IsApproved { get; set; }
        public string IsApproveds { get; set; }
        public bool CanDelete { get; set; }

    }
}
