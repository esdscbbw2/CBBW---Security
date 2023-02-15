using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class EntryIINote 
    {
        public string NoteNumber { get; set; }
    }
    public class EntryIIDCDetails: EntryIINote
    {
        public DateTime NoteDate { get; set; }
        public int FromLocationCode { get; set; }
        public string FromLocationCodeName { get; set; }
        public int ToLocationCode { get; set; }
        public string ToLocationCodeName { get; set; }
        public string NoteRemarks { get; set; }
    }
    public class EntryIIItemDetails : EntryIINote 
    {
        public int ItemTypeCode { get; set; }
        public string ItemTypeText { get; set; }
        public int ItemCode { get; set; }
        public string ItemCodeName { get; set; }
        public string UOM { get; set; }
        public int ItemQtyinBags { get; set; }
        public int ItemQtyInKgs { get; set; }
    }


}
