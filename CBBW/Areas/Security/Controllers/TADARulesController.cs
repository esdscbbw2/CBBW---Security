using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.Areas.Security.ViewModel.Rule;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.Areas.Security.Controllers
{
    public class TADARulesController : Controller
    {
        IUserRepository _iUser;
        ITADARulesRepository _iTADARules;
        TADARuleVM rulevm;
        TADARuleViewVM ruleviewvm;
        string pMsg;
        public TADARulesController(ITADARulesRepository iTADARule, IUserRepository iUser)
        {
            _iTADARules = iTADARule;
            _iUser = iUser;
            pMsg = "";
        }
        public ActionResult AddRule() 
        {
            TempData["TADARuleV2"] = null;
            return RedirectToAction("CreateRule");
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            if (string.IsNullOrEmpty(url)) { url = "/Security/TADARules/Index"; }
            TempData["TADARuleV2View"] = null;
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewRedirection(int CBUID, string NoteNumber = "") 
        {
            DateTime mEffeDate = _iTADARules.GetAffectedRuleID(ref pMsg);
            List<CustomComboOptionsWithString> obj1 = _iTADARules.GetCatCodesForTADARuleView(mEffeDate, ref pMsg);
            if (obj1 != null && obj1.Count > 0) 
            {
                return RedirectToAction("ViewRuleV2", new { EffectiveDate = mEffeDate.ToString("dd-MM-yyyy"), isDelete = false });
            } 
            else 
            {
                return RedirectToAction("EmptyView");
            }            
        }
        public ActionResult EmptyView()
        {
            return View();
        }
        //public ActionResult ViewRule(int id, bool isDelete)
        //{
        //    TADARuleDetails model = _iTADARules.GetTADARuleByID(id, ref pMsg);
        //    model.mDelete = isDelete;
        //    ViewBag.IsDelete = isDelete;
        //    model.CallBaclkUrl = TempData["Tadacallbackurl"] != null ? TempData["Tadacallbackurl"].ToString(): "/Security/TADARules/Index" ;
        //    TempData["Tadacallbackurl"] = model.CallBaclkUrl;
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult ViewRule(TADARuleDetails model, string Submit)
        //{            
        //    if (Submit == "TAParam")
        //    {                
        //        return RedirectToAction("ViewParam",new { id = model.ID, isDelete=model.mDelete });
        //    }
        //    else if (Submit == "TMDtl")
        //    {
        //        return RedirectToAction("ViewTransModeDtls", new { id = model.ID, isDelete = model.mDelete });
        //    }
        //    else if (Submit == "Delete") 
        //    {
        //        //return RedirectToAction("RemoveRule", new { id = model.ID});
        //    }
        //    return View(model);
        //}
        //public ActionResult ViewParam(int id, bool isDelete) 
        //{
        //    TADARuleDetails model = _iTADARules.GetTADARuleByID(id, ref pMsg);
        //    if (model.TADAParam == null) { model.TADAParam = new TADAParam(); }
        //    model.TADAParam.IsLocalConvAllowed =model.LocalConvEligibility;
        //    model.TADAParam.mDelete = isDelete;
        //    return View(model.TADAParam);
        //}
        //public ActionResult ViewTransModeDtls(int id,bool isDelete)
        //{
        //    TADARuleDetails obj = _iTADARules.GetTADARuleByID(id, ref pMsg);
        //    TADATransViewModel model = new TADATransViewModel();
        //    model.CompTranOptions = obj.CompTranOptions;
        //    model.PubTranOptions = obj.PubTranOptions;
        //    model.mRuleID = id;
        //    model.mDelete = isDelete;
        //    TempData["TADARulePubTrans"] = obj.PubTranOptions;
        //    return View(model);
        //}
        // GET: Security/TADARules
        public ActionResult Index()
        {
            IEnumerable<TADARule> model=_iTADARules.GetTADARules(ref pMsg);
            TempData["TADARuleDetail"] = null;
            return View(model);
        }
        //[HttpPost]
        
        public ActionResult CreateRule() 
        {
            rulevm=CastTADATempData();            
            //TempData["TADARuleV2"] = rulevm;                        
            return View(rulevm);
        }
        [HttpPost]
        public ActionResult CreateRule(TADARuleVM model,string Submit) 
        {
            model.TADARule.EffectiveDateDisplay = model.TADARule.EffectiveDate.ToString("yyyy-MM-dd");
            rulevm=CastTADATempData();
            rulevm.CategoryList=rulevm.CategoryList!=null ? rulevm.CategoryList : _iTADARules.GetCatCodesForTADARule(model.TADARule.EffectiveDate, ref pMsg);
            rulevm.TADARule = model.TADARule;
            TempData["TADARuleV2"] = rulevm;
            if (Submit == "TAParam")
            {
                return RedirectToAction("CreateParam");
            }
            else if (Submit == "TMDtl")
            {
                return RedirectToAction("UpdateTransModeDtls");
            }
            else if (Submit == "save") 
            {
                if (_iTADARules.SetTADARuleV2(model.TADARule, ref pMsg)) 
                {
                    ViewBag.Msg = "Rule Saved Successfully For "+model.TADARule.CategoryText;
                    rulevm.CategoryList = _iTADARules.GetCatCodesForTADARule(model.TADARule.EffectiveDate, ref pMsg);
                    //rulevm.IsSubmitActive = rulevm.CategoryList.Where(o => o.IsSelected == false).Count() > 0 ? 0 : 1;
                    rulevm.TADARule.CategoryIds = "";
                    rulevm.IsParamBtn = 0;
                    rulevm.IsTransBtn = 0;
                    TempData["TADARuleV2"] = rulevm;
                }
                else 
                {
                    ViewBag.ErrMsg = "Data Updation Failed.";
                }
            }
            else if (Submit == "create")
            {
                if (_iTADARules.FinalSubmitTADARuleV2(model.TADARule.EffectiveDate, ref pMsg))
                {
                    ViewBag.SMsg = "Rule Submitted Successfully";
                    TempData["TADARuleV2"] = null;
                }
                else 
                {
                    ViewBag.ErrMsg = "Data Updation Failed.";
                }
            }
             return View(rulevm);
        }
        public ActionResult CreateParam() 
        {
            rulevm = CastTADATempData();
            return View(rulevm);
        }
        [HttpPost]
        public ActionResult CreateParam(TADARuleVM model, string Submit) 
        {
            rulevm = CastTADATempData();
            rulevm.IsTransBtn = model.IsTransBtn;
            rulevm.TADARule = model.TADARule;
            if (Submit == "create") 
            {
                rulevm.IsParamBtn = 1;
                TempData["TADARuleV2"] = rulevm;
                return RedirectToAction("CreateRule");
            }
            return View(model);
        }
        public ActionResult UpdateTransModeDtls()
        {
            rulevm = CastTADATempData();            
            return View(rulevm);
        }
        [HttpPost]
        public ActionResult UpdateTransModeDtls(TADARuleVM model, string Submit)
        {
            rulevm = CastTADATempData();
            rulevm.TADARule = model.TADARule;
            rulevm.IsParamBtn = model.IsParamBtn;
            if (Submit == "create")
            {
                rulevm.IsTransBtn = 1;
                TempData["TADARuleV2"] = rulevm;
                return RedirectToAction("CreateRule");
            }
            else if (Submit == "back") 
            {
                rulevm.IsTransBtn = 0;
                TempData["TADARuleV2"] = rulevm;
                return RedirectToAction("CreateRule");
            }
            return View(model);
        }        
        public JsonResult getListOfRules(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<TADARuleListData> ruleList = _iTADARules.getTADARules(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, ref pMsg);
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
        public ActionResult ViewRuleV2(string EffectiveDate, bool isDelete = false, bool isFromIndex = false) 
        {
            ruleviewvm=CastTADAViewTempData(DateTime.Parse(EffectiveDate));
            ruleviewvm.IsDelete = isDelete;
            if (isFromIndex) 
            { 
                ruleviewvm.SelectedCategory = null;
                ruleviewvm.CategoryList= _iTADARules.GetCatCodesForTADARuleView(ruleviewvm.EffectiveDate, ref pMsg);
            }
            try
            {
                //ruleviewvm.EffectiveDate = DateTime.Parse(EffectiveDate);
                ruleviewvm.SelectedCategory = ruleviewvm.SelectedCategory == null ? ruleviewvm.CategoryList==null || ruleviewvm.CategoryList .Count==0? "":ruleviewvm.CategoryList.FirstOrDefault().ID : ruleviewvm.SelectedCategory;
                ruleviewvm.TADARule = _iTADARules.GetTADARuleV2(ruleviewvm.EffectiveDate, ruleviewvm.SelectedCategory, ref pMsg);
                TempData["TADARuleV2View"] = ruleviewvm;
            }
            catch { }
            return View(ruleviewvm);
        }
        [HttpPost]
        public ActionResult ViewRuleV2(TADARuleViewVM model, string Submit) 
        {
            ruleviewvm = CastTADAViewTempData(model.EffectiveDate);
            ruleviewvm.SelectedCategory = model.SelectedCategory;
            ruleviewvm.IsDelete = model.IsDelete;
            ruleviewvm.TADARule = _iTADARules.GetTADARuleV2(model.EffectiveDate, model.SelectedCategory, ref pMsg);
            TempData["TADARuleV2View"] = ruleviewvm;
            if (Submit == "TAParam")
            {
                return RedirectToAction("CreateParamView2",new { EffectiveDate= model.EffectiveDate });
            }
            else if (Submit == "TMDtl")
            {
                return RedirectToAction("UpdateTransModeDtlsView2", new { EffectiveDate = model.EffectiveDate });
            }
            return View(ruleviewvm);
        }        
        public ActionResult CreateParamView2(DateTime EffectiveDate) 
        {
            ruleviewvm = CastTADAViewTempData(EffectiveDate);

            return View(ruleviewvm);
        }
        public ActionResult UpdateTransModeDtlsView2(DateTime EffectiveDate) 
        {
            ruleviewvm = CastTADAViewTempData(EffectiveDate);

            return View(ruleviewvm);
        }
        #region - V2 functions
        public JsonResult GetCategories(string EffectiveDate) 
        {
            List<CustomCheckBoxOption> result=null;
            try
            {
                rulevm = CastTADATempData();
                if (rulevm.TADARule != null && rulevm.TADARule.EffectiveDate == DateTime.Parse(EffectiveDate))
                {
                    if (rulevm.CategoryList != null && rulevm.CategoryList.Count > 0)
                        result = rulevm.CategoryList;
                }
                if (result == null)
                {
                    result = _iTADARules.GetCatCodesForTADARule(EffectiveDate == "" ? new DateTime(1, 1, 1) : DateTime.Parse(EffectiveDate), ref pMsg);
                }
                rulevm.CategoryList = result;
                rulevm.IsSubmitActive = 0;
                TempData["TADARuleV2"] = rulevm;
            }
            catch { }
            return Json(result.OrderBy(o=>o.ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoryAvblCount(string EffectiveDate)
        {
            int result = 1;
            try
            {
                if (!string.IsNullOrEmpty(EffectiveDate))
                {
                    List<CustomCheckBoxOption> obj1 = _iTADARules.GetCatCodesForTADARule(DateTime.Parse(EffectiveDate), ref pMsg);
                    if (obj1 != null && obj1.Count > 0) { result = obj1.Where(o => o.IsSelected == false).Count(); }
                }
            }
            catch { }     
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRuleData(string EffectiveDate)
        {
            TADARuleV2 result = null;
            try
            {
                rulevm = CastTADATempData();
                if (rulevm.TADARule != null && rulevm.TADARule.EffectiveDate == DateTime.Parse(EffectiveDate))
                {
                    result = _iTADARules.GetLastTADARuleV2(rulevm.TADARule.EffectiveDate, ref pMsg);
                }
                else
                {
                    result = _iTADARules.GetLastTADARuleV2(DateTime.Parse(EffectiveDate), ref pMsg);
                }
                rulevm.TADARule = result;
                TempData["TADARuleV2"] = rulevm;
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RemoveRuleV2(string EffectiveDate, string ServiceTypeCodes)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iTADARules.RemoveTADARuleV2(DateTime.Parse(EffectiveDate), ServiceTypeCodes, ref pMsg))
            {
                result.bResponseBool = true;
                result.sResponseString = "Data Successfully Deleted.";
            }
            else
            {
                result.bResponseBool = false;
                result.sResponseString = "Failed To Delete Data Due To - " + pMsg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region - Private functions
        private TADARuleVM CastTADATempData()
        {
            if (TempData["TADARuleV2"] != null)
            {
                rulevm = TempData["TADARuleV2"] as TADARuleVM;
            }
            else
            {
                rulevm = new TADARuleVM();
            }            
            TempData["TADARuleV2"] = rulevm;
            return rulevm;
        }
        private TADARuleViewVM CastTADAViewTempData(DateTime EffectiveDate)
        {
            if (TempData["TADARuleV2View"] != null)
            {
                ruleviewvm = TempData["TADARuleV2View"] as TADARuleViewVM;
            }
            else
            {
                ruleviewvm = new TADARuleViewVM();
            }
            if (ruleviewvm.CategoryList == null) 
            {
                ruleviewvm.CategoryList = _iTADARules.GetCatCodesForTADARuleView(EffectiveDate, ref pMsg);
            }
            ruleviewvm.EffectiveDate = EffectiveDate;
            TempData["TADARuleV2View"] = ruleviewvm;
            return ruleviewvm;
        }
        #endregion

    }
}