using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EntryII;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class LWInnerPageVM
    {
        public string NoteNumber { get; set; }
        public int DefaultPersonID { get; set; }
        public bool IsOffline { get; set; }
        public List<EntryIIPersons> PersonDetails { get; set; }
        public List<LocationWisePersons> PersonDateWiseDetails { get; set; }

    }
}