using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EHG;

namespace CBBW.Areas.Security.ViewModel.EHG
{
    public class EHGTravellingPersonsVM
    {
        public string NoteNumber { get; set; }
        public List<EHGTravelingPersondtls> PersonDtls { get; set; }
    }
}