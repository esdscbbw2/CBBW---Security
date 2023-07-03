using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.Task;
using CBBW.BOL.CustomModels;
using CBBW.DAL.Entities;
using CBBW.BOL.CTV;

namespace CBBW.BLL.Repository
{
   public class TaskRepository:ITaskRepository
    {
        TaskEntities _ModuleEntities;
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        TFDEntities _TFDEntities;
        TourEntities _tourEntities;
        public TaskRepository()
        {
            _ModuleEntities = new TaskEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }
        public List<TaskList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _ModuleEntities.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }

        public IEnumerable<CustomCheckBoxOption> GetTaskNameList(int Id, ref string pMsg)
        {
            return _ModuleEntities.GetTaskNameList(Id, ref pMsg);
        }

        public bool SetAddTaskModule(int status, int UserId,int NavigationId, List<TaskMaster> dtldata, ref string pMsg)
        {
            return _ModuleEntities.SetAddTaskModule(status, UserId,NavigationId, dtldata, ref pMsg);
        }
        //public bool SetAddNavigationModule(int status, int UserId, int ModuleId,int SubModuleId, List<TaskMaster> dtldata, ref string pMsg)
        //{
        //    return _ModuleEntities.SetAddNavigationModule(status, UserId, ModuleId, SubModuleId, dtldata, ref pMsg);
        //}

        public Header GetTaskMasterDetails(int ID, ref string pMsg)
        {
            List<TaskMaster> subobj = new List<TaskMaster>();
            Header obj = new Header();
            obj.navlist = _ModuleEntities.GetTaskMasterDetails(ID, ref pMsg);

            //if (obj.navlist != null)
            //{
            //    foreach (var item in obj.navlist.Where(x => x.ID != null).Select(x => new { x.ModuleId, x.SubModuleId, x.NavigationId}).Distinct().ToList())
            //    {
            //        obj.ModuleId = item.ModuleId;
            //        obj.SubModuleId = item.SubModuleId;
            //        obj.NavigationId = item.NavigationId;
              
            //    }
            //}
            return obj;
        }
    }
}
