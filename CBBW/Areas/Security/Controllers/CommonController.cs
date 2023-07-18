using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.Alerts;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using SelectPdf;

namespace CBBW.Areas.Security.Controllers
{
    public class CommonController : Controller
    {
        IUserRepository _iUser;
        IAlertRepository _iAlert;
        string pMsg;
        UserInfo user;
        // GET: Security/Common
        public CommonController(IUserRepository iUser, IAlertRepository iAlert)
        {
            _iUser = iUser;
            _iAlert = iAlert;
            user = iUser.getLoggedInUser(this);
            ViewBag.LogInUser = user.UserName;
        }
        public ActionResult Index()
        {
            List<AlertMaster> obj = new List<AlertMaster>();
            obj = _iAlert.GetAlertMasterDetails(0, ref pMsg);
            return View(obj);
        }
        public ActionResult Index2()
        {
            List<PageInformation> model =new List<PageInformation>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> controllerTypes = assembly.GetTypes();
            controllerTypes= controllerTypes.Where(t => t.BaseType.Name=="Controller");
            foreach (Type controllerType in controllerTypes)
            {
                string controllerName = controllerType.Name.Replace("Controller", "");
                MethodInfo[] methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in methods)
                {
                    if(method.IsPublic && !method.IsConstructor && method.ReturnType.Name=="ActionResult")
                    model.Add(new PageInformation
                    {
                        ControllerName = controllerName,
                        ActionName = method.Name
                    });
                }
            }
            List<PageInformation> model2 = model.Distinct(new PageInformationEqualityComparer()).ToList();
            return View(model2);
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            FileUploadResponse result = new FileUploadResponse();
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    string extName = _ext.ToString().ToUpper();
                    if (extName == ".PDF" || extName == ".PNG" || extName == ".JPG" || extName == ".JPEG")
                    {
                        _imgname = Guid.NewGuid().ToString();
                        var _comPath = Server.MapPath("/Upload/Forms/") + _imgname + _ext;
                        _imgname = _imgname + _ext;
                        ViewBag.Msg = _comPath;
                        var path = _comPath;
                        pic.SaveAs(path);
                        result.ResponseStat = 1;
                        result.ResponseMsg = "File Successfully Uploaded.";
                    }
                    else 
                    {
                        result.ResponseMsg = "Only PDF,PNG,JPEG & JPG Files Can Be Uploaded.";                        
                    }
                    // Saving Image in Original Mode
                    
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
            result.FileName = _imgname;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GeneratePdf(string ViewUrl, string PdfFileName)
        {
            PdfFileName = "MGP_" + PdfFileName;
            var converter = new HtmlToPdf();
            var doc = converter.ConvertUrl(MyCodeHelper.BaseUrl + ViewUrl);

            var pdfPath = Server.MapPath("~/Upload/pdf/" + PdfFileName + ".pdf");
            doc.Save(pdfPath);

            return File(pdfPath, "application/pdf", PdfFileName + ".pdf");
        }

        public JsonResult GetAlertMasterDetails()
        {
            List<AlertMaster> obj = new List<AlertMaster>();
            obj = _iAlert.GetAlertMasterDetails(0, ref pMsg);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAlertDetails(int ID)
        {
            List<AlertDetail> obj = new List<AlertDetail>();
            obj = _iAlert.GetAlertDetails(ID, ref pMsg);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

    }
}