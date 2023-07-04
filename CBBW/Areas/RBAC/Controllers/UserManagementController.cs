using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CBBW.Areas.RBAC.ViewModel.User;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;

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
        public ActionResult Index() 
        {
            return View();
        }
        public ActionResult AddUser()
        {
            AddUserVM model = new AddUserVM();
            model.EmployeeList = _iRBACUser.GetListOfActiveEmployees(ref pMsg);
            model.LocationTypeList = _iRBACUser.GetLocationTypes(ref pMsg).OrderBy(o => o.ID).ToList();
            model.RoleList = _iRBACUser.GetListOfRoles(ref pMsg);
            return View(model);
        }
        public ActionResult ViewUser(int EmployeeNumber,int IsDelete=0) 
        {
            return View();
        }
        public ActionResult EditUser(int EmployeeNumber)
        {
            return View();
        }

        #region Ajax Calling
        public JsonResult ValidateUserName(string UserName)
        {
            bool result = _iRBACUser.ValidateUserName(UserName, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetUserData(UpdateUser modelobj) 
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modelobj != null)
                {
                    //Password Encryption
                    modelobj.Password = Crypto.HashPassword(modelobj.Password);
                    modelobj.AdminID = user.EmployeeNumber;
                    if (_iRBACUser.SetUserData(modelobj, ref pMsg))
                    {
                        result.bResponseBool = true;
                        result.sResponseString = "Data successfully updated.";                        
                    }
                    else
                    {
                        result.bResponseBool = false;
                        result.sResponseString = pMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            if (iSortCol_0 == 0) { iSortCol_0 = 1; sSortDir_0 = "des"; }
            List<UserList> userlist = _iRBACUser.GetUserList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, ref pMsg);
            var result = new
            {
                iTotalRecords = userlist.Count == 0 ? 0 : userlist.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = userlist.Count == 0 ? 0 : userlist.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = userlist
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}