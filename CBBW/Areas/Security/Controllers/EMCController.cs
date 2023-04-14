using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EHG;
using CBBW.Areas.Security.ViewModel.EMC;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.EMC;
using CBBW.BOL.Master;

namespace CBBW.Areas.Security.Controllers
{
    public class EMCController : Controller
    {
        // GET: Security/EMC
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        IEMCRepository _iEMC;
        IEMNRepository _iEMN;
        EMCTravellingDetailsVM modelTrav;
        EMCHeaderEntryVM model;
        
        ICTVRepository _iCTV;
        public EMCController(IEMNRepository iEMN, ICTVRepository iCTV, IUserRepository iUser, IEMCRepository iEMC, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
                _iEMC = iEMC;
                _iEMN = iEMN;
                _iCTV = iCTV;
                //iUser.LogIn("praveen", ref pMsg);
                user = iUser.getLoggedInUser();
                ViewBag.LogInUser = user.UserName;
            }
            catch (Exception ex) { pMsg = ex.ToString(); }

        }
        #region Entry Window
        public ActionResult Index()
        {
            TempData["EMC"] = null;
            TempData["EMCData"] =null;
            TempData["BtnSubmit"] = null;
            return View();
        }
        public JsonResult GetEMCNZBDetailsforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
    string sSortDir_0, string sSearch)
        {
            List<EMCNoteList> noteList = _iEMC.GetEMCNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

            var result = new
            {
                iTotalRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                //iTotalDisplayRecords = noteList.Count(),
                iTotalDisplayRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(string NoteNumber = null)
        {
            EMCHeaderEntryVM model = new EMCHeaderEntryVM();
            try
            {
                if (NoteNumber != null)
                {
                    model = TempData["EMC"] as EMCHeaderEntryVM;
                    model.emnHeader.NoteNumber = model.NoteNumber;
                    model.emnHeader.AttachFile = model.AttachFile;
                    model.emnHeader.CenterCodeName = model.CenterCodeName;
                    model.emnHeader.IsEPTour = model.IsEPTour==1?true:false;
                    //model.PersonDtls = _iEMC.GetEMCTravellingPerson(model.NoteNumber, ref pMsg);
                    if (TempData["BtnSubmit"] != null)
                    {
                        model.Btnsubmit = 1;
                    }
                    else
                    {
                        model.Btnsubmit = 0;
                    }
                    TempData["EMC"] = model;
                }
                else
                {
                    if (TempData["EMC"] != null && TempData["EMCData"]!=null)
                    {
                        model = TempData["EMC"] as EMCHeaderEntryVM;
                        modelTrav = TempData["EMCData"] as EMCTravellingDetailsVM;
                        TempData["EMC"] = model;
                        TempData["EMCData"] = modelTrav;
                        model.emnHeader.NoteNumber = modelTrav.NoteNumber;
                        model.emnHeader.AttachFile = modelTrav.AttachFile;
                        model.Btnsubmit = modelTrav.btnSubmit;
                        model.emnHeader.CenterCodeName = model.CenterCodeName;
                        model.emnHeader.IsEPTour = model.IsEPTour == 1 ? true : false;
                        //model.PersonDtls = _iEMC.GetEMCTravellingPerson(modelTrav.NoteNumber, ref pMsg);
                    }
                    else
                    {
                        model.emnHeader = _iEMC.getNewEMCHeader(ref pMsg);

                    }
                }


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EMCHeaderEntryVM hdrmodel, string Submit=null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (Submit == "VAD")
            {
                return RedirectToAction("TravellingDetails", "EMC", new { NoteNumber = hdrmodel.emnHeader.NoteNumber});
            }
            else
            {
                model = TempData["EMC"] as EMCHeaderEntryVM;
                hdrmodel.emnHeader.Status = 1;
                hdrmodel.emnHeader.NoteNumber = hdrmodel.NoteNumber;
                hdrmodel.emnHeader.AttachFile = hdrmodel.AttachFile;
                hdrmodel.emnHeader.CenterCodeName = hdrmodel.CenterCodeName;
                hdrmodel.emnHeader.IsEPTour = model.IsEPTour==1?true:false;
                if (_iEMC.SetEMCDetailsFinalSubmit(hdrmodel.emnHeader, ref pMsg))
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
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult SetTravelingPersonDetails(EMCHeaderEntryVM modelvm)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                model = CastEMCTempData();
                model.emnHeader.NoteNumber = modelvm.NoteNumber;
                model.emnHeader.AttachFile = modelvm.AttachFile;
                model.emnHeader.CenterCodeName = modelvm.CenterCodeName;
                model.emnHeader.IsEPTour = modelvm.IsEPTour==1?true:false;
                if (model.Btnsubmit == 1)
                {
                    modelvm = TempData["EMC"] as EMCHeaderEntryVM;
                    TempData["EMC"] = modelvm;
                    result.bResponseBool = true;

                }
                else
                {
                    if (modelvm != null)
                    {
                        
                        var CenterName = modelvm.CenterCodeName.Split('/').Skip(1).FirstOrDefault();
                        var CenterCode = modelvm.CenterCodeName.Split('/').FirstOrDefault();
                        if (_iEMC.SetEMCTravellingPerson(modelvm.NoteNumber, Convert.ToInt32(CenterCode), CenterName.Trim(), modelvm.PersonDtls, ref pMsg))
                        {
                            TempData["EMC"] = modelvm;
                            result.bResponseBool = true;
                            result.sResponseString = "Data successfully updated.";

                        }
                        else
                        {
                            result.bResponseBool = false;
                            result.sResponseString = pMsg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TravellingDetails(int Btnsubmit = 0,string NoteNumber=null, string ServiceTypeCode = "1")
        {
            EMCTravellingDetailsVM modeltravvm = new EMCTravellingDetailsVM();
            try
            {
                model = TempData["EMC"] as EMCHeaderEntryVM;
                if (Btnsubmit != 1)
                {   

                    if (TempData["EMC"] != null)
                    {

                        if (model.PersonDtls.Where(x => x.PersonType == 2 || x.PersonType == 4).FirstOrDefault() != null)
                            modeltravvm.PersonType = model.PersonDtls.Where(x => x.PersonType == 2 || x.PersonType == 4).FirstOrDefault().PersonType > 0 ? 4 : 1;
                        else
                        modeltravvm.PersonType = 1;

                        modeltravvm.IsEPTour = model.IsEPTour;
                        modeltravvm.NoteNumber = model.NoteNumber;
                        modeltravvm.AttachFile = model.AttachFile;
                        modeltravvm.CenterCodenName = model.CenterCodeName;
                        
                        var DateNo = _iEMN.GetTourInfoForServiceType(ServiceTypeCode, ref pMsg);
                        modeltravvm.TourFromdateStr = DateTime.Today.AddDays(DateNo.MaxDayAllowed).ToString("yyyy-MM-dd");
                        modeltravvm.TodateStr = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
                        modeltravvm.FromdateStr = DateTime.Today.ToString("yyyy-MM-dd");


                    }

                }
                string baseUrl = "/Security/EMC/Create?NoteNumber=" + model.NoteNumber;
                ViewBag.BackUrl = baseUrl;

                ViewBag.BtnClear = "/Security/EMC/TravellingDetails?Btnsubmit=" + model.Btnsubmit;
                TempData["EMC"] = model;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            return View(modeltravvm);
        }
        [HttpPost]
        public ActionResult SetTravNTourDetails(EMCTravellingDetailsVM models)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            EMCTravellingDetails TdModel = new EMCTravellingDetails();
            List<EMCTravellingDetails> TModel = new List<EMCTravellingDetails>();
            try
            {
                foreach (var item in models.TravellingDetails)
                {
                    TdModel.NoteNumber = models.NoteNumber;
                    TdModel.PublicTransport = item.PublicTransport;
                    TdModel.VehicleType = item.VehicleType;
                    TdModel.ReasonVehicleReq = item.ReasonVehicleReq != null ? item.ReasonVehicleReq : "NA";
                    TdModel.VehicleTypeProvided = item.VehicleTypeProvided > 0 ? item.VehicleTypeProvided : 0;
                    TdModel.ReasonVehicleProvided = item.ReasonVehicleProvided != null ? item.ReasonVehicleProvided : "NA";
                    TdModel.SchFromDate = item.SchFromDate;
                    TdModel.SchFromTime = item.SchFromTime;
                    TdModel.SchTourToDate = item.SchTourToDate;
                    TdModel.PurposeOfVisit = item.PurposeOfVisit != null ? item.PurposeOfVisit : "NA";
                }

                TModel.Add(TdModel);
                if (models != null)
                {
                    if (_iEMC.setEMCTravDetailsNTourDetails(models.NoteNumber, TModel, models.dateTour, ref pMsg))
                    {
                        models.btnSubmit = 1;
                        models.Tourcat = models.dateTour.Where(c => c.TourCategory.Contains("3")).ToList().Count > 0 ? true : false;
                        TempData["EMCData"] = models;
                        result.bResponseBool = true;

                        result.sResponseString = "Data successfully updated.";

                    }
                    else
                    {
                        result.bResponseBool = false;
                        result.sResponseString = pMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetTraveelingPersonReverseData(string NoteNumber,int CenterCode=0,int status=0)
        {
            EMCHeaderEntryVM modelhdr = new EMCHeaderEntryVM();
            CustomAjaxResponse result = new CustomAjaxResponse();
            List<EMCTravellingPerson> modelTP = new List<EMCTravellingPerson>();
            try
            {
                if (TempData["EMC"] != null)
                {
                    modelhdr = TempData["EMC"] as EMCHeaderEntryVM;
                    TempData["EMC"] = modelhdr;
                    
                    modelhdr.PersonDtls = _iEMC.GetEMCTravellingPerson(NoteNumber,user.CentreCode, 1, ref pMsg);
                }
                }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return Json(modelhdr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTraveelingDetailsReverseData()
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            EMCTravellingDetailsVM TdModel = new EMCTravellingDetailsVM();
            try
            {
                if (TempData["EMCData"] != null)
                {

                    TdModel = TempData["EMCData"] as EMCTravellingDetailsVM;
                    if (TdModel.btnSubmit == 1)
                    {
                        TempData["EMCData"] = TdModel;
                        TdModel.travDetails = _iEMC.GetEMCTravellingDetails(TdModel.NoteNumber, ref pMsg);
                        TdModel.travDetails.SchFromDateStr = TdModel.travDetails.SchFromDate.ToString("yyyy-MM-dd");
                        TdModel.travDetails.SchTourToDateStr = TdModel.travDetails.SchTourToDate.ToString("yyyy-MM-dd");
                        //TdModel.travDetails.SchFromDateDisplay
                        TdModel.dateTour = _iEMC.GetEMCDateWiseTour(TdModel.NoteNumber, ref pMsg);
                        
                        TempData["BtnSubmit"] = 1;
                    }
                    TempData["EMCData"] = TdModel;
                }
               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(TdModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(string NoteNumber, int CanDelete, int CBUID = 0)
        {
            EMCHeaderEntryVM modelvmobj = new EMCHeaderEntryVM();
            try
            {
                modelvmobj.emnHeader = _iEMC.GetEMCHdrEntry(NoteNumber, ref pMsg);
                if (modelvmobj.emnHeader.IsApproved.HasValue)
                {
                    modelvmobj.emnHeader.IsApproveds = modelvmobj.emnHeader.IsApproved == true ? "Yes" : "No";
                }
                else
                {
                    modelvmobj.emnHeader.IsApproveds = "-";
                }
                modelvmobj.emnHeader.ApproveDatestr = modelvmobj.emnHeader.ApproveDatestr != "01/01/0001" ? modelvmobj.emnHeader.ApproveDatestr : "-";
                modelvmobj.emnHeader.ApproveTime = modelvmobj.emnHeader.ApproveTime != null ? modelvmobj.emnHeader.ApproveTime : "-";
                modelvmobj.emnHeader.ApprovedReason = modelvmobj.emnHeader.ApprovedReason != null ? modelvmobj.emnHeader.ApprovedReason : "-";
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = CanDelete == 1 ? "Delete" : "View";

            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult Details(EMCHeaderEntryVM modelobj, string Submit)
        {
            string baseUrl = "/Security/EMC/Details?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.CanDelete + "&CBUID=" + modelobj.CBUID;
            ViewBag.HeaderText = modelobj.HeaderText;
            if (Submit == "Delete")
            {
                if (_iEMC.RemoveEMCNoteNumber(modelobj.NoteNumber, 0, 1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";

                }
                else { ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber; }

            }
            else if (Submit == "DTD")
            {
                TempData["BackUrl"] = baseUrl;
                return RedirectToAction("TravellingDetailsView", "EMC", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            modelobj.emnHeader = _iEMC.GetEMCHdrEntry(modelobj.NoteNumber, ref pMsg);
            

            return View(modelobj);
        }
        public ActionResult TravellingDetailsView(string NoteNumber, int CBUID)
        {
            EMCTravellingDetailsVM modelvm = new EMCTravellingDetailsVM();
            try
            {
                ViewBag.BackUrl = TempData["BackUrl"] as string;
                modelvm.travDetails = _iEMC.GetEMCTravellingDetails(NoteNumber, ref pMsg);
                modelvm.dateTour = _iEMC.GetEMCDateWiseTour(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvm);
        }
        #endregion
        #region Approval

        public JsonResult GetEMCNZBApprovalforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<EMCNoteList> noteList = _iEMC.GetEMCNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 2, ref pMsg);

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
        public ActionResult EMCNoteApproveList()
        {
            TempData["EMCApproveTrav"] = null;
            return View();
        }
        public ActionResult EMCApproveNote(string NoteNumber = null)
        {
            EMCNoteApproveVM modelobj = new EMCNoteApproveVM();
            if (NoteNumber != null)
            {
                if (TempData["EMCApproveTrav"] != null)
                {
                    modelobj = TempData["EMCApproveTrav"] as EMCNoteApproveVM;
                    TempData["EMCApproveTrav"] = modelobj;
                }
                modelobj.Notelist = _iEMC.GetEMCNoteListToBeApproved(user.CentreCode, 1, ref pMsg);
                modelobj.NoteNumber = modelobj.Notelist.Where(x => x.NoteNumber == NoteNumber).FirstOrDefault().NoteNumber;

            }
            else
            {
                modelobj.Notelist = _iEMC.GetEMCNoteListToBeApproved(user.CentreCode, 1, ref pMsg);
            }
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult EMCApproveNote(EMCNoteApproveVM modelobj, string Submit = null)
        {

            if (Submit == "btnTravDetails")
            {
                return RedirectToAction("EMCApprovedTravDetails",
                 new { Area = "Security", NoteNumber = modelobj.NoteNumber, CBUID = 1 });
            }
            else if (Submit == "TravSubmit")
            {

            }
            else
            {
                CustomAjaxResponse result = new CustomAjaxResponse();
                modelobj.travdetails.NoteNumber = modelobj.NoteNumber;
                modelobj.travdetails.IsApproved = modelobj.IsApprove == 1 ? true : false;
                modelobj.travdetails.ApprovedReason = modelobj.ApproveReason != null ? modelobj.ApproveReason : "NA";
                modelobj.travdetails.ReasonVehicleProvided = "NA";
                modelobj.travdetails.VehicleTypeProvided = 0;
                modelobj.travdetails.EmployeeNonName = "NA";
                modelobj.travdetails.status = 1;
                if (_iEMC.SetEMCApprovalData(modelobj.travdetails, ref pMsg))
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";
                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return View(modelobj);
        }
        public ActionResult EMCApprovedTravDetails(string NoteNumber, int CBUID)
        {
            EMCTravellingDetailsVM modelvms = new EMCTravellingDetailsVM();
            EMCHeaderEntryVM result = new EMCHeaderEntryVM();
            try
            {
                modelvms.travDetails = _iEMC.GetEMCTravellingDetails(NoteNumber, ref pMsg);
                modelvms.dateTour = _iEMC.GetEMCDateWiseTour(NoteNumber, ref pMsg);
                string baseUrl = "/Security/EMC/EMCApproveNote?NoteNumber=" + NoteNumber;
                ViewBag.BackUrl = baseUrl;
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvms);

        }
        public JsonResult GetEmployeeNoName(string NoteNo,int status=0)
        {
            EMCHeaderEntryVM result = new EMCHeaderEntryVM();
            List<CustomComboOptionsWithString> resulmn = new List<CustomComboOptionsWithString>();
            result.PersonDtls = _iEMC.GetEMCTravellingPerson(NoteNo,0, status, ref pMsg);

            if (result.PersonDtls != null)
            {
                foreach (var item in result.PersonDtls)
                {
                    CustomComboOptionsWithString cmb = new CustomComboOptionsWithString();
                    cmb.ID = item.EmployeeNonName;
                    cmb.DisplayText = item.EmployeeNonName;
                    resulmn.Add(cmb);
                }
            }

            return Json(resulmn, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetApprovalTravDetails(EMCTravellingDetailsVM model)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (model != null)
            {
                EMCNoteApproveVM Travmodel = new EMCNoteApproveVM();
                Travmodel.travdetails.NoteNumber = model.NoteNumber;
                Travmodel.travdetails.VehicleTypeProvided = model.VehicleTypeProvided;
                Travmodel.travdetails.ReasonVehicleProvided = model.ReasonVehicleProvided != null ? model.ReasonVehicleProvided : "NA";
                Travmodel.travdetails.EmployeeNonName = model.EmployeeNonName != null ? model.EmployeeNonName : "NA";
                Travmodel.travdetails.ApprovedReason = "NA";
                Travmodel.travdetails.status = 2;
                if (_iEMC.SetEMCApprovalData(Travmodel.travdetails, ref pMsg))
                {
                    Travmodel.NoteNumber = model.NoteNumber;
                    Travmodel.btnDisplay = 1;
                    TempData["EMCApproveTrav"] = Travmodel;
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";
                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovalDetails(string NoteNumber, int CanDelete, int CBUID = 0)
        {
            EMCHeaderEntryVM modelvmobj = new EMCHeaderEntryVM();
            try
            {
                modelvmobj.emnHeader = _iEMC.GetEMCHdrEntry(NoteNumber, ref pMsg);
                if (modelvmobj.emnHeader.IsRatified.HasValue)
                {
                    modelvmobj.emnHeader.IsRatifieds = modelvmobj.emnHeader.IsRatified == true ? "Yes" : "No";
                }
                else
                {
                    modelvmobj.emnHeader.IsRatifieds = "-";
                }
                modelvmobj.emnHeader.RatifiedDatestr = modelvmobj.emnHeader.RatifiedDatestr != "01/01/0001" ? modelvmobj.emnHeader.RatifiedDatestr : "-";

                modelvmobj.emnHeader.RatifiedTime = modelvmobj.emnHeader.RatifiedTime != null ? modelvmobj.emnHeader.RatifiedTime : "-";
                modelvmobj.emnHeader.RatifiedReason = modelvmobj.emnHeader.RatifiedReason != null ? modelvmobj.emnHeader.RatifiedReason : "-";
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = "Approval";


            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult ApprovalDetails(EMCHeaderEntryVM modelobj, string Submit)
        {
            string baseUrls = "/Security/EMC/ApprovalDetails?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=0&CBUID=0";


            if (Submit == "DTD")
            {
                TempData["ABackUrl"] = baseUrls;
                return RedirectToAction("ApprovalTravellingDetailsView", "EMC", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }

            return View(modelobj);
        }
        public ActionResult ApprovalTravellingDetailsView(string NoteNumber, int CBUID)
        {
            EMCTravellingDetailsVM modelvm = new EMCTravellingDetailsVM();
            try
            {
                ViewBag.BackUrls = TempData["ABackUrl"] as string;
                modelvm.travDetails = _iEMC.GetEMCTravellingDetails(NoteNumber, ref pMsg);
                modelvm.dateTour = _iEMC.GetEMCDateWiseTour(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvm);
        }
        #endregion
        #region Common Use
        public JsonResult getBranchType(int CenterId)
        {
            IEnumerable<LocationMaster> result = _master.GetBranchOfaCentre(CenterId, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocationsFromTypes(string TypeIDs)
        {
            TypeIDs = TypeIDs.Replace('_', ',');
            IEnumerable<LocationMaster> result = _master.GetCentresFromTourCategory(TypeIDs, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            IEnumerable<LocationMaster> result = null;
            if (TypeID == 6)
            {
                result = _master.GetCentresFromTourCategory(Convert.ToString(2), ref pMsg);
                result = result.Where(x => x.ID == 13);
            }
            else
            {
                result = _master.GetCentresFromTourCategory(TypeID.ToString(), ref pMsg);
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetEPTourNoteNumber(int empID)
        {
            //empID = 130317;
            IEnumerable<TPEPNote> tempobj ;
            tempobj = _iEMC.GetEPTourNoteNumber(empID,user.CentreCode, ref pMsg);
            return Json(tempobj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStaffList(int CentreCode)
        {
            IEnumerable<CustomComboOptions> result;
            EHGHeaderEntryVM tempobj = new EHGHeaderEntryVM(true);
            result = tempobj.getStaffList(CentreCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVehicleTypes(int TypeVal = 0, string PT = null)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();

            EHGMaster master = EHGMaster.GetInstance;
            if (PT == null)
            {   
                if (TypeVal == 4 )
                {
                    result = master.VehicleTypes.Where(x => x.ID != 3).ToList();
                }
                else
                {
                    result = master.VehicleTypes.Where(x => x.ID ==2).ToList();
                }

            }
            else
            {
                if (TypeVal == 1)
                {
                    result = master.VehicleTypes.ToList();
                }
                else if (TypeVal == 2)
                {
                    result = master.VehicleTypes.Where(x => x.ID != 1).ToList();
                }
                else
                {
                    result = master.VehicleTypes.Where(x => x.ID == 3).ToList();
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEMCHdrDetails(string NoteNumber)
        {
            EMCHeaderEntryVM result = new EMCHeaderEntryVM();
            result.NoteNumber = NoteNumber;
            result.emnHeader = _iEMC.GetEMCHdrEntry(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTravellingPersonForEMC(string NoteNumber,int CenterCode=0,int status=0)
        {
            EMCHeaderEntryVM modelhdr = new EMCHeaderEntryVM();
            modelhdr.PersonDtls = _iEMC.GetEMCTravellingPerson(NoteNumber, user.CentreCode, status, ref pMsg);
            return Json(modelhdr.PersonDtls, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonTypes()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            EHGMaster master = EHGMaster.GetInstance;
            long[] ids = { 1,2};
            result = master.PersonType.Where(x => ids.Contains(x.ID)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetTourCategories(int PTval=0)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            EHGMaster master = EHGMaster.GetInstance;
            long[] ids = { 1, 4 };
            if (PTval == 2)
            {
                result = master.TourCategoryForNZB.Where(x =>x.ID==6).ToList();
            }
            else
            {
                if (user.CentreCode == 15)
                {
                    
                    result = master.TourCategoryForNZB.Where(x => ids.Contains(x.ID)).ToList();
                }
                else
                {
                    if (PTval == 1)
                    {
                        result = master.TourCategoryForNZB.Where(x => ids.Contains(x.ID)).ToList();
                    }
                    else
                    {
                        result = master.TourCategoryForNZB.Where(x =>x.ID==1).ToList();
                    }
                }
                
            }
           
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Private Function for Teamp Data
        private EMCHeaderEntryVM CastEMCTempData()
        {
            if (TempData["EMC"] != null)
            {
                model = TempData["EMC"] as EMCHeaderEntryVM;
            }
            else
            {
                model = new EMCHeaderEntryVM();
            }
            TempData["EMC"] = model;
            return model;
        }
        #endregion
    }
}