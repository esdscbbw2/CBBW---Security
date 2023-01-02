using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;
using CBBW.BOL.Tour;
using System.Globalization;

namespace CBBW.DAL.DBMapper
{
    public class RulesDBMapper
    {
        public TourRuleSaveInfo Map_TourRuleSaveInfo(DataRow dr) 
        {
            TourRuleSaveInfo result = new TourRuleSaveInfo();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PublicTranDelay_HalfDA"]))
                        result.PublicTranDelay_HalfDA = float.Parse(dr["PublicTranDelay_HalfDA"].ToString());
                    if (!DBNull.Value.Equals(dr["OtherTranDelay_HalfDA"]))
                        result.OtherTranDelay_HalfDA = float.Parse(dr["OtherTranDelay_HalfDA"].ToString());
                    if (!DBNull.Value.Equals(dr["PublicTranDelay_FullDA"]))
                        result.PublicTranDelay_FullDA = float.Parse(dr["PublicTranDelay_FullDA"].ToString());
                    if (!DBNull.Value.Equals(dr["OtherTranDelay_FullDA"]))
                        result.OtherTranDelay_FullDA = float.Parse(dr["OtherTranDelay_FullDA"].ToString());
                    if (!DBNull.Value.Equals(dr["NightPunch_From"]))
                        result.NightPunch_From = dr["NightPunch_From"].ToString();
                    if (!DBNull.Value.Equals(dr["NightPunch_To"]))
                        result.NightPunch_To = dr["NightPunch_To"].ToString();
                    if (!DBNull.Value.Equals(dr["EarlyMorningPunch_From"]))
                        result.EarlyMorningPunch_From = dr["EarlyMorningPunch_From"].ToString();
                    if (!DBNull.Value.Equals(dr["EarlyMorningPunch_To"]))
                        result.EarlyMorningPunch_To = dr["EarlyMorningPunch_To"].ToString();
                    if (!DBNull.Value.Equals(dr["MaxDayAllowed"]))
                        result.MaxDayAllowed = int.Parse(dr["MaxDayAllowed"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxTraveltime_ComVeh_50km"]))
                        result.MaxTraveltime_ComVeh_50km = float.Parse(dr["MaxTraveltime_ComVeh_50km"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxTraveltime_PubTran_50km"]))
                        result.MaxTraveltime_PubTran_50km = float.Parse(dr["MaxTraveltime_PubTran_50km"].ToString());
                    if (!DBNull.Value.Equals(dr["GracePeriod_200km"]))
                        result.GracePeriod_200km = float.Parse(dr["GracePeriod_200km"].ToString());
                    if (!DBNull.Value.Equals(dr["MinutesGracePeriodAllowed"]))
                        result.MinutesGracePeriodAllowed = bool.Parse(dr["MinutesGracePeriodAllowed"].ToString());
                    if (!DBNull.Value.Equals(dr["LICAllowTour"]))
                        result.LICAllowTour = bool.Parse(dr["LICAllowTour"].ToString());
                    if (!DBNull.Value.Equals(dr["ServiceTypeCodes"]))
                        result.ServiceTypeCodes = dr["ServiceTypeCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["ServiceTypeTexts"]))
                        result.ServiceTypeTexts = dr["ServiceTypeTexts"].ToString();
                    if (!DBNull.Value.Equals(dr["CreatedBy"]))
                        result.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                }
            }
            catch { }
            return result;
        }
        public TourRuleServiceTypes Map_TourRuleServiceTypes(DataTable sts,DataTable cco) 
        {
            TourRuleServiceTypes result = new TourRuleServiceTypes();
            try
            {
                List<ServiceTypeSelector> objlist1 = new List<ServiceTypeSelector>();
                List<CustomCheckBoxOption> objlist2 = new List<CustomCheckBoxOption>();
                if (sts != null && sts.Rows.Count > 0)
                {
                    for (int i = 0; i < sts.Rows.Count; i++)
                    {
                        ServiceTypeSelector obj1 = new ServiceTypeSelector();
                        if (!DBNull.Value.Equals(sts.Rows[i]["ServiceTypeCodes"]))
                            obj1.ServiceTypeCodes = sts.Rows[i]["ServiceTypeCodes"].ToString();
                        if (!DBNull.Value.Equals(sts.Rows[i]["ServiceTypeTexts"]))
                            obj1.ServiceTypeTexts = sts.Rows[i]["ServiceTypeTexts"].ToString();
                        objlist1.Add(obj1);
                    }
                }
                if (cco != null && cco.Rows.Count > 0)
                {
                    for (int i = 0; i < cco.Rows.Count; i++)
                    {
                        CustomCheckBoxOption obj2 = new CustomCheckBoxOption();
                        if (!DBNull.Value.Equals(cco.Rows[i]["ID"]))
                            obj2.ID = int.Parse(cco.Rows[i]["ID"].ToString());
                        if (!DBNull.Value.Equals(cco.Rows[i]["DisplayText"]))
                            obj2.DisplayText = cco.Rows[i]["DisplayText"].ToString();
                        if (!DBNull.Value.Equals(cco.Rows[i]["IsSelected"]))
                            obj2.IsSelected = bool.Parse(cco.Rows[i]["IsSelected"].ToString());
                        objlist2.Add(obj2);
                    }
                }
                result.RuleServiceTypeList = objlist1;
                result.MasterServiceTypeList = objlist2;
            }
            catch { }
            return result;
        }
        public TourRuleListData Map_TourRuleListData(DataRow dr)
        {
            TourRuleListData result = new TourRuleListData();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.SL = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.FilteredCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalRecords"]))
                        result.TotalCount = int.Parse(dr["TotalRecords"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    if (result.EffectiveDate <= DateTime.Now)
                        result.IsApplied = true;
                    if (!DBNull.Value.Equals(dr["EffectiveDateSTR"]))
                        result.EffectiveDateDisplay =dr["EffectiveDateSTR"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDateSTR"]))
                        result.EntryDateDisplay = dr["EntryDateSTR"].ToString();
                }
            }
            catch { }
            return result;
        }

        public TourRule Map_TourRule(DataRow dr, int SL)
        {
            TourRule result = new TourRule();
            try
            {
                if (dr != null)
                {
                    result.SL = SL;
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    if (result.EffectiveDate <= DateTime.Now)
                        result.IsApplied = true;
                }
            }
            catch { }
            return result;
        }
        public TourRuleDetails Map_TourRuleDetails(DataRow dr, DataTable dt) 
        {
            TourRuleDetails result = new TourRuleDetails();
            try
            {
                DBResponseMapper _dbResponse = new DBResponseMapper();
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PublicTranDelay_HalfDA"]))
                        result.PublicTranDelay_HalfDA = float.Parse(dr["PublicTranDelay_HalfDA"].ToString());
                    if (!DBNull.Value.Equals(dr["OtherTranDelay_HalfDA"]))
                        result.OtherTranDelay_HalfDA = float.Parse(dr["OtherTranDelay_HalfDA"].ToString());
                    if (!DBNull.Value.Equals(dr["PublicTranDelay_FullDA"]))
                        result.PublicTranDelay_FullDA = float.Parse(dr["PublicTranDelay_FullDA"].ToString());
                    if (!DBNull.Value.Equals(dr["OtherTranDelay_FullDA"]))
                        result.OtherTranDelay_FullDA = float.Parse(dr["OtherTranDelay_FullDA"].ToString());
                    if (!DBNull.Value.Equals(dr["NightPunch_From"]))
                        result.NightPunch_From = dr["NightPunch_From"].ToString();
                    if (!DBNull.Value.Equals(dr["NightPunch_To"]))
                        result.NightPunch_To = dr["NightPunch_To"].ToString();
                    if (!DBNull.Value.Equals(dr["EarlyMorningPunch_From"]))
                        result.EarlyMorningPunch_From = dr["EarlyMorningPunch_From"].ToString();
                    if (!DBNull.Value.Equals(dr["EarlyMorningPunch_To"]))
                        result.EarlyMorningPunch_To = dr["EarlyMorningPunch_To"].ToString();
                    if (!DBNull.Value.Equals(dr["ReadRule1"]))
                        result.ReadRule1 = bool.Parse(dr["ReadRule1"].ToString());
                    if (!DBNull.Value.Equals(dr["ReadRule2"]))
                        result.ReadRule2 = bool.Parse(dr["ReadRule2"].ToString());
                    if (!DBNull.Value.Equals(dr["ReadRule3"]))
                        result.ReadRule3 = bool.Parse(dr["ReadRule3"].ToString());
                    if (!DBNull.Value.Equals(dr["ReadRule4"]))
                        result.ReadRule4 = bool.Parse(dr["ReadRule4"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxDayAllowed"]))
                        result.MaxDayAllowed = int.Parse(dr["MaxDayAllowed"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxTraveltime_ComVeh_50km"]))
                        result.MaxTraveltime_ComVeh_50km = float.Parse(dr["MaxTraveltime_ComVeh_50km"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxTraveltime_PubTran_50km"]))
                        result.MaxTraveltime_PubTran_50km = float.Parse(dr["MaxTraveltime_PubTran_50km"].ToString());
                    if (!DBNull.Value.Equals(dr["GracePeriod_200km"]))
                        result.GracePeriod_200km = float.Parse(dr["GracePeriod_200km"].ToString());
                    
                }
                //-----------------  Service types--------------------------
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<CustomCheckBoxOption> sts = new List<CustomCheckBoxOption>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sts.Add(_dbResponse.Map_CustomCheckBoxOption(dt.Rows[i]));
                    }
                    result.ServiceTypes = sts;
                }
                //-----------------  Service types--------------------------
            }
            catch { }
            return result;
        }
        public TADARule Map_TADARule(DataRow dr,int SL)
        {
            TADARule result = new TADARule();
            try
            {
                if (dr != null)
                {
                    result.SL = SL;
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ConnectingID"]))
                        result.ConnectingID = dr["ConnectingID"].ToString();
                    if (result.EffectiveDate <= DateTime.Now)
                        result.IsApplied = true;                    
                }
            }
            catch { }
            return result;
        }
        public TADARuleDetails Map_TADARuleDetails(DataRow dr,DataTable dtcategories,DataTable dtCompanyTranOptions,DataTable dtPubtranOptions,DataTable dtTADAParam)
        {
            TADARuleDetails result = new TADARuleDetails();
            try
            {
                DBResponseMapper _dbResponse = new DBResponseMapper();
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["EffectiveDate"]))
                        result.EffectiveDate = DateTime.Parse(dr["EffectiveDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ConnectingID"]))
                        result.ConnectingID = dr["ConnectingID"].ToString();
                    if (!DBNull.Value.Equals(dr["MinHoursForHalfDA"]))
                        result.MinHoursForHalfDA = int.Parse(dr["MinHoursForHalfDA"].ToString());
                    if (!DBNull.Value.Equals(dr["MinKmsForDA"]))
                        result.MinKmsForDA = int.Parse(dr["MinKmsForDA"].ToString());
                    if (!DBNull.Value.Equals(dr["LodgingExpOnCompAcco"]))
                        result.LodgingExpOnCompAcco = bool.Parse(dr["LodgingExpOnCompAcco"].ToString());
                    if (!DBNull.Value.Equals(dr["LocalConvEligibility"]))
                        result.LocalConvEligibility = bool.Parse(dr["LocalConvEligibility"].ToString());
                    if (!DBNull.Value.Equals(dr["DepuStaffDAEligibility"]))
                        result.DepuStaffDAEligibility = bool.Parse(dr["DepuStaffDAEligibility"].ToString());
                    if (!DBNull.Value.Equals(dr["ExtraDAApplicability"]))
                        result.ExtraDAApplicability = bool.Parse(dr["ExtraDAApplicability"].ToString());
                    
                }
                //-----------------  Categories -------------------------
                if (dtcategories != null && dtcategories.Rows.Count > 0)
                {
                    List<CustomCheckBoxOption> sts = new List<CustomCheckBoxOption>();
                    //List<int> selectedCatIds = new List<int>();
                    for (int i = 0; i < dtcategories.Rows.Count; i++)
                    {
                        sts.Add(_dbResponse.Map_CustomCheckBoxOption(dtcategories.Rows[i]));
                    }
                    result.Categories = sts;
                }
                else { result.Categories =new List<CustomCheckBoxOption>(); }
                //-----------------  Comp Trans Options--------------------------
                if (dtCompanyTranOptions != null && dtCompanyTranOptions.Rows.Count > 0)
                {
                    List<CustomCheckBoxOption> comptrans = new List<CustomCheckBoxOption>();
                    for (int i = 0; i < dtCompanyTranOptions.Rows.Count; i++)
                    {
                        comptrans.Add(_dbResponse.Map_CustomCheckBoxOption(dtCompanyTranOptions.Rows[i]));
                    }
                    result.CompTranOptions = comptrans;
                }
                else { result.CompTranOptions = new List<CustomCheckBoxOption>(); }
                //-----------------  Public Trans Options--------------------------
                if (dtPubtranOptions != null && dtPubtranOptions.Rows.Count > 0)
                {
                    List<TADAPubTransOption> pubtrans = new List<TADAPubTransOption>();
                    for (int i = 0; i < dtPubtranOptions.Rows.Count; i++)
                    {
                        pubtrans.Add(_dbResponse.Map_TADAPubTransOption(dtPubtranOptions.Rows[i]));
                    }
                    result.PubTranOptions = pubtrans;
                }
                else { result.PubTranOptions = new List<TADAPubTransOption>(); }
                //-----------------  TADAParam------------------------
                if (dtTADAParam != null && dtTADAParam.Rows.Count > 0)
                {
                    result.TADAParam = Map_TADAParam(dtTADAParam.Rows[0]);
                }
                else { result.TADAParam = new TADAParam(); }

            }
            catch { }
            return result;
        }
        public TADAParam Map_TADAParam(DataRow dr) 
        {
            TADAParam result = new TADAParam();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["ConnectingID"]))
                        result.ConnectingID = dr["ConnectingID"].ToString();
                    if (!DBNull.Value.Equals(dr["IsDAActualSpend"]))
                        result.IsDAActualSpend =bool.Parse(dr["IsDAActualSpend"].ToString());
                    if (!DBNull.Value.Equals(dr["IsLodgingAllowed"]))
                        result.IsLodgingAllowed = bool.Parse(dr["IsLodgingAllowed"].ToString());
                    if (!DBNull.Value.Equals(dr["Metro_DAPerDay"]))
                        result.Metro_DAPerDay = int.Parse(dr["Metro_DAPerDay"].ToString());
                    if (!DBNull.Value.Equals(dr["City_DAPerDay"]))
                        result.City_DAPerDay = int.Parse(dr["City_DAPerDay"].ToString());
                    if (!DBNull.Value.Equals(dr["Town_DAPerDay"]))
                        result.Town_DAPerDay = int.Parse(dr["Town_DAPerDay"].ToString());
                    if (!DBNull.Value.Equals(dr["Metro_MaxLodgingExp"]))
                        result.Metro_MaxLodgingExp = int.Parse(dr["Metro_MaxLodgingExp"].ToString());
                    if (!DBNull.Value.Equals(dr["City_MaxLodgingExp"]))
                        result.City_MaxLodgingExp = int.Parse(dr["City_MaxLodgingExp"].ToString());
                    if (!DBNull.Value.Equals(dr["Town_MaxLodgingExp"]))
                        result.Town_MaxLodgingExp = int.Parse(dr["Town_MaxLodgingExp"].ToString());
                    if (!DBNull.Value.Equals(dr["Metro_MaxLocalConv"]))
                        result.Metro_MaxLocalConv = int.Parse(dr["Metro_MaxLocalConv"].ToString());
                    if (!DBNull.Value.Equals(dr["City_MaxLocalConv"]))
                        result.City_MaxLocalConv = int.Parse(dr["City_MaxLocalConv"].ToString());
                    if (!DBNull.Value.Equals(dr["Town_MaxLocalConv"]))
                        result.Town_MaxLocalConv = int.Parse(dr["Town_MaxLocalConv"].ToString());
                }
            }
            catch { }
            return result;
        }


    }
}
