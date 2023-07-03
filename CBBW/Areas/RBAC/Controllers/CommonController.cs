using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using SelectPdf;

namespace CBBW.Areas.RBAC.Controllers
{
    public class CommonController : Controller
    {
        IUserRepository _iUser;
        string pMsg;
        UserInfo user;
        // GET: Security/Common
        public CommonController(IUserRepository iUser)
        {
            _iUser = iUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
       
    
    }
}