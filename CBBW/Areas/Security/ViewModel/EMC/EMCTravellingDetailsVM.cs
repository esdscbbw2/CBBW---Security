using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EMC;


namespace CBBW.Areas.Security.ViewModel.EMC
{
    public class EMCTravellingDetailsVM
    {
        public EMCTravellingDetailsVM()
        {
            travDetails = new EMCTravellingDetails();
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
        public EMCTravellingDetails travDetails { get; set; }
        public List<EMCTravellingDetails> TravellingDetails { get; set; }
        public List<EMCDateWiseTour> dateTour { get; set; }
        public EMCApproveTravDetails appTravData { get; set; }
        public int VehicleTypeProvided { get; set; }
        public string ReasonVehicleProvided { get; set; }
        public int IsEPTour { get; set; }
        public bool Tourcat { get; set; }
    }
}