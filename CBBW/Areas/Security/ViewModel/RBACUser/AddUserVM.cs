using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;

namespace CBBW.Areas.Security.ViewModel.RBACUser
{
    public class AddUserVM
    {
        public List<Employee> EmployeeList { get; set; }
        public List<CustomComboOptions> CentreList { get; set; }
        public List<MyRole> RoleList { get; set; }
    }
}