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


        public ActionResult Index()
        {
            TempData["ETS"] = null;
            TempData["ETSData"] = null;


            return View();
        }
        public ActionResult Create()
        {
            ETSHeaderEntryVM model = new ETSHeaderEntryVM();
            try
            {
                if (TempData["ETS"] != null && TempData["ETSData"] != null)
                {

                    model = TempData["ETS"] as ETSHeaderEntryVM;
                    modelTrav = TempData["ETSData"] as ETSTravellingDetailsVM;
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

        public JsonResult GetVehicleEligibility(int EmployeeNumber)
        {
            return Json(_master.getVehicleEligibility(EmployeeNumber, ref pMsg), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTravelingPersonDetails(ETSHeaderEntryVM modelvm)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                model = CastETSTempData();
                model.etsHeader.NoteNumber = modelvm.NoteNumber;
                model.etsHeader.AttachFile = modelvm.AttachFile;
                model.etsHeader.CenterCodeName = modelvm.CenterCodeName;
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
        public ActionResult TravellingDetails()
        {
            ETSTravellingDetailsVM modeltravvm = new ETSTravellingDetailsVM();
            try
            {
                if (TempData["ETS"] != null)
                {
                    model = TempData["ETS"] as ETSHeaderEntryVM;
                    TempData["ETS"] = model;
                    modeltravvm.PersonType = model.PersonDtls.Select(o => o.PersonType).Max();
                    modeltravvm.NoteNumber = model.NoteNumber;
                    modeltravvm.AttachFile = model.AttachFile;
                    modeltravvm.CenterCodenName = model.CenterCodeName;
                    modeltravvm.TodateStr = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
                    modeltravvm.FromdateStr = DateTime.Today.ToString("yyyy-MM-dd");

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            return View(modeltravvm);
        }

        public ActionResult Details(string NoteNumber, int CanDelete, int CBUID = 0)
        {
            ETSHeaderEntryVM modelvmobj = new ETSHeaderEntryVM();
            try
            {
                modelvmobj.etsHeader = _iETS.GetETSHdrEntry(NoteNumber, ref pMsg);
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
            string baseUrl = "/Security/ETS/Details?NoteNumber="+ modelobj.NoteNumber +"&CanDelete=" + modelobj.CanDelete + "&CBUID=" + modelobj.CBUID;
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

        public ActionResult TravellingDetailsView(string NoteNumber,int CBUID)
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
            List<ETSNoteList> noteList = _iETS.GetETSNZBDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, ref pMsg);

            var result = new
            {
                iTotalRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = noteList.Count(),
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVehicleTypes(int TypeVal)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();

            EHGMaster master = EHGMaster.GetInstance;
            if (TypeVal == 3 || TypeVal == 4)
            {
                result = master.VehicleTypes;
            }
            else
            {
                result = master.VehicleTypes.Where(x => x.ID == 2).ToList();
            }

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
            IEnumerable<CustomComboOptions> result = _iCTV.getLocationsFromType(TypeIDs, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetLocationsFromType(int TypeID)
        {
            IEnumerable<CustomComboOptions> result = _iCTV.getLocationsFromType(TypeID, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public JsonResult getBranchType(int CenterId)
        {
            IEnumerable<CustomComboOptions> result = _master.getBranchType(CenterId, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetBranchCode(string CId)
        //{
        //    List<CustomComboOptions> result = new List<CustomComboOptions>();
        //    EHGMaster master = EHGMaster.GetInstance;
        //    result = master.BranchCode;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

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