using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.RBACUsers;

namespace CBBW.Areas.RBAC.ViewModel.User
{
    public class ViewUserVM
    {
        public int IsBackDenied { get; set; }
        public int IsDelete { get; set; }
        public int EmployeeNumber { get; set; }
        public ViewUserData DataHdr { get; set; }
        public List<ViewUserData> DataDetails { get; set; }

    }
    public class DeleteUserVM 
    {
        public int EmployeeNumber { get; set; }
        public string RoleIDs { get; set; }
    }
}