using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Task
{
    public class Header
    {
        public int ID { get; set; }
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int NavigationId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string NavigationName { get; set; }
        public string TaskName { get; set; }
        public bool CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
        public List<TaskMaster> navlist { get; set; }
    }
    public class TaskMaster
    {
        public int? ID { get; set; }
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int NavigationId { get; set; }
        public int? TaskId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string NavigationName { get; set; }
        public string TaskName { get; set; }
        public bool IsActive { get; set; }
        public int IsActiveInt { get; set; }
        public int UserId { get; set; }
        public bool IsSubmit { get; set; }
        public DateTime EntryDate { get; set; }
        public bool CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }

    }
}
