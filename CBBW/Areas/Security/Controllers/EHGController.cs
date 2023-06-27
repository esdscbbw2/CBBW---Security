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
using CBBW.BOL.Master;

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
        EHGNotApprovalVM appmodel;
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
        [HttpPost]
        public ActionResult ApproveNote(EHGNotApprovalVM modelobj, string Submit)
        {
            string baseUrl = "/Security/EHG/ApproveNote";
            if (Submit == "create")
            {
                if (_iEHG.SetEHGHdrAppStatus(modelobj.NoteNumber2,modelobj.AppStatus==1?true:false,
                    modelobj.ReasonForDisApproval,user.EmployeeNumber,ref pMsg))
                {
                    ViewBag.Msg = "Approval Status Of Note Number " + modelobj.NoteNumber2 + " Updated Successfully.";
                    TempData["EHGApp"] = null;
                }
                else { ViewBag.ErrMsg = "Approval Status Updation Failed For Note Number " + modelobj.NoteNumber2; }
            }        
            else if (Submit == "VAD")
            {                
                if (modelobj.IsDocOpened == 1)
                {
                    modelobj.VAActive = 1;
                    TempData["EHGApp"] = modelobj;
                    _iUser.RecordCallBack(baseUrl);
                    return RedirectToAction("ViewVADetails", "EHG", new { NoteNumber = modelobj.NoteNumber2, CBUID = 1 });
                }
                else 
                {
                    ViewBag.ErrMsg = "Attached Travelling Request Form Must Be Opened Before Proceeding.";
                }
            }
            else if (Submit == "DWT")
            {
                if (modelobj.IsDocOpened == 1) {
                    modelobj.DWTActive = 1;
                    TempData["EHGApp"] = modelobj;
                    _iUser.RecordCallBack(baseUrl);
                    return RedirectToAction("ViewDWTDetails", "EHG", new { NoteNumber = modelobj.NoteNumber2, CBUID = 1 });
                }
                else
                {
                    ViewBag.ErrMsg = "Attached Travelling Request Form Must Be Opened Before Proceeding.";
                }
            }
            appmodel = CastEHGAppTempData();
            return View(appmodel);
        }
        public ActionResult ApproveNote() 
        {
            appmodel = CastEHGAppTempData();
            appmodel.AppStatus = -1;
            return View(appmodel);
        }
        public ActionResult ViewVADetails(string NoteNumber,int CBUID=0) 
        {
            VehicleAllotmentDetails modelobject = _iEHG.getVehicleAllotmentDetails(NoteNumber, 1, ref pMsg);
            ViewBag.HeaderText= CBUID == 1 ? "APPROVAL" : "ENTRY";
            return View(modelobject);
        }
        public ActionResult ViewDWTDetails(string NoteNumber,int CBUID=0) 
        {
            List<DateWiseTourDetails> modelobject = _iEHG.getDateWiseTourDetails(NoteNumber, 1, ref pMsg);
            ViewBag.HeaderText = CBUID == 1 ? "APPROVAL" : "ENTRY";
            return View(modelobject);
        }
        public ActionResult ViewNote(string NoteNumber,int CanDelete=0,int CBUID=0) 
        {
            if (CBUID == 0) { _iUser.RecordCallBack("/Security/EHG/Index"); }
            if (CBUID == 1) { _iUser.RecordCallBack("/Security/EHG/NoteApproveList"); }
            EHGHeaderDisplayVM modelobj = new EHGHeaderDisplayVM();
            modelobj.NoteNumber = NoteNumber;
            modelobj.HeaderData = _iEHG.getEHGNoteHdr(NoteNumber, ref pMsg);
            modelobj.TPDetails = _iEHG.getTravelingPersonDetails(NoteNumber, 1, ref pMsg);
            modelobj.CanDelete = CanDelete == 1 ? true : false;
            modelobj.CBUID = CBUID;
            modelobj.DeleteBtn = CanDelete;
            modelobj.HeaderText = CBUID == 1 ? "APPROVAL" : "ENTRY";
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult ViewNote(EHGHeaderDisplayVM modelobj, string Submit) 
        {
            string baseUrl="/Security/EHG/ViewNote?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.DeleteBtn+"&CBUID="+modelobj.CBUID;
            if (Submit == "Delete")
            {
                if (_iEHG.RemoveEHGNote(modelobj.NoteNumber, 0, 1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";
                    TempData["EHG"] = null;
                }
                else { ViewBag.ErrMsg = "Failed To Delete Note Number " + modelobj.NoteNumber+". Because It Is Under Process Of Approval."; }

            }
            else if (Submit == "DWT") 
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewDWTDetails","EHG",new { NoteNumber=modelobj.NoteNumber, CBUID=modelobj.CBUID });
            } 
            else if (Submit == "VAD") 
            {
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewVADetails","EHG", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            modelobj.HeaderData = _iEHG.getEHGNoteHdr(modelobj.NoteNumber, ref pMsg);
            modelobj.TPDetails = _iEHG.getTravelingPersonDetails(modelobj.NoteNumber, 1, ref pMsg);
            return View(modelobj);
        }
        public ActionResult Index()
        {
            //if (_master.GetHGOpenOrNot(user.CentreCode, ref pMsg))
            //    return View();
            //else
            //    return RedirectToAction("NotOpen");
            return View();
        }
        public ActionResult NoteApproveList() 
        {
            //if (_master.GetHGOpenOrNot(user.CentreCode, ref pMsg))
            //    return View();
            //else
            //    return RedirectToAction("NotOpen");
            return View();
        }
        public ActionResult NotOpen() 
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
            model.OkToOpen = _master.GetHGOpenOrNot(user.CentreCode, ref pMsg) ? 1 : 0;
            TempData["EHG"] = model;
     
            //model.ehgHeader.DocFileName = "9e42056b-1e9c-4cc6-a022-e0fc811ce63b.png"; // Dummy code
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EHGHeaderEntryVM model, string Submit) 
        {            
            if (Submit == "create")
            {
                if (model.VehicleType == 1 && model.POA == 1)
                {
                    model.ehgHeader.AuthorisedEmployeeName = model.AuthorisedEmpNameForManagement;
                    model.ehgHeader.AuthorisedEmpNo = model.AuthorisedEmpNoForManagement2;
                    model.ehgHeader.VehicleType = model.VehicleType;
                    model.ehgHeader.MaterialStatus = model.MaterialStatus;
                    model.ehgHeader.Instructor = model.Instructor;
                    model.ehgHeader.PurposeOfAllotment = model.POA;
                    //model.ehgHeader.VehicleType = model.VehicleType;
                    EHGTravelingPersondtlsForManagement dtl = new EHGTravelingPersondtlsForManagement();
                    dtl.NoteNumber = model.ehgHeader.NoteNumber;
                    dtl.EmployeeNo = model.DriverNoForManagement2;
                    dtl.EmployeeNonName = model.DriverNameForManagement;
                    dtl.DesignationCode = _myHelper.getFirstIntegerFromString(model.DesgCodeNNameForManagement, '/');
                    dtl.DesignationCodenName = model.DesgCodeNNameForManagement;
                    dtl.FromDate = model.FromdateForMang2;
                    dtl.FromTime = model.FromTimeForMang2;
                    dtl.ToDate = model.ToDateForMang2;
                    dtl.PurposeOfVisit = model.PurposeOfVisitFoeMang2;
                    //To be changed after rfid implementation
                    dtl.ActualTourOutDate= model.FromdateForMang2;
                    dtl.ActualTourOutTime= model.FromTimeForMang2;
                    dtl.RequiredTourInDate= model.ToDateForMang2;
                    dtl.RequiredTourInTime = " ";
                    dtl.ActualTourInDate= model.ToDateForMang2;
                    dtl.ActualTourInTime = " ";
                    dtl.TourStatus = 0;

                    if (model.TADADeniedForManagement2 == 1)
                        dtl.TADADenied = true;
                    else
                        dtl.TADADenied = false;
                    if (_iEHG.SetEHGHdrForManagement(model.ehgHeader, dtl, ref pMsg))
                    { 
                        ViewBag.Msg = "Note Number " + model.ehgHeader.NoteNumber + " Submited Successfully.";
                        TempData["EHG"] = null;
                    }
                    else { ViewBag.ErrMsg = "Updation Failed For Note Number " + model.ehgHeader.NoteNumber; }
                }
                else 
                {
                    model.ehgHeader.VehicleType = model.VehicleType;
                    model.ehgHeader.MaterialStatus = model.MaterialStatus;
                    model.ehgHeader.Instructor = model.Instructor;
                    //model.ehgHeader.AuthorisedEmpNo = model.AuthorisedEmpNo;
                    model.ehgHeader.PurposeOfAllotment = model.POA;
                    if (_iEHG.UpdateEHGHdr(model.ehgHeader,ref pMsg))
                    { 
                        ViewBag.Msg = "Note Number " + model.ehgHeader.NoteNumber + " Submited Successfully.";
                        TempData["EHG"] = null;
                    }
                    else { ViewBag.ErrMsg = "Updation Failed For Note Number " + model.ehgHeader.NoteNumber; }
                }
            }
            else if (Submit == "VAD")
            {
                model = CastEHGTempData();
                return RedirectToAction("VehicleAllotment");
            }
            if (model.MDDICList == null) { model.MDDICList = model.getMDDICList(user.CentreCode); }
            if (model.DriverList == null) { model.DriverList=model.getDriverList(user.CentreCode);}
            if (model.OtherStaffList == null) { model.OtherStaffList = model.getOtherStaffList(user.CentreCode); }
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
            try
            {
                VehicleAllotmentDetails obj = _iEHG.getVehicleAllotmentDetails(model.ehgHeader.NoteNumber, 0, ref pMsg);
                model.VADetails = obj;
                model.VehicleList = _master.getVehicleList("L.C.V", model.ehgHeader.VehicleType == 1 ? 4 : 2, ref pMsg,user.CentreCode);
                model.DriverList = _iEHG.getDriverListForOfficeWork(model.ehgHeader.NoteNumber, ref pMsg);
                if (model.VADetails == null || string.IsNullOrEmpty(model.VADetails.VehicleNumber))
                {
                    int authPersonType = model.PersonDtls.Where(o => o.EmployeeNonName == model.ehgHeader.AuthorisedEmployeeName).FirstOrDefault().PersonType;
                    model.VADetails = new VehicleAllotmentDetails();
                    model.VADetails.NoteNumber = model.ehgHeader.NoteNumber;
                    model.VADetails.AuthorisedEmpName = model.ehgHeader.AuthorisedEmployeeName;
                    model.VADetails.AuthorisedEmpNumber = _myHelper.getFirstIntegerFromString(model.ehgHeader.AuthorisedEmployeeName, '/');
                    model.VADetails.DesignationText = _master.GetDesgCodenName(model.VADetails.AuthorisedEmpNumber, authPersonType);
                    model.VADetails.DesignationCode = _myHelper.getFirstIntegerFromString(model.VADetails.DesignationText, '/');
                    model.VADetails.MaterialStatus = -1;
                    model.VADetails.DriverNumber = -1;
                    model.VADetails.VehicleType = model.ehgHeader.VehicleType == 1 ? "LV" : "2 Wheeler";
                }
                model.VADetails.DriverNumber = model.DriverList != null ? model.DriverList.Where(o=>o.ID!=-1).FirstOrDefault().ID : -1;
                model.DriverNumber = model.VADetails.DriverNumber;
                if (model.DriverList != null && model.DriverList.Where(o => o.ID != -1).Count() > 1)
                {
                    model.IsDriverCtrlEnable = 1;
                    model.VADetails.DriverNumber = -1;
                    model.DriverNumber = -1;
                }
                else { model.IsDriverCtrlEnable = 0; }
                //if(model.VADetails!=null)
                //    model.OthVehNo = model.VADetails.OtherVehicleNumber;
                model.IsBtn = 0;
                if (model.PersonDtls != null) 
                {
                    model.FromdateForMang = model.PersonDtls.Select(o => o.FromDate).Min();
                    model.ToDateForMang = model.PersonDtls.Select(o => o.ToDate).Max();
                }                
            }
            catch { }
            return View(model);
        }
        [HttpPost]
        public ActionResult VehicleAllotment(EHGHeaderEntryVM modelobj, string Submit) 
        {
            model = CastEHGTempData();
            if (modelobj.VADetails != null) { modelobj.VADetails.DriverNumber = modelobj.DriverNumber; }
            model.VADetails = modelobj.VADetails;            
            if (Submit == "Save") 
            {
                if (_iEHG.SetEHGVehicleAllotmentDetails(model.VADetails, ref pMsg))
                {
                    model.VASubmitBtnActive = 1;
                    TempData["EHG"] = model;
                    //return RedirectToAction("Create");
                    ViewBag.Msg = "Vehicle Allotment Details Updated Successfully.";
                }
                else { ViewBag.ErrMsg = "Updation Failed."; }
            }
            TempData["EHG"] = model;
            return View(model);
        }
        public ActionResult AddNote(int ID) 
        {
            TempData["EHG"] = null;
            TempData["EHGApp"] = null;
            if (ID == 0) { return RedirectToAction("Create"); }
            else { return RedirectToAction("ApproveNote"); }
        }
        #region AjaxCalling 
        public JsonResult GetNoteHdrTPD(string NoteNumber)
        {
            EHGNotApprovalVM result = new EHGNotApprovalVM();
            result.NoteNumber = NoteNumber;
            result.Header = _iEHG.getEHGNoteHdr(NoteNumber, ref pMsg,1,user.EmployeeNumber);
            result.TPDetails = _iEHG.getTravelingPersonDetails(NoteNumber, 1, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
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
            else if (PageID == 3)
            {
                TempData["EHGApp"] = null;
                return RedirectToAction("ApproveNote");
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
        public JsonResult GetTourLocations(string CategoryIDs)
        {
            IEnumerable<LocationMaster> result;
            CategoryIDs = CategoryIDs.Replace('_', ',');
            IEnumerable<LocationMaster> result2 =_master.GetCentresFromTourCategory(CategoryIDs, ref pMsg);
            result = result2.OrderBy(o => o.TypeID).ThenBy(o => o.ID).ToList();
            return Json(result.Where(o=>o.CentreCode!=user.CentreCode).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCategories()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            
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
            //result = tempobj.getStaffList(user.CentreCode);
            result = _master.GetEmployeeListV2(user.CentreCode,ref pMsg);
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
            if (result != null) { result.ModelName = result.ModelName == null ? "NA" : result.ModelName; }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDesgCodenName(int empID,int empType)
        {//empType : 2-driver, 1-Others
            return Json(_master.GetDesgCodenName(empID, empType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNoteList(int iDisplayLength,int iDisplayStart,int iSortCol_0,
            string sSortDir_0,string sSearch) 
        {
            List<EHGNoteList> noteList = _iEHG.GetEHGNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch,user.CentreCode,false,ref pMsg);
            var result = new
            {
                iTotalRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart= iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getApprovedNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<EHGNoteList> noteList = _iEHG.GetEHGNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, true, ref pMsg);
            var result = new
            {
                iTotalRecords = noteList.Count==0?0:noteList.FirstOrDefault().TotalCount,
                //iTotalDisplayRecords = noteList.Count(),
                iTotalDisplayRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
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
            model.ehgHeader.InstructorName = modelobj.InstructorName;
            model.ehgHeader.DocFileName = modelobj.DocFileName;
            model.PersonDtls = modelobj.PersonDtls;
            model.VehicleType= modelobj.VehicleType;
            model.MaterialStatus = modelobj.MaterialStatus;
            model.Instructor= modelobj.Instructor;
            model.POA= modelobj.PurposeOfAllotment;
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
                    model = CastEHGTempData();
                    model.DWSubmitBtnActive = 1;
                    TempData["EHG"] = model;
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
        public ActionResult GetEmployeeValidationForTour(string Employees,DateTime FromDate,DateTime ToDate )
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (Employees != null && Employees!="null")
            {
                if (_master.GetEmployeeValidationForTour(user.CentreCode, Employees, FromDate, ToDate, ref pMsg))
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Validated Successfully.";
                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }
            }
            else {
                result.bResponseBool = true;
                result.sResponseString = "Validated Successfully.";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVehicleValidationForTour(string VehicleNumber,int KMLimit, string FromDate, string ToDate)
        {
            VehicleBasicInfo result=new VehicleBasicInfo();
            
            if (VehicleNumber != null && VehicleNumber != "null")
            {
                if (_iEHG.VehicleAvailableValidationForHG(VehicleNumber,user.CentreCode,DateTime.Parse(FromDate), DateTime.Parse(ToDate), KMLimit, ref pMsg))
                {
                    result = _master.getVehicleBasicInfo(VehicleNumber, ref pMsg);
                    if (result != null) { result.ModelName = result.ModelName == null ? "NA" : result.ModelName; }
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Msg = pMsg;
                }
            }
            else
            {
                result.IsSuccess = true;
                result.Msg = "Validated Successfully.";
            }
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
        private EHGNotApprovalVM CastEHGAppTempData()
        {
            if (TempData["EHGApp"] != null)
            {
                appmodel = TempData["EHGApp"] as EHGNotApprovalVM;
            }
            else
            {
                appmodel = new EHGNotApprovalVM();
                appmodel.AppStatus = -1;
            }
            if (appmodel.NoteList == null)
            {
                appmodel.NoteList = _iEHG.getNoteListToBeApproved(user.CentreCode, ref pMsg);
            }
            TempData["EHGApp"] = appmodel;
            return appmodel;
        }
        #endregion
    }
}