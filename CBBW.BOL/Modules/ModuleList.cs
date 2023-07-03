using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Modules
{
   public class ModuleList
    {
        public int RowNumber { get; set; }
        public int ID { get; set; }
        public int TotalCount { get; set; }
        public string ModuleName { get; set; }
        public DateTime EntryDate { get; set; }
        public bool? IsActive { get; set; }
        public bool CanDelete { get; set; }
    }
}
