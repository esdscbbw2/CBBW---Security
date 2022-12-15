using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPNote
    {
        public string NoteNo { get; set; }
    }
    public class MGPNotes : MGPNote
    {
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public int CenterCode { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public string FortheMonthnYear { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<MGPNote> ListofNotes { get; set; }

        public string CallBackUrl { get; set; }
        public int ISSubmitActive { get; set; }



    }

    public class ButtonActive
    {
       public bool OutButtonActive { get; set; } 
       public bool InButtonActive { get; set; }
    }
}
