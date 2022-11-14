using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.Controllers
{
    public class MaterialGatePassController : Controller
    {
        string pMsg;
        IMGPRepository _IMGP;
        public MaterialGatePassController(IMGPRepository IMGP)
        {
            _IMGP = IMGP;
            pMsg = "";
        }
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
        // GET: Security/MaterialGatePass
        public ActionResult Index()
        {
            UserInfo user = getLogInUserInfo();
            IEnumerable<MGPNotes> obj=_IMGP.getApprovedNoteNumbers(13,ref pMsg);
            return View(obj);
        }
    }
}