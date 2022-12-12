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

namespace CBBW.Areas.Security.Controllers
{
    public class EHGController : Controller
    {
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        string pMsg;
        UserInfo user;
        IEHGRepository _iEHG;
        EHGHeaderEntryVM model;         
        public EHGController(IUserRepository iUser, IEHGRepository iEHG, IMyHelperRepository myHelper)
        {
            _iUser = iUser;
            _iEHG = iEHG;
            _myHelper = myHelper;
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
            }
            else { model = TempData["EHG"] as EHGHeaderEntryVM; }

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
                    if(model.TADADeniedForManagement==1)
                        dtl.TADADenied = true;
                    else
                        dtl.TADADenied = false;
                    if (_iEHG.SetEHGHdrForManagement(model.ehgHeader, dtl, ref pMsg))
                    { ViewBag.Msg = "Note number " + model.ehgHeader.NoteNumber + " submited successfully."; }
                    else { ViewBag.ErrMsg = "Updation failed for Note number " + model.ehgHeader.NoteNumber; }
                }                
            }
            TempData["EHG"] = model;
            return View(model);
        }
        public ActionResult DateWiseTourDetails() 
        {

            return View();
        }

        //Actions for AJAX Calling
        public JsonResult GetPersonTypes()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            model = CastEHGTempData();
            if (model.PersonType == null)
            {
                EHGMaster master = EHGMaster.GetInstance;
                result = master.PersonType;
            }
            else { result = model.PersonType;}
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverList()
        {
            IEnumerable<CustomComboOptions> result;
            model = CastEHGTempData();
            if (model.DriverList == null)
                result = model.getDriverList(user.CentreCode);
            else  
                result = model.DriverList; 
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStaffList()
        {
            IEnumerable<CustomComboOptions> result;
            model = CastEHGTempData();
            if (model.StaffList == null)
                result = model.getStaffList(user.CentreCode);
            else
                result = model.StaffList;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDesgCodenName(int empID,int empType)
        {
            string result= "4 / DIC";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetTravelingPersonDetails(EHGTravellingPersonsVM model) 
        {
            if (model != null) 
            {
                _iEHG.SetEHGTravellingPersonDetails(model.NoteNumber, model.PersonDtls, ref pMsg);
            }
            return RedirectToAction("index");
        }
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
        
    }
}