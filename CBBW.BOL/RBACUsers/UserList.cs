using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.RBACUsers
{
    public class UserList
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string IsActivestr { get; set; }
    }
}
