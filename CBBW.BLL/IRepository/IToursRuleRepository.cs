using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Tour;

namespace CBBW.BLL.IRepository
{
    public interface IToursRuleRepository
    {
        TourRuleSaveInfo getLastTourInfoFromServiceTypeCodes(string serviceTypeCodes, int IsView, DateTime EffectiveDate, ref string pMsg);
        bool RemoveToursRuleV2(DateTime EffectiveDate, string ServiceTypeCodes, ref string pMsg);
        bool FinalSubmitToursRuleV2(DateTime EffectiveDate, ref string pMsg);
        List<TourRuleListData> GetTourRules(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, ref string pMsg);
        TourRuleServiceTypes getServiceTypesFromEffectiveDate(DateTime EffectiveDate, ref string pMsg);
        IEnumerable<TourRule> GetTourRules(ref string pMsg);
        TourRuleDetails GetToursRuleByID(int ID, ref string pMsg);
        TourRuleDetails GetLastToursRule(ref string pMsg);
        bool RemoveTourRule(int ID, ref string pMsg);
        bool CreateNewTourRule(TourRuleDetails trd, ref string pMsg);
        bool IsValidRule(TourRuleDetails trd, ref string pMsg);
        DateTime GetAffectedRuleID(ref string pMsg);
        bool CreateNewTourRuleV2(TourRuleSaveInfo trd, ref string pMsg);
        DateTime? getLastEffectiveDatePartiallyFilled(int RuleType, ref string pMsg);


    }
}
