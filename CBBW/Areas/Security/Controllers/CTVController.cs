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
        // GET: Security/CTV
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
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TripScheduleHdr model, string Submit) 
        {
            if (TempData["CTVHDR"] != null)
                model.ListofVehicles = (TempData["CTVHDR"] as TripScheduleHdr).ListofVehicles;
            else
                model.ListofVehicles = _iCTV.getLCVMCVVehicleList(ref pMsg);
            if (Submit == "OVT")
            {
            }
            else if (Submit == "LVT")
            {
            }
            else if (Submit == "create") 
            { 
            
            }

                return View();
        }
        public ActionResult OtherTrip() 
        {
            
            return View();
        }
        public JsonResult GetLocationTypes()
        {
            return Json(_iCTV.getLocationTypes(ref pMsg), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            return Json(_iCTV.getLocationsFromType(TypeID,ref pMsg), JsonRequestBehavior.AllowGet);            
        }
        public JsonResult GetVehicleInfo(string VehicleNo)
        {
            return Json(_iCTV.getVehicleInfo(VehicleNo, ref pMsg), JsonRequestBehavior.AllowGet);
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
            VehicleNo = "AP25X7140";
            CustomAjaxResponse result = new CustomAjaxResponse();
            DateTime SChFromDate = DateTime.Parse(ScheduleDate);
            result.bResponseBool = _iCTV.CheckScheduleDateAvailibility(VehicleNo, SChFromDate, ref msg);
            result.sResponseString = msg;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getOTVSchData(OtherTripScheduleEntryVM model) 
        {
            string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_iCTV.UpdateOthTripSchDtl(model.NoteNumber, model.OTSchList, ref msg))
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
    }
}