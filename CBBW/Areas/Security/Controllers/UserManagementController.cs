using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.RBACUser;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.Controllers
{
    public class UserManagementController : Controller
    {
        IUserRepository _iUser;
        string pMsg="";
        UserInfo user;
        IRBACUserRepository _iRBACUser;
        public UserManagementController(IUserRepository iUser, IRBACUserRepository iRBACUser)
        {
            _iUser = iUser;
            _iRBACUser = iRBACUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        // GET: Security/UserManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddUser() 
        {
            AddUserVM model = new AddUserVM();
            model.EmployeeList = _iRBACUser.GetListOfActiveEmployees(ref pMsg);
            model.CentreList = _iRBACUser.GetCentreList(ref pMsg);
            model.RoleList = _iRBACUser.GetListOfRoles(ref pMsg);
            return View(model);
        }


        #region Ajax Calling

        #endregion
    }
}