using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Modules;
using CBBW.BOL.CustomModels;

namespace CBBW.BLL.IRepository
{
    public interface IModuleRepository
    {
        List<ModuleList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        bool SetAddModule(int status, List<Module> dtldata,int UserId, ref string pMsg);
        Module GetModuleDetails(int ID, ref string pMsg);
    }
}
