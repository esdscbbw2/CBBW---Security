using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.Navigation;
using CBBW.BOL.CustomModels;
using CBBW.DAL.Entities;
using CBBW.BOL.CTV;

namespace CBBW.BLL.Repository
{
   public class NavigationRepository:INavigationRepository
    {
        NavigationEntities _ModuleEntities;
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        TFDEntities _TFDEntities;
        TourEntities _tourEntities;
        public NavigationRepository()
        {
            _ModuleEntities = new NavigationEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }
        public List<NavigationList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _ModuleEntities.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }
        public bool SetAddNavigationModule(int status, int UserId, int ModuleId,int SubModuleId, List<Navigations> dtldata, ref string pMsg)
        {
            return _ModuleEntities.SetAddNavigationModule(status, UserId, ModuleId, SubModuleId, dtldata, ref pMsg);
        }
        public Header GetNavigationDetails(int ID, ref string pMsg)
        {
            List<Navigations> subobj = new List<Navigations>();
            Header obj = new Header();
            obj.navlist = _ModuleEntities.GetNavigationDetails(ID, ref pMsg);

            if (obj.navlist != null)
            {
                foreach (var item in obj.navlist.Select(x => new { x.ModuleId, x.ModuleName,x.SubModuleId,x.SubModuleName }).Distinct().ToList())
                {
                    obj.ModuleId = item.ModuleId;
                    obj.ModuleName = item.ModuleName;
                    obj.SubModuleId = item.SubModuleId;
                    obj.SubModuleName = item.SubModuleName;
                }
            }
            return obj;
        }
        public bool SetNavigationDelete(int ID, ref string pMsg)
        {
            return _ModuleEntities.SetNavigationDelete(ID, ref pMsg);
        }
    }

}
