using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CTV2;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.DAL.DataSync;
namespace CBBW.Areas.Security.Controllers
{
    public class CTVController : Controller
    {
        IUserRepository _iUser;
        string pMsg;
        ICTVRepository _iCTV;
        IToursRuleRepository _toursRule;
        UserInfo user;
        //private UserInfo getLogInUserInfo()
        //{
        //    UserInfo user = new UserInfo(true);
        //    if (TempData["LogInUser"] != null)
        //    {
        //        user = TempData["LogInUser"] as UserInfo;
        //    }
        //    TempData["LogInUser"] = user;
        //    return user;
        //}
        public CTVController(ICTVRepository iCTV, IUserRepository iUser, IToursRuleRepository toursRule)
        {
            _iCTV = iCTV;
            _iUser = iUser;
            pMsg = "";
            _toursRule = toursRule;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        public ActionResult ViewRedirection(string NoteNumber="",int CBUID=0) 
        {
            if (CBUID == 2)
            {
                _iUser.RecordCallBack("/Security/CTV/ApprovalLists");
                return RedirectToAction("ViewNote", new { NoteNumber = NoteNumber, IsDelete = 0, CBUID=2 });
            }
            else if (CBUID==1) 
            {
                _iUser.RecordCallBack("/Security/CTV/ScheduleLists");
                return RedirectToAction("ViewNote", new { NoteNumber = NoteNumber, IsDelete = 1 });
            }
            else
            {
                _iUser.RecordCallBack("/Security/CTV/ScheduleLists");
                return RedirectToAction("ViewNote", new { NoteNumber = NoteNumber, IsDelete = 0 });
            }
        }
        public JsonResult BackButtonClicked() 
        {
            string url = _iUser.GetCallBackUrl();
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OtherTripSchEdit() 
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
                model.MaxDate = obj.ToDate.ToString("yyyy-MM-dd");
                TempData["CTVHDR"] = obj;
            }
            return View(model);
        }
        public ActionResult LocVehTripSchEdit(int CBUID, string NoteNumber = "")
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
            //model.SCHFromDate = new DateTime(2022, 11, 1);
            //model.SCHToDate = new DateTime(2022, 11, 15);
            //model.VehicleNo = "AP25X1541";
            model.NoteNo = obj.NoteNo;

            model.DriverCodenName = obj.DriverNonName;
            model.LVSchDtl = _iCTV.getLocalVehicleSChedules(model.VehicleNo, model.SCHFromDate, model.SCHToDate, ref pMsg);
            model.ListofDrivers = _iCTV.getDriverList(model.DriverCodenName, ref pMsg);

