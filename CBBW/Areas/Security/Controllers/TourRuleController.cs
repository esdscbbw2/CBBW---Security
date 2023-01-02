using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.Tour;

namespace CBBW.Areas.Security.Controllers
{
    public class TourRuleController : Controller
    {
        IUserRepository _iUser;
        IToursRuleRepository _toursRule;
        string pMsg;
        UserInfo user;
        public TourRuleController(IToursRuleRepository toursRule, IUserRepository iUser)
        {
            _toursRule = toursRule;
            _iUser = iUser;
            pMsg = "";
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
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
        public ActionResult ViewRuleV2(string EffectiveDate,string EntryDate, bool isDelete=false)
        {
            
            return View();
        }
        public JsonResult getListOfRules(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<TourRuleListData> ruleList = _toursRule.GetTourRules(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch,ref pMsg);
            var result = new
            {
                //iTotalRecords = ruleList.Count == 0 ? 0 : ruleList.FirstOrDefault().TotalCount,
                iTotalRecords = ruleList.Count == 0 ? 0 : ruleList.FirstOrDefault().FilteredCount,
                iTotalDisplayRecords = ruleList.Count == 0 ? 0 : ruleList.FirstOrDefault().FilteredCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = ruleList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getServiceTypeList(string EffectiveDate) 
        {
            return Json(_toursRule.getServiceTypesFromEffectiveDate(DateTime.Parse(EffectiveDate), ref pMsg).MasterServiceTypeList, JsonRequestBehavior.AllowGet);
        }
    }
}