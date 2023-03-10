using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class LWInnerPageVM
    {
        public string NoteNumber { get; set; }
        public int DefaultPersonID { get; set; }
        public bool IsOffline { get; set; }
        public DateTime SchFromDate { get; set; }
        public DateTime SchToDate { get; set; }
        public List<EntryIIPersons> PersonDetails { get; set; }
        public List<LocationWisePersons> PersonDateWiseDetails { get; set; }
        public VehicleAllotmentDetails VehicleDetails { get; set; }
        public List<RFID> RFIDCardList { get; set; }
        public string RFIDCardIn { get; set; }
        public string RFIDCardOut { get; set; }
        public int KMIn { get; set; }
        public int KMOut { get; set; }
        public int IsManagementPerson { get; set; }
        public int IsBranchVisit { get; set; }
    }
}