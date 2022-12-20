using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.Tour;

namespace CBBW.Areas.Security.ViewModel
{
    public class TourRulesViewModel
    {
        public List<int> SelectedServiceTypeIds { get; set; }
        public TourRuleDetails tourruleDetail { get; set; }
    }
}