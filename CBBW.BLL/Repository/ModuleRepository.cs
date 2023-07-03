using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.Modules;
using CBBW.BOL.CustomModels;
using CBBW.DAL.Entities;
using CBBW.BOL.CTV;

namespace CBBW.BLL.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        ModuleEntities _ModuleEntities;
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        TFDEntities _TFDEntities;
        TourEntities _tourEntities;
        public ModuleRepository()
        {
            _ModuleEntities = new ModuleEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }

        public List<ModuleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _ModuleEntities.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }

        public Module GetModuleDetails(int ID, ref string pMsg)
        {
            return _ModuleEntities.GetModuleDetails(ID, ref pMsg);
        }

        public bool SetAddModule(int status, List<Module> dtldata,int UserId, ref string pMsg)
        {
            return _ModuleEntities.SetAddModule(status, dtldata, UserId, ref pMsg);
        }
    }
}
