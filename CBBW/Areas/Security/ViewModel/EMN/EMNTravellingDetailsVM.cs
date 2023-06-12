using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EMN;


namespace CBBW.Areas.Security.ViewModel.EMN
{
    public class EMNTravellingDetailsVM
    {
        public EMNTravellingDetailsVM()
        {
            travDetails = new EMNTravellingDetails();
        }
         public string NoteNumber { get; set; }
         public string EmployeeNonName { get; set; }
        public string EmployeeNonNametxt { get; set; }
        public string CenterCodenName { get; set; }
        public string AttachFile { get; set; }
        public int PersonType { get; set; }
        public string FromdateStr { get; set; }
        public string TodateStr { get; set; }
        public string TourFromdateStr { get; set; }
        public int btnSubmit { get; set; }
        public EMNTravellingDetails travDetails { get; set; }
        public List<EMNTravellingDetails> TravellingDetails { get; set; }
        public List<EMNDateWiseTour> dateTour { get; set; }
        public EMNApproveTravDetails appTravData { get; set; }
        public int VehicleTypeProvided { get; set; }
        public string ReasonVehicleProvided { get; set; }
        public bool Tourcat { get; set; }
        public string EmplyoyeeNoList { get; set; }

    }
}