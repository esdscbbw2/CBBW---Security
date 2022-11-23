using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
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
        IMasterRepository _masterrepo;
        string pMsg;
        ICTVRepository _iCTV;
        UserInfo user;
        private UserInfo getLogInUserInfo()
        {
            UserInfo user = new UserInfo(true);
            if (TempData["LogInUser"] != null)
            {
                user = TempData["LogInUser"] as UserInfo;
            }
            TempData["LogInUser"] = user;
            return user;
        }
        public HomeController(IMasterRepository masterrepo, ICTVRepository iCTVRepo)
        {
            _masterrepo = masterrepo;
            _iCTV = iCTVRepo;
            pMsg = "";
        }
        public ActionResult LogIn() 
        {
            UserInfo model = new UserInfo();
            return View(model);
        }
        [HttpPost]
        public ActionResult LogIn(UserInfo model)
        {
            model = _iCTV.getUserInfo(model.UserName, ref pMsg);
            TempData["LogInUser"] = model;
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            user = getLogInUserInfo();
            

            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
    }
}