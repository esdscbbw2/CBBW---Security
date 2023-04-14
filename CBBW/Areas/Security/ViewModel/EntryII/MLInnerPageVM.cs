using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.ViewModel.EntryII
{
    public class MLInnerPageVM
    {
        public string NoteNumber { get; set; }
        public int DefaultPersonID { get; set; }
        public bool IsOffline { get; set; }
        public DateTime SchFromDate { get; set; }
        public DateTime SchToDate { get; set; }
        public List<MainLocationPersons> TPDetails { get; set; }
        public string RFIDCardIn { get; set; }
        public string RFIDCardOut { get; set; }
        public int KMIn { get; set; }
        public int KMOut { get; set; }
        public VehicleAllotmentDetails VehicleDetails { get; set; }
        public List<RFID> RFIDCardList { get; set; }
        public bool IsVehicleProvided { get; set; }
        public int RequiredKMIn { get; set; }
        public bool IsOutSaved { get; set; }
        public bool IsInSaved { get; set; }
    }
}