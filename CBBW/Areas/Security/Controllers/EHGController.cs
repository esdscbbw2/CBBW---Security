using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EHG;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using System.Globalization;

namespace CBBW.Areas.Security.Controllers
{
    public class EHGController : Controller
    {
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        string pMsg;
        UserInfo user;
        IEHGRepository _iEHG;
        EHGHeaderEntryVM model;        
        public EHGController(IUserRepository iUser, IEHGRepository iEHG, 
            IMyHelperRepository myHelper, IMasterRepository master)
        {
            _iUser = iUser;
            _iEHG = iEHG;
            _myHelper = myHelper;
            _master = master;
            pMsg = "";
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        // GET: Security/EHG        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create() 
        {            
            if (TempData["EHG"] == null)
            {
                model = new EHGHeaderEntryVM(user.CentreCode);
                model.ehgHeader = _iEHG.getNewEHGHeader(ref pMsg);
                model.FromdateForMang = DateTime.Today.AddDays(-1);
                model.ToDateForMang = DateTime.Today.AddDays(-1);
                model.MaxFromDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                model.MinFromDate = DateTime.Today.ToString("yyyy-MM-dd");
                model.TADADeniedForManagement = -1;
                if (TempData["BackBtn"] != null) 
                {
                    model.BackBtnActive = int.Parse(TempData["BackBtn"].ToString());
                }
            }
            else 
            { 
                model = TempData["EHG"] as EHGHeaderEntryVM;
                if (model.MDDICList == null) { model.MDDICList = model.getMDDICList(user.CentreCode); }
                if (model.DriverList == null) { model.DriverList = model.getDriverList(user.CentreCode); }
                if (model.StaffList == null) { model.StaffList = model.getStaffList(user.CentreCode); }
                if (model.OtherStaffList == null) { model.OtherStaffList = model.getOtherStaffList(user.CentreCode); }
            }

            TempData["EHG"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EHGHeaderEntryVM model, string Submit) 
        {
            TempData["EHG"] = model;
            if (Submit == "create")
            {
                if (model.ehgHeader.VehicleType == 1 && model.ehgHeader.PurposeOfAllotment == 1)
                {
                    model.ehgHeader.AuthorisedEmployeeName = model.AuthorisedEmpNameForManagement;
                    model.ehgHeader.AuthorisedEmpNo = model.AuthorisedEmpNoForManagement;
                    EHGTravelingPersondtls dtl = new EHGTravelingPersondtls();
                    dtl.NoteNumber = model.ehgHeader.NoteNumber;
                    dtl.EmployeeNo = model.DriverNoForManagement;
                    dtl.EmployeeNonName = model.DriverNameForManagement;
                    dtl.DesignationCode = _myHelper.getFirstIntegerFromString(model.DesgCodeNNameForManagement, '/');
                    dtl.DesignationCodenName = model.DesgCodeNNameForManagement;
                    dtl.FromDate = model.FromdateForMang;
                    dtl.FromTime = model.FromTimeForMang;
                    dtl.ToDate = model.ToDateForMang;
                    dtl.PurposeOfVisit = model.PurposeOfVisitFoeMang;
                    if (model.TADADeniedForManagement == 1)
                        dtl.TADADenied = true;
                    else
                        dtl.TADADenied = false;
                    if (_iEHG.SetEHGHdrForManagement(model.ehgHeader, dtl, ref pMsg))
                    { ViewBag.Msg = "Note number " + model.ehgHeader.NoteNumber + " submited successfully."; }
                    else { ViewBag.ErrMsg = "Updation failed for Note number " + model.ehgHeader.NoteNumber; }
                }
                else 
                {
                    if (_iEHG.UpdateEHGHdr(model.ehgHeader,ref pMsg))
                    { ViewBag.Msg = "Note number " + model.ehgHeader.NoteNumber + " submited successfully."; }
                    else { ViewBag.ErrMsg = "Updation failed for Note number " + model.ehgHeader.NoteNumber; }
                }
            }
            else if (Submit == "VAD")
            {
                return RedirectToAction("VehicleAllotment");
            }            
            return View(model);
        }
        public ActionResult DateWiseTourDetails() 
        {
            model = CastEHGTempData();
            model.FromdateForMang= model.PersonDtls.Select(o => o.FromDate).Min();
            model.ToDateForMang = model.PersonDtls.Select(o => o.ToDate).Max();
            model.FromdateStrForDisplay = model.FromdateForMang.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            model.FromdateStr = model.FromdateForMang.ToString("yyyy-MM-dd");
            model.TodateStr = model.ToDateForMang.ToString("yyyy-MM-dd");
            return View(model);
        }
        public ActionResult VehicleAllotment() 
        {
            model = CastEHGTempData();
            VehicleAllotmentDetails obj= _iEHG.getVehicleAllotmentDetails(model.ehgHeader.NoteNumber, 0, ref pMsg);
            model.VADetails = obj;
            model.VehicleList = _master.getVehicleList("L.C.V",model.ehgHeader.VehicleType==1?4:2, ref pMsg);
            if (model.DriverList == null) { model.DriverList = model.getDriverList(user.CentreCode); }
            if (model.VADetails == null || string.IsNullOrEmpty(model.VADetails.VehicleNumber)) 
            {
                model.VADetails = new VehicleAllotmentDetails();
                model.VADetails.NoteNumber = model.ehgHeader.NoteNumber;
                model.VADetails.AuthorisedEmpName = model.ehgHeader.AuthorisedEmployeeName;
                model.VADetails.AuthorisedEmpNumber = _myHelper.getFirstIntegerFromString(model.ehgHeader.AuthorisedEmployeeName, '/');
                model.VADetails.DesignationText = _master.GetDesgCodenName(model.VADetails.AuthorisedEmpNumber, 1);
                model.VADetails.DesignationCode = _myHelper.getFirstIntegerFromString(model.VADetails.DesignationText, '/');
                model.VADetails.MaterialStatus = -1;
                model.VADetails.VehicleType = model.ehgHeader.VehicleType==1?"LV": "2 Wheeler";                
            }            
            return View(model);
        }
        [HttpPost]
        public ActionResult VehicleAllotment(EHGHeaderEntryVM modelobj, string Submit) 
        {
            model = CastEHGTempData();
            model.VADetails = modelobj.VADetails;
            TempData["EHG"] = model;
            if (Submit == "Save") 
            {
                if (_iEHG.SetEHGVehicleAllotmentDetails(model.VADetails, ref pMsg))
                {
                    return RedirectToAction("Create");
                }
                else { }
            }
                       
            return View(model);
        }
        #region AjaxCalling
        public ActionResult ClearBtnClicked(int PageID=0) 
        {
            model = CastEHGTempData();
            if (PageID == 1)
            {
                model.DWBackBtnActive = 1;
                TempData["EHG"] = model;
                _iEHG.RemoveEHGNote(model.ehgHeader.NoteNumber, 2, 0,ref pMsg);
                return RedirectToAction("DateWiseTourDetails");
            }
            else if (PageID == 2)
            {
                model.VABackBtnActive = 1;
                TempData["EHG"] = model;
                _iEHG.RemoveEHGNote(model.ehgHeader.NoteNumber, 3, 0, ref pMsg);
                return RedirectToAction("VehicleAllotment");
            }
            else
            {
                TempData["BackBtn"] = 1;
                _iEHG.RemoveEHGNote(model.ehgHeader.NoteNumber, 1, 0, ref pMsg);
                TempData["EHG"] = null;
                return RedirectToAction("Create");
            }
            //string url = "/Security/EHG/Create";
            //return Json(url, JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult BackButtonClicked()
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourLocations(string CategoryID)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            //model = CastEHGTempData();
            //if (model.PersonType == null)
            //{
            //    EHGMaster master = EHGMaster.GetInstance;
            //    result = master.TourCategory;
            //}
            //else { result = model.TourCategory; }
            EHGMaster master = EHGMaster.GetInstance;
            result = master.TourCategory;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCategories()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            //model = CastEHGTempData();
            //if (model.PersonType == null)
            //{
            //    EHGMaster master = EHGMaster.GetInstance;
            //    result = master.TourCategory;
            //}
            //else { result = model.TourCategory; }
            EHGMaster master = EHGMaster.GetInstance;
            result = master.TourCategory;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonTypes()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            //model = CastEHGTempData();
            //if (model.PersonType == null)
            //{
            //    EHGMaster master = EHGMaster.GetInstance;
            //    result = master.PersonType;
            //}
            //else { result = model.PersonType;}
            EHGMaster master = EHGMaster.GetInstance;
            result = master.PersonType;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverList()
        {
            IEnumerable<CustomComboOptions> result;
            //model = CastEHGTempData();
            //if (model.DriverList == null)
            //    result = model.getDriverList(user.CentreCode);
            //else  
            //    result = model.DriverList; 
            EHGHeaderEntryVM tempobj = new EHGHeaderEntryVM(true);
            result = tempobj.getDriverList(user.CentreCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStaffList()
        {
            IEnumerable<CustomComboOptions> result;
            //model = CastEHGTempData();
            //if (model.StaffList == null)
            //    result = model.getStaffList(user.CentreCode);
            //else
            //    result = model.StaffList;
            EHGHeaderEntryVM tempobj = new EHGHeaderEntryVM(true);
            result = tempobj.getStaffList(user.CentreCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTPDetails(string NoteNumber)
        {
            List<EHGTravelingPersondtlsForManagement> result=_iEHG.getTravelingPersonDetails(NoteNumber, 0, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDWTDetails(string NoteNumber,int isActive)
        {
            List<DateWiseTourDetails> result = _iEHG.getDateWiseTourDetails(NoteNumber, isActive, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult GetVehicleBasicInfo(string VehicleNumber)
        {
            VehicleBasicInfo result=_master.getVehicleBasicInfo(VehicleNumber,ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDesgCodenName(int empID,int empType)
        {//empType : 2-driver, 1-Others
            return Json(_master.GetDesgCodenName(empID, empType), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetTravelingPersonDetails(EHGTravellingPersonsVM modelobj) 
        {
            model = CastEHGTempData();
            model.ehgHeader.VehicleType = modelobj.VehicleType;
            model.ehgHeader.PurposeOfAllotment = modelobj.PurposeOfAllotment;
            model.ehgHeader.MaterialStatus = modelobj.MaterialStatus;
            model.ehgHeader.Instructor = modelobj.Instructor;
            model.ehgHeader.AuthorisedEmployeeName = modelobj.AuthorisedEmployeeName;
            model.PersonDtls = modelobj.PersonDtls;
            TempData["EHG"] = model;
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (modelobj != null) 
            {
                if (_iEHG.SetEHGTravellingPersonDetails(modelobj.NoteNumber, modelobj.AuthorisedEmployeeName,
                    modelobj.PersonDtls, ref pMsg)) 
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";
                   // _iUser.RecordCallBack("/Security/EHG/Create");
                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }                
            }
            //TempData["EHG"] = model;
            //return RedirectToActionPermanent("DateWiseTourDetails");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetDateWiseTourDtls(DateWiseTourDtlVM modelobj)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (modelobj != null)
            {
                if (_iEHG.SetDateWiseTourDetails(modelobj.NoteNumber, modelobj.DateWiseList, ref pMsg))
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";
                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }
            }
            //return RedirectToAction("index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Private Functions
        //Private Functions
        private EHGHeaderEntryVM CastEHGTempData() 
        {
            if (TempData["EHG"] != null)
            {
                model = TempData["EHG"] as EHGHeaderEntryVM;
            }
            else 
            {
                model = new EHGHeaderEntryVM();
            }
            TempData["EHG"] = model;
            return model;
        }
        #endregion
    }
}