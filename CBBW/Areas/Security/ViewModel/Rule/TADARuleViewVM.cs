using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.Areas.Security.ViewModel.Rule
{
    public class TADARuleViewVM
    {
        public DateTime EffectiveDate { get; set; }
        public TADARuleV2 TADARule { get; set; }
        public List<CustomComboOptionsWithString> CategoryList { get; set; }
        public string SelectedCategory { get; set; }
        public bool IsDelete { get; set; }
        public int IsBackButton { get; set; }
    }
}