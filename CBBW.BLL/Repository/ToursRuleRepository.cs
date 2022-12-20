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

        public bool CreateNewTourRule(TourRuleDetails trd, ref string pMsg)
        {
            return _tourEntities.CreateNewTourRule(trd, ref pMsg);
        }

        public int GetAffectedRuleID(ref string pMsg)
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
