using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;

namespace CBBW.BLL.IRepository
{
    public interface ITADARulesRepository
    {
        List<TADARuleListData> getTADARules(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, ref string pMsg);

        IEnumerable<TADARule> GetTADARules(ref string pMsg);
        TADARuleDetails GetLastTADARule(ref string pMsg);
        TADARuleDetails GetTADARuleByID(int ID, ref string pMsg);
        bool RemoveTADARule(int ID, ref string pMsg);
        bool CreateNewTADARule(TADARuleDetails trd, ref string pMsg);
        bool IsValidRule(TADARuleDetails trd, ref string pMsg);
        IEnumerable<PublicTransportType> GetPublicTransportTypes(ref string pMsg);
        IEnumerable<TADAPubTransOption> GetPublicTransportClassTypes(int ID,ref string pMsg);
        DateTime GetAffectedRuleID(ref string pMsg);
    }
}
