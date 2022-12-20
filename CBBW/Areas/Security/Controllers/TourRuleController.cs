using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.Tour;

namespace CBBW.Areas.Security.Controllers
{
    public class TourRuleController : Controller
    {
        IUserRepository _iUser;
        IToursRuleRepository _toursRule;
        string pMsg;
        public TourRuleController(IToursRuleRepository toursRule, IUserRepository iUser)
        {
            _toursRule = toursRule;
            _iUser = iUser;
            pMsg = "";
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            if (string.IsNullOrEmpty(url)) { url = "/Security/TourRule/Index"; }
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewRedirection(int CBUID,string NoteNumber="")
        {
            int RuleID = _toursRule.GetAffectedRuleID(ref pMsg);
            if (RuleID > 0)
                return RedirectToAction("ViewRule", new { id = RuleID, isDelete = false });
            else
                return View();
        }
        // GET: Security/TourRule
        public ActionResult Index()
        {
            IEnumerable<TourRule> model = _toursRule.GetTourRules(ref pMsg);
            return View(model);
        }

        public ActionResult CreateRule()
        {
            TourRuleDetails model = _toursRule.GetLastToursRule(ref pMsg);
            model.MinDate = DateTime.Today.ToString("yyyy-MM-dd");
            model.MaxDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd");
            model.ReadRule5 = true;
            TempData["TourDetails"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateRule(TourRuleDetails model )
        {
            model.EntryDate = DateTime.Now;
            model.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
            if (_toursRule.IsValidRule(model,ref pMsg))
            {
                bool b = _toursRule.CreateNewTourRule(model, ref pMsg);
                ViewBag.Message = "New rule created successfully";

            }
            else 
            {
                ViewBag.DupMsg = "Rule Alearedy Exist";
            }
            
            TourRuleDetails obj = TempData.Peek("TourDetails") as TourRuleDetails;
            if (obj != null)
            {
                model.ServiceTypes = obj.ServiceTypes;
            }
            return View(model);

        }

        public ActionResult DeleteRule(int id)
        {
            _toursRule.RemoveTourRule(id ,ref pMsg);
           // return View();
            return RedirectToAction("Index");
        }
        
        public ActionResult ViewRule(int id ,bool isDelete)
        {
            
         
           TourRuleDetails model= _toursRule.GetToursRuleByID(id,ref pMsg);
            model.ReadRule5 = true;
            model.CallBackUrl= TempData["Tourcallbackurl"] != null ? TempData["Tourcallbackurl"].ToString() : "/Security/TourRule/Index";

            ViewBag.isDelete = isDelete;
            return View(model);




           
        }
    }
}