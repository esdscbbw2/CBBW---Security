using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.RBACUsers
{
    public class UserMenuRaw 
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public int SubModuleID { get; set; }
        public string SubModuleName { get; set; }
        public int NavigationID { get; set; }
        public string NavigationName { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public bool IsSubTask { get; set; }
        public string SubTaskName { get; set; }
        public string URL { get; set; }
    }
    public class UserMenu
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public List<UserSubModule> SubModules { get; set; }
    }
    public class UserSubModule 
    {
        public int SubModuleID { get; set; }
        public string SubModuleName { get; set; }
        public List<UserNavigation> Navigations { get; set; }
    }
    public class UserNavigation 
    {
        public int NavigationID { get; set; }
        public string NavigationName { get; set; }
        public List<UserTask> Tasks { get; set; }
    }
    public class UserTask
    {        
        public string TaskName { get; set; }
        public List<UserSubTask> SubTasks { get; set; }
        public string URL { get; set; }
    }
    public class UserSubTask
    {
        public int TaskID { get; set; }
        public bool IsSubTask { get; set; }
        public string SubTaskName { get; set; }
        public string URL { get; set; }
    }
}
