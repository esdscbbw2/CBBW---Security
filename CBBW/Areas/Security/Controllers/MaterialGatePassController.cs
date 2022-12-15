using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.Controllers
{
    public class MaterialGatePassController : Controller
    {
        string pMsg;
        IMGPRepository _IMGP;
        ICTVRepository _ICTV;
        string mNoteNumber;
        bool btnactive;
        UserInfo user;
        public MaterialGatePassController(IMGPRepository IMGP,ICTVRepository ICTV,IUserRepository iUser)
        {
            _IMGP = IMGP;
            _ICTV = ICTV;
            pMsg = "";
            mNoteNumber = "200001-CTV-20221201-00009";
            btnactive = false;
            iUser.LogIn("praveen", ref pMsg);
            user = iUser.getLoggedInUser();
        }
        // GET: Security/MaterialGatePass
        #region For Out Details
        public ActionResult Index()
        {
            IEnumerable<MGPListDetails> model = _IMGP.getMGPDetailsforListPage(ref pMsg);
            return View(model);
        }
        public ActionResult Create()
        {
            MGPNotes model = new MGPNotes();
            try
            {
                //UserInfo user = getLogInUserInfo();
                if (TempData["MGPVM"] != null)
                {
                    model = TempData["MGPVM"] as MGPNotes;
                }
                
                model.ListofNotes = _IMGP.getApprovedNoteNumbers(user.CentreCode, ref pMsg);
                if (TempData["btnactivetrue"] != null)
                {
                    TempData["btnactive"] = TempData["btnactivetrue"];
                    //string noteno= TempData["notenumber"] as string;
                   // TempData["notenumber"] = noteno;
                    //model.ListofNotes.Select(x=>x.NoteNo==noteno);
                }
                else
                {
                    TempData["btnactive"] = false;
                }
                if (TempData["notenumber"] != null) 
                { 
                    model.NoteNo = TempData["notenumber"] as string;
                    TempData["notenumber"] = model.NoteNo;
                }
                if (TempData["btnactivetrue"] != null) 
                {
                    model.ISSubmitActive = int.Parse(TempData["btnactivetrue"].ToString());
                }
                TempData["MGPVM"] = model;
                return View(model);
            }
            catch { }
            TempData["MGPVM"] = model;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(MGPNotes model, string Submit)
        {
           
            if (TempData["MGPVM"] != null)
            {
                model.ListofNotes = (TempData["MGPVM"] as MGPNotes).ListofNotes;
            }
            else { model.ListofNotes = _IMGP.getApprovedNoteNumbers(user.CentreCode, ref pMsg); }
            //TempData["MGPVM"] = model;
            if (Submit== "CMOD")
            {
                return RedirectToAction("VehicleMaterialOutDetails",
                 new { Area = "Security", CBUID = 1, NoteNumber = model.NoteNo });
            }
            else if (Submit == "CMID")
            {
                return RedirectToAction("VehicleMaterialInDetails",
                    new { Area = "Security", CBUID = 1, NoteNumber = model.NoteNo });
            }

            if (TempData["OUTorIN"] as string == "In") {
                string noteno=TempData["notenumber"] as string;
                var mID = TempData["IDs"];
                if (_IMGP.spUpdateOutDetailsflag(noteno, Convert.ToInt32(mID), 2, ref pMsg))
                {
                    model.ISSubmitActive = 0;
                    ViewBag.Msg = "Note Updated Successfully.";
                }
                else
                {
                    ViewBag.ErrMsg = "Note Updation Failed.";
                }
            }
            else { 
            if (TempData["notenumber"] != null) { 
            string noteno = TempData["notenumber"] as string;
            List<MGPOutInDetails> outinmodel = new List<MGPOutInDetails>();
            outinmodel = _IMGP.getMGPOutDetails(noteno, ref pMsg);

                long mID = 0;
                if (outinmodel != null && outinmodel.Count > 0) 
                { mID = outinmodel.OrderByDescending(x => x.ID).FirstOrDefault().ID; }
            
                if (_IMGP.spUpdateOutDetailsflag(noteno, mID,1, ref pMsg))
                {
                    model.ISSubmitActive = 0;
                    ViewBag.Msg = "Note Updated Successfully.";
                }
                else 
                {
                    ViewBag.ErrMsg = "Note Updation Failed.";
                }
                
            }
            }
            model.EntryDate = DateTime.Today;
            model.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
            model.IsActive = true;

            //TempData["MGPVM"] = model;

            //return RedirectToAction("Index",
            //    new { Area = "Security" });
            return View(model);
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult VehicleMaterialOutDetails(string NoteNumber, int CBUID)
        {
           
            MGPOutInVM model = new MGPOutInVM();
            
            if (CBUID == 1)
            {
                ViewBag.CallBackUrl = "/Security/MaterialGatePass/Create";
            }
            //UserInfo user = getLogInUserInfo();
            ViewBag.ListofMatOut = _IMGP.getMGPOutDetails(NoteNumber, ref pMsg);
            ViewBag.ListofRFID = _IMGP.getRFIDCards(ref pMsg);
            //model.ListCurrentOutDetails = _IMGP.getSchDtlsForMGP(mNoteNumber, ref pMsg);
            return View(model);
        }
        [HttpPost]
        public JsonResult SaveVehicleMaterialOutDCDetails(MGPSaveOutDetailsVM model)
        {

            MGPOutSave mgpoutsave = new MGPOutSave();
            foreach (var item in model.ListCurrentOutData)
            {
                mgpoutsave.NoteNumber=item.NoteNumber;
                mgpoutsave.DriverNo = item.DriverNo;
                mgpoutsave.Drivername = (item.Drivername == null || item.Drivername == "") ? "NA" : item.Drivername;
                mgpoutsave.DesignationCode = item.DesignationCode;
                mgpoutsave.DesignationName=(item.DesignationName == null || item.DesignationName == "") ? "NA": item.DesignationName;
                mgpoutsave.TripType = item.TripType;
                mgpoutsave.TripTypeStr = item.TripTypeStr;
                mgpoutsave.ToLocationCodeName = item.ToLocationCodeName;
                mgpoutsave.CarryingOutMat = item.CarryingOutMat;
                mgpoutsave.LoadPercentage = item.LoadPercentage;
                mgpoutsave.SchFromDate = item.SchFromDate;
                mgpoutsave.KMOUT = item.KMOUT;
                mgpoutsave.VehicleNumber = item.VehicleNumber;
                mgpoutsave.RFIDCard = item.RFIDCard;
                mgpoutsave.ActualTripOutDate = item.ActualTripOutDate;
                mgpoutsave.ActualTripOutTime = item.ActualTripOutTime;
                mgpoutsave.OutRemarks = item.OutRemarks == null?"NA": item.OutRemarks;
              
            }
           // string msg = "";
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_IMGP.setMGPOutDetails(mgpoutsave, model.ListofMGPReferenceDCData, ref pMsg))
            {
                TempData["btnactivetrue"] = 1;
                TempData["notenumber"] = mgpoutsave.NoteNumber;
                TempData["OUTorIN"] = "out";
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";
              
            }
            else
            {
                result.bResponseBool = false;
               // result.sResponseString = msg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
         public JsonResult GetReferenceDCDetails(string VehicleNo, string FromDT)
        {

            //VehicleNo = "AP25X1541";
            DateTime FromDTs = DateTime.Parse(FromDT);
                DateTime    ToDT= new DateTime(2022, 8, 15);
            //UserInfo user = getLogInUserInfo();
            MGPOutInVM model = new MGPOutInVM();
            model.ListofMGPReferenceDCDetails = _IMGP.getReferenceDCDetails(VehicleNo, FromDTs, ToDT, ref pMsg);
            return Json(model.ListofMGPReferenceDCDetails, JsonRequestBehavior.AllowGet) ;
        }
        public JsonResult GetHistoryDCDetails(long ID)
        {
        
            //UserInfo user = getLogInUserInfo();
            MGPOutInVM model = new MGPOutInVM();
            model.ListMGPHistoryDCDetails = _IMGP.getMGPHistoryDCDetails(ID,1, ref pMsg);
            return Json(model.ListMGPHistoryDCDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemWiseDetails(string NoteNumber)
        {
            //NoteNumber = "200001-MIB-20110119-00015";
            //UserInfo user = getLogInUserInfo();
            MGPOutInVM model = new MGPOutInVM();
            model.ListofMGPItemWiseDetails = _IMGP.getItemWiseDetails(NoteNumber, ref pMsg);
            return Json(model.ListofMGPItemWiseDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRFIdCards()
        {
            //UserInfo user = getLogInUserInfo();
            IEnumerable<RFID> result = _IMGP.getRFIDCards(ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetcurentOutDetails(string NoteNumber)
        { 
            List<MGPVehicleOutDetails> model = new List<MGPVehicleOutDetails>();
            model = _IMGP.getSchDtlsForMGP(NoteNumber, ref pMsg);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckAvailableNoteNoforOut(string NoteNumber)
        {
            List<MGPOutInDetails> model = new List<MGPOutInDetails>();
            model = _IMGP.getMGPOutDetails(NoteNumber, ref pMsg);
            var data= model.OrderByDescending(x => x.ID).Take(1);
            TempData["OutData"] = data;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //Start Material In Details
        #region Create New In details 
        [HttpPost]
        public ActionResult SaveVehicleMaterialInDCDetails(MGPSaveInDetailsVM model)
        {
            MGPInSave mgpinsave = new MGPInSave();
            foreach(var item in model.ListCurrentInData)
            {
                mgpinsave.ID= item.ID;
                mgpinsave.NoteNumber = item.NoteNumber;
                mgpinsave.RFIDCardIn = item.RFIDCardIn;
                mgpinsave.FromLocationType = item.FromLocationType;
                mgpinsave.FromLocationCode = item.FromLocationCode;
                mgpinsave.FromLocationName = item.FromLocationName;
                mgpinsave.CarryingInMaterial = item.CarryingInMaterial;
                mgpinsave.LoadPercentageIn = item.LoadPercentageIn;
                mgpinsave.ActualTripInDate = item.ActualTripInDate;
                mgpinsave.ActualTripInTime = item.ActualTripInTime;
                mgpinsave.RequiredKmIn = item.RequiredKmIn>0? item.RequiredKmIn:0;
                mgpinsave.ActualKmIn = item.ActualKmIn>0? item.ActualKmIn:0;
                mgpinsave.KMRunInTrip = item.KMRunInTrip;
                mgpinsave.RemarkIn = item.RemarkIn==""?"NA": item.RemarkIn;

             }

            CustomAjaxResponse result = new CustomAjaxResponse();
            if (_IMGP.setMGPInDetails(mgpinsave, model.ListofMGPReferenceInDCData, ref pMsg))
            {
                TempData["btnactivetrue"] = 1;
                TempData["notenumber"] = mgpinsave.NoteNumber;
                TempData["IDs"] = mgpinsave.ID;
                TempData["OUTorIN"] = "In";
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";

            }
            else
            {
                result.bResponseBool = false;
                // result.sResponseString = msg;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
           
        }
        
        public ActionResult VehicleMaterialInDetails(string NoteNumber)
        {
            MGPInDetailsVM model = new MGPInDetailsVM();
            try
            {
               
                model.ListInDetails = _IMGP.getMGPOutDetails(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }
            return View(model);
        }

        public JsonResult GetcurentInDetails(string NoteNumber)
        {
            try
            {
                List<MGPCurrentInDetails> model = new List<MGPCurrentInDetails>();
                model = _IMGP.getMGPCurrentOutDetailsForIn(NoteNumber, ref pMsg);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(ex.ToString());
            }
            
        }


        public JsonResult GetRefInDCDetails(string VehicleNo, string FromDT)
        {
            try
            {
                DateTime FromDTs = DateTime.Parse(FromDT);
                DateTime ToDT = new DateTime(2022, 8, 15);
                MGPInDetailsVM model = new MGPInDetailsVM();
                model.ListofMGPReferenceInDCDetails = _IMGP.getReferenceInDCDetails(VehicleNo, FromDTs, ToDT, ref pMsg);
                return Json(model.ListofMGPReferenceInDCDetails, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        public JsonResult GetItemWiseInDetails(string NoteNumber)
        {

            MGPInDetailsVM model = new MGPInDetailsVM();
            model.ListofMGPItemWiseInDetails = _IMGP.getItemWiseDetails(NoteNumber, ref pMsg);
            return Json(model.ListofMGPItemWiseInDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistoryInDCDetails(long ID)
        {

            //UserInfo user = getLogInUserInfo();
            MGPOutInVM model = new MGPOutInVM();
            model.ListMGPHistoryDCDetails = _IMGP.getMGPHistoryDCDetails(ID,2, ref pMsg);
            return Json(model.ListMGPHistoryDCDetails, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region In/ Out Button Active
        public JsonResult getMGPButtonStatus(string NoteNumber)
        {
            try
            {
                ButtonActive model = new ButtonActive();
                model = _IMGP.getMGPButtonStatus(NoteNumber, ref pMsg);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }
        #endregion



    }
}