using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.Tour;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class ToursRuleRepository : IToursRuleRepository
    {
        TourEntities _tourEntities;
        MasterEntities _masterentities;
        public ToursRuleRepository()
        {
            _tourEntities = new TourEntities();
            _masterentities = new MasterEntities();
        }
        #region - Tour Rule V2
        public bool CreateNewTourRuleV2(TourRuleSaveInfo trd, ref string pMsg)
        {
            trd.EntryDate = DateTime.Today;
            trd.EntryTime = DateTime.Now.ToString("hh:mm tt");
            return _tourEntities.CreateNewTourRuleV2(trd, ref pMsg);
        }
        public TourRuleServiceTypes getServiceTypesFromEffectiveDate(DateTime EffectiveDate, ref string pMsg)
        {
            return _tourEntities.getServiceTypesFromEffectiveDate(EffectiveDate, ref pMsg);
        }
        public TourRuleSaveInfo getLastTourInfoFromServiceTypeCodes(string serviceTypeCodes, int IsView, DateTime EffectiveDate, ref string pMsg) 
        {
            return _tourEntities.getLastTourInfoFromServiceTypeCodes(serviceTypeCodes,IsView, EffectiveDate, ref pMsg);
        }
        public bool FinalSubmitToursRuleV2(DateTime EffectiveDate, ref string pMsg) 
        {
            return _tourEntities.FinalSubmitToursRuleV2(EffectiveDate, ref pMsg);
        }
        public bool RemoveToursRuleV2(DateTime EffectiveDate, string ServiceTypeCodes, ref string pMsg)
        {
           return _tourEntities.RemoveToursRuleV2(EffectiveDate, ServiceTypeCodes, ref pMsg);
        }
        public DateTime? getLastEffectiveDatePartiallyFilled(int RuleType, ref string pMsg) 
        {
            return _tourEntities.getLastEffectiveDatePartiallyFilled(RuleType, ref pMsg);
        }
        #endregion

        public bool CreateNewTourRule(TourRuleDetails trd, ref string pMsg)
        {
            return _tourEntities.CreateNewTourRule(trd, ref pMsg);
        }
        public DateTime GetAffectedRuleID(ref string pMsg)
        {
            //return 0;
            return _masterentities.getEffectedRuleID(1, ref pMsg);
        }
        public TourRuleDetails GetLastToursRule(ref string pMsg)
        {
            TourRuleDetails result= _tourEntities.GetLastTourRule(ref pMsg);
            if (result.EffectiveDate.Year == 1)
                result.EffectiveDate = DateTime.Today;
            result.EntryDate = DateTime.Today;
            result.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
            if (result.ServiceTypes != null && result.ServiceTypes.Count > 0)
            {
                result.SelectedServiceTypeIds = result.ServiceTypes.Where(o => o.IsSelected == true).Select(o => o.ID).ToList();
            }
            return result;
        }
        public IEnumerable<TourRule> GetTourRules(ref string pMsg)
        {
            return _tourEntities.GetTourRules(ref pMsg);
        }
        public List<TourRuleListData> GetTourRules(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, ref string pMsg)
        {
            return _tourEntities.GetTourRules(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, ref pMsg);
        }
        public TourRuleDetails GetToursRuleByID(int ID, ref string pMsg)
        {
            TourRuleDetails result= _tourEntities.GetTourRuleByID(ID, ref pMsg);
            if (result.ServiceTypes != null && result.ServiceTypes.Count > 0)
            {
                result.SelectedServiceTypeIds = result.ServiceTypes.Where(o => o.IsSelected == true).Select(o => o.ID).ToList();
            }
            return result;
        }
        public bool IsValidRule(TourRuleDetails trd, ref string pMsg)
        {
            return _tourEntities.IsValidTourRule(trd, ref pMsg);
        }
        public bool RemoveTourRule(int ID, ref string pMsg)
        {            
            return _tourEntities.RemoveTourRule(ID, ref pMsg);
        }
        
    }
}
