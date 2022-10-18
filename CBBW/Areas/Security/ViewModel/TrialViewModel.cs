using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;

namespace CBBW.Areas.Security.ViewModel
{
    public class TrialViewModel
    {
        public List<CustomCheckBoxOption> ServiceTypes { get; set; }
        public List<CustomCheckBoxOption> VegetableTypes { get; set; }
    }
}