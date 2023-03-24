using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.Tour;
using System.Globalization;
using CBBW.BOL.CustomModels;

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
        public ActionResult AddRule()
        {
            TempData["TourDetails"] = null;
            return RedirectToAction("CreateRule");
        }
        public ActionResult ViewRedirection(int CBUID,string NoteNumber="")
        {
            string mEffectiveDate = _toursRule.GetAffectedRuleID(ref pMsg).ToString("dd-MM-yyyy");
            return RedirectToAction("ViewRuleV2", new { EffectiveDate = mEffectiveDate, isDelete = false, isFromIndex=false });            
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
            var lasteffDt = _toursRule.getLastEffectiveDatePartiallyFilled(1, ref pMsg);
            if (lasteffDt != null && lasteffDt >= DateTime.Today)
            {
                model.EffectiveDateOfLastPartiallyFilledRule = DateTime.Parse(lasteffDt.ToString()).ToString("yyyy-MM-dd");
                model.EffectiveDate = DateTime.Parse(lasteffDt.ToString());
            }
            model.MinDate = DateTime.Today.ToString("yyyy-MM-dd");
            model.MaxDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd");
            //model.ReadRule5 = true;
            TempData["TourDetails"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateRule(TourRuleDetails model,string Submit)
        {
            model.CreatedBy = user.EmployeeNumber;
            if (Submit == "save")
            {
                if (_toursRule.CreateNewTourRuleV2(model, ref pMsg))
                {
                    ViewBag.SaveMessage = "Rules Saved For Service Type - " + model.ServiceTypeTexts;
                }
                else
                {
                    ViewBag.ErrorMsg = "Faild To Save Date Due To : " + pMsg;
                }
            }
            else if (Submit == "create") 
            {
                if (_toursRule.FinalSubmitToursRuleV2(model.EffectiveDate, ref pMsg)) 
                { ViewBag.Message = "New Rule Created With Effective From " + model.EffectiveDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture); } 
                else { ViewBag.ErrorMsg = "Faild To Create New Rule Due To : " + pMsg; }
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
            model.CallBackUrl= TempData["Tourcallbackurl"] != null ? TempData["Tourcallbackurl"].ToString() : "/Security/TourRule/Index";

            ViewBag.isDelete = isDelete;
            return View(model);
           
        }
        public ActionResult ViewRuleV2(string EffectiveDate,bool isDelete=false,bool isFromIndex=false)
        {
            if (isFromIndex) { _iUser.RecordCallBack("/Security/TourRule/Index"); }
            TourRuleDetails model = new TourRuleDetails();
            model.IsbtnDeleteActive = isDelete ? 1 : 0;
            model.EffectiveDate = DateTime.Parse(EffectiveDate);
            model.EffectiveDateDisplay = model.EffectiveDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            model.RuleServiceTypeList = _toursRule.getServiceTypesFromEffectiveDate(model.EffectiveDate, ref pMsg).RuleServiceTypeList;
            return View(model);
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
            return Json(_toursRule.getServiceTypesFromEffectiveDate(EffectiveDate==""? DateTime.Today: DateTime.Parse(EffectiveDate), ref pMsg)
                .MasterServiceTypeList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLastTourInfoFromServiceTypeCodes(string serviceTypeCodes,string EffectiveDate,int IsView= 0)
        {
            TourRuleSaveInfo result = _toursRule.getLastTourInfoFromServiceTypeCodes(serviceTypeCodes, IsView,DateTime.Parse(EffectiveDate), ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RemoveToursRuleV2(string EffectiveDate, string ServiceTypeCodes)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_toursRule.RemoveToursRuleV2(DateTime.Parse(EffectiveDate), ServiceTypeCodes, ref pMsg))
            {
                result.bResponseBool = true;
                result.sResponseString = "Data Successfully Deleted.";
            }
            else 
            {
                result.bResponseBool = false;
                result.sResponseString = "Failed To Delete Data Due To - "+pMsg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}