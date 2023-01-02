﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;

namespace CBBW.BOL.Tour
{
    public class TourRuleSaveInfo : TourRule 
    {
        public float PublicTranDelay_HalfDA { get; set; }
        public float OtherTranDelay_HalfDA { get; set; }
        public float PublicTranDelay_FullDA { get; set; }
        public float OtherTranDelay_FullDA { get; set; }
        public string NightPunch_From { get; set; }
        public string NightPunch_To { get; set; }
        public string EarlyMorningPunch_From { get; set; }
        public string EarlyMorningPunch_To { get; set; }
        public int MaxDayAllowed { get; set; }
        public float MaxTraveltime_ComVeh_50km { get; set; }
        public float MaxTraveltime_PubTran_50km { get; set; }
        public float GracePeriod_200km { get; set; }
        public bool MinutesGracePeriodAllowed { get; set; }
        public bool LICAllowTour { get; set; }
        public string ServiceTypeCodes { get; set; }
        public string ServiceTypeTexts { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class TourRuleDetails : TourRuleSaveInfo    {
        public bool ReadRule1 { get; set; }
        public bool ReadRule2 { get; set; }
        public bool ReadRule3 { get; set; }
        public bool ReadRule4 { get; set; }
        public bool ReadRule5 { get; set; }
        public List<CustomCheckBoxOption> ServiceTypes { get; set; }
        public List<int> SelectedServiceTypeIds { get; set; }
        public string CallBackUrl { get; set; }
        public string MinDate { get; set; }
        public string MaxDate { get; set; }

    }
    public class TourRuleServiceTypes
    {
        public List<ServiceTypeSelector> RuleServiceTypeList { get; set; }
        public List<CustomCheckBoxOption> MasterServiceTypeList { get; set; }
    }
    public class ServiceTypeSelector
    {
        public string ServiceTypeCodes { get; set; }
        public string ServiceTypeTexts { get; set; }
    }

}
