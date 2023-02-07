using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETSEdit
{
    public class EditNoteNumber 
    {
        public string NoteNumber { get; set; }
    }
    public class EditNoteList: EditNoteNumber
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecord { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public int EmployeeNumber { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLocked { get; set; }
        public int IsIndividualEdit { get; set; }
    }
    public class EditNoteDetails : EditNoteList
    {
        public string EntryTime { get; set; }
        public int POA { get; set; }
        public string POAText { get; set; }
        public int EPTour { get; set; }
        public string EPTourText { get; set; }
        public string IsApprovedDisplay { get; set; }
        public DateTime AppDateTime { get; set; }
        public string AppDateTimeDisplay { get; set; }
        public string NotAppReason { get; set; }
        public bool IsRatified { get; set; }
        public string IsRatifiedDisplay { get; set; }
        public DateTime RetDateTime { get; set; }
        public string RetDateTimeDisplay { get; set; }
        public string RetReason { get; set; }
        public int IsCancelled { get; set; }
        public string AuthorisedEmpNonName { get; set; }
        public string DesgCodenNameOfAE { get; set; }
        public int VehicleType { get; set; }
    }
}
