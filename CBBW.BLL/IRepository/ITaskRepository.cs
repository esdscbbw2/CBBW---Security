using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Task;

namespace CBBW.BLL.IRepository
{
   public interface ITaskRepository
    {
        List<TaskList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        IEnumerable<CustomCheckBoxOption> GetTaskNameList(int Id, ref string pMsg);
        bool SetAddTaskModule(int status, int UserId,int NavigationId, List<TaskMaster> dtldata, ref string pMsg);
        //bool SetAddNavigationModule(int status, int UserId, int ModuleId,int SubModuleId, List<TaskMaster> dtldata, ref string pMsg);
        Header GetTaskMasterDetails(int ID, ref string pMsg);
    }
}
