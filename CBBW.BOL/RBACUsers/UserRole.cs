using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.RBACUsers
{
    public class UpdateUser 
    {
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int AdminID { get; set; }
        public List<UserRoleFromGrid> UserRoleList { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
    public class UserRoleFromGrid 
    {
        public string RoleID { get; set; }
        public string LocationCodes { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public DateTime EffectiveToDate { get; set; }
    }
    public class UserRole
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public int LocationTypeCode { get; set; }
        public int LocationCode { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public DateTime EffectiveToDate { get; set; }
    }
    
}
