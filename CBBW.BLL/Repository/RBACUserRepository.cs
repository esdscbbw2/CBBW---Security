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
        MasterEntities _MasterEntities;
        public RBACUserRepository()
        {
            _RBACUserEntities = new RBACUserEntities();
            _MasterEntities=new MasterEntities ();
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
        public IEnumerable<CustomComboOptions> GetLocationTypes(ref string pMsg)
        {
            return _MasterEntities.getLocationTypes(ref pMsg);
        }
        public List<UserList> GetUserList(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, ref string pMsg)
        {
            return _RBACUserEntities.GetUserList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText,ref pMsg);
        }
        public bool SetUserData(UpdateUser data, ref string pMsg)
        {
            if (data != null) 
            {
                if (data.UserRoleList != null && data.UserRoleList.Count > 0)
                {
                    List<UserRole> userroles = new List<UserRole>();
                    foreach (var item in data.UserRoleList)
                    {
                        string[] roles = item.RoleID.Split(',');
                        foreach (string role in roles)
                        {
                            string[] locations = item.LocationCodes.Split(',');
                            foreach (string location in locations)
                            {
                                int loctypecode = int.Parse(location.Split('-')[0]);
                                int loccode = int.Parse(location.Split('-')[1]);
                                UserRole userrole = new UserRole()
                                {
                                    RoleID = role,
                                    RoleName = "",
                                    LocationTypeCode = int.Parse(location.Split('-')[0]),
                                    LocationCode = int.Parse(location.Split('-')[1]),
                                    EffectiveFromDate = item.EffectiveFromDate,
                                    EffectiveToDate = item.EffectiveToDate
                                };
                                userroles.Add(userrole);
                            }
                        }
                    }
                    data.UserRoles = userroles;
                }
            }

            return _RBACUserEntities.SetUserData(data, ref pMsg);
        }
        public bool ValidateUserName(string UserName, ref string pMsg)
        {
            return _RBACUserEntities.ValidateUserName(UserName,ref pMsg);
        }
    }
}
