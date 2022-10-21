using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
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
        public ActionResult Create(TripScheduleHdr model) 
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
    }
}