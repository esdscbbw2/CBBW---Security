using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TADA
{
    public class TADARuleForListing 
    {
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateDisplay { get; set; }
        public string RuleStatus { get; set; }
    }
    public class TADARuleV2: TADARuleForListing
    {
        public int MinHoursForHalfDA { get; set; }
        public int MinKmsForDA { get; set; }
        public bool LodgingExpOnCompAcco { get; set; }
        public bool LocalConvEligibility { get; set; }
        public bool DepuStaffDAEligibility { get; set; }
        public bool ExtraDAApplicability { get; set; }
        public bool IsActive { get; set; }
        public string CategoryIds { get; set; }
        public string CategoryText { get; set; }
        public string CompanyTransIDs { get; set; }
        public string PublicTransIDs { get; set; }
        public string PubTransClassIDs { get; set; }
        public bool IsDAActualSpend { get; set; }
        public bool IsLodgingAllowed { get; set; }
        public int Metro_DAPerDay { get; set; }
        public int City_DAPerDay { get; set; }
        public int Town_DAPerDay { get; set; }
        public int Metro_MaxLodgingExp { get; set; }
        public int City_MaxLodgingExp { get; set; }
        public int Town_MaxLodgingExp { get; set; }
        public int Metro_MaxLocalConv { get; set; }
        public int City_MaxLocalConv { get; set; }
        public int Town_MaxLocalConv { get; set; }
    }
}
