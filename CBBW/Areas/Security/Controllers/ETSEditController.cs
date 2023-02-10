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
            pMsg = "";
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClearBtnClicked(int PageID = 0)
        {
            model = CastEHGEditTempData();
            if (PageID == 1)
            {
                return RedirectToAction("TourEdit");
            }
            else if (PageID == 2)
            {
                return RedirectToAction("IndividualEdit");
            }
            else if (PageID == 3)
            {
                return RedirectToAction("ApproveNote");
            }
            else if (PageID == 4)
            {
                return RedirectToAction("RatNote");
            }
            else
            {
                TempData["EHGEdit"] = null;
                return RedirectToAction("Create");
            }            
        }
        public ActionResult AddNote(int ID)
        {
            TempData["EHGEdit"] = null;
            //TempData["EHGEditApp"] = null;
            if (ID == 0) { return RedirectToAction("Create"); }
            else if (ID == 1) { return RedirectToAction("ApproveNote"); }
            else { return RedirectToAction("RatNote"); }
        }
        public ActionResult RatNote() 
        {
            model = CastEHGEditRatTempData();
            return View(model);
        }
        [HttpPost]
        public ActionResult RatNote(ETSEditCreateVM modelobj, string Submit)
        {
            string baseUrl = "/Security/ETSEdit/ApproveNote";
            model = CastEHGEditRatTempData();
            modelobj.ToBeEditNoteList = model.ToBeEditNoteList;
            modelobj.backbtnactive = 0;
            if (Submit == "TED")
            {
                modelobj.btnTourEdit = 1;
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack("/Security/ETSEdit/RatNote");
                return RedirectToAction("ViewTourEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = 2 });
            }
            else if (Submit == "IED")
            {
                modelobj.btnIndividualEdit = 1;
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack("/Security/ETSEdit/RatNote");
                return RedirectToAction("ViewIndividualEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = 2 });
            }
            else if (Submit == "create")
            {
                if (_IETSEdit.SetETSEditRatificationStatus(modelobj.NoteNumber, modelobj.IsApproved == 1 ? true : false, modelobj.AppReason, user.EmployeeNumber, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Ratified Successfully.";
                    TempData["EHGEdit"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Ratification Failed For Note Number " + modelobj.NoteNumber;
                    TempData["EHGEdit"] = modelobj;
                }
            }
            else if (Submit == "TourRule")
            {
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            return View(modelobj);
        }
        public ActionResult ApproveNote() 
        {
            model = CastEHGEditAppTempData();
            return View(model);
        }
        [HttpPost]
        public ActionResult ApproveNote(ETSEditCreateVM modelobj, string Submit)
        {
            string baseUrl = "/Security/ETSEdit/ApproveNote";
            model = CastEHGEditTempData();
            modelobj.ToBeEditNoteList = model.ToBeEditNoteList;
            modelobj.backbtnactive = 0;            
            if (Submit == "TED")
            {
                modelobj.btnTourEdit = 1;
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack("/Security/ETSEdit/ApproveNote");
                return RedirectToAction("ViewTourEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = 1 });
            }
            else if (Submit == "IED")
            {
                modelobj.btnIndividualEdit = 1;
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack("/Security/ETSEdit/ApproveNote");
                return RedirectToAction("ViewIndividualEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = 1 });
            }
            else if (Submit == "create")
            {
                if (_IETSEdit.SetETSEditAppStatus(modelobj.NoteNumber,modelobj.IsApproved==1?true:false,modelobj.AppReason,user.EmployeeNumber, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Approved Successfully.";
                    TempData["EHGEdit"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Approval Failed For Note Number " + modelobj.NoteNumber;
                    TempData["EHGEdit"] = modelobj;
                }
            }
            else if (Submit == "TourRule")
            {
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                TempData["EHGEdit"] = modelobj;
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            return View(modelobj);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ApprovalIndex() 
        {
            return View();
        }
        public ActionResult RatificationIndex()
        {
            return View();
        }
        public ActionResult ViewNoteRat(string NoteNumber, int CBUID = 0) 
        {
            if (CBUID == 0) { ViewBag.HeaderText = "- EDIT"; _iUser.RecordCallBack("/Security/ETSEdit/RatificationIndex"); }
            else if (CBUID == 1) { ViewBag.HeaderText = "EDIT - APPROVAL"; }
            else if (CBUID == 2) { ViewBag.HeaderText = "EDIT - RATIFICATION"; }
            ETSEditViewVM modelobj = new ETSEditViewVM();
            modelobj.CBUID = CBUID;
            modelobj.NoteNumber = NoteNumber;
            if (!string.IsNullOrEmpty(NoteNumber))
            {
                string notetag = NoteNumber.Substring(7, 3);
                switch (notetag)
                {
                    case "EHG":
                        modelobj.NoteDescription = "Ref. Employee’s Travelling  Details & Vehicle Allotment (By HG)  –  ENTRY Note No.";
                        break;
                    case "EZB":
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR NZB STAFF) Note No.";
                        break;
                    case "EMN":
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS RECORDED AT NZB) Note No.";
                        break;
                    default:
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS) Note No.";
                        break;
                }
                modelobj.NoteDetails = _IETSEdit.getEditNoteHdr(NoteNumber, ref pMsg);
            }
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult ViewNoteRat(ETSEditViewVM modelobj, string Submit)
        {
            string baseUrl = "/Security/ETSEdit/ViewNoteRat?NoteNumber=" + modelobj.NoteNumber + "&CBUID=" + modelobj.CBUID;
            if (Submit == "TourEditBtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewTourEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            else if (Submit == "IndividualEditBtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewIndividualEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            return View(modelobj);
        }
        public ActionResult ViewNoteApp(string NoteNumber, int CBUID = 0) 
        {
            if (CBUID == 0) { ViewBag.HeaderText = "- EDIT"; _iUser.RecordCallBack("/Security/ETSEdit/ApprovalIndex"); }
            else if (CBUID == 1) { ViewBag.HeaderText = "EDIT - APPROVAL"; }
            else if (CBUID == 2) { ViewBag.HeaderText = "EDIT - RATIFICATION"; }
            ETSEditViewVM modelobj = new ETSEditViewVM();
            modelobj.CBUID = CBUID;
            modelobj.NoteNumber = NoteNumber;
            if (!string.IsNullOrEmpty(NoteNumber))
            {
                string notetag = NoteNumber.Substring(7, 3);
                switch (notetag)
                {
                    case "EHG":
                        modelobj.NoteDescription = "Ref. Employee’s Travelling  Details & Vehicle Allotment (By HG)  –  ENTRY Note No.";
                        break;
                    case "EZB":
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR NZB STAFF) Note No.";
                        break;
                    case "EMN":
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS RECORDED AT NZB) Note No.";
                        break;
                    default:
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS) Note No.";
                        break;
                }
                modelobj.NoteDetails = _IETSEdit.getEditNoteHdr(NoteNumber, ref pMsg);
            }
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult ViewNoteApp(ETSEditViewVM modelobj, string Submit) 
        {
            string baseUrl = "/Security/ETSEdit/ViewNoteApp?NoteNumber=" + modelobj.NoteNumber + "&CBUID=" + modelobj.CBUID;
            if (Submit == "TourEditBtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewTourEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            else if (Submit == "IndividualEditBtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewIndividualEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            return View(modelobj);
        }
        public ActionResult ViewNote(string NoteNumber, int CanDelete = 0, int CBUID = 0) 
        {
            if (CBUID == 0) { ViewBag.HeaderText = "- EDIT"; _iUser.RecordCallBack("/Security/ETSEdit/Index"); }
            else if (CBUID == 1) { ViewBag.HeaderText = "EDIT - APPROVAL"; }
            else if (CBUID == 2) { ViewBag.HeaderText = "EDIT - RATIFICATION"; }
            ETSEditViewVM modelobj = new ETSEditViewVM();
            modelobj.DeleteBtn = CanDelete;
            modelobj.CBUID = CBUID;
            modelobj.NoteNumber = NoteNumber;
            if (!string.IsNullOrEmpty(NoteNumber)) 
            {
                string notetag = NoteNumber.Substring(7, 3);
                switch (notetag)
                {
                    case "EHG":
                        modelobj.NoteDescription = "Ref. Employee’s Travelling  Details & Vehicle Allotment (By HG)  –  ENTRY Note No.";
                        break;
                    case "EZB":
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR NZB STAFF) Note No.";
                        break;
                    case "EMN":
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS RECORDED AT NZB) Note No.";
                        break;
                    default:
                        modelobj.NoteDescription = "Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS) Note No.";
                        break;
                }
                modelobj.NoteDetails = _IETSEdit.getEditNoteHdr(NoteNumber, ref pMsg);
            }            
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult ViewNote(ETSEditViewVM modelobj, string Submit) 
        {
            string baseUrl = "/Security/ETSEdit/ViewNote?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.DeleteBtn + "&CBUID=" + modelobj.CBUID;
            if (Submit == "Delete")
            {
                modelobj.NoteDetails = _IETSEdit.getEditNoteHdr(modelobj.NoteNumber, ref pMsg);
                if (_IETSEdit.RemoveETSEditNote(modelobj.NoteNumber,1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";
                }
                else { ViewBag.ErrMsg = "Failed To Delete Note Number " + modelobj.NoteNumber + ". Because It Is Under Process Of Approval."; }
            }
            else if (Submit == "TourEditBtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewTourEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            else if (Submit == "IndividualEditBtn") 
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewIndividualEdit", "ETSEdit", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            return View(modelobj);
        }
        public ActionResult ViewTourEdit(string NoteNumber,int CBUID) 
        {
            ViewBag.HeaderText = CBUID == 0 ? "- EDIT" : CBUID==1?"EDIT - APPROVAL":"EDIT - RATIFICATION";
            ETSEditViewVM modelobj = new ETSEditViewVM();
            if (!string.IsNullOrEmpty(NoteNumber)) 
            {
                modelobj.TravelingPersonDetails=_IETSEdit.getEditTPDetails(NoteNumber, ref pMsg);
                modelobj.DWTDetailsHistory= _IETSEdit.getDateWiseTourHistory(NoteNumber, 0, 0, 0, " ", ref pMsg,true);
                if (modelobj.DWTDetailsHistory != null && modelobj.DWTDetailsHistory.Count > 0) 
                {
                    modelobj.BaseDWTDetailsHistory = modelobj.DWTDetailsHistory.Where(o => o.EditSL == 0).ToList();
                    modelobj.EditSequence = modelobj.DWTDetailsHistory.OrderBy(o=>o.EditSL).Select(o => o.EditSL).Distinct().ToList();
                    modelobj.MaxRowID = modelobj.DWTDetailsHistory.Max(o => o.EditSL);
                }
            }
            return View(modelobj);
        }
        public ActionResult ViewIndividualEdit(string NoteNumber, int CBUID)
        {
            ViewBag.HeaderText = CBUID == 0 ? "- EDIT" : CBUID == 1 ? "EDIT - APPROVAL" : "EDIT - RATIFICATION";
            ETSEditViewVM modelobj = new ETSEditViewVM();
            if (!string.IsNullOrEmpty(NoteNumber)) 
            {
                modelobj.TravelingPersonDetails = _IETSEdit.getEditTPDetails(NoteNumber, ref pMsg);
            }
            return View(modelobj);
        }
        public ActionResult TourEdit() 
        {
            model = CastEHGEditTempData();
            model.TravelingPersonDetails = _IETSEdit.getEditTPDetails(model.NoteNumber, ref pMsg);
            model.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(model.NoteNumber, 0,0,0," ", ref pMsg,true);
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
            model.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(model.NoteNumber, 0, 0, 0, " ", ref pMsg,true);
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
                    ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber + " Due To : " + pMsg;
                }
            }
            else if (Submit == "TourRule") 
            {
                _iUser.RecordCallBack("/Security/ETSEdit/Create");
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                _iUser.RecordCallBack("/Security/ETSEdit/Create");
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            return View(modelobj);
        }
        #region Ajax Calling
        public JsonResult GetNoteInfo(string NoteNumber)
        {
            EditNoteDetails result = _IETSEdit.getEditNoteHdr(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoteInfonLock(string NoteNumber)
        {
            EditNoteDetails result = _IETSEdit.getETSEditHdr(NoteNumber,1, ref pMsg);
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
                
                model = CastEHGEditTempData();
                model.btnTourEdit = 1;
                TempData["EHGEdit"] = model;
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

                model = CastEHGEditTempData();
                model.btnIndividualEdit = 1;
                TempData["EHGEdit"] = model;
            }
            //return RedirectToAction("index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<EditNoteList> noteList = _IETSEdit.GetETSEditNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 0, ref pMsg);
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
        public JsonResult getAppNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<EditNoteList> noteList = _IETSEdit.GetETSEditNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode,1, ref pMsg);
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
        public JsonResult getRatNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<EditNoteList> noteList = _IETSEdit.GetETSEditNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 2, ref pMsg);
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
            modelobj.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(NoteNumber, 0, PersonType, PersonID, PersonName, ref pMsg,true);
            if (modelobj.DWTDetailsHistory != null && modelobj.DWTDetailsHistory.Count > 0)
            {
                modelobj.EditSequence = modelobj.DWTDetailsHistory.Select(o => o.EditSL).Distinct().ToList();
            }
            return View("~/Areas/Security/Views/ETSEdit/_IndividualEditHistory.cshtml",modelobj);
        }
        public ActionResult IndEditHistoryView(string NoteNumber, int PersonType, int PersonID, string PersonName)
        {
            PersonName = PersonName.Trim();
            ETSEditViewVM modelobj = new ETSEditViewVM();
            if (!string.IsNullOrEmpty(NoteNumber))
            {
                modelobj.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(NoteNumber,0,PersonType,PersonID,PersonName,ref pMsg,true);
                if (modelobj.DWTDetailsHistory != null && modelobj.DWTDetailsHistory.Count > 0)
                {
                    modelobj.BaseDWTDetailsHistory = modelobj.DWTDetailsHistory.Where(o => o.EditSL == 0).ToList();
                    modelobj.EditSequence = modelobj.DWTDetailsHistory.OrderBy(o => o.EditSL).Select(o => o.EditSL).Distinct().ToList();
                    modelobj.MaxRowID = modelobj.DWTDetailsHistory.Max(o => o.EditSL);
                }
            }            
            return View("~/Areas/Security/Views/ETSEdit/_IndHistoryView.cshtml", modelobj);
        }
        public ActionResult TourCancelPartialView(string NoteNumber, int PersonType, 
            int PersonID, string PersonName,int Edittagid=1)
        {
            PersonName = PersonName.Trim();
            TourCancelVM modelobj = new TourCancelVM();
            modelobj.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(NoteNumber,0,PersonType, PersonID,PersonName,ref pMsg,true);
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
        private ETSEditCreateVM CastEHGEditAppTempData()
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
                model.ToBeEditNoteList = _IETSEdit.getETSEditNoteListForDropDown(user.CentreCode,1, ref pMsg);

            TempData["EHGEdit"] = model;
            return model;
        }
        private ETSEditCreateVM CastEHGEditRatTempData()
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
                model.ToBeEditNoteList = _IETSEdit.getETSEditNoteListForDropDown(user.CentreCode, 2, ref pMsg);

            TempData["EHGEdit"] = model;
            return model;
        }
    }
}