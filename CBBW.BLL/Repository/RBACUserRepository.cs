using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class RBACUserRepository : IRBACUserRepository
    {
        RBACUserEntities _RBACUserEntities;
        public RBACUserRepository()
        {
            _RBACUserEntities = new RBACUserEntities();
        }
        public List<CustomComboOptions> GetCentreList(ref string pMsg)
        {
            return _RBACUserEntities.GetCentreList(ref pMsg);
        }
        public List<Employee> GetListOfActiveEmployees(ref string pMsg)
        {
            return _RBACUserEntities.GetListOfActiveEmployees(ref pMsg).OrderBy(o=>o.EmployeeNumber).ToList();
        }
        public List<MyRole> GetListOfRoles(ref string pMsg)
        {
            return _RBACUserEntities.GetListOfRoles(ref pMsg);
        }
        public bool ValidateUserName(string UserName, ref string pMsg)
        {
            return _RBACUserEntities.ValidateUserName(UserName,ref pMsg);
        }
    }
}
