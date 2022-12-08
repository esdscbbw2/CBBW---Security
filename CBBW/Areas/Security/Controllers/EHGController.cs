using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EHG;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;

namespace CBBW.Areas.Security.Controllers
{
    public class EHGController : Controller
    {
        IUserRepository _iUser;
        string pMsg;
        UserInfo user;
        IEHGRepository _iEHG;
        EHGHeaderEntryVM model;        
        public EHGController(IUserRepository iUser, IEHGRepository iEHG)
        {
            _iUser = iUser;
            _iEHG = iEHG;
            pMsg = "";
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        // GET: Security/EHG
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create() 
        {            
            if (TempData["EHG"] == null)
            {
                model = new EHGHeaderEntryVM(user.CentreCode);
                model.ehgHeader = _iEHG.getNewEHGHeader(ref pMsg);
            }
            else { model = CastEHGTempData(); }

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EHGHeaderEntryVM model) 
        {
            return View(model);
        }
        public ActionResult DateWiseTourDetails() 
        {

            return View();
        }

        //Actions for AJAX Calling
        public JsonResult GetPersonTypes()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            model = CastEHGTempData();
            if (model == null || model.PersonType == null)
            {
                EHGMaster master = EHGMaster.GetInstance;
                result = master.PersonType;
            }
            else { result = model.PersonType;}
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Private Functions
        private EHGHeaderEntryVM CastEHGTempData() 
        {            
            if (TempData["EHG"] != null)
            {
                model = TempData["EHG"] as EHGHeaderEntryVM;
                TempData["EHG"] = model;
            }
            return model;
        }
        
    }
}