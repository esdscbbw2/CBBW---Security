using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.SubModules;
using CBBW.BOL.CustomModels;

namespace CBBW.BLL.IRepository
{
    public interface ISubModuleRepository
    {
        List<SubModuleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        bool SetAddSubModule(int status, int UserId, int ModuleId, List<SubModule> dtldata, ref string pMsg);
        Header GetSubModuleDetails(int ID, ref string pMsg);
        bool SetSubmoduleDelete(int ID, ref string pMsg);
       
    }
}
