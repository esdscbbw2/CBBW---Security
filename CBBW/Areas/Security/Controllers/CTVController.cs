﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;

namespace CBBW.Areas.Security.Controllers
{
    public class CTVController : Controller
    {
        string pMsg;
        ICTVRepository _iCTV;
        
        public CTVController(ICTVRepository iCTV)
        {
            _iCTV = iCTV;
            pMsg = "";
        }
        public ActionResult OtherTripSchDisplay(string NoteNumber,int CBUID=2) 
        {            
            getLogInUserInfo();
            OtherTripSchDisplayVM model = new OtherTripSchDisplayVM();
            CTVHdrDtl obj = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg);
            if (obj != null) 
            {
                model.SchDetailList = obj.SchDetailList;
                model.NoteNumber = obj.SchHdrData.NoteNo;
                model.TripPurpose = obj.SchHdrData.TripPurpose;
            }
            if (CBUID == 2)
            {
                model.CallBackUrl = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                //TempData["LVTScallbackurl"] = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                //model.IsSaveVisible = 0;
            }
            return View(model);
        }
        public ActionResult ViewNote(string NoteNumber) 
        {
            getLogInUserInfo();
            TripScheduleHdr model = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg).SchHdrData;
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewNote(TripScheduleHdr model, string Submit) 
        {
            UserInfo user = getLogInUserInfo();
            TempData["CTVHDR"] = model;
            if (Submit == "OVT")
            {
                return RedirectToAction("OtherTripSchDisplay", new { Area = "Security", NoteNumber = model.NoteNo });
            }
            else if (Submit == "TADARules")
            {
                return RedirectToAction("ViewRedirection", "TADARules", 
                    new { Area = "Security", CBUID = 2, NoteNumber = model.NoteNo });
            }
            else if (Submit == "TourRule")
            {
                return RedirectToAction("ViewRedirection", "TourRule", 
                    new { Area = "Security", CBUID = 2, NoteNumber=model.NoteNo });
            }
            else if (Submit == "LVT")
            {
                //TempData["CTVHDR"] = model;
                return RedirectToAction("LocVehTripSch", new { CBUID = 2, NoteNumber = model.NoteNo });
            }
            else if (Submit == "LVTSChMat")
            {
                return RedirectToAction("LocalVehicleTripSchFromMat", new { CBUID = 2, NoteNumber = model.NoteNo });
            }
            
            return View(model);
        }
        public ActionResult DeleteTripSchedule(string NoteNumber)
        {
            //UserInfo user = getLogInUserInfo();
            if (!string.IsNullOrEmpty(NoteNumber)) 
            {
                bool x=_iCTV.RemoveNote(NoteNumber, 3, ref pMsg);
                if (x) 
                { 
                
                }
            }            
            // return View();
            return RedirectToAction("ScheduleLists");

        }
        public ActionResult LocalVehicleTripSchFromMat(int CBUID=1,string NoteNumber="") 
        {            
            LVTSFromMatVM model = new LVTSFromMatVM();
            if (CBUID == 1)
            {
                model.CallBackUrl = "/Security/CTV/Create";
            }
            else if (CBUID == 2) { model.CallBackUrl = "/Security/CTV/ViewNote?NoteNumber="+ NoteNumber; }
            TripScheduleHdr obj;
            if (TempData["CTVHDR"] != null) 
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                model.FromDate = obj.FromDate;
                model.ToDate = obj.ToDate;
                //model.FromDate= new DateTime(2022, 8, 1);
                //model.ToDate= new DateTime(2022, 8, 15);
                model.LVSDataList = _iCTV.getLocalVehicleSChedules("0", model.FromDate, model.ToDate, ref pMsg).OrderBy(o=>o.FromDate).ThenBy(o=>o.VehicleNumber).ToList();
                TempData["CTVHDR"] = obj;
            }
            return View(model);
        }
        public ActionResult Approval() 
        {
            UserInfo user = getLogInUserInfo();
            CTVApprovalVM model = new CTVApprovalVM();
            model.ListofNoteNumbers = _iCTV.GetNoteNumbersTobeApproved(123,user.CentreCode,ref pMsg);
            model.DateTimeofApproval = DateTime.Now;
            return View(model);
        }
        //[HttpPost]
        //public ActionResult LocalVehicleTripSchFromMat() 
        //{

        //}
        public ActionResult LocVehTripSch(int CBUID,string NoteNumber="") 
        {            
            TripScheduleHdr obj = new TripScheduleHdr();
            if (TempData["CTVHDR"] != null) 
            { 
                obj = TempData["CTVHDR"] as TripScheduleHdr;                
                TempData["CTVHDR"] = obj;
            }
            LocalVehicleTripScheduleVM model = new LocalVehicleTripScheduleVM();
            model.SCHFromDate = obj.FromDate;
            model.SCHToDate = obj.ToDate;
            model.VehicleNo = obj.Vehicleno;
            ////model.SCHFromDate = new DateTime(2022, 8, 1);
            ////model.SCHToDate = new DateTime(2022, 8, 15);
            ////model.VehicleNo = "AP25X1541";

            model.DriverCodenName = obj.DriverNonName;
            model.LVSchDtl = _iCTV.getLocalVehicleSChedules(model.VehicleNo, model.SCHFromDate, model.SCHToDate, ref pMsg);

            if (CBUID == 1)
            {
                model.CallBackUrl= "/Security/CTV/Create";
                //TempData["LVTScallbackurl"] = "/Security/CTV/Create";
                model.IsSaveVisible = 1;
            }
            if (CBUID == 2)
            {
                model.CallBackUrl= "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                //TempData["LVTScallbackurl"] = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                model.IsSaveVisible = 0;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult LocVehTripSch(LocalVehicleTripScheduleVM model) 
        {
           //string callbackurl=TempData["LVTScallbackurl"] != null ? TempData["LVTScallbackurl"].ToString() : "/Security/CTV/Create";
            TripScheduleHdr obj = new TripScheduleHdr();
            if (TempData["CTVHDR"] != null)
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                obj.IsOTSActivated = 1;
                TempData["CTVHDR"] = obj;
            }
            return Redirect(model.CallBackUrl);
        }
        public ActionResult Index()
        {
            UserInfo model = new UserInfo();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(UserInfo model)
        {
            model = _iCTV.getUserInfo(model.UserName, ref pMsg);
            TempData["LogInUser"] = model;
            return RedirectToAction("ScheduleLists");
        }
        public ActionResult ScheduleLists() 
        {
            UserInfo user = getLogInUserInfo();
            IEnumerable<TripScheduleHdr> model = _iCTV.getCtvSchedule(user.CentreCode, ref pMsg);
            return View(model);
        }
        [HttpPost]
        public ActionResult ScheduleLists(string Submit)
        {
            UserInfo user = getLogInUserInfo();
            TempData["CTVHDR"] = null;
            return RedirectToAction("Create");
        }
        private UserInfo getLogInUserInfo() 
        {
            UserInfo user = new UserInfo(true);
            if (TempData["LogInUser"] != null)
            {
                user = TempData["LogInUser"] as UserInfo;
            }
            TempData["LogInUser"] = user;
            return user;
        }
        public ActionResult Create() 
        {
            UserInfo user= getLogInUserInfo();
            TripScheduleHdr model;
            if (TempData["CTVHDR"] == null)
            {
                string schpattern = "200001-CTV-" + DateTime.Today.ToString("yyyyMMdd") + "-";
                TempData["CTVHDR"] = _iCTV.NewTripScheduleNo(schpattern, ref pMsg);
            }
            model = TempData["CTVHDR"] as TripScheduleHdr;
            TempData["CTVHDR"] = model;
            if (model.ListofVehicles == null)
            {
                model.ListofVehicles=_iCTV.getLCVMCVVehicleList(ref pMsg);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TripScheduleHdr model, string Submit) 
        {
            UserInfo user= getLogInUserInfo();
            TempData["CTVHDR"] = model;
            if (Submit == "OVT")
            {
                return RedirectToAction("OtherTrip");
            }
            else if (Submit == "TADARules")
            {
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TourRule") 
            {
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "LVT")
            {
                //TempData["CTVHDR"] = model;
                return RedirectToAction("LocVehTripSch",new { CBUID =1});
            }
            else if (Submit == "LVTSChMat")
            {
                return RedirectToAction("LocalVehicleTripSchFromMat");
            }
            else if (Submit == "create")
            {
                if (model.ListofVehicles == null)
                    model.ListofVehicles = _iCTV.getLCVMCVVehicleList(ref pMsg);

                //model.IsOTSSaved = 0;
                model.EntryDate = DateTime.Today;
                model.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
                model.IsActive = true;
                model.EmployeeNumber = user.EmployeeNumber;
                //TempData["CTVHDR"] = model;
                if (_iCTV.CreateNewCTVHdr(model, ref pMsg))
                {
                    ViewBag.Msg = "Note number " + model.NoteNo + " submited successfully.";
                    TempData["CTVHDR"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation failed for Note number " + model.NoteNo;
                }
            }
                return View(model);
        }
        public ActionResult OtherTrip() 
        {
            OtherTripScheduleEntryVM model = new OtherTripScheduleEntryVM();
            model.MinDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            TripScheduleHdr obj;
            if (TempData["CTVHDR"] != null) 
            { 
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                model.NoteNumber = obj.NoteNo;
                model.VehicleNo = obj.Vehicleno;
                model.TripPurpose = obj.TripPurpose;
                model.DriverCode = obj.DriverNo;
                model.DriverName = obj.DriverName;                
                model.MaxDate=obj.ToDate.ToString("yyyy-MM-dd");
                TempData["CTVHDR"] = obj; 
            }
                      
            //model.VehicleNo = "AP25X1541";
            return View(model);
        }
        [HttpPost]
        public ActionResult OtherTrip(OtherTripScheduleEntryVM model) 
        {            
            model.MinDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            TripScheduleHdr obj;
            if (TempData["CTVHDR"] != null)
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                model.NoteNumber = obj.NoteNo;
                model.VehicleNo = obj.Vehicleno;
                model.TripPurpose = obj.TripPurpose;
                model.DriverCode = obj.DriverNo;
                model.DriverName = obj.DriverName;
                model.MaxDate = obj.ToDate.ToString("yyyy-MM-dd");
                TempData["CTVHDR"] = obj;
                _iCTV.RemoveNote(model.NoteNumber, 1, ref pMsg);
            }
            return View(model);
        }
        public JsonResult GetLocationTypes()
        {
            return Json(_iCTV.getLocationTypes(ref pMsg), JsonRequestBehavior.AllowGet);
        }  
        
        public JsonResult GetToLocationsFromType(string TypeIDs) 
        {
            IEnumerable<CustomComboOptions> result = _iCTV.getLocationsFromType(TypeIDs, ref pMsg);
            //result = result.Take(5);
            return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(_iCTV.getLocationsFromType(TypeIDs, ref pMsg), JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            //int x = 1;
            return Json(_iCTV.getLocationsFromType(TypeID, ref pMsg), JsonRequestBehavior.AllowGet);            
        }
        public JsonResult GetVehicleInfo(string VehicleNo,string NoteNumber,int RemoveTemp=0)
        {
            if (RemoveTemp == 1) 
            { 
                _iCTV.RemoveNote(NoteNumber, 1, ref pMsg); 
            }            
            VehicleInfo result = _iCTV.getVehicleInfo(VehicleNo, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSchToDate(string Fromdate, string FromTime, int FromLocation,
            string ToLocationType, string ToLocation,string VehicleNo)
        {
            //int mtolocation = 4;
            //int mtolocationtype = 3;
            CustomAjaxResponse result = new CustomAjaxResponse();
            DateTime SChFromDate = DateTime.Parse(Fromdate +" "+ FromTime);
            DateTime st= _iCTV.getSchToDateFromMultiLocation(VehicleNo,SChFromDate, FromLocation,
                ToLocationType, ToLocation, ref pMsg);
            if (st != new DateTime(1, 1, 1))
            {
                result.bResponseBool = true;
                result.sResponseString = st.ToString("dd-MM-yyyy");
            }
            else 
            { 
                result.sResponseString = "";
                result.bResponseBool = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckSchDateAvl(string VehicleNo, string ScheduleDate) 
        {
            string msg = "";
            //VehicleNo = "AP25X7140";
            CustomAjaxResponse result = new CustomAjaxResponse();
            DateTime SChFromDate = DateTime.Parse(ScheduleDate);
            result.bResponseBool = _iCTV.CheckScheduleDateAvailibility(VehicleNo, SChFromDate, ref msg);
            result.sResponseString = msg;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult setOTVSchData(OtherTripScheduleEntryVM model) 
        {
            TripScheduleHdr obj = new TripScheduleHdr();
            if (TempData["CTVHDR"] != null)
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
            }            
            TempData["CTVHDR"] = obj;
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.UpdateOthTripSchDtl(model.NoteNumber,model.TripPurpose, model.OTSchList, ref msg))
            {
                obj.IsOTSSaved = 1;
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";
            }
            else 
            {
                result.bResponseBool = false;
                result.sResponseString = msg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RemoveNote(string NoteNumber)
        {
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.RemoveNote(NoteNumber,0, ref msg))
            {
                result.bResponseBool = true;
                result.sResponseString = "removed Note number "+ NoteNumber;
            }
            else
            {
                result.bResponseBool = false;
                result.sResponseString = msg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getOTVSChDetailData(string Notenumber) 
        {
            CTVHdrDtl result=_iCTV.getSchDetailsFromNote(Notenumber, ref pMsg);
            return Json(result.SchDetailList,JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDataFromNote(string Notenumber)
        {
            CTVHdrDtl result = _iCTV.getSchDetailsFromNote(Notenumber, ref pMsg);
            return Json(result.SchHdrData, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetNotesToBeApproved(int EmpNo,int centrecode)
        //{
        //    //int x = 1;
        //    return Json(_iCTV.GetNoteNumbersTobeApproved(EmpNo, centrecode,ref pMsg), JsonRequestBehavior.AllowGet);
        //}
    }
}