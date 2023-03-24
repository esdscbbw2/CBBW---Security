using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.TFD;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.TFD;

namespace CBBW.Areas.Security.Controllers
{
    public class TFDController : Controller
    {
        // GET: Security/TFD
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        ITFDRepository _iTFD;
        ICTVRepository _iCTV;
        TFDHdrVM modelhdrvm;
        TourFeedBackDetailsVM TourFBVM;

        public TFDController(ITFDRepository iTFD, ICTVRepository iCTV, IUserRepository iUser, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
                _iTFD = iTFD;
                _iCTV = iCTV;
                user = iUser.getLoggedInUser();
                ViewBag.LogInUser = user.UserName;
            }
            catch (Exception ex) { pMsg = ex.ToString(); }

        }
        public ActionResult Index()
        {
            TempData["TFDDetails"] = null;
            TempData["HdrTFD"] = null;
            TempData["BtnSubmit"] = null;
            return View();
        }
        public JsonResult GetTFDDetailsforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<TFDNoteList> noteList = _iTFD.GetTFDDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        [HttpGet]
        public ActionResult Create(string NoteNumber = null, string RefNoteNumber = null)

        {
            TFDHdrVM model = new TFDHdrVM();
            try
            
            
            {
                TFDHdrVM Vmhdr = TempData["HdrTFD"] as TFDHdrVM;
                if (NoteNumber != null)
                {
                   
                    model.tfdHdr.NoteNumber = NoteNumber;
                    model.Notelist = _iTFD.GetNoteNumberList(user.CentreCode, 1, ref pMsg);
                   // model.tfdHdr.AuthEmployeeCode = TourFBVM.EmployeeNo;
                    if (RefNoteNumber != "")
                    {
                        
                        model.tfdHdr.RefNoteNumber = Vmhdr.NoteNo.Trim();
                        model.tfdHdr.NoteNumber = NoteNumber;
                        model.tfdHdr.AuthEmployeeCode = Vmhdr.tfdHdr.AuthEmployeeCode;
                        model.NoteNo = RefNoteNumber.Trim();
                        TempData["HdrTFD"] = Vmhdr;
                    }

                    if (TempData["BtnSubmit"] != null)
                    {
                        model.submitcount = 1;
                    }
                    else
                    {
                        model.submitcount = 0;
                    }
                }
                else
                {
                    //TFDHdrVM Vmhdr = TempData["HdrTFD"] as TFDHdrVM;
                    if (TempData["TFDDetails"] != null)
                    {
                        TourFBVM = TempData["TFDDetails"] as TourFeedBackDetailsVM;
                        model.Notelist = _iTFD.GetNoteNumberList(user.CentreCode, 1, ref pMsg);
                        //model.NoteNumber = model.Notelist.Where(x => x.NoteNumber == TourFBVM.RefNoteNumber).FirstOrDefault().NoteNumber;
                        model.NoteNo = Vmhdr.NoteNo;
                        model.tfdHdr.NoteNumber = Vmhdr.tfdHdr.NoteNumber;
                        model.tfdHdr.RefNoteNumber = Vmhdr.tfdHdr.RefNoteNumber;
                        model.submitcount = TourFBVM.submitcount;
                        model.tfdHdr.AuthEmployeeCode = Vmhdr.tfdHdr.AuthEmployeeCode;
                        TempData["TFDDetails"] = TourFBVM;
                    }
                    else
                    {
                        model.Notelist = _iTFD.GetNoteNumberList(user.CentreCode, 1, ref pMsg);
                        model.tfdHdr = _iTFD.getNewTFDNoteNumber(ref pMsg);
                        TempData["NewNoteNo"] = model.tfdHdr.NoteNumber;
                       
                    }
                    TempData["HdrTFD"] = Vmhdr;
                }
            }
            catch (Exception ex) { ex.ToString(); }

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TFDHdr models, TFDHdrVM hdrvm, string Submit = null)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                TempData["HdrTFD"] = null;
               // hdrvm.tfdHdr.NoteNumber = TempData["NewNoteNo"] as string;
                TempData["HdrTFD"] = hdrvm;
                string baseUrl = "/Security/TFD/Create?NoteNumber=" + hdrvm.tfdHdr.NoteNumber + "&RefNoteNumber=" + hdrvm.NoteNo;
                TempData["Backurl"] = baseUrl;
                if (Submit == "FBD")
                {
      
                    return RedirectToAction("TourFeedBackDetails", "TFD", new { NoteNumber = hdrvm.tfdHdr.NoteNumber, RefNoteNumber = hdrvm.NoteNo });
                }
                else
                {
                    models.UserID = user.EmployeeNumber;
                    models.UserName = user.UserName;
                    models.CenterCode = user.CentreCode;
                    models.CenterCodeName = user.CentreCode + "/" + user.CentreName;
                    models.status = 1;
                    if (_iTFD.SetTFDetailsFinalSubmit(models, ref pMsg))
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
            catch (Exception ex) { ex.ToString(); }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTFDHeaderData(string NoteNumber)
        {
            TFDHdrVM result = new TFDHdrVM();
            result.tfdHdr = _iTFD.GetTFDHeaderData(NoteNumber, user.CentreCode, 1, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetENTAuthEmployeeList(string Notenumber)
        {
            IEnumerable<CustomCheckBoxOption> result;
            result = _iTFD.GetENTAuthEmployeeList(Notenumber, user.CentreCode, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TPView(string NoteNumber)
        {
            IEnumerable<TFDTravellingPerson> modelobj = _iTFD.GetENTTravellingPerson(NoteNumber, user.CentreCode, 1, ref pMsg);
            List<int> persons = modelobj.Select(x => x.PersonID).Distinct().ToList();
            TempData["Persons"] = persons;
            return View("~/Areas/Security/Views/TFD/_TravellingPersonDetails.cshtml", modelobj);
        }
        public ActionResult ApprovalDateWiseTourView(string NoteNumbers,  int EmployeeNo = 0)
        {
            string URl = "~/Areas/Security/Views/TFD/_DateWiseTourDataApproval.cshtml";
            int status =2;
            List<int> persons = TempData["Persons"] as List<int>;
            IEnumerable<TFDDateWiseTourData> modelobj = _iTFD.GetENTDateWiseTourData(NoteNumbers, 0, EmployeeNo, 0, status, ref pMsg);
            modelobj=modelobj.Where(x => persons.Contains(x.PersonID));

            return View(URl, modelobj);
        }
        public ActionResult DateWiseTourView(string NoteNumbers, int PersonType=0, int EmployeeNo=0, int PersonCentre=0, string ApprovalTime=null)
        {
            int status = EmployeeNo == 0?2:1;
            string urlDetails = "~/Areas/Security/Views/TFD/_DateWiseTourDataDetails.cshtml";
            string urlapproval = "~/Areas/Security/Views/TFD/_DateWiseTourDataApproval.cshtml";
            string URl = status == 2 ? urlapproval : urlDetails;
            IEnumerable<TFDDateWiseTourData> modelobj;
            if (ApprovalTime == null || ApprovalTime =="") { 
           modelobj = _iTFD.GetENTDateWiseTourData(NoteNumbers, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
            }
            else
            {
                modelobj = _iTFD.GetTFDDateWiseTourData(NoteNumbers, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
            }
            return View(URl, modelobj);
        }
        public ActionResult ViewDateWiseTourView(string NoteNumbers, int PersonType = 0, int EmployeeNo = 0, int PersonCentre = 0)
        {
            int status = EmployeeNo == 0 ? 2 : 1;
            string urlDetails = "~/Areas/Security/Views/TFD/_DateWiseTourDataDetails.cshtml";
            string URl = urlDetails;
            IEnumerable<TFDDateWiseTourData> modelobj = _iTFD.GetTFDDateWiseTourData(NoteNumbers, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
            return View(URl, modelobj);
        }
        [HttpGet]
        public ActionResult TourFeedBackDetails(string NoteNumber, string RefNoteNumber)
        {

            TourFeedBackDetailsVM obj = new TourFeedBackDetailsVM();
            obj.NoteNumber = NoteNumber;
            obj.RefNoteNumber = RefNoteNumber;
            ViewBag.BackUrl = TempData["Backurl"];
            ViewBag.BtnClear = "/Security/TFD/TourFeedBackDetails?NoteNumber=" + NoteNumber + "&RefNoteNumber=" + RefNoteNumber;

            return View(obj);
        }
        [HttpPost]
        public ActionResult SetTourFeedBackDetails(TourFeedBackDetailsVM fbmodel, string NoteNumber)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {

                if (_iTFD.SetTFDFeedBackDetails(fbmodel.NoteNumber, fbmodel.tfdfbdetails, ref pMsg))
                {
                    fbmodel.submitcount = 1;
                    TempData["BtnSubmit"] = 1;
                    TempData["TFDDetails"] = fbmodel;
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";

                }
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = pMsg;
                }

            }
            catch (Exception ex) { ex.ToString(); }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFDReverseData(string NoteNumber, int CenterCode = 0, int status = 0)
        {

            CustomAjaxResponse result = new CustomAjaxResponse();
            TourFeedBackDetailsVM fbmodelvm = new TourFeedBackDetailsVM();
            TourFeedBackDetailsVM fbmodel = new TourFeedBackDetailsVM();
            try
            {
                if (TempData["TFDDetails"] != null)
                {
                    fbmodelvm = TempData["TFDDetails"] as TourFeedBackDetailsVM;
                    TempData["TFDDetails"] = fbmodelvm;
                    fbmodel.NoteNumber = fbmodelvm.NoteNumber;
                    fbmodel.RefNoteNumber = fbmodelvm.RefNoteNumber;
                    fbmodel.submitcount = fbmodelvm.submitcount;
                    fbmodel.tfdfbdetails = _iTFD.GetTFDTourFeedBackDetails(fbmodelvm.NoteNumber, user.CentreCode, 1, ref pMsg);
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return Json(fbmodel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(string NoteNumber, int CanDelete, int CBUID = 0)
        {
            TFDHdrVM modelvm = new TFDHdrVM();
            modelvm.CanDelete = CanDelete;
            modelvm.CBUID = CBUID;
            modelvm.tfdHdr = _iTFD.GetTFDHeaderDetails(NoteNumber, user.CentreCode, 1, ref pMsg);
          
            return View(modelvm);
        }
        [HttpPost]
        public ActionResult Details(TFDHdrVM modelobj, string Submit)
        {
            string baseUrl = "/Security/TFD/Details?NoteNumber=" + modelobj.tfdHdr.NoteNumber + "&CanDelete=" + modelobj.CanDelete + "&CBUID=" + modelobj.CBUID;

            if (Submit == "Delete")
            {
                if (_iTFD.RemoveTFDNoteNumber(modelobj.tfdHdr.NoteNumber, 0, 1, ref pMsg))
                {
                    ViewBag.Msg = "Note Number " + modelobj.NoteNumber + " Deleted Successfully.";

                }
                else { ViewBag.ErrMsg = "Updation Failed For Note Number " + modelobj.tfdHdr.NoteNumber; }

            }
            else if (Submit == "FBD")
            {
                TempData["BackUrl"] = baseUrl;
                return RedirectToAction("TourFeedBackDetailsView", "TFD", new { NoteNumber = modelobj.tfdHdr.NoteNumber, CBUID = modelobj.CBUID, ApprovalTime = modelobj.tfdHdr.ApprovalTime });
            }

            return View(modelobj);
        }
        public ActionResult TourFeedBackDetailsView(string NoteNumber, int CBUID = 0, string ApprovalTime=null)
        {
            ViewBag.BackUrl = TempData["BackUrl"];
            TourFeedBackDetailsVM fbmodel = new TourFeedBackDetailsVM();
            fbmodel.NoteNumber = NoteNumber;
            fbmodel.ApprovalTime = ApprovalTime;
            fbmodel.tfdfbdetails = _iTFD.GetTFDTourFeedBackDetails(NoteNumber, user.CentreCode, 1, ref pMsg);
            return View(fbmodel);
        }
        public ActionResult ApprovalIndex()
        {

            return View();
        }
        public JsonResult GetApprovalforListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<TFDNoteList> noteList = _iTFD.GetTFDDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 2, ref pMsg);

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

        [HttpGet]
        public ActionResult ApprovalCreate(string NoteNumber="")
        {
            TFDHdrVM model = new TFDHdrVM();
            try
            {
                model.Notelist = _iTFD.GetNoteNumberList(user.CentreCode, 2, ref pMsg);
                if (NoteNumber != "")
                {
                    if(TempData["TFDApproval"]!=null)
                    {
                        model.submitcount = 1;
                    }
                    model.btnEnable = 1;
                    model.tfdHdr.NoteNumber = model.Notelist.Where(x => x.NoteNumber == NoteNumber).FirstOrDefault().NoteNumber;
                }
                
                
            }
            catch(Exception ex) { ex.ToString(); }
            return View(model);
        }
        [HttpPost]
        public ActionResult ApprovalCreate(TFDHdrVM model, string Submit=null)
        {
            
            try
            {
                string baseUrl = "/Security/TFD/ApprovalCreate?NoteNumber=" + model.NoteNo;
                model.submitcount = 1;
                TempData["Backurl"] = baseUrl;
                TempData["AppEnty"] = model;
                if (Submit == "HCVD")
                {
                    return RedirectToAction("HomeCenterVisitingDetails", "TFD", new { NoteNumber = model.tfdHdr.RefNoteNumber, EmployeeNo = model.EmployeeNo});
                }
                else if (Submit == "TFBD")
                {
                    return RedirectToAction("TourFBDetailsApproval", "TFD", new { NoteNumber = model.tfdHdr.NoteNumber});
                }

            }
            catch (Exception ex) { ex.ToString(); }
            return View(model);
        }

        public ActionResult HomeCenterVisitingDetails(string NoteNumber, int EmployeeNo)
        {
            IEnumerable<TFDDateWiseTourData> modelobj=null;
            try
            {
                ViewBag.BackUrl = TempData["Backurl"];
               modelobj = _iTFD.GetENTDateWiseTourData(NoteNumber, 0, EmployeeNo, 0,3, ref pMsg);
            }
            catch(Exception ex) { ex.ToString(); }
            return View(modelobj);
        }
        public ActionResult TourFBDetailsApproval(string NoteNumber)
        {
            TourFeedBackDetailsVM objvm = new TourFeedBackDetailsVM();
            try
            {
                ViewBag.BackUrl = TempData["Backurl"];
                objvm.NoteNumber = NoteNumber;
               // objvm.tfdfbdetails = _iTFD.GetTFDTourFeedBackDetails(NoteNumber, user.CentreCode, 2, ref pMsg);
               // objvm.ConDeptList= _iTFD.GetENTConcernDeptList(NoteNumber, user.CentreCode, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }
            return View(objvm);
        }

        [HttpPost]
        public ActionResult SetApprovalFBDetails(TourFeedBackDetailsVM modelvm)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
              
                    if (modelvm != null)
                    {
                    if (_iTFD.SetTFDFeedBackApproval(modelvm.NoteNumber, modelvm.tfdfApprovalbdetails, ref pMsg))
                    {
                        modelvm.submitcount = 1;
                        TempData["TFDApproval"] = modelvm;

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
        public ActionResult SetFinalApprovalSubmit(TFDHdrVM fbmodel, string NoteNumber)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                fbmodel.IsApprove = fbmodel.IsApproves == 1 ? true : false;
                fbmodel.ApproveReason = fbmodel.ApproveReason != null ? fbmodel.ApproveReason : "NA";
                if (_iTFD.SetTFDDateWiseTourData(fbmodel.NoteNumber, fbmodel.IsApprove, fbmodel.ApproveReason, fbmodel.TFdDateWise, ref pMsg))
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
            catch (Exception ex) { ex.ToString(); }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovalDetails(string NoteNumber, int CanDelete=0, int CBUID = 0)
        {
            TFDHdrVM modelvm = new TFDHdrVM();
            modelvm.CanDelete = CanDelete;
            modelvm.CBUID = CBUID;
            modelvm.tfdHdr = _iTFD.GetTFDHeaderDetails(NoteNumber, user.CentreCode, 1, ref pMsg);
            return View(modelvm);
        }
        [HttpPost]
        public ActionResult ApprovalDetails(TFDHdrVM model, string Submit = null)
        {

            try
            {
                string baseUrl = "/Security/TFD/ApprovalDetails?NoteNumber=" + model.tfdHdr.NoteNumber;
                model.submitcount = 1;
                TempData["Backurl"] = baseUrl;
                TempData["AppEnty"] = model;
                if (Submit == "HCVD")
                {
                    return RedirectToAction("HomeCenterVisitingDetails", "TFD", new { NoteNumber = model.tfdHdr.RefNoteNumber, EmployeeNo = model.EmployeeNo });
                }
                else if (Submit == "TFBD")
                {
                    return RedirectToAction("TourFeedBackDetailsView", "TFD", new { NoteNumber = model.tfdHdr.NoteNumber });
                }

            }
            catch (Exception ex) { ex.ToString(); }
            return View(model);
        }
        #region Common Use
        public JsonResult GetFbDetails(string NoteNumber)
        {
            IEnumerable<TFDTourFeedBackDetails> result=null;
            result = _iTFD.GetTFDTourFeedBackDetails(NoteNumber, user.CentreCode, 2, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetENTConcernDeptList(string Notenumber)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iTFD.GetENTConcernDeptList(Notenumber, user.CentreCode, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTFDHdrData(string NoteNumber)
        {
            TFDHdrVM modelvm = new TFDHdrVM();
            modelvm.tfdHdr = _iTFD.GetTFDHeaderDetails(NoteNumber, user.CentreCode, 1, ref pMsg);
           
            return Json(modelvm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCategories(int PTval = 0,string NoteNumber=null)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            EHGMaster master = EHGMaster.GetInstance;
            if (NoteNumber != null)
            {
                   var TourCat= _iTFD.GetENTTourCategroy(NoteNumber, ref pMsg);
                   long[] Tourids = !string.IsNullOrEmpty(TourCat) ? TourCat.Split(',').Select(long.Parse).ToArray() : new long[] { };
                   result = master.TourCategoryForNZB.Where(x => Tourids.Contains(x.ID)).ToList();
            }
            else {
                long[] ids = { 1, 2, 3 };
                result = master.TourCategoryForNZB.Where(x => ids.Contains(x.ID)).ToList();
            }
          
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocationsFromTypes(string TypeIDs,string NoteNumber=null)
        {
            IEnumerable<CustomComboOptions> result=null;
            if (NoteNumber != null)
            {
                IEnumerable<TFDDateWiseTourData> modelobj = _iTFD.GetENTDateWiseTourData(NoteNumber, 0, 0, 0, 2, ref pMsg);
                var LocationsCode = modelobj.Select(x=>x.LocationsCode).FirstOrDefault();
                long[] Locids = !string.IsNullOrEmpty(LocationsCode) ? LocationsCode.Split(',').Select(long.Parse).ToArray() : new long[] { };
                var locatioins = _iCTV.getLocationsFromType(TypeIDs, ref pMsg);
                result = locatioins.Where(x => Locids.Contains(x.ID)).ToList();

            }
            else
            {
               result = _iCTV.getLocationsFromType(TypeIDs, ref pMsg);
            }
            

            return Json(result, JsonRequestBehavior.AllowGet);


        }
        #endregion
    }
}