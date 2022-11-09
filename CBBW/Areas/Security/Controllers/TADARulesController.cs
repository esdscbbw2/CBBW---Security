using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.Areas.Security.Controllers
{
    public class TADARulesController : Controller
    {
        ITADARulesRepository _iTADARules;
        string pMsg;
        public TADARulesController(ITADARulesRepository iTADARule)
        {
            _iTADARules = iTADARule;
            pMsg = "";
        }
        public ActionResult ViewRedirection(int CBUID, string NoteNumber = "") 
        {
            //string callbackurl = "";
            if (CBUID == 1)
            {
                TempData["Tadacallbackurl"] = "/Security/CTV/Create";
            }
            else if (CBUID == 2) 
            {
                TempData["Tadacallbackurl"] = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
            }
            int RuleID = _iTADARules.GetAffectedRuleID(ref pMsg);

            return RedirectToAction("ViewRule", new { id = RuleID, isDelete = false });
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
            TADARuleDetails model;
            if (TempData["TADARuleDetail"] == null)
            {
                TempData["TADARuleDetail"]=_iTADARules.GetLastTADARule(ref pMsg);
            }
            model = TempData["TADARuleDetail"] as TADARuleDetails;
            TempData["TADARuleDetail"] = model;            
            return View(model);
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
    }
}