using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.RBAC.ViewModel.User;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;

namespace CBBW.Areas.RBAC.Controllers
{
    public class UserManagementController : Controller
    {
        IUserRepository _iUser;
        string pMsg = "";
        UserInfo user;
        IRBACUserRepository _iRBACUser;
        public UserManagementController(IUserRepository iUser, IRBACUserRepository iRBACUser)
        {
            _iUser = iUser;
            _iRBACUser = iRBACUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        // GET: RBAC/UserManagement
        public ActionResult AddUser()
        {
            AddUserVM model = new AddUserVM();
            model.EmployeeList = _iRBACUser.GetListOfActiveEmployees(ref pMsg);
            model.LocationTypeList = _iRBACUser.GetLocationTypes(ref pMsg).OrderBy(o => o.ID).ToList();
            model.RoleList = _iRBACUser.GetListOfRoles(ref pMsg);
            return View(model);
        }



        #region Ajax Calling
        public JsonResult ValidateUserName(string UserName)
        {
            bool result = _iRBACUser.ValidateUserName(UserName, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}