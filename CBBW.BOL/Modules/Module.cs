using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Modules
{
   public class Module
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
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
