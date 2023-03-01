using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EntryII;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;
namespace CBBW.Areas.Security.Controllers
{
    public class EntryIIController : Controller
    {
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IEntryIIRepository _iEntryIIRepository;
        EntryIIHdrVM model;
        public EntryIIController(IEntryIIRepository iEntryIIRepository, IUserRepository iUser)
        {
            _iUser = iUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
            _iEntryIIRepository = iEntryIIRepository;
            pMsg = "";
        }
        // GET: Security/EntryII
        public ActionResult AddNote(int ID)
        {
            TempData["EntryII"] = null;
            if (ID ==1) 
            {
                return RedirectToAction("LWCreate");
            } 
            else 
            {
                return RedirectToAction("MLCreate");
            }            
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClearBtnClicked(int PageID = 0, string NoteNumber = "")
        {            
            if (PageID == 1)
            {
                TempData["EntryII"] = null;
                return RedirectToAction("MLCreate");
            }
            else if (PageID == 2)
            {
                TempData["EntryII"] = null;
                return RedirectToAction("LWCreate");
            }
            if (PageID == 3)
            {
                model = CastEntryIITempData(true);
                return RedirectToAction("MLCreate");
            }
            else
            {
                TempData["EntryII"] = null;
                return RedirectToAction("Create");
            }
        }
        public ActionResult ViewMLNote()
        {
            return View();
        }
        public ActionResult ViewLWNote()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LocationWiseIndex()
        {
            return View();
        }
        public ActionResult MLOutIn(string NoteNumber) 
        {
            MLInnerPageVM modelobj = new MLInnerPageVM();
            modelobj.TPDetails = _iEntryIIRepository.GetMainLocationTPs(NoteNumber, ref pMsg);
            modelobj.DefaultPersonID = modelobj.TPDetails.FirstOrDefault().PersonID;
            modelobj.SchFromDate = modelobj.TPDetails.Min(o => o.SchFromDate);
            modelobj.SchToDate = modelobj.TPDetails.Max(o => o.SchToDate);
            modelobj.RFIDCardList = _iEntryIIRepository.GetRFIDCards(ref pMsg);
            modelobj.VehicleDetails = _iEntryIIRepository.GetEntryIIVehicleAllotmentDetails(NoteNumber, modelobj.SchFromDate, modelobj.SchToDate, user.CentreCode, true, ref pMsg);
            modelobj.IsVehicleProvided = modelobj.TPDetails.FirstOrDefault().IsVehicleProvided;
            modelobj.RequiredKMIn = modelobj.VehicleDetails.KMOut + _iEntryIIRepository.GetTravelKmsOfANote(NoteNumber, modelobj.SchToDate, user.CentreCode, ref pMsg);
            return View(modelobj);
        }
        public ActionResult LWOutIn(string NoteNumber)
        {
            LWInnerPageVM modelobj = new LWInnerPageVM();
            //modelobj.IsOffline = user.IsOffline;
            modelobj.IsOffline = true;
            LocationWiseTPDetails obj1 = _iEntryIIRepository.GetLocationWiseTPs(NoteNumber, user.CentreCode, ref pMsg);
            modelobj.PersonDetails = obj1.PersonDetails;
            modelobj.PersonDateWiseDetails = obj1.PersonDateWiseDetails;
            modelobj.DefaultPersonID = modelobj.PersonDetails.FirstOrDefault().PersonID;
            modelobj.SchFromDate = obj1.PersonDateWiseDetails.Min(o => o.DWFromDate);
            modelobj.SchToDate = obj1.PersonDateWiseDetails.Max(o => o.DWToDate);
            modelobj.RFIDCardList = _iEntryIIRepository.GetRFIDCards(ref pMsg);
            modelobj.VehicleDetails = _iEntryIIRepository.GetEntryIIVehicleAllotmentDetails(NoteNumber,modelobj.SchFromDate,modelobj.SchToDate,user.CentreCode,false,ref pMsg);
            return View(modelobj);
        }        
        public ActionResult MLCreate() 
        {
            model = CastEntryIITempData(true);
            return View(model); 
        }
        [HttpPost]
        public ActionResult MLCreate(EntryIIHdrVM modelobj,string Submit)
        {
            model = CastEntryIITempData(true);
            modelobj.EntryIINotes = model.EntryIINotes;
            TempData["EntryII"] = modelobj;
            if (Submit == "MLOutInBtn")
            {
                string baseUrl = "/Security/EntryII/MLCreate";
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("MLOutIn", "EntryII",new { NoteNumber=modelobj.NoteNumber });
            }
            else if (Submit == "Save") 
            {
                EditNoteDetails noteinfo = _iEntryIIRepository.GetEditNoteHdr(modelobj.NoteNumber, ref pMsg);
                if (_iEntryIIRepository.UpdateEntryIIData(modelobj.NoteNumber, noteinfo.CenterCode, noteinfo.CenterName, noteinfo.EPTour == 1 ? true : false, true, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Submited Successfully.";
                    TempData["EntryII"] = null;
                }
                else 
                {
                    ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber + " Due To : " + pMsg;
                }
            }
            return View(model);
        }
        public ActionResult LWCreate()
        {
            model = CastEntryIITempData(false);
            return View(model);
        }
        [HttpPost]
        public ActionResult LWCreate(EntryIIHdrVM modelobj, string Submit) 
        {
            model = CastEntryIITempData(false);
            modelobj.EntryIINotes = model.EntryIINotes;
            TempData["EntryII"] = modelobj;
            if (Submit == "LWOutInBtn")
            {
                string baseUrl = "/Security/EntryII/LWCreate";
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("LWOutIn", "EntryII", new { NoteNumber = modelobj.NoteNumber });
            }
            else if (Submit == "Save")
            {
                EditNoteDetails noteinfo = _iEntryIIRepository.GetEditNoteHdr(modelobj.NoteNumber, ref pMsg);
                if (_iEntryIIRepository.UpdateEntryIIData(modelobj.NoteNumber, noteinfo.CenterCode, noteinfo.CenterName, noteinfo.EPTour == 1 ? true : false, false, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Submited Successfully.";
                    TempData["EntryII"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber + " Due To : " + pMsg;
                }
            }
            return View(model);
        }
        #region - Ajax Calling
        public JsonResult getMainLocationNoteList(int iDisplayLength, int iDisplayStart,
            int iSortCol_0,string sSortDir_0, string sSearch)
        {
            List<EntryIIList> noteList = _iEntryIIRepository.GetEntryIINoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode,true, ref pMsg);
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
        public JsonResult getLocationWiseNoteList(int iDisplayLength, int iDisplayStart,
            int iSortCol_0, string sSortDir_0, string sSearch)
        {
            List<EntryIIList> noteList = _iEntryIIRepository.GetEntryIINoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, false, ref pMsg);
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
        public JsonResult GetNoteInfo(string NoteNumber) 
        {
            EditNoteDetails result = _iEntryIIRepository.GetEditNoteHdr(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TDView(string NoteNumber)
        {
            IEnumerable<EntryIITravelingDetails> modelobj = _iEntryIIRepository.GetEntryIITravellingDetails(NoteNumber, ref pMsg);
            return View("~/Areas/Security/Views/EntryII/_TravellingDetails.cshtml", modelobj);
        }
        public ActionResult VAView(string NoteNumber)
        {
            VehicleAllotmentDetails modelobj = _iEntryIIRepository.GetEntryIIVehicleAllotmentDetails(NoteNumber, ref pMsg);
            return View("~/Areas/Security/Views/EntryII/_VehicleAllotmentDetails.cshtml", modelobj);
        }
        public ActionResult VIODetails(string NoteNumber)
        {
            VehicleAllotmentDetails modelobj = _iEntryIIRepository.GetEntryIIVehicleAllotmentDetails(NoteNumber, ref pMsg);
            return View("~/Areas/Security/Views/EntryII/_VehicleInOutDetails.cshtml", modelobj);
        }
        [HttpPost]
        public ActionResult SaveLWOutIn(SaveLWInnerPageVM modelobj)
        {
            model = CastEntryIITempData(false);
            CustomAjaxResponse result = new CustomAjaxResponse();
            result.bResponseBool = _iEntryIIRepository.SetEntryIIData(modelobj.NoteNumber, false, user.CentreCode,user.IsOffline, modelobj.TPersons, modelobj.DateWiseDetails,modelobj.VDetails, ref pMsg);
            result.sResponseString = pMsg;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveMLOutIn(SaveLWInnerPageVM modelobj)
        {
            model = CastEntryIITempData(true);
            CustomAjaxResponse result = new CustomAjaxResponse();
            result.bResponseBool = _iEntryIIRepository.SetEntryIIData(modelobj.NoteNumber, true, user.CentreCode,user.IsOffline, modelobj.TPersons, modelobj.DateWiseDetails,modelobj.VDetails, ref pMsg);
            result.sResponseString = pMsg;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRFIDPunchTime(string RFIDNumber,string PunchDate)
        {
            //PunchInDetails result = new PunchInDetails();
            PunchInDetails result = _iEntryIIRepository.GetPunchingDetails(0, DateTime.Parse(PunchDate),user.CentreCode, RFIDNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        #endregion
        #region - Private functions
        private EntryIIHdrVM CastEntryIITempData(bool IsMainLocation)
        {
            if (TempData["EntryII"] != null)
            {
                model = TempData["EntryII"] as EntryIIHdrVM;
            }
            else
            {
                model = new EntryIIHdrVM();
            }
            if (model.EntryIINotes == null)
                model.EntryIINotes = _iEntryIIRepository.GetEntryIINotes(user.CentreCode, IsMainLocation, ref pMsg);
            TempData["EntryII"] = model;
            return model;
        }

        #endregion
    }
}