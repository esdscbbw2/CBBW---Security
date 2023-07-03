using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.SubModules;
using CBBW.BOL.CustomModels;
using CBBW.DAL.Entities;
using CBBW.BOL.CTV;

namespace CBBW.BLL.Repository
{
   public class SubModuleRepository:ISubModuleRepository
    {
        SubModuleEntities _ModuleEntities;
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        TFDEntities _TFDEntities;
        TourEntities _tourEntities;
        public SubModuleRepository()
        {
            _ModuleEntities = new SubModuleEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }
        public List<SubModuleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _ModuleEntities.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }

        public Header GetSubModuleDetails(int ID, ref string pMsg)
        {
            List<SubModule> subobj = new List<SubModule>();
            Header obj = new Header();
            obj.sublist = _ModuleEntities.GetSubModuleDetails(ID, ref pMsg);

            if (obj.sublist != null)
            {
                foreach (var item in obj.sublist.Select(x => new { x.ModuleId, x.ModuleName }).Distinct().ToList())
                {
                    obj.ModuleId = item.ModuleId;
                    obj.ModuleName = item.ModuleName;
                }
            }
                return obj;
        }

        public bool SetAddSubModule(int status, int UserId, int ModuleId, List<SubModule> dtldata, ref string pMsg)
        {
            return _ModuleEntities.SetAddSubModule(status, UserId, ModuleId, dtldata, ref pMsg);
        }
    }
}
