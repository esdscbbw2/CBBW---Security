using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV2
{
    public class CTVNoteList4DT
    {
        public int RowNum { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public string NoteNumber { get; set; }
        public int CentreCode { get; set; }
        public string CentreCodenName { get; set; }
        public string VehicleNumber { get; set; }
        public string ForTheMonthnYear { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsApproved { get; set; }
        public string EntryTime { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
    }
}
