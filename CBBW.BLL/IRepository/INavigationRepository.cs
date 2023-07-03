using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Navigation;

namespace CBBW.BLL.IRepository
{
   public interface INavigationRepository
    {
        List<NavigationList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        bool SetAddNavigationModule(int status, int UserId, int ModuleId,int SubModuleId, List<Navigations> dtldata, ref string pMsg);
        Header GetNavigationDetails(int ID, ref string pMsg);
    }
}
