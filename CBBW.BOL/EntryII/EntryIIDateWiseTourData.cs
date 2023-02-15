using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class EntryIIDateWiseTourData: EntryIINote
    {
        public string TourCategoryCodes { get; set; }
        public string TourCategoryText { get; set; }
        public string CentreCode { get; set; }
        public string CentreCodeName { get; set; }
        public string BranchCodes { get; set; }
        public string BranchNames { get; set; }
        public DateTime SchFromDate { get; set; }
        public DateTime RequiredTourInDate { get; set; }
        public string RequiredTourInTime { get; set; }
        public int NSEMPunch { get; set; }
        public DateTime NSEMPunchDate { get; set; }
        public string NSEMPunchTime { get; set; }
        public DateTime ActualTourInDate { get; set; }
        public string ActualTourInTime { get; set; }
        public DateTime SchToDate { get; set; }
        public DateTime ActualTourOutDate { get; set; }
        public string ActualTourOutTime { get; set; }
        public int LNMPunch { get; set; }
        public DateTime LNMPunchDate { get; set; }
        public string LNMPunchTime { get; set; }
        public int Status { get; set; }
        public int PersonType { get; set; }
        public string PersonIDName { get; set; }
        public int PersonID { get; set; }
        public int ID { get; set; }
        public bool IsActive { get; set; }
    }
}
