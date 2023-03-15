using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.MGP
{
    public class MGPReferenceDCDetails
    {
        public string NoteNumber { get; set; }
        public string RefNoteNumber { get; set; }
        public DateTime NoteDate { get; set; }
        public int FromLocationCode { get; set; }
        public string FromLocationText { get; set; }
        public int ToLocationCode { get; set; }
        public string ToLocationText { get; set; }
        public string VehicleNo { get; set; }
        public string NoteDatestr { get; set; }
        public string FindOk { get; set; }


    }
}
