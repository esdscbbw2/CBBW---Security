using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Role
{
   public  class Header
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool CanDelete { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
        public List<RBACRoles> rolelist { get; set; }
    }
}
