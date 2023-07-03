using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Role
{
   public class RoleList
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateStr { get; set; }
        public bool? IsActive { get; set; }
        public bool CanDelete { get; set; }
    }
}
