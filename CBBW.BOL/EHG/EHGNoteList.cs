using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EHG
{
    public class EHGNoteList
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string NoteNumber { get; set; }
        public string NoteNumber2 { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public string CenterName { get; set; }
        public string PurposeOfAllotment { get; set; }
        public int Instructor { get; set; }
        public string VehicleNumber { get; set; }
        public bool IsApproved { get; set; }
        public bool CanDelete { get; set; }
    }
}
