using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EntryII;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class SaveLWInnerPageVM
    {
        public string NoteNumber { get; set; }
        public List<SaveTPDetails> TPersons { get; set; }
        public List<SaveTPDWDetails> DateWiseDetails { get; set; }
        public List<SaveVehicleDetails> VDetails { get; set; }

    }
}