using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPNote 
    {
        public string NoteNumber { get; set; }
    }
    public class MGPNotes: MGPNote
    {        
        public int CenterCode { get; set; }
        public int CreateEmployeeNo { get; set; }

    }
}
