using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;

namespace CBBW.BOL.Role
{
    public class RBACRoles
    {
        public string RoleId { get; set; }
        public string RoleIds { get; set; }
        public string RoleName { get; set; }
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int NavigationId { get; set; }
        public string TaskName { get; set; }
        public int TaskId { get; set; }
        public int ActionId { get; set; }
        public string CenterCode { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public int CenterC { get; set; }
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string NavigationName { get; set; }
        public string ActionIds { get; set; }
 


    }
}
