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
        IEnumerable<TourRule> GetTourRules(ref string pMsg);
        TourRuleDetails GetToursRuleByID(int ID, ref string pMsg);
        TourRuleDetails GetLastToursRule(ref string pMsg);
        bool RemoveTourRule(int ID, ref string pMsg);
        bool CreateNewTourRule(TourRuleDetails trd, ref string pMsg);
        bool IsValidRule(TourRuleDetails trd, ref string pMsg);
    }
}