            //model.CallBackUrl = "/CTV/EditNote?NoteNumber="+model.NoteNo;
            return View(model);
        }
        public ActionResult EditNote(string NoteNumber) 
        {
            //getLogInUserInfo();
            TripScheduleHdr model;
            if (TempData["CTVHDR"] != null)
            {
                model = TempData["CTVHDR"] as TripScheduleHdr;
                if (model.NoteNo != NoteNumber)
                { model = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg).SchHdrData; }
            }
            else 
            {
                model = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg).SchHdrData;
            } 
            return View(model);
        }
        [HttpPost]
        public ActionResult EditNote(TripScheduleHdr model, string Submit) 
        {
            //UserInfo user = getLogInUserInfo();
            TempData["CTVHDR"] = model;
            if (Submit == "OVT")
            {
                _iUser.RecordCallBack("/Security/CTV/EditNote?NoteNumber=" + model.NoteNo);
                return RedirectToAction("OtherTripSchEdit",
                    new { Area = "Security", NoteNumber = model.NoteNo, CBUID = 5 });
            }
            else if (Submit == "TADARules")
            {
                _iUser.RecordCallBack("/Security/CTV/EditNote?NoteNumber=" + model.NoteNo);
                return RedirectToAction("ViewRedirection", "TADARules",
                    new { Area = "Security", CBUID = 5, NoteNumber = model.NoteNo });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack("/Security/CTV/EditNote?NoteNumber=" + model.NoteNo);
                return RedirectToAction("ViewRedirection", "TourRule",
                    new { Area = "Security", CBUID = 5, NoteNumber = model.NoteNo });
            }
            else if (Submit == "LVT")
            {
                //TempData["CTVHDR"] = model;
                _iUser.RecordCallBack("/Security/CTV/EditNote?NoteNumber=" + model.NoteNo);
                return RedirectToAction("LocVehTripSchEdit", new { CBUID = 5, NoteNumber = model.NoteNo });
            }
            else if (Submit == "LVTSChMat")
            {
                _iUser.RecordCallBack("/Security/CTV/EditNote?NoteNumber="+model.NoteNo);
                return RedirectToAction("LocalVehicleTripSchFromMat", new { CBUID = 5, NoteNumber = model.NoteNo });
            }
            else if (Submit == "Edit") 
            {
                if (_iCTV.SetCTVEditHdr(model.NoteNo,user.EmployeeNumber,model.ApprovalFor, ref pMsg))
                {
                    ViewBag.Msg = "Note number " + model.NoteNo + " submited successfully for re-approval.";
                    TempData["CTVHDR"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation failed for Note number " + model.NoteNo;
                }
            }
            return View(model);
        }
        public ActionResult OtherTripSchDisplay(string NoteNumber,int CBUID=2,int HdrInd=0) 
        {
            ViewBag.HeaderSign = "VIEW";
            if (HdrInd == 2 || CBUID==3) { ViewBag.HeaderSign = "APPROVAL"; }
            //getLogInUserInfo();
            OtherTripSchDisplayVM model = new OtherTripSchDisplayVM();
            CTVHdrDtl obj = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg);
            if (obj != null) 
            {
                model.SchDetailEntryList = obj.SchDetailEntryList;
                model.SchDetailList = obj.SchDetailList;
                model.NoteNumber = obj.SchHdrData.NoteNo;
                model.TripPurpose = obj.SchHdrData.TripPurpose;
            }
            if (CBUID == 2)
            {
                model.CallBackUrl = "/Security/CTV/ViewNote?CBUID=2&NoteNumber=" + NoteNumber;
                //TempData["LVTScallbackurl"] = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                //model.IsSaveVisible = 0;
            }
            if (CBUID == 3)
            {
                model.CallBackUrl = "/Security/CTV2/Approval?NoteNumber=" + NoteNumber;
                CTVApprovalVM tempobj=TempData["AppNoteDtl"] as CTVApprovalVM;
                if (tempobj != null) 
                {
                    tempobj.IsOthViewed = 1;
                    tempobj.IsApprovedComboValue = 0;
                    tempobj.DisapprovalReason = "";
                    TempData["AppNoteDtl"] = tempobj;
                }                
                //TempData["LVTScallbackurl"] = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                //model.IsSaveVisible = 0;
            }
            return View(model);
        }
        public ActionResult ViewNote(string NoteNumber, int CBUID = 1, int IsDelete=0) 
        {
            ViewBag.HeaderSign = "VIEW";
            if (CBUID == 1)
            {
                _iUser.RecordCallBack("/Security/CTV2/Index");
            }
            else if (CBUID == 2)
            {
                _iUser.RecordCallBack("/Security/CTV2/ApprovalIndex");
                //ViewBag.CallBackUrl= "/Security/CTV2/ApprovalIndex";
                ViewBag.HeaderSign = "APPROVAL";
                //TempData["LVTScallbackurl"] = "/Security/CTV/ViewNote?NoteNumber=" + NoteNumber;
                //model.IsSaveVisible = 0;
            }
            //getLogInUserInfo();
            TripScheduleHdr model = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg).SchHdrData;
            model.IsDeleteBtn = IsDelete;
            model.CBUID = CBUID;
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewNote(TripScheduleHdr model, string Submit) 
        {
            //UserInfo user = getLogInUserInfo();
            TempData["CTVHDR"] = model;            
            if (Submit == "OVT")
            {
                _iUser.RecordCallBack("/Security/CTV/ViewNote?NoteNumber=" + model.NoteNo+"&CBUID="+model.CBUID);
                return RedirectToAction("OtherTripSchDisplay", new { Area = "Security", NoteNumber = model.NoteNo, HdrInd=model.CBUID });
            }
            else if (Submit == "TADARules")
            {
                _iUser.RecordCallBack("/Security/CTV/ViewNote?NoteNumber=" + model.NoteNo + "&CBUID=" + model.CBUID);
                return RedirectToAction("ViewRedirection", "TADARules", 
                    new { Area = "Security", CBUID = 2, NoteNumber = model.NoteNo });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack("/Security/CTV/ViewNote?NoteNumber=" + model.NoteNo + "&CBUID=" + model.CBUID);
                return RedirectToAction("ViewRedirection", "TourRule", 
                    new { Area = "Security", CBUID = 2, NoteNumber=model.NoteNo });
            }
            else if (Submit == "LVT")
            {
                //TempData["CTVHDR"] = model;
                _iUser.RecordCallBack("/Security/CTV/ViewNote?NoteNumber=" + model.NoteNo + "&CBUID=" + model.CBUID);
                return RedirectToAction("LocVehTripSch", new { CBUID = model.CBUID, NoteNumber = model.NoteNo });
            }
            else if (Submit == "LVTSChMat")
            {
                _iUser.RecordCallBack("/Security/CTV/ViewNote?NoteNumber="+model.NoteNo + "&CBUID=" + model.CBUID);
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
            return RedirectToAction("Index","CTV2");

        }
        public ActionResult LocalVehicleTripSchFromMat(int CBUID=1,string NoteNumber="") 
        {            
            LVTSFromMatVM model = new LVTSFromMatVM();
            if (CBUID == 1)
            {
                model.CallBackUrl = "/Security/CTV/Create";
            }
            else if (CBUID == 2) { model.CallBackUrl = "/Security/CTV/ViewNote?NoteNumber="+ NoteNumber; }
            else if (CBUID == 3) { model.CallBackUrl = "/Security/CTV/Approval?NoteNumber=" + NoteNumber; }
            else if (CBUID == 5) { model.CallBackUrl = "/Security/CTV/EditNote?NoteNumber=" + NoteNumber; }
            int yr = DateTime.Today.Year;
            int mon = DateTime.Today.Month;
            int day = DateTime.Today.Day;
            int lastday = DateTime.DaysInMonth(yr, mon);
            if (day > 15)
            {
                model.FromDate = new DateTime(yr, mon, 16);
                model.ToDate = new DateTime(yr, mon, lastday);
            }
            else
            {
                model.FromDate = new DateTime(yr, mon, 1);
                model.ToDate = new DateTime(yr, mon, 15);
            }

            TripScheduleHdr obj;
            if (TempData["CTVHDR"] != null)
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                model.FromDate = obj.FromDate;
                model.ToDate = obj.ToDate;
                TempData["CTVHDR"] = obj;
            }

            //model.FromDate = new DateTime(2022, 8, 1);
            //model.ToDate = new DateTime(2022, 8, 15);
            model.LVSDataList = _iCTV.getLocalVehicleSChedules("0", model.FromDate, model.ToDate, ref pMsg).OrderBy(o => o.FromDate).ThenBy(o => o.VehicleNumber).ToList();
            
            return View(model);
        }
        public ActionResult ApprovalLists() 
        {
            //UserInfo user = getLogInUserInfo();
            IEnumerable<TripScheduleHdr> model = _iCTV.getApprovedCtvSchedule(1000, 0, 1, "des", "", user.CentreCode, ref pMsg);
            return View(model);
        }
        [HttpPost]
        public ActionResult ApprovalLists(string Submit)
        {
            //UserInfo user = getLogInUserInfo();
            TempData["AppNoteDtl"] = null;
            return RedirectToAction("Approval");
        }
        public ActionResult Approval() 
        {
            CTVApprovalVM model= new CTVApprovalVM(); ;
            try
            {
                //UserInfo user = getLogInUserInfo();
                if (TempData["AppNoteDtl"] != null) 
                {
                    model = TempData["AppNoteDtl"] as CTVApprovalVM;
                }         
            
                model.ListofNoteNumbers = _iCTV.GetNoteNumbersTobeApproved(user.EmployeeNumber, user.CentreCode, ref pMsg);
                model.DateTimeofApproval = DateTime.Now;
                if (model.ListofNoteNumbers == null) { model.ListofNoteNumbers = new List<NoteNumber>(); }

            }
            catch { }
            TempData["AppNoteDtl"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult Approval(CTVApprovalVM model, string Submit)
        {
            //TempData["CTVHDR"]=_iCTV.getSchDetailsFromNote(model.NoteNo, ref pMsg).SchHdrData;
            model.IsApproved = model.IsApprovedComboValue == 1 ? true : false;
            TempData["AppNoteDtl"] = model;
            //UserInfo user = getLogInUserInfo();
            if (Submit == "OVT")
            {
                model.IsTabClicked = 1;
                TempData["AppNoteDtl"] = model;
                _iUser.RecordCallBack("/Security/CTV/Approval");
                return RedirectToAction("OtherTripSchDisplay", 
                    new { Area = "Security", CBUID = 3, NoteNumber = model.NoteNo });
            }
            else if (Submit == "TADARules")
            {
                _iUser.RecordCallBack("/Security/CTV/Approval");
                return RedirectToAction("ViewRedirection", "TADARules",
                    new { Area = "Security", CBUID = 3, NoteNumber = model.NoteNo });
            }
            else if (Submit == "TourRule")
            {
                _iUser.RecordCallBack("/Security/CTV/Approval");
                return RedirectToAction("ViewRedirection", "TourRule",
                    new { Area = "Security", CBUID = 3, NoteNumber = model.NoteNo });
            }
            else if (Submit == "LVT")
            {
                model.IsTabClicked = 1;
                TempData["AppNoteDtl"] = model;
                //TempData["CTVHDR"] = model;
                _iUser.RecordCallBack("/Security/CTV/Approval");
                return RedirectToAction("LocVehTripSch", new { CBUID = 3, NoteNumber = model.NoteNo });
            }
            else if (Submit == "LVTSChMat")
            {
                _iUser.RecordCallBack("/Security/CTV/Approval");
                return RedirectToAction("LocalVehicleTripSchFromMat", 
                    new { CBUID = 3, NoteNumber = model.NoteNo });
            }
            else if (Submit == "clear")
            {
                TempData["AppNoteDtl"] = null;
                return RedirectToAction("Approval");
            }
            else if (Submit == "create")
            {
                if (model.ListofNoteNumbers == null)
                    model.ListofNoteNumbers = _iCTV.GetNoteNumbersTobeApproved(user.EmployeeNumber, user.CentreCode, ref pMsg);

                //TempData["CTVHDR"] = model;
                if (_iCTV.setCTVApproval(model.NoteNo,user.EmployeeNumber,model.IsApproved,
                    DateTime.Now,model.DisapprovalReason, ref pMsg))
                {
                    ViewBag.Msg = "Approval status for Note number " + model.NoteNo + " has been updated successfully.";
                    TempData["AppNoteDtl"] = null;
                }
                else
                {
                    ViewBag.ErrMsg = "Updation failed for Note number " + model.NoteNo;
                }
            }
            return View(model);
        }
        public ActionResult LocVehTripSch(int ShowSaveBtn=0,string NoteNumber="",int CBUID=0) 
        {
            ViewBag.HeaderSign = "VIEW";
            if (CBUID == 2 || CBUID == 3) { ViewBag.HeaderSign = "APPROVAL"; }
            else if (ShowSaveBtn == 1) { ViewBag.HeaderSign = "ENTRY"; }
            //ViewBag.HeaderText = "VIEW";
            //if (ShowSaveBtn == 1) { ViewBag.HeaderText = "ENTRY"; }
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
            //model.SCHFromDate = new DateTime(2022, 11, 1);
            //model.SCHToDate = new DateTime(2022, 11, 15);
            //model.VehicleNo = "AP25X1541";

            model.DriverCodenName = obj.DriverNonName;
            model.LVSchDtl = _iCTV.getLocalVehicleSChedules(model.VehicleNo, model.SCHFromDate, model.SCHToDate, ref pMsg);
            model.IsSaveVisible = ShowSaveBtn;
            
            TempData["mLVTSData"] = model;
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
                obj.IsOTSSaved = 1;
                TempData["CTVHDR"] = obj;
            }
            if (TempData["mLVTSData"] != null)
            {
                model = TempData["mLVTSData"] as LocalVehicleTripScheduleVM;
            }
            string note = obj.NoteNo == null ? "Temp" : obj.NoteNo;
            _iCTV.setLocalTripSchDtls(note, model.LVSchDtl, ref pMsg);           
            return Redirect(_iUser.GetCallBackUrl());
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
            _iUser.ClearCallBackRecording();
            //UserInfo user = getLogInUserInfo();
            //int cencode = user.CentreCode;
            IEnumerable<TripScheduleHdr> model = _iCTV.getCtvSchedule(1000,0,1,"des","",user.CentreCode, ref pMsg);
            return View(model);
        }
        [HttpPost]
        public ActionResult ScheduleLists(string Submit)
        {
            //UserInfo user = getLogInUserInfo();
            TempData["CTVHDR"] = null;
            _iUser.RecordCallBack("/Security/CTV/ScheduleLists");
            return RedirectToAction("Create");
        }
        public ActionResult Create() 
        {
            //UserInfo user= getLogInUserInfo();
            TripScheduleHdr model;
            if (TempData["CTVHDR"] == null)
            {
                string schpattern = "200001-CTV-" + DateTime.Today.ToString("yyyyMMdd") + "-";
                TempData["CTVHDR"] = _iCTV.NewTripScheduleNo(schpattern, ref pMsg);
            }
            model = TempData["CTVHDR"] as TripScheduleHdr;
            model.CenterCode = user.CentreCode;
            model.CentreCodenName = user.CentreCode.ToString().Trim() + "/" + user.CentreName;
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
            //UserInfo user= getLogInUserInfo();
            TempData["CTVHDR"] = model;
            if (Submit == "OVT")
            {
                _iUser.RecordCallBack("/Security/CTV/Create");
                return RedirectToAction("OtherTrip");
            }
            else if (Submit == "TADARules")
            {
                _iUser.RecordCallBack("/Security/CTV/Create");
                return RedirectToAction("ViewRedirection", "TADARules", new { Area = "Security", CBUID = 1 });
            }
            else if (Submit == "TourRule") 
            {
                _iUser.RecordCallBack("/Security/CTV/Create");
                return RedirectToAction("ViewRedirection", "TourRule", new { Area = "Security", CBUID = 1 });                
            }
            else if (Submit == "LVT")
            {
                _iUser.RecordCallBack("/Security/CTV/Create");
                //TempData["CTVHDR"] = model;
                return RedirectToAction("LocVehTripSch",new { ShowSaveBtn = 1,CBUID=1});
            }
            else if (Submit == "LVTSChMat")
            {
                _iUser.RecordCallBack("/Security/CTV/Create");
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
            DateTime MaxDT = DateTime.Today.AddDays(2);
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
                MaxDT = MaxDT <= obj.ToDate ? MaxDT : obj.ToDate;
                model.MaxDate= MaxDT.ToString("yyyy-MM-dd");
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
                obj.IsOTSSaved = 0;
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
        public JsonResult GetToLocationsFromType(string TypeIDs,int m=0) 
        {
            TypeIDs= TypeIDs.Replace('_', ',');
            IEnumerable<LocationMaster> result = _iCTV.GetLocationsFromTypes(TypeIDs, ref pMsg);
            //result = result.Take(5);
            return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(_iCTV.getLocationsFromType(TypeIDs, ref pMsg), JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            IEnumerable<LocationMaster> result = _iCTV.GetLocationsFromTypes(TypeID.ToString(), ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(_iCTV.getLocationsFromType(TypeID, ref pMsg), JsonRequestBehavior.AllowGet);            
        }
        public JsonResult GetVehicleInfo(string VehicleNo,string NoteNumber,int RemoveTemp=0)
        {
            if (RemoveTemp == 1) 
            { 
                _iCTV.RemoveNote(NoteNumber, 1, ref pMsg); 
            }            
            VehicleInfo result = _iCTV.getVehicleInfo(VehicleNo, ref pMsg);
            if (!result.IsActive) { result.Msg = "Vehicle is not active or information is missing"; }
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
                obj.TripPurpose = model.TripPurpose;
            }            
            TempData["CTVHDR"] = obj;
            if (model.NoteNumber == null) { model.NoteNumber = "Temp"; }
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
        public JsonResult setOTVSchEditData(OtherTripScheduleEntryVM model)
        {
            TripScheduleHdr obj = new TripScheduleHdr();
            if (TempData["CTVHDR"] != null)
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                obj.TripPurpose = model.TripPurpose;
            }
            //TempData["CTVHDR"] = obj;
            if (model.NoteNumber == null) { model.NoteNumber = "Temp"; }
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.EditOthTripSchDtl(model.NoteNumber, model.TripPurpose, model.OTSchList, ref msg))
            {
                obj.IsOTSSaved = 1;
                obj.ApprovalFor = obj.ApprovalFor == 1 ? 3 : 2;
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";
                TripScheduleHdr x = CastCTVEditTempData(model.NoteNumber);
                x.IsOTSSaved = 1;
                TempData["CTVEdit"] = x;
            }
            else
            {
                result.bResponseBool = false;
                result.sResponseString = msg;
            }
            TempData["CTVHDR"] = obj;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult setLVSDriverChange(LocalVehicleTripScheduleEditVM model)
        {
            TripScheduleHdr obj = new TripScheduleHdr();
            if (TempData["CTVHDR"] != null)
            {
                obj = TempData["CTVHDR"] as TripScheduleHdr;
            }
            
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.setLocalTripSchDriver(model.NoteNo,model.DriverList, ref msg))
            {
                obj.IsOTSSaved = 1;
                obj.ApprovalFor = obj.ApprovalFor == 2 ? 3 : 1;
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";
                TripScheduleHdr x = CastCTVEditTempData(model.NoteNo);
                x.IsLTSSaved = 1;
                TempData["CTVEdit"] = x;
            }
            else
            {
                result.bResponseBool = false;
                result.sResponseString = msg;
            }
            TempData["CTVHDR"] = obj;
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
        
        public JsonResult RemoveNoteDetails(string NoteNumber)
        {
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.RemoveNote(NoteNumber,1, ref msg))
            {
                result.bResponseBool = true;
                result.sResponseString = "removed Note number " + NoteNumber;
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
        public JsonResult getOTVSChDetailEntryData(string Notenumber)
        {
            CTVHdrDtl result = _iCTV.getSchDetailsFromNote(Notenumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getOTVSChDetailDataCount(string Notenumber)
        {
            string msg = "";
           CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                result.iRespinseInteger = _iCTV.getSchDetailsFromNote(Notenumber, ref msg).SchDetailList.Count();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDataFromNote(string Notenumber)
        {
            CTVHdrDtl result = _iCTV.getSchDetailsFromNote(Notenumber, ref pMsg);
            return Json(result.SchHdrData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverList(string ExpDriverName)
        {            
            IEnumerable<CustomComboOptions> result = _iCTV.getDriverList(ExpDriverName,ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getSCHNotes(int iDisplayLength, int iDisplayStart,
        //    int iSortCol_0, string sSortDir_0, string sSearch)
        //{
        //    CTVHdrDtl result = _iCTV.getSchDetailsFromNote(Notenumber, ref pMsg);
        //    return Json(result.SchHdrData, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetNotesToBeApproved(int EmpNo,int centrecode)
        //{
        //    //int x = 1;
        //    return Json(_iCTV.GetNoteNumbersTobeApproved(EmpNo, centrecode,ref pMsg), JsonRequestBehavior.AllowGet);
        //}
        #region Ajax Calling - New
        public JsonResult GetEntryNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<CTVNoteList4DT> noteList = _iCTV.GetNoteListForDataTable(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, false, ref pMsg);
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

        #endregion
        private TripScheduleHdr CastCTVEditTempData(string NoteNumber)
        {
            TripScheduleHdr model;
            if (TempData["CTVEdit"] != null)
            {
                model = TempData["CTVEdit"] as TripScheduleHdr;
            }
            else
            {
                model = _iCTV.getSchDetailsFromNote(NoteNumber, ref pMsg).SchHdrData;
            }
            TempData["CTVEdit"] = model;
            return model;
        }
    }
}