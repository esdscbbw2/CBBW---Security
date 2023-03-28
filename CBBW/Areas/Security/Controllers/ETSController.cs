using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.EHG;
using CBBW.Areas.Security.ViewModel.ETS;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.ETS;
using CBBW.BOL.Master;

namespace CBBW.Areas.Security.Controllers
{
    public class ETSController : Controller
    {
        // GET: Security/ETS
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        IETSRepository _iETS;
        ETSHeaderEntryVM model;
        ETSTravellingDetailsVM modelTrav;
        ICTVRepository _iCTV;
        public ETSController(ICTVRepository iCTV, IUserRepository iUser, IETSRepository iETS, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
                _iETS = iETS;
                _iCTV = iCTV;
                //iUser.LogIn("praveen", ref pMsg);
                user = iUser.getLoggedInUser();
                ViewBag.LogInUser = user.UserName;
            }
            catch (Exception ex) { pMsg = ex.ToString(); }

        }
      
        #region For Entry Process
        public ActionResult Index()
        {
            TempData["ETS"] = null;
            TempData["ETSData"] = null;


            return View();
        }
        public ActionResult Create(string NoteNumber = null)
        {
            ETSHeaderEntryVM model = new ETSHeaderEntryVM();
            try
            {
                if (NoteNumber != null)
                {
                    model = TempData["ETS"] as ETSHeaderEntryVM;
                    model.etsHeader.NoteNumber = model.NoteNumber;
                    model.etsHeader.AttachFile = model.AttachFile;
                    model.etsHeader.CenterCodeName = model.CenterCodeName;
                    model.PersonDtls = _iETS.GetETSTravellingPerson(model.NoteNumber, ref pMsg);
                    if (TempData["BtnSubmit"] != null)
                    {
                        model.Btnsubmit = 1;
                    }
                    else
                    {
                        model.Btnsubmit = 0;
                    }
                    TempData["ETS"] = model;
                }
                else
                {
                    if (TempData["ETS"] != null && TempData["ETSData"] != null)
                    {

                        model = TempData["ETS"] as ETSHeaderEntryVM;
                        modelTrav = TempData["ETSData"] as ETSTravellingDetailsVM;
                        TempData["ETS"] = model;
                        TempData["ETSData"] = modelTrav;
                        model.etsHeader.NoteNumber = modelTrav.NoteNumber;
                        model.etsHeader.AttachFile = modelTrav.AttachFile;
                        model.Btnsubmit = modelTrav.btnSubmit;
                        model.etsHeader.CenterCodeName = model.CenterCodeName;
                        model.PersonDtls = _iETS.GetETSTravellingPerson(modelTrav.NoteNumber, ref pMsg);
                    }
                    else
                    {
                        model.etsHeader = _iETS.getNewETSHeader(ref pMsg);

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
        public ActionResult Create(ETSHeaderEntryVM hdrmodel)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            hdrmodel.etsHeader.Status = 1;
            hdrmodel.etsHeader.NoteNumber = hdrmodel.NoteNumber;
            hdrmodel.etsHeader.AttachFile = hdrmodel.AttachFile;
            hdrmodel.etsHeader.CenterCodeName = hdrmodel.CenterCodeName;
            //hdrmodel.etsHeader.CenterCode = user.CentreCode;
            if (_iETS.SetETSDetailsFinalSubmit(hdrmodel.etsHeader, ref pMsg))
            {
                result.bResponseBool = true;
                result.sResponseString = "Data successfully updated.";
                // return RedirectToAction("Index", new { Area = "Security" });
            }
            else
            {
                result.bResponseBool = false;
                result.sResponseString = pMsg;

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
       
        [HttpPost]
        public ActionResult SetTravelingPersonDetails(ETSHeaderEntryVM modelvm)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                model = CastETSTempData();
                model.etsHeader.NoteNumber = modelvm.NoteNumber;
                model.etsHeader.AttachFile = modelvm.AttachFile;
                model.etsHeader.CenterCodeName = modelvm.CenterCodeName;
                //if (model.Btnsubmit == 2 || model.Btnsubmit==1) {
                //     modelvm= TempData["ETS"] as ETSHeaderEntryVM;
                //    TempData["ETS"] = modelvm;
                //    result.bResponseBool = true;

                //} else { 
                if (model.Btnsubmit == 1)
                {
                    modelvm = TempData["ETS"] as ETSHeaderEntryVM;
                    TempData["ETS"] = modelvm;
                    result.bResponseBool = true;

                }
                else
                {
                    if (modelvm != null)
                    {
                        if (_iETS.SetETSTravellingPerson(modelvm.NoteNumber, modelvm.PersonDtls, ref pMsg))
                        {
                            TempData["ETS"] = modelvm;
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
        [HttpPost]
        public ActionResult SetTravNTourDetails(ETSTravellingDetailsVM models)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            ETSTravellingDetails TdModel = new ETSTravellingDetails();
            List<ETSTravellingDetails> TModel = new List<ETSTravellingDetails>();
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
                    if (_iETS.setETSTravDetailsNTourDetails(models.NoteNumber, TModel, models.dateTour, ref pMsg))
                    {
                        models.btnSubmit = 1;
                        TempData["ETSData"] = models;
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
        public ActionResult TravellingDetails(int Btnsubmit = 0)
        {
            ETSTravellingDetailsVM modeltravvm = new ETSTravellingDetailsVM();
            try
            {
                model = TempData["ETS"] as ETSHeaderEntryVM;
                if (Btnsubmit != 1)
                {

                    if (TempData["ETS"] != null)
                    {

                        if (model.PersonDtls.Where(x => x.PersonType == 2 || x.PersonType == 4).FirstOrDefault() != null)
                            modeltravvm.PersonType = model.PersonDtls.Where(x => x.PersonType == 2 || x.PersonType == 4).FirstOrDefault().PersonType > 0 ? 4 : 1;
                        else
                            modeltravvm.PersonType = 1;
                        modeltravvm.NoteNumber = model.NoteNumber;
                        modeltravvm.AttachFile = model.AttachFile;
                        modeltravvm.CenterCodenName = model.CenterCodeName;
                        modeltravvm.TodateStr = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
                        modeltravvm.FromdateStr = DateTime.Today.ToString("yyyy-MM-dd");


                    }

                }
                string baseUrl = "/Security/ETS/Create?NoteNumber=" + model.NoteNumber;
                ViewBag.BackUrl = baseUrl;

                ViewBag.BtnClear = "/Security/ETS/TravellingDetails?Btnsubmit=" + model.Btnsubmit;
                TempData["ETS"] = model;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            return View(modeltravvm);
        }
        public JsonResult GetTraveelingDetailsReverseData()
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            ETSTravellingDetailsVM TdModel = new ETSTravellingDetailsVM();
            try
            {
                if (TempData["ETSData"] != null)
                {

                    TdModel = TempData["ETSData"] as ETSTravellingDetailsVM;
                    if (TdModel.btnSubmit == 1)
                    {
                        TempData["ETSData"] = TdModel;
                        TdModel.travDetails = _iETS.GetETSTravellingDetails(TdModel.NoteNumber, ref pMsg);
                        TdModel.travDetails.SchFromDateStr = TdModel.travDetails.SchFromDate.ToString("yyyy-MM-dd");
                        TdModel.travDetails.SchTourToDateStr = TdModel.travDetails.SchTourToDate.ToString("yyyy-MM-dd");
                        //TdModel.travDetails.SchFromDateDisplay
                        TdModel.dateTour = _iETS.GetETSDateWiseTour(TdModel.NoteNumber, ref pMsg);
                        TempData["BtnSubmit"] = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(TdModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTraveelingPersonReverseData()
        {
            ETSHeaderEntryVM modelhdr = new ETSHeaderEntryVM();
            CustomAjaxResponse result = new CustomAjaxResponse();
            List<ETSTravellingPerson> modelTP = new List<ETSTravellingPerson>();
            try
            {
                if (TempData["ETS"] != null)
                {
                    modelhdr = TempData["ETS"] as ETSHeaderEntryVM;
                    TempData["ETS"] = modelhdr;
                    modelhdr.PersonDtls = _iETS.GetETSTravellingPerson(modelhdr.NoteNumber, ref pMsg);
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return Json(modelhdr, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(string NoteNumber, int CanDelete, int CBUID = 0)
        {
            ETSHeaderEntryVM modelvmobj = new ETSHeaderEntryVM();
            try
            {
                modelvmobj.etsHeader = _iETS.GetETSHdrEntry(NoteNumber, ref pMsg);
                if (modelvmobj.etsHeader.IsApproved.HasValue)
                {
                    modelvmobj.etsHeader.IsApproveds = modelvmobj.etsHeader.IsApproved == true ? "Yes" : "No";
                }
                else
                {
                    modelvmobj.etsHeader.IsApproveds = "-";
                }
                modelvmobj.etsHeader.ApproveDatestr = modelvmobj.etsHeader.ApproveDatestr != "01/01/0001" ? modelvmobj.etsHeader.ApproveDatestr : "-";

                modelvmobj.etsHeader.ApproveTime = modelvmobj.etsHeader.ApproveTime != null ? modelvmobj.etsHeader.ApproveTime : "-";
                modelvmobj.etsHeader.ApprovedReason = modelvmobj.etsHeader.ApprovedReason != null ? modelvmobj.etsHeader.ApprovedReason : "-";

                if (modelvmobj.etsHeader.IsRatified.HasValue)
                {
                    modelvmobj.etsHeader.IsRatifieds = modelvmobj.etsHeader.IsRatified == true ? "Yes" : "No";
                }
                else
                {
                    modelvmobj.etsHeader.IsRatifieds = "-";
                }
                modelvmobj.etsHeader.RatifiedDatestr = modelvmobj.etsHeader.RatifiedDatestr != "01/01/0001" ? modelvmobj.etsHeader.RatifiedDatestr : "-";

                modelvmobj.etsHeader.RatifiedTime = modelvmobj.etsHeader.RatifiedTime != null ? modelvmobj.etsHeader.RatifiedTime : "-";
                modelvmobj.etsHeader.RatifiedReason = modelvmobj.etsHeader.RatifiedReason != null ? modelvmobj.etsHeader.RatifiedReason : "-";

                modelvmobj.PersonDtls = _iETS.GetETSTravellingPerson(NoteNumber, ref pMsg);
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = CanDelete == 1 ? "Delete" : "View";

            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult Details(ETSHeaderEntryVM modelobj, string Submit)
        {
            string baseUrl = "/Security/ETS/Details?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=" + modelobj.CanDelete + "&CBUID=" + modelobj.CBUID;
            ViewBag.HeaderText = modelobj.HeaderText;
            if (Submit == "Delete")
            {
                if (_iETS.RemoveETSNoteNumber(modelobj.NoteNumber, 0, 1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";

                }
                else { ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.NoteNumber; }

            }
            else if (Submit == "DTD")
            {
                TempData["BackUrl"] = baseUrl;
                return RedirectToAction("TravellingDetailsView", "ETS", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }
            modelobj.etsHeader = _iETS.GetETSHdrEntry(modelobj.NoteNumber, ref pMsg);
            modelobj.PersonDtls = _iETS.GetETSTravellingPerson(modelobj.NoteNumber, ref pMsg);

            return View(modelobj);
        }
        public ActionResult TravellingDetailsView(string NoteNumber, int CBUID)
        {
            ETSTravellingDetailsVM modelvm = new ETSTravellingDetailsVM();
            try
            {
                ViewBag.BackUrl = TempData["BackUrl"] as string;
                modelvm.travDetails = _iETS.GetETSTravellingDetails(NoteNumber, ref pMsg);
                modelvm.dateTour = _iETS.GetETSDateWiseTour(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvm);
        }
        public JsonResult GetETSNZBDetailsforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
    string sSortDir_0, string sSearch)
        {
            List<ETSNoteList> noteList = _iETS.GetETSNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        #region For Approval process
        public JsonResult GetETSNZBApprovalforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<ETSNoteList> noteList = _iETS.GetETSNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 2, ref pMsg);

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
        public ActionResult ETSNoteApproveList()
        {
            TempData["ETSApproveTrav"] = null;
            return View();
        }
        public ActionResult ETSApproveNote(string NoteNumber = null)
        {
            ETSNoteApproveVM modelobj = new ETSNoteApproveVM();
            if (NoteNumber != null)
            {
                if (TempData["ETSApproveTrav"] != null)
                {
                    modelobj = TempData["ETSApproveTrav"] as ETSNoteApproveVM;
                    TempData["ETSApproveTrav"] = modelobj;
                }
                modelobj.Notelist = _iETS.GetETSNoteListToBeApproved(user.CentreCode, 1, ref pMsg);
                modelobj.NoteNumber = modelobj.Notelist.Where(x => x.NoteNumber == NoteNumber).FirstOrDefault().NoteNumber;

            }
            else
            {
                modelobj.Notelist = _iETS.GetETSNoteListToBeApproved(user.CentreCode, 1, ref pMsg);
            }
            return View(modelobj);
        }
        [HttpPost]
        public ActionResult ETSApproveNote(ETSNoteApproveVM modelobj, string Submit = null)
        {

            if (Submit == "btnTravDetails")
            {
                return RedirectToAction("ETSApprovedTravDetails",
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
                if (_iETS.SetETSApprovalData(modelobj.travdetails, ref pMsg))
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
        public ActionResult ETSApprovedTravDetails(string NoteNumber, int CBUID)
        {
            ETSTravellingDetailsVM modelvms = new ETSTravellingDetailsVM();
            ETSHeaderEntryVM result = new ETSHeaderEntryVM();
            List<Employee> objemp = new List<Employee>();
            try
            {
                Employee obhemp = new Employee();
                result.PersonDtls = _iETS.GetETSTravellingPerson(NoteNumber, ref pMsg);
                modelvms.travDetails = _iETS.GetETSTravellingDetails(NoteNumber, ref pMsg);
                modelvms.dateTour = _iETS.GetETSDateWiseTour(NoteNumber, ref pMsg);
                string baseUrl = "/Security/ETS/ETSApproveNote?NoteNumber=" + NoteNumber;
                ViewBag.BackUrl = baseUrl;
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvms);

        }
      
        //public JsonResult GetApprovalTravDetailsforReverseData(string NoteNumber)
        //{
        //    ETSTravellingDetailsVM modelvms = new ETSTravellingDetailsVM();
        //   try{

        //        modelvms.travDetails = _iETS.GetETSTravellingDetails(NoteNumber, ref pMsg);

        //    }
        //    catch (Exception ex) { ex.ToString(); }
            
        //    return Json(modelvms.travDetails, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetETSHdrDetails(string NoteNumber)
        {
            ETSHeaderEntryVM result = new ETSHeaderEntryVM();
            result.NoteNumber = NoteNumber;
            result.etsHeader = _iETS.GetETSHdrEntry(NoteNumber, ref pMsg);
            result.PersonDtls = _iETS.GetETSTravellingPerson(NoteNumber, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeNoName(string NoteNo)
        {
            ETSHeaderEntryVM result = new ETSHeaderEntryVM();
            //List<Employee> objemp = new List<Employee>();
            List<CustomComboOptionsWithString> results = new List<CustomComboOptionsWithString>();
            result.PersonDtls = _iETS.GetETSTravellingPerson(NoteNo, ref pMsg);

            if (result.PersonDtls != null)
            {
                foreach (var item in result.PersonDtls)
                {
                    CustomComboOptionsWithString cmb = new CustomComboOptionsWithString();
                    //Employee emp = new Employee();
                    //emp.EmployeeNonName = item.EmployeeNonName;
                    cmb.ID= item.EmployeeNonName;
                    cmb.DisplayText = item.EmployeeNonName;
                    results.Add(cmb);
                }
            }
            
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetApprovalTravDetails(ETSTravellingDetailsVM model)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            if (model != null)
            {
                ETSNoteApproveVM Travmodel = new ETSNoteApproveVM();
                Travmodel.travdetails.NoteNumber = model.NoteNumber;
                Travmodel.travdetails.VehicleTypeProvided = model.VehicleTypeProvided;
                Travmodel.travdetails.ReasonVehicleProvided = model.ReasonVehicleProvided!=null? model.ReasonVehicleProvided:"NA";
                Travmodel.travdetails.EmployeeNonName = model.EmployeeNonName!=null? model.EmployeeNonName:"NA";
                Travmodel.travdetails.ApprovedReason = "NA";
                Travmodel.travdetails.status = 2;
                if (_iETS.SetETSApprovalData(Travmodel.travdetails, ref pMsg))
                {
                    Travmodel.NoteNumber = model.NoteNumber;
                    Travmodel.btnDisplay = 1;
                    TempData["ETSApproveTrav"] = Travmodel;
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
            ETSHeaderEntryVM modelvmobj = new ETSHeaderEntryVM();
            try
            {
                modelvmobj.etsHeader = _iETS.GetETSHdrEntry(NoteNumber, ref pMsg);
                if (modelvmobj.etsHeader.IsRatified.HasValue)
                 {
                    modelvmobj.etsHeader.IsRatifieds = modelvmobj.etsHeader.IsRatified == true ? "Yes" : "No";
                }
                else
                {
                    modelvmobj.etsHeader.IsRatifieds = "-";
                }
                modelvmobj.etsHeader.RatifiedDatestr = modelvmobj.etsHeader.RatifiedDatestr != "01/01/0001" ? modelvmobj.etsHeader.RatifiedDatestr : "-";
       
                modelvmobj.etsHeader.RatifiedTime = modelvmobj.etsHeader.RatifiedTime != null ? modelvmobj.etsHeader.RatifiedTime : "-";
                modelvmobj.etsHeader.RatifiedReason = modelvmobj.etsHeader.RatifiedReason != null ? modelvmobj.etsHeader.RatifiedReason : "-";
                modelvmobj.PersonDtls = _iETS.GetETSTravellingPerson(NoteNumber, ref pMsg);
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = "Approval";


            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult ApprovalDetails(ETSHeaderEntryVM modelobj, string Submit)
        {
            string baseUrls = "/Security/ETS/ApprovalDetails?NoteNumber=" + modelobj.NoteNumber + "&CanDelete=0&CBUID=0";


            if (Submit == "DTD")
            {
                TempData["ABackUrl"] = baseUrls;
                return RedirectToAction("ApprovalTravellingDetailsView", "ETS", new { NoteNumber = modelobj.NoteNumber, CBUID = modelobj.CBUID });
            }

            return View(modelobj);
        }
        public ActionResult ApprovalTravellingDetailsView(string NoteNumber, int CBUID)
        {
            ETSTravellingDetailsVM modelvm = new ETSTravellingDetailsVM();
            try
            {
                ViewBag.BackUrls = TempData["ABackUrl"] as string;
                modelvm.travDetails = _iETS.GetETSTravellingDetails(NoteNumber, ref pMsg);
                modelvm.dateTour = _iETS.GetETSDateWiseTour(NoteNumber, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvm);
        }

        //public JsonResult GetETSTravellingperson(string Notenumber)
        //{
        //    ETSHeaderEntryVM modelobj = new ETSHeaderEntryVM();

        //    return Json(modelobj, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region For RATIFICATION window
        public JsonResult GetETSRTFCNforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
  string sSortDir_0, string sSearch)
        {
            List<ETSNoteList> noteList = _iETS.GetETSNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 3, ref pMsg);

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
            ETSNoteApproveVM modelobj = new ETSNoteApproveVM();
            try
            {
                if (NoteNumber != null)
                {
                    modelobj.btnDisplay = btnDisplay;
                    modelobj.Notelist = _iETS.GetETSNoteListToBeApproved(user.CentreCode, 2, ref pMsg);
                    if (modelobj.Notelist != null)
                    {
                        modelobj.NoteNumber = modelobj.Notelist.Where(x => x.NoteNumber == NoteNumber).FirstOrDefault().NoteNumber;
                    }
                }
                else
                {
                    modelobj.Notelist = _iETS.GetETSNoteListToBeApproved(user.CentreCode, 2, ref pMsg);
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }

            return View(modelobj);
        }
        [HttpPost]
        public ActionResult RTFNCreate(ETSNoteApproveVM modelobj, string Submit = null)
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
                modelobj.ratified.RatifiedReason = modelobj.RatifiedReason!=null? modelobj.RatifiedReason:"NA";
                modelobj.ratified.status = 1;
                if (_iETS.SetETSRatifiedData(modelobj.ratified, ref pMsg))
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
            ETSTravellingDetailsVM modelvms = new ETSTravellingDetailsVM();
            ETSHeaderEntryVM result = new ETSHeaderEntryVM();
            List<Employee> objemp = new List<Employee>();
            string baseUrl = "";
            try
            {
                Employee obhemp = new Employee();
                // result.PersonDtls = _iETS.GetETSTravellingPerson(NoteNumber, ref pMsg);
                modelvms.travDetails = _iETS.GetETSTravellingDetails(NoteNumber, ref pMsg);
                ViewBag.PublicTrans = modelvms.travDetails.PublicTransports;
                modelvms.dateTour = _iETS.GetETSDateWiseTour(NoteNumber, ref pMsg);
                if (CBUID == 2)
                {
                    baseUrl = "/Security/ETS/RTFNDetailsView?NoteNumber=" + NoteNumber;
                }
                else
                {
                    baseUrl = "/Security/ETS/RTFNCreate?NoteNumber=" + NoteNumber;
                }
                ViewBag.BackUrl = baseUrl;
                ViewBag.CBUID = CBUID;
            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvms);
        }
        [HttpPost]
        public ActionResult RTFNTravellingDetails(ETSTravellingDetailsVM modelobj)
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
            ETSHeaderEntryVM modelvmobj = new ETSHeaderEntryVM();
            try
            {
                modelvmobj.etsHeader = _iETS.GetETSHdrEntry(NoteNumber, ref pMsg);
                modelvmobj.PersonDtls = _iETS.GetETSTravellingPerson(NoteNumber, ref pMsg);
                modelvmobj.CanDelete = CanDelete;// == 1 ? true : false;
                modelvmobj.HeaderText = "Ratification";


            }
            catch (Exception ex) { ex.ToString(); }

            return View(modelvmobj);
        }
        [HttpPost]
        public ActionResult RTFNDetailsView(ETSHeaderEntryVM model)
        {
            if (model.NoteNumber != null)
            {
                return RedirectToAction("RTFNTravellingDetails",
                         new { Area = "Security", NoteNumber = model.NoteNumber, CBUID = 2 });
            }
            return View();
        }
        #endregion
        #region For Common use data
        public JsonResult GetVehicleEligibility(int EmployeeNumber)
        {
            CustomComboOptions result = _master.getVehicleEligibility(EmployeeNumber, ref pMsg);
            if (result == null)
            {
                result = new CustomComboOptions() { ID = 0, DisplayText = "NA" };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDesgCodenName(int empID, int empType)
        {//empType : 2-driver, 1-Others
            return Json(_master.GetDesgCodenName(empID, empType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVehicleTypes(int TypeVal = 0, string PT = null)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();

            EHGMaster master = EHGMaster.GetInstance;
            if (PT == null)
            {
                if (TypeVal == 1 || TypeVal == 2)
                {
                    result = master.VehicleTypes.Where(x => x.ID == 2).ToList();
                }
                else
                {
                    result = master.VehicleTypes.Where(x => x.ID != 3).ToList(); 
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
                    result = master.VehicleTypes.Where(x => x.ID!=1).ToList();
                }
                else
                {
                    result = master.VehicleTypes.Where(x => x.ID == 3).ToList();
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonTypes()
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();

            EHGMaster master = EHGMaster.GetInstance;
            result = master.PersonType;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCategories(int PTval)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            EHGMaster master = EHGMaster.GetInstance;
            if (PTval == 1)
            {
                result = master.TourCategoryForNZB.Where(x => x.ID == 4).ToList();
            }
            else
            {
                long[] ids = { 1, 2, 3, 5 };
                result = master.TourCategoryForNZB.Where(x => ids.Contains(x.ID)).ToList();
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocationsFromTypes(string TypeIDs)
        {
            TypeIDs = TypeIDs.Replace('_', ',');
            //IEnumerable<CustomComboOptions> result = _iCTV.getLocationsFromType(TypeIDs, ref pMsg);
            IEnumerable<LocationMaster> result = _master.GetCentresFromTourCategory(TypeIDs, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            //IEnumerable<CustomComboOptions> result = _iCTV.getLocationsFromType(TypeID, ref pMsg);
            IEnumerable<LocationMaster> result = _master.GetCentresFromTourCategory(TypeID.ToString(), ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public JsonResult getBranchType(int CenterId)
        {
            //IEnumerable<CustomComboOptions> result = _master.getBranchType(CenterId, ref pMsg);
            IEnumerable<LocationMaster> result = _master.GetBranchOfaCentre(CenterId, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getVehicleEligibilityStatement(int EligibleVT, int ProvidedVT)
        {
        
            return Json(_master.getVehicleEligibilityStatement(EligibleVT, ProvidedVT, ref pMsg), JsonRequestBehavior.AllowGet);

        }
        //public JsonResult GetBranchCode(string CId)
        //{
        //    List<CustomComboOptions> result = new List<CustomComboOptions>();
        //    EHGMaster master = EHGMaster.GetInstance;
        //    result = master.BranchCode;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region Private Function for Teamp Data
        private ETSHeaderEntryVM CastETSTempData()
        {
            if (TempData["ETS"] != null)
            {
                model = TempData["ETS"] as ETSHeaderEntryVM;
            }
            else
            {
                model = new ETSHeaderEntryVM();
            }
            TempData["ETS"] = model;
            return model;
        }
        #endregion
    }
}