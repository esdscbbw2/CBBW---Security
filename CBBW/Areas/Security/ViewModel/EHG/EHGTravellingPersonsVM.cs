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
        public int VehicleType { get; set; }
        public int PurposeOfAllotment { get; set; }
        public int MaterialStatus { get; set; }
        public int Instructor { get; set; }
        public string AuthorisedEmployeeName { get; set; }
        public string InstructorName { get; set; }
        public string DocFileName { get; set; }
        public List<EHGTravelingPersondtls> PersonDtls { get; set; }
    }
}