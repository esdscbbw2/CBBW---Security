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
        public ETSEditController(IETSEditRepository IETSEdit, IUserRepository iUser)
        {
            _iUser = iUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
            _IETSEdit = IETSEdit;
        }
        // GET: Security/ETSEdit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TourEdit() 
        {
            model = CastEHGEditTempData();
            model.TravelingPersonDetails = _IETSEdit.getEditTPDetails(model.NoteNumber, ref pMsg);
            model.DWTDetailsHistory = _IETSEdit.getDateWiseTourHistory(model.NoteNumber, 0, ref pMsg);
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
                    if (DateTime.Today >= tFromDate && DateTime.Today <= tToDate){ model.IsExtensionAllowed = 1; }
                }
            }
            return View(model);
        }
        public ActionResult IndividualEdit()
        {
            return View();
        }
        public ActionResult Create() 
        {
            if (TempData["EHGEdit"] == null) 
                model=CastEHGEditTempData();
            return View(model);
        }        
        [HttpPost]
        public ActionResult Create(ETSEditCreateVM modelobj, string Submit)
        {
            model=CastEHGEditTempData();
            modelobj.ToBeEditNoteList = model.ToBeEditNoteList;
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
                model.ToBeEditNoteList = _IETSEdit.getETSNoteListToBeEdited(user.CentreCode, ref pMsg);
            }
            TempData["EHGEdit"] = model;
            return model;
        }
    }
}