using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using System.ComponentModel.DataAnnotations;

namespace CBBW.BOL.TADA
{
    public class TADARuleDetails : TADARule
    {
        [Required]
        public int MinHoursForHalfDA { get; set; }
        public int MinKmsForDA { get; set; }
        public bool LodgingExpOnCompAcco { get; set; }
        public bool LocalConvEligibility { get; set; }
        public bool DepuStaffDAEligibility { get; set; }
        public bool ExtraDAApplicability { get; set; }
        public TADAParam TADAParam { get; set; }
        public List<CustomCheckBoxOption> Categories { get; set; }
        public List<TADAPubTransOption> PubTranOptions { get; set; }
        public List<CustomCheckBoxOption> CompTranOptions { get; set; }
        public List<int> SelectedCategoryIds { get; set; }
        public List<int> LastSelectedCategoryIds { get; set; }
        public bool mDelete { get; set; }
    }
}
