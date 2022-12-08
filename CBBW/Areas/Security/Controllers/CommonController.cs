using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.Controllers
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