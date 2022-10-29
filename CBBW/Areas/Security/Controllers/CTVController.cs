using System;
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
        public ActionResult LocalVehicleTripSchFromMat() 
        {
            
            LVTSFromMatVM model = new LVTSFromMatVM();
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
        //[HttpPost]
        //public ActionResult LocalVehicleTripSchFromMat() 
        //{
        
        //}
        public ActionResult LocVehTripSch() 
        {
            TripScheduleHdr obj = new TripScheduleHdr();
            if (TempData["CTVHDR"] != null) { obj = TempData["CTVHDR"] as TripScheduleHdr; }
            LocalVehicleTripScheduleVM model = new LocalVehicleTripScheduleVM();
            model.SCHFromDate = obj.FromDate;
            model.SCHToDate = obj.ToDate;
            model.VehicleNo = obj.Vehicleno;
            //model.SCHFromDate = new DateTime(2022, 8, 1);
            //model.SCHToDate = new DateTime(2022, 8, 15);
            //model.VehicleNo = "AP25X1541";

            model.DriverCodenName = obj.DriverNonName;
            model.LVSchDtl = _iCTV.getLocalVehicleSChedules(model.VehicleNo, model.SCHFromDate, model.SCHToDate, ref pMsg);

            return View(model);
        }
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Create() 
        {            
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
            TempData["CTVHDR"] = model;
            if (Submit == "OVT")
            {
                return RedirectToAction("OtherTrip");
            }
            else if (Submit == "LVT")
            {
                //TempData["CTVHDR"] = model;
                return RedirectToAction("LocVehTripSch");
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
                model.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
                model.IsActive = true;
                //TempData["CTVHDR"] = model;
                if (_iCTV.CreateNewCTVHdr(model, ref pMsg))
                {
                    ViewBag.Msg = "Note number " + model.NoteNo + " submited successfully.";
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
            TripScheduleHdr obj;
            if (TempData["CTVHDR"] != null) 
            { 
                obj = TempData["CTVHDR"] as TripScheduleHdr;
                model.NoteNumber = obj.NoteNo;
                model.VehicleNo = obj.Vehicleno;
                model.TripPurpose = obj.TripPurpose;
                model.DriverCode = obj.DriverNo;
                model.DriverName = obj.DriverName;
                TempData["CTVHDR"] = obj; 
            }
            return View(model);
        }
        public JsonResult GetLocationTypes()
        {
            return Json(_iCTV.getLocationTypes(ref pMsg), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            return Json(_iCTV.getLocationsFromType(TypeID,ref pMsg), JsonRequestBehavior.AllowGet);            
        }
        public JsonResult GetVehicleInfo(string VehicleNo,string NoteNumber)
        {
            _iCTV.RemoveNote(NoteNumber, 1, ref pMsg);
            VehicleInfo result = _iCTV.getVehicleInfo(VehicleNo, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSchToDate(string Fromdate, string FromTime, int FromLocation,
            int ToLocationType, int ToLocation)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            DateTime SChFromDate = DateTime.Parse(Fromdate +" "+ FromTime);
            DateTime st= _iCTV.getSchToDate(SChFromDate, FromLocation, ToLocationType, ToLocation, 1, ref pMsg);
            result.sResponseString = st.ToString("dd-MM-yyyy");
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
            obj.IsOTSSaved = 1;
            TempData["CTVHDR"] = obj;
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.UpdateOthTripSchDtl(model.NoteNumber,model.TripPurpose, model.OTSchList, ref msg))
            {
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
    }
}