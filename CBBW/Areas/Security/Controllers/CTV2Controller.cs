using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.CTV;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CTV2;
using CBBW.BOL.Master;

namespace CBBW.Areas.Security.Controllers
{
    public class CTV2Controller : Controller
    {
        IUserRepository _iUser;
        IMasterRepository _iMaster;
        ICTVRepository _iCTV;
        UserInfo user;
        string pMsg = "";
        public CTV2Controller(ICTVRepository iCTV, IUserRepository iUser, IMasterRepository iMaster)
        {
            _iCTV = iCTV;
            _iUser = iUser;
            _iMaster = iMaster;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ApprovalIndex() 
        {
            return View();
        }
        public ActionResult CreateNote() 
        {
            CreateNoteVM model = new CreateNoteVM();
            model.NoteNumber = CastNoteNumberFromTemp();
            model.VehicleNumber = CastVehicleNumberFromTemp("");
            model.ListofVehicles = _iCTV.getLCVMCVVehicleList(ref pMsg);
            model.CentreCodeName = user.CentreCode + " / " + user.CentreName;
            
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateNote(CreateNoteVM model, string Submit) 
        {
            CastVehicleNumberFromTemp(model.VehicleNumber);
            model.ListofVehicles = _iCTV.getLCVMCVVehicleList(ref pMsg);
            model.CentreCodeName = user.CentreCode + " / " + user.CentreName;
            if (Submit == "OVT")
            {
                _iUser.RecordCallBack("/Security/CTV/Create");
                return RedirectToAction("CreateOtherTrip");
            }
            return View();
        }
        public ActionResult CreateOtherTrip() 
        {
            AddOtherTripVM model = new AddOtherTripVM();
            model.NoteNumber= CastNoteNumberFromTemp(); 
            model.VehicleNumber= CastVehicleNumberFromTemp("");
            model.LocationTypes = _iCTV.getLocationTypes(ref pMsg).OrderBy(o=>o.ID);
            return View(model);
        }
        #region Ajax Calling
        public JsonResult GetEntryNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<CTVNoteList4DT> noteList = _iCTV.GetNoteListForDataTable(iDisplayLength,iDisplayStart, iSortCol_0, sSortDir_0,sSearch,user.CentreCode,false, ref pMsg); 
            var result = new
            {
                iTotalRecords = noteList.Count==0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetApprovedNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<CTVNoteList4DT> noteList = _iCTV.GetNoteListForDataTable(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, true, ref pMsg);
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
        public JsonResult GetVehicleInfo(string VehicleNumber) 
        {
            VehicleInfo result = _iCTV.getVehicleInfo(VehicleNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocationTypes()
        {
            return Json(_iCTV.getLocationTypes(ref pMsg), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetToLocationsFromTypes(string TypeIDs)
        {
            IEnumerable<LocationMaster> result = _iCTV.GetLocationsFromTypes(TypeIDs, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion Ajax Calling
        private string CastNoteNumberFromTemp()
        {
            string result = "";
            if (TempData["CTVNoteNumber"] != null)
            {
                result = TempData["CTVNoteNumber"] as string;
            }
            else 
            {
                result = _iMaster.GetNewNoteNumber(MyCodeHelper.GetNotePattern("CTV"), ref pMsg);
            }
            TempData["CTVNoteNumber"] = result;
            return result;
        }
        private string CastVehicleNumberFromTemp(string VehicleNumber)
        {
            if (string.IsNullOrEmpty(VehicleNumber)) 
            {
                if (TempData["CTVVehicleNumber"] != null)
                {
                    VehicleNumber = TempData["CTVVehicleNumber"] as string;
                }
            }                    
            TempData["CTVVehicleNumber"] = VehicleNumber;
            return VehicleNumber;
        }
    }
}