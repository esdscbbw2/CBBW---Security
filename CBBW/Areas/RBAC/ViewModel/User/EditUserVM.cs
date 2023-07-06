﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;

namespace CBBW.Areas.RBAC.ViewModel.User
{
    public class EditUserVM
    {
        public int IsBackDenied { get; set; }
        public int EmployeeNumber { get; set; }
        public ViewUserData DataHdr { get; set; }
        public List<ViewUserData> DataDetails { get; set; }
        //public List<CustomComboOptions> LocationList { get; set; }
        public IEnumerable<CustomComboOptions> LocationTypeList { get; set; }
        public List<MyRole> RoleList { get; set; }        
    }
}