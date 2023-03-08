using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.BIL;
using CBBW.BLL.IRepository;
using CBBW.BOL.BIL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;

namespace CBBW.Areas.Security.Controllers
{
    public class BILController : Controller
    {
        // GET: Security/BIL
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        IBILRepository _iBIL;
        ITFDRepository _iTFD;
        ICTVRepository _iCTV;
        public BILController(ITFDRepository iTFD,IBILRepository iBIL, ICTVRepository iCTV, IUserRepository iUser, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
                _iBIL = iBIL;
                _iCTV = iCTV;
                _iTFD = iTFD;
                user = iUser.getLoggedInUser();
                ViewBag.LogInUser = user.UserName;
            }
            catch (Exception ex) { pMsg = ex.ToString(); }

        }
        public ActionResult Index()
        {
            TempData["HdrData"] = null;
            TempData["Details"] = null;
            return View();
        }
        public JsonResult GetIndexListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<IndexList> noteList = _iBIL.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        public ActionResult Create()
        {
            TADABillGeneration model = new TADABillGeneration();
            TADABillGeneration Nmodel = new TADABillGeneration();
            try
            {
                ViewBag.RNotelist = _iBIL.GetNoteNumberList(user.CentreCode, 1, ref pMsg);


                if (TempData["HdrData"] != null)
                {
                    Nmodel = _iBIL.getNewNoteNumber(ref pMsg);
                    TADABillGeneration models = TempData["HdrData"] as TADABillGeneration;
                    TempData["HdrData"] = models;
                    model.RefNoteNumber = models.RefNoteNumber;
                    model.NoteNumber = Nmodel.NoteNumber;
                    model.EmpNo = models.EmployeeNo;
                    model.SubmitCount = models.SubmitCount;
                    model.CenterCodeName = user.CentreName;
                    model.TAAmount = models.TAAmount;
                    model.EmployeeCodeName = models.EmployeeCodeName;
                    model.LocalConveyance = models.LocalConveyance;
                    model.Lodging = models.Lodging;
                }
                else
                {
                    model = _iBIL.getNewNoteNumber(ref pMsg);
                }

            }
            catch (Exception ex) { ex.ToString(); }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TADABillGeneration model, string Submit = null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                string baseUrl = "/Security/BIL/Create";
                TempData["Backurl"] = baseUrl;
                TempData["HdrData"] = model;
                ViewBag.RNotelist = _iBIL.GetNoteNumberList(user.CentreCode, 1, ref pMsg);
                if (Submit == "DAD")
                {
                    return RedirectToAction("DeductionsFromDA", "BIL", new { EmployeeNo = model.EmployeeNo, NoteNumber = model.RefNoteNumber });
                }
                else if (Submit == "Save")
                {

                    model.NoteNumber = model.NoteNumber != null ? model.NoteNumber : "NA";
                    model.RefNoteNumber = model.RefNoteNumber != null ? model.RefNoteNumber : "NA";
                    model.EmployeeNo = model.EmployeeNo != 0 ? model.EmployeeNo : 0;
                    model.EmployeeCodeName = model.EmployeeCodeName != null ? model.EmployeeCodeName : "NA";
                    if (model.RefEntryDatestr != null) { model.RefEntryDate = model.RefEntryDate; };
                    model.RefEntryTime = model.RefEntryTime != null ? model.RefEntryTime : "NA";
                    model.PersonTypetxt = model.PersonTypetxt != null ? model.PersonTypetxt : "NA";
                    if (model.PersonTypetxt != null && model.PersonTypetxt != "NA")
                    {
                        string Code = model.PersonTypetxt.Split('/')[0];
                        model.PersonType = Convert.ToInt32(Code);
                    }
                    else
                    {
                        model.PersonType = model.PersonType != 0 ? model.PersonType : 0;
                    }
                    model.CenterCode = user.CentreCode != 0 ? user.CentreCode : 0;
                    model.CenterCodeName = user.CentreCode + "/" + user.CentreName;
                    model.DesigCodeName = model.DesigCodeName != null ? model.DesigCodeName : "NA";
                    if (model.DesigCodeName != null && model.DesigCodeName != "NA")
                    {
                        string Code = model.DesigCodeName.Split('/')[0];
                        model.DesigCode = Convert.ToInt32(Code);
                    }
                    else
                    {
                        model.DesigCode = model.DesigCode != 0 ? model.DesigCode : 0;
                    }


                    model.DAAmount = model.DAAmount != 0 ? model.DAAmount : 0;
                    model.DADeducted = model.DADeducted != 0 ? model.DADeducted : 0;
                    model.EDAllowance = model.EDAllowance != 0 ? model.EDAllowance : 0;
                    model.TAAmount = model.TAAmount != 0 ? model.TAAmount : 0;
                    model.LocalConveyance = model.LocalConveyance != 0 ? model.LocalConveyance : 0;
                    model.Lodging = model.Lodging != 0 ? model.Lodging : 0;
                    model.TotalExpenses = model.TotalExpenses != 0 ? model.TotalExpenses : 0;
                    model.TourFromDateNTime = model.TourFromDateNTime != null ? model.TourFromDateNTime : "NA";
                    model.TourToDateNTime = model.TourToDateNTime != null ? model.TourToDateNTime : "NA";
                    if (model.TourFromDateNTime != null && model.TourFromDateNTime != "NA")
                    {
                        model.TourFromDate = model.TourFromDate;
                    }
                    if (model.TourToDateNTime != null && model.TourToDateNTime != "NA")
                    {
                        model.TourFromDate = model.TourFromDate;
                    }


                    model.NoOfDays = model.NoOfDays != 0 ? model.NoOfDays : 0;
                    model.PurposeOfVisit = model.PurposeOfVisit != null ? model.PurposeOfVisit : "NA";

                    if (_iBIL.SetSetTADABillGeneration(model, ref pMsg))
                    {
                        ViewBag.Msg = "Employee Number " + model.EmployeeCodeName + " Submited Successfully.";
                    }
                    else { ViewBag.ErrMsg = "Updation Failed For Employee Number " + model.EmployeeCodeName; }
                   
                }
                else if (Submit == "Final")
                {
                    if (_iBIL.SetBillGenerationFinalSubmit(model.RefNoteNumber, 1, ref pMsg))
                    { ViewBag.FinalMsg = "Note Number " + model.RefNoteNumber + " Submited Successfully."; }
                    else { ViewBag.ErrMsg = "Updation Failed For Note Number " + model.NoteNumber; }
                }


            }
            catch (Exception ex) { ex.ToString(); }
            return View(model);

        }
        public ActionResult Details(string NoteNumber = null)
        {
            TADABillGeneration model = new TADABillGeneration();
            TADABillGeneration models = new TADABillGeneration();
            if (TempData["Details"] != null)
            {
                models = TempData["Details"] as TADABillGeneration;
                model = _iBIL.GetTADABillGenerationData(models.NoteNumber, "Na", 0, 1, ref pMsg);
            }
            else
            {
                model = _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 1, ref pMsg);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Details(TADABillGeneration models, string Submit = null)
        {
            try
            {
                string baseUrl = "/Security/BIL/Details";
                TempData["Backurl"] = baseUrl;
                TempData["Details"] = models;
                if (Submit == "Delete")
                {
                    if (_iBIL.RemoveBILNoteNumber(models.NoteNumber, 0, 1, ref pMsg))
                    {
                        ViewBag.Msg = "Note Number " + models.NoteNumber + " Deleted Successfully.";

                    }
                    else { ViewBag.ErrMsg = "Updation Failed For Note Number " + models.NoteNumber; }

                }
                else if (Submit == "DAD")
                {
                    return RedirectToAction("DeductionsFromDA", "BIL", new { EmployeeNo = models.EmployeeNo, NoteNumber = models.RefNoteNumber });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View();
        }
        public ActionResult ApprovalIndex()
        {
            return View();
        }
        public JsonResult GetApprovalIndexListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<IndexList> noteList = _iBIL.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 2, ref pMsg);

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

        public ActionResult ApprovalCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ApprovalCreate(TADABillGeneration Model, string Submit = null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (Model.status == 1) {
                if (_iBIL.SetApprovalTADABillGeneration(Model, ref pMsg))
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";

                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }
                }else if (Model.status == 2)
                {

                    if (_iBIL.SetAnPFinalSubmit(1,Model.NoteList, ref pMsg))
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
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovalDetails(string NoteNumber)
        {
            TADABillGeneration modelobj = _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 3, ref pMsg);
            

            return View(modelobj);
        }
        public ActionResult PaymentIndex()
        {
            return View();
        }
        public JsonResult GetPaymentIndexListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
  string sSortDir_0, string sSearch)
        {
            List<IndexList> noteList = _iBIL.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 3, ref pMsg);

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
        public ActionResult PaymentCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PaymentCreate(TADABillGeneration Model, string Submit = null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (Model.status == 3)
                {
                    if (_iBIL.SetApprovalTADABillGeneration(Model, ref pMsg))
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
                else if (Model.status == 2)
                {

                    if (_iBIL.SetAnPFinalSubmit(2, Model.NoteList, ref pMsg))
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


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetDeductionFromData(TADABillGeneration Model, string Submit = null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                Model.status = 1;
                if (Model.status == 1)
                {
                    if (_iBIL.SetDeductionFormDA(Model, ref pMsg))
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
                //else if (Model.TadabollGen.status == 3)
                //{
                //    if (_iBIL.SetApprovalTADABillGeneration(Model.TadabollGen, ref pMsg))
                //    {
                //        result.bResponseBool = true;
                //        result.sResponseString = "Data successfully updated.";

                //    }
                //    else
                //    {
                //        result.bResponseBool = false;
                //        result.sResponseString = pMsg;
                //    }
                //}

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PaymentDetails(string NoteNumber)
        {
            TADABillGeneration modelobj = _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 3, ref pMsg);


            return View(modelobj);
        }


        #region Common Use
        public ActionResult ExpensesDetails(string NoteNumber)
        {
            TADABillGeneration modelobj = _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 3, ref pMsg);
            return View("~/Areas/Security/Views/BIL/_ExpensesDetails.cshtml", modelobj);
        }
        public ActionResult PaymentExpensesDetails(string NoteNumber)
        {
            TADABillGeneration modelobj = _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 3, ref pMsg);
            return View("~/Areas/Security/Views/BIL/_PymtExpensesDetails.cshtml", modelobj);
        }
        public JsonResult GetTADABillGenerationDataHdr(string NoteNumber)
        {

            TADABillGeneration modelobj = _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 1, ref pMsg);
            return Json(modelobj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBILNoteNumberList()
        {
            IEnumerable<CustomOptionsWithString> result;
            result = _iBIL.GetBILNoteNumberList(user.CentreCode, 1, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBILPaymentNoteNumberList()
        {
            IEnumerable<CustomOptionsWithString> result;
            result = _iBIL.GetBILNoteNumberList(user.CentreCode, 2, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VMDeductionsFromDA(int EmployeeNo, string NoteNumber)
        {
            DaDetailsVM model = new DaDetailsVM();
            try
            {
                model.DADetailslist = _iBIL.GetDADetails(EmployeeNo, user.CentreCode, NoteNumber, ref pMsg);
                model.TotalDA = 0;
                model.TotalDaDect = 0;
                model.TotalElg = 0;
                foreach (var item in model.DADetailslist)
                {
                    model.TotalDA = Math.Round(model.TotalDA + item.DAAmount);
                    model.TotalDaDect = Math.Round(model.TotalDaDect + item.DADeducted);
                    model.TotalElg = Math.Round(model.TotalElg + item.EAmount);

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View("~/Areas/Security/Views/BIL/_VDeductionFromDA.cshtml", model);
        }
        public ActionResult VPytmDeductionsFromDA(int EmployeeNo, string RefNoteNumber, string NoteNumber,float ETotalAmount,int status=0)
        {
            DaDetailsVM model = new DaDetailsVM();
            try
            {
                model.DADetailslist = _iBIL.GetDADetails(EmployeeNo, user.CentreCode, RefNoteNumber, ref pMsg);
                model.TotalDA = 0;
                model.TotalDaDect = 0;
                model.TotalElg = 0;
                foreach (var item in model.DADetailslist)
                {
                    model.TotalDA = Math.Round(model.TotalDA + item.DAAmount);
                    model.TotalDaDect = Math.Round(model.TotalDaDect + item.DADeducted);
                    model.TotalElg = Math.Round(model.TotalElg + item.EAmount);

                }

                model.TadabollGen= _iBIL.GetTADABillGenerationData(NoteNumber, "Na", 0, 3, ref pMsg);
               
                model.TadabollGen.RequisitionDateMin = DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd");
                model.TadabollGen.RequisitionDateMax = DateTime.Today.ToString("yyyy-MM-dd");
                model.Deptlist = _iTFD.GetENTConcernDeptList(NoteNumber, user.CentreCode,ref pMsg);
                model.TadabollGen.ETotalAmount = ETotalAmount;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            if (status == 1)
            {
                return View("~/Areas/Security/Views/BIL/_VPytmDeductionFromDAView.cshtml", model);
            }
            else { 
            return View("~/Areas/Security/Views/BIL/_VPytmDeductionFromDA.cshtml", model);
            }
        }
        public ActionResult PytmTravellingDetails(int EmployeeNo, string RefNoteNumber)
        {
            DaDetailsVM model = new DaDetailsVM();
            try
            {
                model.TravDetails = _iBIL.GetTraveelingDetails(EmployeeNo, user.CentreCode, RefNoteNumber, ref pMsg);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View("~/Areas/Security/Views/BIL/_TravellingDetails.cshtml", model);
        }
        public JsonResult GetDeptWiseEmployee(int DeptId)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iBIL.GetDeptWiseEmployeeList(DeptId, user.CentreCode, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeductionsFromDA(int EmployeeNo, string NoteNumber)
        {
            DaDetailsVM model = new DaDetailsVM();
            TADABillGeneration tamodel = new TADABillGeneration();
            try
            {
                if (TempData["HdrData"] != null)
                {
                    tamodel = TempData["HdrData"] as TADABillGeneration;
                    tamodel.SubmitCount = 1;
                    TempData["HdrData"] = tamodel;
                }
                else if (TempData["Details"] != null)
                {
                    tamodel = TempData["Details"] as TADABillGeneration;
                    tamodel.SubmitCount = 1;
                    TempData["Details"] = tamodel;
                }
                ViewBag.BackUrl = TempData["Backurl"];
                //EmployeeNo = 1712;
                model.DADetailslist = _iBIL.GetDADetails(EmployeeNo, user.CentreCode, NoteNumber, ref pMsg);
                model.TotalDA = 0;
                model.TotalDaDect = 0;
                model.TotalElg = 0;
                foreach (var item in model.DADetailslist)
                {
                    model.TotalDA = Math.Round(model.TotalDA + item.DAAmount);
                    model.TotalDaDect = Math.Round(model.TotalDaDect + item.DADeducted);
                    model.TotalElg = Math.Round(model.TotalElg + item.EAmount);


                }

            }
            catch (Exception ex) { ex.ToString(); }
            return View(model);
        }
        public JsonResult GetNoteHdr(string Notenumber)
        {
            TADABillGeneration obj = new TADABillGeneration();
            obj = _iBIL.GetNoteHdr(Notenumber, ref pMsg);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeList(string Notenumber)
        {
            IEnumerable<CustomCheckBoxOption> result;
            result = _iBIL.GetEmployeeList(Notenumber, user.CentreCode, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetTourDate(string Notenumber)
        //{
        //    IEnumerable<CustomComboOptions> result;
        //    result = _iBIL.GetEmployeeList(Notenumber, user.CentreCode, ref pMsg);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetTAdARuleData(int EmployeeNumber, string NoteNumber)
        {

            TADARuleData Model = new TADARuleData();
            TADABillGeneration models = new TADABillGeneration();
            models = _iBIL.GetTADABillGenerationData("Na", NoteNumber, EmployeeNumber, 2, ref pMsg);
            if (models.NoteNumber == null)
            {
                Model = _iBIL.GetTAdARuleData(EmployeeNumber, user.CentreCode, NoteNumber, ref pMsg);
                Model.status = 1;
                models = _iBIL.getNewNoteNumber(ref pMsg);
                Model.NoteNumber = models.NoteNumber;
                return Json(Model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Model.NoteNumber = models.NoteNumber;
                Model.PersonType = models.PersonTypetxt;
                Model.DesginationCodeName = models.DesigCodeName;
                Model.DAAmount = Convert.ToInt32(models.DAAmount);
                Model.DADeducted = Convert.ToInt32(models.DADeducted);
                Model.EAmount = Convert.ToInt32(models.EDAllowance);
                Model.ActualTourInDatestr = models.TourFromDateNTime;
                Model.ActualTourOutDatestr = models.TourToDateNTime;
                Model.TotalNoOfDays = models.NoOfDays;
                Model.PurposeOfVisit = models.PurposeOfVisit;
                Model.ActualTourInDate = models.TourFromDate;
                Model.ActualTourOutDate = models.TourToDate;
                Model.TAAmount = models.TAAmount;
                Model.LocalConveyance = models.LocalConveyance;
                Model.Lodging = models.Lodging;
                Model.TotalExpenses = models.TotalExpenses;

                Model.status = 2;
                return Json(Model, JsonRequestBehavior.AllowGet);
            }




        }
        #endregion
    }
}