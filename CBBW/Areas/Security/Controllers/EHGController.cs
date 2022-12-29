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
                if (_iEHG.SetEHGHdrAppStatus(modelobj.NoteNumber,modelobj.AppStatus==1?true:false,
                    modelobj.ReasonForDisApproval,user.EmployeeNumber,ref pMsg))
                {
                    ViewBag.Msg = "Approval Status Of Note Number " + modelobj.NoteNumber + " Updated Successfully.";
                    TempData["EHGApp"] = null;
                }
                else { ViewBag.ErrMsg = "Approval Status Updation Failed For Note Number " + modelobj.NoteNumber; }
            }        
            else if (Submit == "VAD")
            {
                modelobj.VAActive = 1;
                TempData["EHGApp"] = modelobj;
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewVADetails", "EHG", new { NoteNumber = modelobj.NoteNumber, CBUID = 1 });
            }
            else if (Submit == "DWT")
            {
                modelobj.DWTActive = 1;
                TempData["EHGApp"] = modelobj;
                _iUser.RecordCallBack(baseUrl);
                return RedirectToAction("ViewDWTDetails", "EHG", new { NoteNumber = modelobj.NoteNumber, CBUID = 1 });
            }
            appmodel = CastEHGAppTempData();
            return View(appmodel);
        }
        public ActionResult ApproveNote() 
        {
            appmodel = CastEHGAppTempData();            
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
            modelobj.HeaderText = CBUID == 1 ? "APPROVAL" : "ENTRY";
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult ViewNote(EHGHeaderDisplayVM modelobj, string Submit) 
        {
            string baseUrl="/Security/EHG/ViewNote?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.CanDelete+"&CBUID="+modelobj.CBUID;
            if (Submit == "Delete")
            {
                if (_iEHG.RemoveEHGNote(modelobj.NoteNumber, 0, 1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";
                    TempData["EHG"] = null;
                }
                else { ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber; }

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
            return View();
        }
        public ActionResult NoteApproveList() 
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
            if (Submit == "create")
            {
                if (model.ehgHeader.VehicleType == 1 && model.ehgHeader.PurposeOfAllotment == 1)
                {
                    model.ehgHeader.AuthorisedEmployeeName = model.AuthorisedEmpNameForManagement;
                    model.ehgHeader.AuthorisedEmpNo = model.AuthorisedEmpNoForManagement;
                    EHGTravelingPersondtlsForManagement dtl = new EHGTravelingPersondtlsForManagement();
                    dtl.NoteNumber = model.ehgHeader.NoteNumber;
                    dtl.EmployeeNo = model.DriverNoForManagement;
                    dtl.EmployeeNonName = model.DriverNameForManagement;
                    dtl.DesignationCode = _myHelper.getFirstIntegerFromString(model.DesgCodeNNameForManagement, '/');
                    dtl.DesignationCodenName = model.DesgCodeNNameForManagement;
                    dtl.FromDate = model.FromdateForMang;
                    dtl.FromTime = model.FromTimeForMang;
                    dtl.ToDate = model.ToDateForMang;
                    dtl.PurposeOfVisit = model.PurposeOfVisitFoeMang;
                    //To be changed after rfid implementation
                    dtl.ActualTourOutDate= model.FromdateForMang;
                    dtl.ActualTourOutTime= model.FromTimeForMang;
                    dtl.RequiredTourInDate= model.ToDateForMang;
                    dtl.RequiredTourInTime = " ";
                    dtl.ActualTourInDate= model.ToDateForMang;
                    dtl.ActualTourInTime = " ";
                    dtl.TourStatus = 0;

                    if (model.TADADeniedForManagement == 1)
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
            if (Submit == "Save") 
            {
                if (_iEHG.SetEHGVehicleAllotmentDetails(model.VADetails, ref pMsg))
                {
                    model.VASubmitBtnActive = 1;
                    TempData["EHG"] = model;
                    return RedirectToAction("Create");
                }
                else { }
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
            result.Header = _iEHG.getEHGNoteHdr(NoteNumber, ref pMsg);
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
        public JsonResult getNoteList(int iDisplayLength,int iDisplayStart,int iSortCol_0,
            string sSortDir_0,string sSearch) 
        {
            List<EHGNoteList> noteList = _iEHG.GetEHGNoteList(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch,user.CentreCode,false,ref pMsg);
            var result = new
            {
                iTotalRecords = noteList.Count==0?0:noteList.FirstOrDefault().TotalCount,
                //iPages=10,
                //iCurrentPage=1,
                iTotalDisplayRecords = noteList.Count(),
                iDisplayLength= iDisplayLength,
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
                //iPages=10,
                //iCurrentPage=1,
                iTotalDisplayRecords = noteList.Count(),
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