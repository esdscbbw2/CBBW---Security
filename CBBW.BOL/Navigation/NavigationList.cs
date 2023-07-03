using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Navigation
{
   public class NavigationList
    {
        public int RowNumber { get; set; }
        public int ID { get; set; }
        public int TotalCount { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string NavigationName { get; set; }
        public DateTime EntryDate { get; set; }
        public bool? IsActive { get; set; }
        public bool CanDelete { get; set; }
    }
}
