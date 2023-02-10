using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EHG;
using CBBW.Areas.Security.ViewModel.EMN;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.EMN;

namespace CBBW.Areas.Security.Controllers
{
    public class EMNController : Controller
    {
        // GET: Security/EMN
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        IEMNRepository _iEMN;
        EMNTravellingDetailsVM modelTrav;
        EMNHeaderEntryVM model;
        
        ICTVRepository _iCTV;
        public EMNController(ICTVRepository iCTV, IUserRepository iUser, IEMNRepository iEMN, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
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
            TempData["EMN"] = null;
            TempData["EMNData"] =null;
            TempData["BtnSubmit"] = null;
            return View();
        }
        public JsonResult GetEMNNZBDetailsforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
    string sSortDir_0, string sSearch)
        {
            List<EMNNoteList> noteList = _iEMN.GetEMNNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        public ActionResult Create(string NoteNumber = null)
        {
            EMNHeaderEntryVM model = new EMNHeaderEntryVM();
            try
            {
                if (NoteNumber != null)
                {
                    model = TempData["EMN"] as EMNHeaderEntryVM;
                    model.emnHeader.NoteNumber = model.NoteNumber;
                    model.emnHeader.AttachFile = model.AttachFile;
                    model.emnHeader.CenterCodeName = model.CenterCodeName;
                   //model.PersonDtls = _iEMN.GetEMNTravellingPerson(model.NoteNumber, ref pMsg);
                    if (TempData["BtnSubmit"] != null)
                    {
                        model.Btnsubmit = 1;
                    }
                    else
                    {
                        model.Btnsubmit = 0;
                    }
                    TempData["EMN"] = model;
                }
                else
                {
                    if (TempData["EMN"] != null && TempData["EMNData"]!=null)
                    {
                        model = TempData["EMN"] as EMNHeaderEntryVM;
                        modelTrav = TempData["EMNData"] as EMNTravellingDetailsVM;
                        TempData["EMN"] = model;
                        TempData["EMNData"] = modelTrav;
                        model.emnHeader.NoteNumber = modelTrav.NoteNumber;
                        model.emnHeader.AttachFile = modelTrav.AttachFile;
                        model.Btnsubmit = modelTrav.btnSubmit;
                        model.emnHeader.CenterCodeName = model.CenterCodeName;
                       //model.PersonDtls = _iEMN.GetEMNTravellingPerson(modelTrav.NoteNumber, ref pMsg);
                    }
                    else
                    {
                        model.emnHeader = _iEMN.getNewEMNHeader(ref pMsg);

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
        public ActionResult Create(EMNHeaderEntryVM hdrmodel, string Submit=null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (Submit == "VAD")
            {
                return RedirectToAction("TravellingDetails", "EMN", new { NoteNumber = hdrmodel.emnHeader.NoteNumber});
            }
            else
            {
               
                hdrmodel.emnHeader.Status = 1;
                hdrmodel.emnHeader.NoteNumber = hdrmodel.NoteNumber;
                hdrmodel.emnHeader.AttachFile = hdrmodel.AttachFile;
                hdrmodel.emnHeader.CenterCodeName = hdrmodel.CenterCodeName;
                if (_iEMN.SetEMNDetailsFinalSubmit(hdrmodel.emnHeader, ref pMsg))
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
        public ActionResult SetTravelingPersonDetails(EMNHeaderEntryVM modelvm)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                model = CastEMNTempData();
                model.emnHeader.NoteNumber = modelvm.NoteNumber;
                model.emnHeader.AttachFile = modelvm.AttachFile;
                model.emnHeader.CenterCodeName = modelvm.CenterCodeName;
                if (model.Btnsubmit == 1)
                {
                    modelvm = TempData["EMN"] as EMNHeaderEntryVM;
                    TempData["EMN"] = modelvm;
                    result.bResponseBool = true;

                }
                else
                {
                    if (modelvm != null)
                    {
                        
                        var CenterName = modelvm.CenterCodetxt.Split('/').Skip(1).FirstOrDefault();
                        if (_iEMN.SetEMNTravellingPerson(modelvm.NoteNumber, modelvm.CenterCode, CenterName.Trim(), modelvm.PersonDtls, ref pMsg))
                        {
                            TempData["EMN"] = modelvm;
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
        public ActionResult TravellingDetails(int Btnsubmit = 0,string NoteNumber=null,string ServiceTypeCode="1")
        {
            EMNTravellingDetailsVM modeltravvm = new EMNTravellingDetailsVM();
            try
            {
                model = TempData["EMN"] as EMNHeaderEntryVM;
                if (Btnsubmit != 1)
                {

                    if (TempData["EMN"] != null)
                    {

                        if (model.PersonDtls.Where(x => x.PersonType == 2 || x.PersonType == 4).FirstOrDefault() != null)
                            modeltravvm.PersonType = model.PersonDtls.Where(x => x.PersonType == 2 || x.PersonType == 4).FirstOrDefault().PersonType > 0 ? 4 : 1;
                        else
                            modeltravvm.PersonType = 1;
                        modeltravvm.NoteNumber = model.NoteNumber;
                        modeltravvm.AttachFile = model.AttachFile;
                        modeltravvm.CenterCodenName = model.CenterCodeName;
                        
                        var DateNo = _iEMN.GetTourInfoForServiceType(ServiceTypeCode, ref pMsg);
                      
                        modeltravvm.TourFromdateStr = DateTime.Today.AddDays(DateNo.MaxDayAllowed).ToString("yyyy-MM-dd");
                        modeltravvm.TodateStr = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
                        modeltravvm.FromdateStr = DateTime.Today.ToString("yyyy-MM-dd");


                    }

                }
                string baseUrl = "/Security/EMN/Create?NoteNumber=" + model.NoteNumber;
                ViewBag.BackUrl = baseUrl;

                ViewBag.BtnClear = "/Security/EMN/TravellingDetails?Btnsubmit=" + model.Btnsubmit;
                TempData["EMN"] = model;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            return View(modeltravvm);
        }
        [HttpPost]
        public ActionResult SetTravNTourDetails(EMNTravellingDetailsVM models)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            EMNTravellingDetails TdModel = new EMNTravellingDetails();
            List<EMNTravellingDetails> TModel = new List<EMNTravellingDetails>();
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
                    if (_iEMN.setEMNTravDetailsNTourDetails(models.NoteNumber, TModel, models.dateTour, ref pMsg))
                    {
                        models.btnSubmit = 1;
                        TempData["EMNData"] = models;
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
        public JsonResult GetTraveelingPersonReverseData(string NoteNumber,int CenterCode,int status=0)
        {
            EMNHeaderEntryVM modelhdr = new EMNHeaderEntryVM();
            CustomAjaxResponse result = new CustomAjaxResponse();
            List<EMNTravellingPerson> modelTP = new List<EMNTravellingPerson>();
            try
            {
                modelhdr.PersonDtls = _iEMN.GetEMNTravellingPerson(NoteNumber, CenterCode, status, ref pMsg);  
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
            EMNTravellingDetailsVM TdModel = new EMNTravellingDetailsVM();
            try
            {
                if (TempData["EMNData"] != null)
                {

                    TdModel = TempData["EMNData"] as EMNTravellingDetailsVM;
                    if (TdModel.btnSubmit == 1)
                    {
                        TempData["EMNData"] = TdModel;
                        TdModel.travDetails = _iEMN.GetEMNTravellingDetails(TdModel.NoteNumber, ref pMsg);
                        TdModel.travDetails.SchFromDateStr = TdModel.travDetails.SchFromDate.ToString("yyyy-MM-dd");
                        TdModel.travDetails.SchTourToDateStr = TdModel.travDetails.SchTourToDate.ToString("yyyy-MM-dd");
                        //TdModel.travDetails.SchFromDateDisplay
                        TdModel.dateTour = _iEMN.GetEMNDateWiseTour(TdModel.NoteNumber, ref pMsg);
                        
                        TempData["BtnSubmit"] = 1;
                    }
                    TempData["EMNData"] = TdModel;
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
            EMNHeaderEntryVM modelvmobj = new EMNHeaderEntryVM();
            try
            {
                modelvmobj.emnHeader = _iEMN.GetEMNHdrEntry(NoteNumber, ref pMsg);
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
                //modelvmobj.PersonDtls = _iEMN.GetEMNTravellingPerson(NoteNumber, ref pMsg);
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = CanDelete == 1 ? "Delete" : "View";

            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult Details(EMNHeaderEntryVM modelobj, string Submit)
        {
            string baseUrl = "/Security/EMN/Details?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.CanDelete + "&CBUID=" + modelobj.CBUID;
            ViewBag.HeaderText = modelobj.HeaderText;
            if (Submit == "Delete")
            {
                if (_iEMN.RemoveEMNNoteNumber(modelobj.NoteNumber, 0, 1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";

                }
                else { ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber; }

            }
            else if (Submit == "DTD")
            {
                TempData["BackUrl"] = baseUrl;
                return RedirectToAction("TravellingDetailsView", "EMN", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            modelobj.emnHeader = _iEMN.GetEMNHdrEntry(modelobj.NoteNumber, ref pMsg);
            

            return View(modelobj);
        }
        public ActionResult TravellingDetailsView(string NoteNumber, int CBUID)
        {
            EMNTravellingDetailsVM modelvm = new EMNTravellingDetailsVM();
            try
            {
                ViewBag.BackUrl = TempData["BackUrl"] as string;
                modelvm.travDetails = _iEMN.GetEMNTravellingDetails(NoteNumber, ref pMsg);
                modelvm.dateTour = _iEMN.GetEMNDateWiseTour(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvm);
        }
        #endregion
        #region Approval

        public JsonResult GetEMNNZBApprovalforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<EMNNoteList> noteList = _iEMN.GetEMNNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 2, ref pMsg);

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
        public ActionResult EMNNoteApproveList()
        {
            TempData["EMNApproveTrav"] = null;
            return View();
        }
        public ActionResult EMNApproveNote(string NoteNumber = null)
        {
            EMNNoteApproveVM modelobj = new EMNNoteApproveVM();
            if (NoteNumber != null)
            {
                if (TempData["EMNApproveTrav"] != null)
                {
                    modelobj = TempData["EMNApproveTrav"] as EMNNoteApproveVM;
                    TempData["EMNApproveTrav"] = modelobj;
                }
                modelobj.Notelist = _iEMN.GetEMNNoteListToBeApproved(user.CentreCode, 1, ref pMsg);
                modelobj.NoteNumber = modelobj.Notelist.Where(x => x.NoteNumber == NoteNumber).FirstOrDefault().NoteNumber;

            }
            else
            {
                modelobj.Notelist = _iEMN.GetEMNNoteListToBeApproved(user.CentreCode, 1, ref pMsg);
            }
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult EMNApproveNote(EMNNoteApproveVM modelobj, string Submit = null)
        {

            if (Submit == "btnTravDetails")
            {
                return RedirectToAction("EMNApprovedTravDetails",
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
                if (_iEMN.SetEMNApprovalData(modelobj.travdetails, ref pMsg))
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
        public ActionResult EMNApprovedTravDetails(string NoteNumber, int CBUID)
        {
            EMNTravellingDetailsVM modelvms = new EMNTravellingDetailsVM();
            EMNHeaderEntryVM result = new EMNHeaderEntryVM();
            try
            {
                modelvms.travDetails = _iEMN.GetEMNTravellingDetails(NoteNumber, ref pMsg);
                modelvms.dateTour = _iEMN.GetEMNDateWiseTour(NoteNumber, ref pMsg);
                string baseUrl = "/Security/EMN/EMNApproveNote?NoteNumber=" + NoteNumber;
                ViewBag.BackUrl = baseUrl;
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvms);

        }
        public JsonResult GetEmployeeNoName(string NoteNo,int status=0)
        {
            EMNHeaderEntryVM result = new EMNHeaderEntryVM();
            List<CustomComboOptionsWithString> resulmn = new List<CustomComboOptionsWithString>();
            result.PersonDtls = _iEMN.GetEMNTravellingPerson(NoteNo,-1, status, ref pMsg);

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
        public ActionResult SetApprovalTravDetails(EMNTravellingDetailsVM model)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (model != null)
            {
                EMNNoteApproveVM Travmodel = new EMNNoteApproveVM();
                Travmodel.travdetails.NoteNumber = model.NoteNumber;
                Travmodel.travdetails.VehicleTypeProvided = model.VehicleTypeProvided;
                Travmodel.travdetails.ReasonVehicleProvided = model.ReasonVehicleProvided != null ? model.ReasonVehicleProvided : "NA";
                Travmodel.travdetails.EmployeeNonName = model.EmployeeNonName != null ? model.EmployeeNonName : "NA";
                Travmodel.travdetails.ApprovedReason = "NA";
                Travmodel.travdetails.status = 2;
                if (_iEMN.SetEMNApprovalData(Travmodel.travdetails, ref pMsg))
                {
                    Travmodel.NoteNumber = model.NoteNumber;
                    Travmodel.btnDisplay = 1;
                    TempData["EMNApproveTrav"] = Travmodel;
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
            EMNHeaderEntryVM modelvmobj = new EMNHeaderEntryVM();
            try
            {
                modelvmobj.emnHeader = _iEMN.GetEMNHdrEntry(NoteNumber, ref pMsg);
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
        public ActionResult ApprovalDetails(EMNHeaderEntryVM modelobj, string Submit)
        {
            string baseUrls = "/Security/EMN/ApprovalDetails?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=0&CBUID=0";


            if (Submit == "DTD")
            {
                TempData["ABackUrl"] = baseUrls;
                return RedirectToAction("ApprovalTravellingDetailsView", "EMN", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }

            return View(modelobj);
        }
        public ActionResult ApprovalTravellingDetailsView(string NoteNumber, int CBUID)
        {
            EMNTravellingDetailsVM modelvm = new EMNTravellingDetailsVM();
            try
            {
                ViewBag.BackUrls = TempData["ABackUrl"] as string;
                modelvm.travDetails = _iEMN.GetEMNTravellingDetails(NoteNumber, ref pMsg);
                modelvm.dateTour = _iEMN.GetEMNDateWiseTour(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvm);
        }


        #endregion
        #region RIFC
        public JsonResult GetEMNRTFCNforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
 string sSortDir_0, string sSearch)
        {
            List<EMNNoteList> noteList = _iEMN.GetEMNNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 3, ref pMsg);

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
        public ActionResult RTFNIndex()
        {

            return View();
        }
        public ActionResult RTFNCreate(string NoteNumber = null, int btnDisplay = 0)
        {
            EMNNoteApproveVM modelobj = new EMNNoteApproveVM();
            try
            {
                if (NoteNumber != null)
                {
                    modelobj.btnDisplay = btnDisplay;
                    modelobj.Notelist = _iEMN.GetEMNNoteListToBeApproved(user.CentreCode, 2, ref pMsg);
                    if (modelobj.Notelist != null)
                    {
                        modelobj.NoteNumber = modelobj.Notelist.Where(x => x.NoteNumber == NoteNumber).FirstOrDefault().NoteNumber;
                    }
                }
                else
                {
                    modelobj.Notelist = _iEMN.GetEMNNoteListToBeApproved(user.CentreCode, 2, ref pMsg);
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }

            return View(modelobj);
        }
        [HttpPost]
        public ActionResult RTFNCreate(EMNNoteApproveVM modelobj, string Submit = null)
        {
            if (Submit == "btnTravDetails")
            {
                return RedirectToAction("RTFNTravellingDetails",
                 new { Area = "Security", NoteNumber = modelobj.NoteNumber, CBUID = 1 });
            }
            else
            {
                CustomAjaxResponse result = new CustomAjaxResponse();
                modelobj.ratified.NoteNumber = modelobj.NoteNumber;
                modelobj.ratified.IsRatified = modelobj.IsRatified == 1 ? true : false;
                modelobj.ratified.RatifiedReason = modelobj.RatifiedReason != null ? modelobj.RatifiedReason : "NA";
                modelobj.ratified.status = 1;
                if (_iEMN.SetEMNRatifiedData(modelobj.ratified, ref pMsg))
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
        public ActionResult RTFNTravellingDetails(string NoteNumber, int CBUID = 0)
        {
            EMNTravellingDetailsVM modelvms = new EMNTravellingDetailsVM();
            EMNHeaderEntryVM result = new EMNHeaderEntryVM();
           
            string baseUrl = "";
            try
            {
               
                modelvms.travDetails = _iEMN.GetEMNTravellingDetails(NoteNumber, ref pMsg);
                ViewBag.PublicTrans = modelvms.travDetails.PublicTransports;
                modelvms.dateTour = _iEMN.GetEMNDateWiseTour(NoteNumber, ref pMsg);
                if (CBUID == 2)
                {
                    baseUrl = "/Security/EMN/RTFNDetailsView?NoteNumber=" + NoteNumber;
                }
                else
                {
                    baseUrl = "/Security/EMN/RTFNCreate?NoteNumber=" + NoteNumber;
                }
                ViewBag.BackUrl = baseUrl;
                ViewBag.CBUID = CBUID;
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvms);
        }
        [HttpPost]
        public ActionResult RTFNTravellingDetails(EMNTravellingDetailsVM modelobj)
        {
            if (modelobj != null)
            {
                return RedirectToAction("RTFNCreate",
                     new { Area = "Security", NoteNumber = modelobj.NoteNumber, btnDisplay = 1 });

            }
            return View(modelobj);
        }
        public ActionResult RTFNDetailsView(string NoteNumber, int CanDelete = 0, int CBUID = 0)
        {
            EMNHeaderEntryVM modelvmobj = new EMNHeaderEntryVM();
            try
            {
                modelvmobj.emnHeader = _iEMN.GetEMNHdrEntry(NoteNumber, ref pMsg);
               
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = "Ratification";


            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult RTFNDetailsView(EMNHeaderEntryVM model)
        {
            if (model.NoteNumber != null)
            {
                return RedirectToAction("RTFNTravellingDetails",
                         new { Area = "Security", NoteNumber = model.NoteNumber, CBUID = 2 });
            }
            return View();
        }
        #endregion
        #region Common Use
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
                    result = master.VehicleTypes.Where(x => x.ID != 3).ToList();   
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
        public JsonResult GetEMNHdrDetails(string NoteNumber)
        {
            EMNHeaderEntryVM result = new EMNHeaderEntryVM();
            result.NoteNumber = NoteNumber;
            result.emnHeader = _iEMN.GetEMNHdrEntry(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTravellingPersonForEMN(string NoteNumber,int CenterCode,int status=0)
        {
            EMNHeaderEntryVM modelhdr = new EMNHeaderEntryVM();
            modelhdr.PersonDtls = _iEMN.GetEMNTravellingPerson(NoteNumber, CenterCode, status, ref pMsg);
            return Json(modelhdr.PersonDtls, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonTypes()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();

            EHGMaster master = EHGMaster.GetInstance;
            result = master.PersonType.Where(x=> x.ID != 4).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCenterCodeListFromTravellingPerson(string NoteNumber)
        {
         
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            result = _iEMN.getCenterCodeListFromTravellingPerson(NoteNumber,1, ref pMsg).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCenterCodeListFromTravellingPersonNotActive(string NoteNumber)
        {

            List<CustomComboOptions> result = new List<CustomComboOptions>();
            result = _iEMN.getCenterCodeListFromTravellingPerson(NoteNumber,2, ref pMsg).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCenterCodeList(int center = 0)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            result = _iEMN.getCenterCodeList(center, ref pMsg).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCategories(int PTval=0)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            EHGMaster master = EHGMaster.GetInstance;
            long[] ids = {2,3};
            result = master.TourCategoryForNZB.Where(x => ids.Contains(x.ID)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Private Function for Teamp Data
        private EMNHeaderEntryVM CastEMNTempData()
        {
            if (TempData["EMN"] != null)
            {
                model = TempData["EMN"] as EMNHeaderEntryVM;
            }
            else
            {
                model = new EMNHeaderEntryVM();
            }
            TempData["EMN"] = model;
            return model;
        }
        #endregion
    }
}