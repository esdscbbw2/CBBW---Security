using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.RBACUsers
{
    public class UpdatePassword 
    {
        public int EmployeeNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class BaseUserData 
    {
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }
    }
    public class ViewUserData: BaseUserData
    {
        public string RoleIDs { get; set; }
        public string RoleName { get; set; }
        public string LocationTypeCodes { get; set; }
        public string LocationTypeCodeDesc { get; set; }
        public string LocationCodes { get; set; }
        public string LocationCodeDesc { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public DateTime EffectiveToDate { get; set; }
        public string EffectiveFromDateStr { get; set; }
        //public string EffectiveToDateStr { get; set; }
        //public string EffectiveFromDateStrLbl { get; set; }
        //public string EffectiveToDateStrLbl { get; set; }
    }
    public class UpdateUser : BaseUserData
    {        
        public string Password { get; set; }        
        public int AdminID { get; set; }
        public List<UserRoleFromGrid> UserRoleList { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }    
    public class UserRoleFromGrid 
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string LocationTypeCodes { get; set; }
        public string LocationTypeCodeDesc { get; set; }
        public string LocationCodes { get; set; }
        public string LocationCodeDesc { get; set; }
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
