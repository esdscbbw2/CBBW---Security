using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.CTV;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CTV2;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.Areas.Security.ViewModel;
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
        public ActionResult AddNote() 
        {
            TempData["CTVCreate"] = null;
            return RedirectToAction("CreateNote");
        }
        public ActionResult ApproveNote()
        {
            TempData["CTVApprove"] = null;
            return RedirectToAction("Approval");
        }
        public ActionResult CreateNote(int isClear=0) 
        {
            CreateNoteVM model = CastCTVCreateTempData();            
            if (isClear == 1) { model.VehicleNumber = ""; }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateNote(CreateNoteVM modelobj, string Submit) 
        {
            CreateNoteVM model = CastCTVCreateTempData();
            model.NoteNumber = modelobj.NoteNumber;
            model.VehicleNumber = modelobj.VehicleNumber2;
            model.VehicleNumber2 = modelobj.VehicleNumber2;
            model.VehicleType = modelobj.VehicleType;
            model.ModelName = modelobj.ModelName;
            model.DriverName = modelobj.DriverName;
            model.DriverNo = modelobj.DriverNo;
            model.IsLocalAvbl = modelobj.IsLocalAvbl;
            model.IsOtherAvbl = modelobj.IsOtherAvbl;
            model.IsOthDtlSaved = modelobj.IsOthDtlSaved;
            model.IsLocalDtlSaved = modelobj.IsLocalDtlSaved;
            model.IsDriverEntered = modelobj.IsDriverEntered;
            model.DriverNo = MyCodeHelper.GetEmpNoFromString(modelobj.DriverName);
            TempData["CTVCreate"] = model;
            if (Submit == "OVT")
            {
                _iUser.RecordCallBack("/Security/CTV2/CreateNote");
                return RedirectToAction("CreateOtherTrip");
            }
            else if (Submit == "LVT")
            {
                _iUser.RecordCallBack("/Security/CTV2/CreateNote");
                return RedirectToAction("LocVehTripSch", new { CBUID = 1 });
            }
            else if (Submit == "TADARule")
            {
                _iUser.RecordCallBack("/Security/CTV2/CreateNote");
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack("/Security/CTV2/CreateNote");
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "LocalVehicleSchedule")
            {
                _iUser.RecordCallBack("/Security/CTV2/CreateNote");
                return RedirectToAction("AllVehicleTripSchFromMat");
            }
            else if (Submit == "create")
            {
                if (model.ListofVehicles == null)
                    model.ListofVehicles = _iCTV.getLCVMCVVehicleList(ref pMsg,user.CentreCode);
                CTVHeaderToSet setobj = new CTVHeaderToSet();
                setobj.NoteNumber = model.NoteNumber;
                setobj.CentreCode = user.CentreCode;
                setobj.CentreName = user.CentreName;
                setobj.FortheMonth = DateTime.Today.Month;
                setobj.FortheYear = DateTime.Today.Year;
                setobj.FromDate = model.FromDate;
                setobj.ToDate = model.ToDate;
                setobj.VehicleNo = model.VehicleNumber;
                setobj.VehicleType = model.VehicleType;
                setobj.ModelName = model.ModelName;
                setobj.DriverNo = model.DriverNo;
                setobj.DriverName = model.DriverName;
                setobj.EmployeeNumber = user.EmployeeNumber;
                
                if (_iCTV.CreateCTVHdrV2(setobj, ref pMsg))
                {
                    ViewBag.Msg = "Note number " + model.NoteNumber + " submited successfully.";
                    TempData["CTVCreate"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation failed for Note number " + model.NoteNumber;
                }
            }
            return View(model);
        }
        public ActionResult CreateOtherTrip() 
        {
            CreateNoteVM parentmodel = CastCTVCreateTempData();
            AddOtherTripVM model = new AddOtherTripVM();
            model.NoteNumber= parentmodel.NoteNumber;
            //model.VehicleNumber= parentmodel.VehicleNumber;
            model.VehicleNumber = "AP25X7140";
            model.LocationTypes = _iCTV.getLocationTypes(ref pMsg).OrderBy(o=>o.ID);
            model.Slots = _iCTV.GetSlots(model.VehicleNumber, 0, ref pMsg);
            return View(model);
        }
        public ActionResult AllVehicleTripSchFromMat()
        {
            LVTSFromMatVM model = new LVTSFromMatVM();
            model.LVSDataList = _iCTV.getLocalVehicleSChedules("0", model.FromDate, model.ToDate, ref pMsg).OrderBy(o => o.FromDate).ThenBy(o => o.VehicleNumber).ToList();
            return View(model);
        }
        public ActionResult LocVehTripSch(int CBUID = 0) 
        {
            CreateNoteVM parentmodel = CastCTVCreateTempData();
            LocalVehicleTripScheduleVM model = new LocalVehicleTripScheduleVM();
            ViewBag.HeaderSign = "VIEW";
            if (CBUID == 2 || CBUID == 3) { ViewBag.HeaderSign = "APPROVAL"; }
            else if (CBUID == 1) { ViewBag.HeaderSign = "ENTRY"; model.IsSaveVisible = 1; }
            model.NoteNo = parentmodel.NoteNumber;
            model.VehicleNo= parentmodel.VehicleNumber;
            //model.VehicleNo = "AP25X7140";
            model.DriverCodenName = parentmodel.DriverNo + " / " + parentmodel.DriverName;
            model.LVSchDtl = _iCTV.getLocalVehicleSChedules(model.VehicleNo, model.SCHFromDate, model.SCHToDate, ref pMsg).OrderBy(o => o.FromDate).ToList();
            return View(model);
        }
        public ActionResult Approval(int isClear = 0,int isOthViewed=0,int isLVViewed=0) 
        {
            CTVApprovalVM model = CastCTVApprovalTempData();
            model.IsLVViewed = model.IsLVViewed == 1 ? 1 : isLVViewed;
            model.IsOthViewed = model.IsOthViewed == 1 ? 1 : isOthViewed;
            if (isClear == 1) { model.NoteNumber = "";model.NoteNo = ""; }
            return View(model);
        }
        [HttpPost]
        public ActionResult Approval(CTVApprovalVM modelobj, string Submit)
        {
            CTVApprovalVM model = CastCTVApprovalTempData();
            model.NoteNumber = modelobj.NoteNumber;
            model.NoteNo = modelobj.NoteNumber;
            model.IsApproved = modelobj.IsApproved;
            model.IsApprovedComboValue = modelobj.IsApprovedComboValue;
            model.DisapprovalReason = modelobj.DisapprovalReason;
            TempData["CTVApprove"] = model;
            if (Submit == "TourRule")
            {
                _iUser.RecordCallBack("/Security/CTV2/Approval");
                return RedirectToAction("ViewRedirection", "TourRule",
                    new { Area = "Security", CBUID = 3, NoteNumber = modelobj.NoteNo });
            }
            else if (Submit == "TADARules")
            {
                _iUser.RecordCallBack("/Security/CTV2/Approval");
                return RedirectToAction("ViewRedirection", "TADARules",
                    new { Area = "Security", CBUID = 3, NoteNumber = modelobj.NoteNo });
            }
            else if (Submit == "LVTSChMat")
            {
                _iUser.RecordCallBack("/Security/CTV2/Approval");
                return RedirectToAction("LocalVehicleTripSchFromMat","CTV",
                    new { CBUID = 3, NoteNumber = modelobj.NoteNo });
            }
            else if (Submit == "OVT")
            {
                model.IsTabClicked = 1;
                TempData["CTVApprove"] = model;
                _iUser.RecordCallBack("/Security/CTV2/Approval?isOthViewed=1");
                return RedirectToAction("OtherTripSchDisplay","CTV",
                    new { Area = "Security", CBUID = 3, NoteNumber = model.NoteNo });
            }            
            else if (Submit == "LVT")
            {
                model.IsTabClicked = 1;
                TempData["CTVApprove"] = model;
                _iUser.RecordCallBack("/Security/CTV2/Approval?isLVViewed=1");
                return RedirectToAction("LocVehTripSch","CTV", new { CBUID = 3, NoteNumber = model.NoteNo });
            }
            else if (Submit == "create")
            {
                if (model.ListofNoteNumbers == null)
                    model.ListofNoteNumbers = _iCTV.GetNoteNumbersTobeApproved(user.EmployeeNumber, user.CentreCode, ref pMsg);

                //TempData["CTVHDR"] = model;
                if (_iCTV.setCTVApproval(model.NoteNo, user.EmployeeNumber, model.IsApproved,
                    DateTime.Now, model.DisapprovalReason, ref pMsg))
                {
                    ViewBag.Msg = "Approval status for Note number " + model.NoteNo + " has been updated successfully.";
                    TempData["CTVApprove"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation failed for Note number " + model.NoteNo;
                }
            }
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
        public JsonResult GetToDate(string FromDate, int FromLocationType,
            int FromLocation, string ToLocation)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            DateTime tdt = _iCTV.GetToDate(DateTime.Parse(FromDate), FromLocationType,FromLocation,ToLocation, ref pMsg);
            if (tdt != new DateTime(1, 1, 1))
            {
                result.bResponseBool = true;
                result.sResponseString = tdt.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
                result.sResponseString2 = tdt.ToString("yyyy-MM-dd");
            }
            else
            {
                result.sResponseString = "";
                result.sResponseString2 = "";
                result.bResponseBool = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetData(CTVOtherTrip model) 
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.SetCTVOtherTrip(model, ref pMsg)) 
            {
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";
                CreateNoteVM parentmodel = CastCTVCreateTempData();
                parentmodel.IsOthDtlSaved = 1;
                TempData["CTVCreate"] = parentmodel;
            } 
            else 
            {
                result.bResponseBool = false;
                result.sResponseString = pMsg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetLocData(string NoteNumber,string VehicleNo)
        {
            LocalVehicleTripScheduleVM model = new LocalVehicleTripScheduleVM();
            List<LocVehSchFromMat> data =_iCTV.getLocalVehicleSChedules(VehicleNo,model.SCHFromDate,model.SCHToDate, ref pMsg).OrderBy(o => o.FromDate).ToList();
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (data != null && data.Count > 0) 
            {
                if (_iCTV.setLocalTripSchDtls(NoteNumber, data, ref pMsg))
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";
                    CreateNoteVM parentmodel = CastCTVCreateTempData();
                    parentmodel.IsLocalDtlSaved = 1;
                    TempData["CTVCreate"] = parentmodel;
                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }
            }
            else
            {
                result.bResponseBool = false;
                result.sResponseString = "No Data Found To Update";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOtheEntryData(string NoteNumber)
        {
            CTVOtherTrip result = _iCTV.GetOthTripSchEntryData(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion Ajax Calling
        private CreateNoteVM CastCTVCreateTempData()
        {
            CreateNoteVM model;
            if (TempData["CTVCreate"] != null)
            {
                model = TempData["CTVCreate"] as CreateNoteVM;
            }
            else
            {
                model = new CreateNoteVM();
                model.NoteNumber=_iMaster.GetNewNoteNumber(MyCodeHelper.GetNotePattern("CTV"), ref pMsg);
                model.ListofVehicles = _iCTV.getLCVMCVVehicleList(ref pMsg, user.CentreCode);
                model.CentreCodeName = user.CentreCode + " / " + user.CentreName;
            }
            TempData["CTVCreate"] = model;
            return model;
        }
        private CTVApprovalVM CastCTVApprovalTempData()
        {
            CTVApprovalVM model;
            if (TempData["CTVApprove"] != null)
            {
                model = TempData["CTVApprove"] as CTVApprovalVM;
            }
            else
            {
                model = new CTVApprovalVM();
                model.ListofNoteNumbers = _iCTV.GetNoteNumbersTobeApproved(user.EmployeeNumber, user.CentreCode, ref pMsg);
                model.DateTimeofApproval = DateTime.Now;
                model.IsApprovedComboValue = -1;
                if (model.ListofNoteNumbers == null) { model.ListofNoteNumbers = new List<NoteNumber>(); }
            }
            TempData["CTVApprove"] = model;
            return model;
        }
        //private string CastNoteNumberFromTemp()
        //{
        //    string result = "";
        //    if (TempData["CTVNoteNumber"] != null)
        //    {
        //        result = TempData["CTVNoteNumber"] as string;
        //    }
        //    else 
        //    {
        //        result = _iMaster.GetNewNoteNumber(MyCodeHelper.GetNotePattern("CTV"), ref pMsg);
        //    }
        //    TempData["CTVNoteNumber"] = result;
        //    return result;
        //}
        //private string CastVehicleNumberFromTemp(string VehicleNumber)
        //{
        //    if (string.IsNullOrEmpty(VehicleNumber)) 
        //    {
        //        if (TempData["CTVVehicleNumber"] != null)
        //        {
        //            VehicleNumber = TempData["CTVVehicleNumber"] as string;
        //        }
        //    }                    
        //    TempData["CTVVehicleNumber"] = VehicleNumber;
        //    return VehicleNumber;
        //}
    }
}