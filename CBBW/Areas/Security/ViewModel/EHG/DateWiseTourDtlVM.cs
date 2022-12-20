using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EHG;

namespace CBBW.Areas.Security.ViewModel.EHG
{
    public class DateWiseTourDtlVM
    {
        public string NoteNumber { get; set; }
        public List<DateWiseTourDetails> DateWiseList { get; set; }
    }
}