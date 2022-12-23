using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Upload/Forms/") + _imgname + _ext;
                    _imgname =_imgname + _ext;
                    ViewBag.Msg = _comPath;
                    var path = _comPath;
                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                    //_imagePath=
                    //// resizing image
                    //MemoryStream ms = new MemoryStream();
                    //WebImage img = new WebImage(_comPath);

                    //if (img.Width > 200)
                    //    img.Resize(200, 200);
                    //img.Save(_comPath);
                    //// end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
    }
}