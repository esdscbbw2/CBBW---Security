using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.ETSEdit;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.Controllers
{
    public class EntryIController : Controller
    {
        IETSEditRepository _IETSEdit;
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        EntrICreateVM model;
        IMasterRepository _master;
        public EntryIController(IMasterRepository imaster,IETSEditRepository IETSEdit, IUserRepository iUser)
        {
            _iUser = iUser;
            _master = imaster;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
            _IETSEdit = IETSEdit;
            pMsg = "";
        }
        // GET: Security/EntryI
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VehicleAllotmentView(string NoteNumber) 
        {
            model = CastEntryITempData();
            model.VADetails = _IETSEdit.GetVehicleAllotmentDetails(NoteNumber, 1, ref pMsg);
            TempData["EntryI"] = model;
            return View(model);
        }
        public ActionResult ViewNote(string NoteNumber, int CanDelete = 0, int CBUID = 0) 
        {
            model = new EntrICreateVM();
            model.NoteNumber = NoteNumber;
            model.CanDelete = CanDelete;
            TempData["EntryI"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewNote(EntrICreateVM modelobj, string Submit) 
        {
            string baseUrl = "/Security/EntryI/ViewNote?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.CanDelete;
            TempData["EntryI"] = modelobj;
            if (Submit == "TDBtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("TravelingDetailsView", "EntryI", new { NoteNumber = modelobj.NoteNumber });
            }
            else if (Submit == "VABtn")
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("VehicleAllotmentView", "EntryI", new { NoteNumber = modelobj.NoteNumber });
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
            else if (Submit == "Delete")
            {
                if (_IETSEdit.RemoveEntryINote(modelobj.NoteNumber, true, ref pMsg)) 
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";
                }
                else { ViewBag.ErrMsg = "Failed To Delete Note Number " + modelobj.NoteNumber ; }
            }
            return View(modelobj);
        }
        public ActionResult ClearBtnClicked(int PageID = 0,string NoteNumber="")
        {
            model = CastEntryITempData();
            if (PageID == 1)
            {
                return RedirectToAction("VehicleAllotment",new { NoteNumber = NoteNumber });
            }
            else
            {
                TempData["EntryI"] = null;
                return RedirectToAction("Create");
            }
        }
        public ActionResult AddNote(int ID)
        {
            TempData["EntryI"] = null;
            return RedirectToAction("Create");            
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TravelingDetailsView(string NoteNumber) 
        {
            model = CastEntryITempData();
            model.IsVABtnEnabled = 1;
            TempData["EntryI"] = model;
            EntryITourDetails modelobj = _IETSEdit.GetEntryITourData(NoteNumber, 1, ref pMsg);
            return View(modelobj);
        }
        public ActionResult VehicleAllotment(string NoteNumber) 
        {
            model = CastEntryITempData();
            model.IsBtn = 0;
            model.VABackBtnActive = 0;
            model.IsVABtnEnabled = 1;
            model.VehicleList= _master.getVehicleList("L.C.V", model.VehicleType == 1 ? 4 : 2, ref pMsg,user.CentreCode);
            model.DriverList = _IETSEdit.GETDriverList(NoteNumber, ref pMsg);
            model.VADetails = _IETSEdit.GetVehicleAllotmentDetails(NoteNumber, 0, ref pMsg);
            
            if (model.VADetails == null)
            {
                model.VADetails = new VehicleAllotmentDetails();
                model.VADetails.NoteNumber = NoteNumber;
                model.VADetails.MaterialStatus = -1;
                model.VADetails.AuthorisedEmpName = model.AuthorisedEmpNonName;
                model.VADetails.AuthorisedEmpNumber = MyCodeHelper.GetEmpNoFromString(model.AuthorisedEmpNonName);
                model.VADetails.DesignationText = model.DesgCodenNameOfAE;
                model.VADetails.VehicleType = model.VehicleType == 2 ? "2 Wheeler" : model.VehicleType == 1 ? "LV" : "NA";
                if (model.DriverList != null && model.DriverList.Where(o => o.PersonID != -1).Count() > 1)
                {
                    model.IsDriverCtrlEnable = 1;
                    model.VADetails.DriverNumber = model.DriverList.FirstOrDefault().PersonID;
                    //model.DriverNumber = -1;
                }
                else { model.IsDriverCtrlEnable = 0; }
            }
            else 
            { 
                model.VADetails.OtherVehicleNumber = model.VADetails.VehicleBelongsTo == 2 ? model.VADetails.VehicleNumber : "";
                model.VADetails.OtherVehicleModelName = "NA";
            }
            model.AuthorisedEmpNonName2 = model.AuthorisedEmpNonName;
            model.DesgCodenNameOfAE2 = model.DesgCodenNameOfAE;
            TempData["EntryI"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult VehicleAllotment(EntrICreateVM modelobj, string Submit) 
        {
            model = CastEntryITempData();
            if (model.VehicleList == null) { model.VehicleList = _master.getVehicleList("L.C.V", model.VehicleType == 1 ? 4 : 2, ref pMsg, user.CentreCode);}
            if (model.DriverList == null) { model.DriverList = _IETSEdit.GETDriverList(modelobj.NoteNumber, ref pMsg); }
            if (Submit == "Save")
            {
                if (modelobj.VADetails != null) 
                {
                    modelobj.VADetails.MaterialStatus = modelobj.MaterialStatus;
                    modelobj.VADetails.VehicleBelongsTo = modelobj.VehicleBelongsTo2;
                    modelobj.VADetails.VehicleNumber = modelobj.VehicleNumber;
                    modelobj.VADetails.OtherVehicleNumber = modelobj.OtherVehicleNumber;
                    modelobj.VADetails.DriverName = modelobj.DriverName;
                    modelobj.VADetails.DriverNumber =MyCodeHelper.GetEmpNoFromString(modelobj.DriverName);
                }
                if (_IETSEdit.SetETSVehicleAllotmentDetails(modelobj.VADetails,user.CentreCode,user.CentreName, ref pMsg))
                {
                    model.VASubmitBtnActive = 1;
                    ViewBag.Msg = "Data Saved Successfully";
                    model.VADetails = modelobj.VADetails;
                    TempData["EntryI"] = model;
                    //return RedirectToAction("Create");
                }
                else 
                {
                    ViewBag.ErrMsg = pMsg;
                }
            }
            modelobj.VehicleList = model.VehicleList;
            modelobj.DriverList = model.DriverList;
            return View(modelobj);
        }
        public ActionResult Create() 
        {
            model = CastEntryITempData();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EntrICreateVM modelobj, string Submit) 
        {
            model = CastEntryITempData();
            modelobj.DropDownNoteList = model.DropDownNoteList;
            //modelobj.backbtnactive = 0;
            modelobj.IsVABtnEnabled = 0;
            TempData["EntryI"] = modelobj;
            if (Submit == "TDBtn")
            {
                string baseUrl = "/Security/EntryI/Create";
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("TravelingDetailsView","EntryI",new { NoteNumber = modelobj.NoteNumber });
            }
            else if (Submit == "VABtn")
            {
                return RedirectToAction("VehicleAllotment", "EntryI", new { NoteNumber = modelobj.NoteNumber });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack("/Security/EntryI/Create");
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                _iUser.RecordCallBack("/Security/EntryI/Create");
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "Save")
            {
                if (_IETSEdit.UpdateETSVehicleAllotmentDetails(modelobj.NoteNumber, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Submited Successfully.";
                    TempData["EntryI"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber + " Due To : " + pMsg;
                }
            }
            return View(modelobj);
        }
        #region - Ajax calling
        public JsonResult GetNoteInfo(string NoteNumber)
        {
            EditNoteDetails result= _IETSEdit.getEditNoteHdr(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoteInfoForView(string NoteNumber)
        {
            EditNoteDetails result= _IETSEdit.GetNoteHdrForEntryI(NoteNumber, 0, ref pMsg); ;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<EntryINoteList> noteList = _IETSEdit.GetEntryINoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, ref pMsg);
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
        public ActionResult TPView(string NoteNumber)
        {
            IEnumerable<EditTPDetails> modelobj= _IETSEdit.getEditTPDetails(NoteNumber, ref pMsg);
            return View("~/Areas/Security/Views/EntryI/_TravelingPersonDetails.cshtml", modelobj);
        }
        #endregion
        #region - Private functions
        private EntrICreateVM CastEntryITempData()
        {
            if (TempData["EntryI"] != null)
            {
                model = TempData["EntryI"] as EntrICreateVM;
            }
            else
            {
                model = new EntrICreateVM();
            }
            if (model.DropDownNoteList == null)
                model.DropDownNoteList = _IETSEdit.GetNoteListForEntryI(user.CentreCode, ref pMsg);

            TempData["EntryI"] = model;
            return model;
        }
        #endregion
    }
}