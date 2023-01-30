using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.ETSEdit;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.ETSEdit;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;

namespace CBBW.Areas.Security.Controllers
{
    public class ETSEditController : Controller
    {
        IETSEditRepository _IETSEdit;
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        ETSEditCreateVM model;
        EHGMaster _EHGmaster = EHGMaster.GetInstance;
        public ETSEditController(IETSEditRepository IETSEdit, IUserRepository iUser)
        {
            _iUser = iUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
            _IETSEdit = IETSEdit;
        }
        // GET: Security/ETSEdit
        public ActionResult AddNote(int ID)
        {
            TempData["EHGEdit"] = null;
            TempData["EHGEditApp"] = null;
            if (ID == 0) { return RedirectToAction("Create"); }
            else { return RedirectToAction("ApproveNote"); }
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TourEdit() 
        {
            model = CastEHGEditTempData();
            model.TravelingPersonDetails = _IETSEdit.getEditTPDetails(model.NoteNumber, ref pMsg);
            model.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(model.NoteNumber, 0,0,0," ", ref pMsg);
            if (model.DWTDetailsHistory != null && model.DWTDetailsHistory.Count>0) 
            { 
                model.EditSequence = model.DWTDetailsHistory.Select(o => o.EditSL).Distinct().ToList();
                int maxrowid = model.DWTDetailsHistory.Max(o => o.EditSL);
                model.DWTDetailsCurrent = model.DWTDetailsHistory.Where(o => o.EditSL == maxrowid).ToList();
                if (model.DWTDetailsCurrent != null && model.DWTDetailsCurrent.Count > 0) 
                {
                    DateTime tFromDate = model.DWTDetailsCurrent.Min(o => o.SchFromDate);
                    DateTime tToDate = model.DWTDetailsCurrent.Max(o => o.SchToDate);
                    model.ExtensionFromDate = tToDate.AddDays(1);
                    model.MaxSourceID = model.DWTDetailsCurrent.Max(o => o.SourceID)+1;
                    if (DateTime.Today >= tFromDate && DateTime.Today <= tToDate){ model.IsExtensionAllowed = 1; }
                }
            }
            return View(model);
        }
        public ActionResult IndividualEdit()
        {
            model = CastEHGEditTempData();
            model.TravelingPersonDetails = _IETSEdit.getEditTPDetails(model.NoteNumber, ref pMsg);
            model.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(model.NoteNumber, 0, 0, 0, " ", ref pMsg);
            if (model.DWTDetailsHistory != null && model.DWTDetailsHistory.Count > 0)
            {
                model.EditSequence = model.DWTDetailsHistory.Select(o => o.EditSL).Distinct().ToList();
                int maxrowid = model.DWTDetailsHistory.Max(o => o.EditSL);
                model.DWTDetailsCurrent = model.DWTDetailsHistory.Where(o => o.EditSL == maxrowid).ToList();
                if (model.DWTDetailsCurrent != null && model.DWTDetailsCurrent.Count > 0)
                {
                    DateTime tFromDate = model.DWTDetailsCurrent.Min(o => o.SchFromDate);
                    DateTime tToDate = model.DWTDetailsCurrent.Max(o => o.SchToDate);
                    model.ExtensionFromDate = tToDate.AddDays(1);
                    model.MaxSourceID = model.DWTDetailsCurrent.Max(o => o.SourceID) + 1;
                    if (DateTime.Today >= tFromDate && DateTime.Today <= tToDate) { model.IsExtensionAllowed = 1; }
                }
            }
            return View(model);
        }
        public ActionResult Create() 
        {
            model = CastEHGEditTempData();
            return View(model);
        }        
        [HttpPost]
        public ActionResult Create(ETSEditCreateVM modelobj, string Submit)
        {
            model=CastEHGEditTempData();
            modelobj.ToBeEditNoteList = model.ToBeEditNoteList;
            modelobj.backbtnactive = 0;
            TempData["EHGEdit"] = modelobj;
            if (Submit == "TED")
            {
                return RedirectToAction("TourEdit");
            }
            else if (Submit == "IED")
            {
                return RedirectToAction("IndividualEdit");
            }
            else if (Submit == "create") 
            {
                if (_IETSEdit.UpdateETSTourEdit(modelobj.NoteNumber, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Submited Successfully.";
                    TempData["EHGEdit"] = null;
                }
                else 
                {
                    ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber;
                }
            }
            return View(modelobj);
        }
        #region Ajax Calling
        public JsonResult GetNoteInfo(string NoteNumber)
        {
            EditNoteDetails result = _IETSEdit.getEditNoteHdr(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCategories()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            EHGMaster master = EHGMaster.GetInstance;
            result = master.TourCategoryForEdit;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetDWTForTourEdit(DWTTourDetailsForEdit modelobj)
        {
            DWTTourDetailsForDB obj = new DWTTourDetailsForDB();
            obj.NoteNumber = modelobj.NoteNumber;
            obj.EditTag = modelobj.EditTag;
            obj.ReasonForEdit = modelobj.ReasonForEdit;
            obj.UserID = user.EmployeeNumber;
            obj.UserName = user.EmployeeName;
            obj.IsIndividualEdit = false;
            obj.PersonName = " ";
            obj.DWTDetails = modelobj.DWTDetails.Where(o => o.EditRowTag == 1).ToList();
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (modelobj != null)
            {
                result.bResponseBool=_IETSEdit.SetETSTourEdit(obj,user.CentreCode,user.CentreName, ref pMsg);
                result.sResponseString = pMsg;
            }
            //return RedirectToAction("index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetDWTForIndividualTourEdit(DWTIndTourDetailsForEdit modelobj)
        {
            DWTTourDetailsForDB obj = new DWTTourDetailsForDB();
            obj.NoteNumber = modelobj.NoteNumber;
            obj.EditTag = modelobj.EditTag;
            obj.ReasonForEdit = modelobj.ReasonForEdit;
            obj.UserID = user.EmployeeNumber;
            obj.UserName = user.EmployeeName;
            obj.IsIndividualEdit = true;
            obj.PersonType = modelobj.PersonType;
            obj.PersonID = modelobj.PersonID;
            obj.PersonName = modelobj.PersonName;
            obj.DWTDetails = modelobj.DWTDetails.Where(o => o.EditRowTag == 1).ToList();
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (modelobj != null)
            {
                result.bResponseBool = _IETSEdit.SetETSTourEdit(obj, user.CentreCode, user.CentreName, ref pMsg);
                result.sResponseString = pMsg;
            }
            //return RedirectToAction("index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<EditNoteList> noteList = _IETSEdit.GetETSEditNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, false, ref pMsg);
            var result = new
            {
                iTotalRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }        
        public ActionResult ShowIndEditHistory(string NoteNumber,int PersonType,int PersonID,string PersonName) 
        {
            PersonName = PersonName.Trim();
            EditHistoryVM modelobj = new EditHistoryVM();
            modelobj.PersonType = _EHGmaster.PersonType.Where(o=>o.ID==PersonType).FirstOrDefault().DisplayText;
            modelobj.EmpNoNName = PersonName;
            modelobj.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(NoteNumber, 0, PersonType, PersonID, PersonName, ref pMsg);
            if (modelobj.DWTDetailsHistory != null && modelobj.DWTDetailsHistory.Count > 0)
            {
                modelobj.EditSequence = modelobj.DWTDetailsHistory.Select(o => o.EditSL).Distinct().ToList();
            }
            return View("~/Areas/Security/Views/ETSEdit/_IndividualEditHistory.cshtml",modelobj);
        }
        public ActionResult TourCancelPartialView(string NoteNumber, int PersonType, 
            int PersonID, string PersonName,int Edittagid=1)
        {
            PersonName = PersonName.Trim();
            TourCancelVM modelobj = new TourCancelVM();
            modelobj.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(NoteNumber,0,PersonType, PersonID,PersonName,ref pMsg);
            //modelobj.DWTDetailsCurrent = _IETSEdit.getDateWiseTourHistory(NoteNumber, 0, PersonType, PersonID, PersonName, ref pMsg);
            if (modelobj.DWTDetailsHistory != null && modelobj.DWTDetailsHistory.Count > 0)
            {
                //modelobj.EditSequence = modelobj.DWTDetailsHistory.Select(o => o.EditSL).Distinct().ToList();
                int maxrowid = modelobj.DWTDetailsHistory.Max(o => o.EditSL);
                modelobj.DWTDetailsCurrent = modelobj.DWTDetailsHistory.Where(o => o.EditSL == maxrowid).ToList();                
            }
            if (Edittagid == 2) {
                return View("~/Areas/Security/Views/ETSEdit/_TourOtherEdit.cshtml", modelobj);
            } else {
                return View("~/Areas/Security/Views/ETSEdit/_TourCancel.cshtml", modelobj);
            }            
        }
        #endregion
        //private functions
        private ETSEditCreateVM CastEHGEditTempData()
        {
            if (TempData["EHGEdit"] != null)
            {
                model = TempData["EHGEdit"] as ETSEditCreateVM;
            }
            else
            {
                model = new ETSEditCreateVM();                
            }
            if (model.ToBeEditNoteList == null)
                model.ToBeEditNoteList = _IETSEdit.getETSNoteListToBeEdited(user.CentreCode, ref pMsg);
            
            TempData["EHGEdit"] = model;
            return model;
        }
    }
}