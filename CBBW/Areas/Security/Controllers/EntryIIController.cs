using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EntryII;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
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
        public ActionResult MLCreate() 
        {
            model = CastEntryIITempData(true);
            return View(model); 
        }
        public ActionResult LWCreate()
        {
            model = CastEntryIITempData(false);
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