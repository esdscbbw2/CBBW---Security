using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.SubModules;


namespace CBBW.Areas.RBAC.ViewModel
{
    public class SubModuleVM
    {

        public int ModuleId { get; set; }
        public List<SubModule> submodulelist { get; set; }
    }
}