using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.Areas.Security.ViewModel.Rule
{
    public class TADARuleVM
    {
        public TADARuleV2 TADARule { get; set; }
        public List<CustomCheckBoxOption> CategoryList { get; set; }
        public int IsSubmitActive { get; set; }
        public int IsParamBtn { get; set; }
        public int IsTransBtn { get; set; }
    }
}