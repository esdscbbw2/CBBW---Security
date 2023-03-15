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
        string pMsg;
        public TADARulesController(ITADARulesRepository iTADARule, IUserRepository iUser)
        {
            _iTADARules = iTADARule;
            _iUser = iUser;
            pMsg = "";
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            if (string.IsNullOrEmpty(url)) { url = "/Security/TADARules/Index"; }
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewRedirection(int CBUID, string NoteNumber = "") 
        {            
            string mEffectiveDate = _iTADARules.GetAffectedRuleID(ref pMsg).ToString("dd-MM-yyyy");
            return RedirectToAction("ViewRule", new { id = 1, isDelete = false });
        }
        public ActionResult ViewRule(int id, bool isDelete)
        {
            TADARuleDetails model = _iTADARules.GetTADARuleByID(id, ref pMsg);
            model.mDelete = isDelete;
            ViewBag.IsDelete = isDelete;
            model.CallBaclkUrl = TempData["Tadacallbackurl"] != null ? TempData["Tadacallbackurl"].ToString(): "/Security/TADARules/Index" ;
            TempData["Tadacallbackurl"] = model.CallBaclkUrl;
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewRule(TADARuleDetails model, string Submit)
        {            
            if (Submit == "TAParam")
            {                
                return RedirectToAction("ViewParam",new { id = model.ID, isDelete=model.mDelete });
            }
            else if (Submit == "TMDtl")
            {
                return RedirectToAction("ViewTransModeDtls", new { id = model.ID, isDelete = model.mDelete });
            }
            else if (Submit == "Delete") 
            {
                //return RedirectToAction("RemoveRule", new { id = model.ID});
            }
            return View(model);
        }
        public ActionResult ViewParam(int id, bool isDelete) 
        {
            TADARuleDetails model = _iTADARules.GetTADARuleByID(id, ref pMsg);
            if (model.TADAParam == null) { model.TADAParam = new TADAParam(); }
            model.TADAParam.IsLocalConvAllowed =model.LocalConvEligibility;
            model.TADAParam.mDelete = isDelete;
            return View(model.TADAParam);
        }
        public ActionResult ViewTransModeDtls(int id,bool isDelete)
        {
            TADARuleDetails obj = _iTADARules.GetTADARuleByID(id, ref pMsg);
            TADATransViewModel model = new TADATransViewModel();
            model.CompTranOptions = obj.CompTranOptions;
            model.PubTranOptions = obj.PubTranOptions;
            model.mRuleID = id;
            model.mDelete = isDelete;
            TempData["TADARulePubTrans"] = obj.PubTranOptions;
            return View(model);
        }
        // GET: Security/TADARules
        public ActionResult Index()
        {
            IEnumerable<TADARule> model=_iTADARules.GetTADARules(ref pMsg);
            TempData["TADARuleDetail"] = null;
            return View(model);
        }
        //[HttpPost]
        public ActionResult RemoveRule(int id) 
        {
            try
            {
                _iTADARules.RemoveTADARule(id, ref pMsg);
                TempData["TADARuleDetail"] = null;
            }
            catch { }
            return RedirectToAction("Index");
        }
        public ActionResult CreateRule() 
        {
            rulevm=CastTADATempData();
            //rulevm.MinDate= DateTime.Today.ToString("yyyy-MM-dd");
            //rulevm.MaxDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd");
            //rulevm.TADARule = _iTADARules.GetLastTADARuleV2(ref pMsg);
            //DateTime mEffectiveDate = rulevm.TADARule.IsActive ? new DateTime(1,1,1) : rulevm.TADARule.EffectiveDate;
            //rulevm.CategoryList = _iTADARules.GetCatCodesForTADARule(mEffectiveDate, ref pMsg);
            TempData["TADARuleV2"] = rulevm;
            //TADARuleDetails model;
            //if (TempData["TADARuleDetail"] == null)
            //{
            //    TADARuleDetails obj= _iTADARules.GetLastTADARule(ref pMsg);
            //    obj.EffectiveDate = new DateTime(1,1,1);
            //    TempData["TADARuleDetail"] = obj; 
            //}
            //model = TempData["TADARuleDetail"] as TADARuleDetails;
            //model.MinDate = DateTime.Today.ToString("yyyy-MM-dd");
            //model.MaxDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd");
            //TempData["TADARuleDetail"] = model;            
            return View(rulevm);
        }
        [HttpPost]
        public ActionResult CreateRule(TADARuleDetails model,string Submit) 
        {
            if (TempData["TADARuleDetail"] == null)
            {
                TempData["TADARuleDetail"] = _iTADARules.GetLastTADARule(ref pMsg);
            }
            TADARuleDetails obj = TempData["TADARuleDetail"] as TADARuleDetails;
            model.TADAParam = obj.TADAParam;
            model.CompTranOptions = obj.CompTranOptions;
            model.PubTranOptions = obj.PubTranOptions;
            model.Categories = obj.Categories;
            TempData["TADARuleDetail"] = model;
            if (Submit == "TAParam")
            {
                return RedirectToAction("CreateParam");
            }
            else if (Submit == "TMDtl")
            {
                return RedirectToAction("UpdateTransModeDtls");
            }
            else if (Submit == "create") 
            {
                if (_iTADARules.IsValidRule(model, ref pMsg))
                {
                    if (_iTADARules.CreateNewTADARule(model, ref pMsg))
                    {
                        ViewBag.Msg = "New rule successfully created";
                        return View(model);
                        //return RedirectToAction("Index");
                    }
                    else 
                    {
                        ViewBag.DupMsg = "System failed to create new rule.";
                    }
                }
                else 
                {
                    ViewBag.DupMsg = "Rule already exist";
                }                            
            }
            //ViewBag.Msg = pMsg;
             return View(model);
        }
        public ActionResult CreateParam() 
        {
            if (TempData["TADARuleDetail"] == null)
            {
                TempData["TADARuleDetail"] = _iTADARules.GetLastTADARule(ref pMsg);
            }
            TADARuleDetails obj = TempData["TADARuleDetail"] as TADARuleDetails;
            obj.TADAParam.IsLocalConvAllowed = obj.LocalConvEligibility;
            TempData["TADARuleDetail"] = obj;
            return View(obj.TADAParam);
        }
        [HttpPost]
        public ActionResult CreateParam(TADAParam model, string Submit) 
        {
            if (TempData["TADARuleDetail"] == null)
            {
                TempData["TADARuleDetail"] = _iTADARules.GetLastTADARule(ref pMsg);
            }
            TADARuleDetails obj = TempData["TADARuleDetail"] as TADARuleDetails;
            if (Submit == "back")
            {
                TempData["TADARuleDetail"] = obj;
                //ViewBag.BackMsg = "Are You Sure Want to Go Back?";
                //return View();
                return RedirectToAction("CreateRule");
            }
            else if (Submit == "clear") 
            {
                return View();
            }
            obj.TADAParam = model;
            obj.IsSubmitBtn = 1;
            TempData["TADARuleDetail"] = obj;
            return RedirectToAction("CreateRule");            
        }
        public ActionResult UpdateTransModeDtls()
        {
            if (TempData["TADARuleDetail"] == null)
            {
                TempData["TADARuleDetail"] = _iTADARules.GetLastTADARule(ref pMsg);
            }
            TADARuleDetails obj = TempData["TADARuleDetail"] as TADARuleDetails;
            
            TADATransViewModel model=new TADATransViewModel();
            model.CompTranOptions = obj.CompTranOptions;
            model.PubTranOptions = obj.PubTranOptions;
            TempData["TADARulePubTrans"] = obj.PubTranOptions;
            
            TempData["TADARuleDetail"] = obj;
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateTransModeDtls(TADATransViewModel model, string Submit)
        {
            //TempData["UTModel"] = model;
            if (TempData["TADARuleDetail"] == null)
            {
                TempData["TADARuleDetail"] = _iTADARules.GetLastTADARule(ref pMsg);
            }
            TADARuleDetails obj = TempData["TADARuleDetail"] as TADARuleDetails;            
            if (Submit == "create")
            {
                List<int> selectedoptions = new List<int>();
                if (model.SelectedpubTranOptions != null && !string.IsNullOrEmpty(model.SelectedpubTranOptions))
                {
                    selectedoptions = model.SelectedpubTranOptions.Split(',').Select(int.Parse).ToList();
                }
                foreach (TADAPubTransOption objoption in obj.PubTranOptions)
                {
                    objoption.IsSelected = selectedoptions.IndexOf(objoption.ID) >= 0 ? true : false;
                }
                obj.CompTranOptions = model.CompTranOptions;
                obj.IsParamBtn = 1;
                TempData["TADARuleDetail"] = obj;
                return RedirectToAction("CreateRule");
            }
            else if (Submit == "back") 
            {   
                TempData["TADARuleDetail"] = obj;
                //ViewBag.BackMsg = "Are You Sure Want to Go Back?";
                //return View(model);
                return RedirectToAction("CreateRule");
            }
            return View(model);
        }        
        public JsonResult GetPublicTransTypes() 
        {
            return Json(_iTADARules.GetPublicTransportTypes(ref pMsg), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClassTypes(int TypeID)
        {
            IEnumerable<TADAPubTransOption> pubtranoptions;
            if (TempData["TADARulePubTrans"] != null) 
            {
                pubtranoptions = TempData["TADARulePubTrans"] as List<TADAPubTransOption>;
            } else 
            {
                pubtranoptions = _iTADARules.GetPublicTransportClassTypes(TypeID, ref pMsg);
            }
            TempData["TADARulePubTrans"] = pubtranoptions;
            return Json(pubtranoptions.Where(o=>o.TransTypeID==TypeID).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInitialPTData()
        {
            IEnumerable<TADAPubTransOption> pubtranoptions;
            List<RowDisplayInfo> PTRows = new List<RowDisplayInfo>();
            if (TempData["TADARulePubTrans"] != null)
            {
                pubtranoptions = TempData["TADARulePubTrans"] as List<TADAPubTransOption>;
                
                foreach (int typeid in pubtranoptions.Where(o => o.IsSelected == true).GroupBy(o => o.TransTypeID).Select(o => o.Key).ToList()) 
                {
                    PTRows.Add(new RowDisplayInfo() {TypeID= typeid });
                }
                TempData["TADARulePubTrans"] = pubtranoptions;
            }
            return Json(PTRows, JsonRequestBehavior.AllowGet);
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
            return View();
        }
        #region - V2 functions
        public JsonResult GetCategories(string EffectiveDate) 
        {
            var result = _iTADARules.GetCatCodesForTADARule(EffectiveDate == "" ? new DateTime(1, 1, 1) : DateTime.Parse(EffectiveDate), ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRuleData(string EffectiveDate)
        {
            var result = _iTADARules.GetLastTADARuleV2(EffectiveDate == "" ? new DateTime(1, 1, 1) : DateTime.Parse(EffectiveDate), ref pMsg);
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
        #endregion

    }
}