using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;
using CBBW.BOL.Tour;
using CBBW.DAL.Entities;
using CBBW.Models;

namespace CBBW.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository _iUser;
        string pMsg;
        UserInfo user;
        //private UserInfo getLogInUserInfo()
        //{
        //    UserInfo user = new UserInfo(true);
        //    if (TempData["LogInUser"] != null)
        //    {
        //        user = TempData["LogInUser"] as UserInfo;
        //    }
        //    TempData["LogInUser"] = user;
        //    return user;
        //}
        public HomeController(IUserRepository iuserrepo)
        {
            // Chage 1 by santosh
            _iUser = iuserrepo;
            pMsg = "";
            _iUser.ClearCallBackRecording();
        }
        public ActionResult LogIn() 
        {
            UserInfo model = new UserInfo();
            //model.IPAddress = MyCodeHelper.GetIPAddress();
            //model.ComputerName = MyCodeHelper.GetComputerName();
            return View(model);
        }
        [HttpPost]
        public ActionResult LogIn(UserInfo model)
        {
            if (_iUser.LogIn(model.UserName, ref pMsg))
            {
                return RedirectToAction("Index","Common",new { area = "Security" });
            }
            else 
            {
                ViewBag.ErrMsg = "Invalid user name";
                return View(model);
            }
        }
        public ActionResult Index()
        {
            user = _iUser.getLoggedInUser();
            return View(user);
        }
        public ActionResult LogOut() 
        {
            _iUser.LogOut();
            return RedirectToAction("LogIn");
        }
        public ActionResult Index2()
        {
            return View();
        }
    }
}