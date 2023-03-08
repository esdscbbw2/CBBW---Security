using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class EntryIIInnerView
    {
        public string NoteNumber { get; set; }
        public int DefaultPersonID { get; set; }
        public SaveVehicleDetails VehicleDetail { get; set; }
        public List<SaveTPDetails> Persons { get; set; }
        public List<SaveTPDWDetails> DWDetails { get; set; }
    }
}
