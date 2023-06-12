using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.GVMR;

namespace CBBW.Areas.Security.Controllers
{
    public class GVMRController : Controller
    {
        UserInfo user;
        string pMsg;
        IGVMRRepository _IGVMR;
        ICTVRepository _ICTV;
        // GET: Security/GVMR
        public GVMRController(IGVMRRepository GVMR, ICTVRepository ICTV, IUserRepository iUser)
        {
            try
            {
                _IGVMR = GVMR;
                _ICTV = ICTV;
                pMsg = "";
                //iUser.LogIn("praveen", ref pMsg);
                user = iUser.getLoggedInUser();
                ViewBag.LogInUser = user.UserName;
            }
            catch (Exception ex) { pMsg = ex.ToString(); }
        }
        public ActionResult Index()
        {
           
                ViewBag.CenterCode = user.CentreCode;
            return View();
        }
        public ActionResult CenterCodeWiseCreate()
        {
            GVMRDetailsVM gvmrvm = new GVMRDetailsVM();
            gvmrvm.listnotenumber = _IGVMR.GetNoteNumbers(user.CentreCode, 1, ref pMsg);
            gvmrvm.CenterName = user.CentreCode + "/" + user.CentreName;
            return View(gvmrvm);
        }
        public ActionResult Create()
        {
            GVMRDetailsVM gvmrvm = new GVMRDetailsVM();
            gvmrvm.listnotenumber = _IGVMR.GetNoteNumbers(user.CentreCode,3, ref pMsg);
            gvmrvm.CenterName = user.CentreCode + "/" + user.CentreName;
            gvmrvm.CenterCode = user.CentreCode;
            return View(gvmrvm);
        }
        [HttpPost]
        public ActionResult Create(GVMRDetailsVM models)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                GVMRDataSave gvmrsave = new GVMRDataSave();
                foreach (var item in models.gvmrdatasave)
                {
                    gvmrsave.Gvmrid = item.Gvmrid;
                    gvmrsave.NoteNo = item.NoteNo;
                    gvmrsave.ActualInRFIDCard = item.ActualInRFIDCard;
                    gvmrsave.ActualTripInDate = item.ActualTripInDate;
                    gvmrsave.ActualTripInTime = item.ActualTripInTime != null ? item.ActualTripInTime : "NA";
                    gvmrsave.ActualTripInKM = item.ActualTripInKM > 0 ? item.ActualTripInKM : 0;
                    gvmrsave.ActualOutRFIDCard = item.ActualOutRFIDCard;
                    gvmrsave.ActualTripOutDate = item.ActualTripOutDate;
                    gvmrsave.ActualTripOutTime = item.ActualTripOutTime != null ? item.ActualTripOutTime : "NA";
                    gvmrsave.ActualTripOutKM = item.ActualTripOutKM > 0 ? item.ActualTripOutKM : 0;
                    gvmrsave.Remark = item.Remark != null ? item.Remark : "NA";
                    gvmrsave.CenterCode = user.CentreCode;
                }
                if (_IGVMR.setGVMRDetails(gvmrsave, ref pMsg))
                {result.bResponseBool = true;
                 result.sResponseString = "Data successfully updated.";
                }else
                {result.bResponseBool = false;
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateMainLocation(GVMRDetailsVM models)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                List<GVMRDataSave> listsave = new List<GVMRDataSave>();
                foreach (var item in models.gvmrdatasave)
                {
                    GVMRDataSave gvmrsave = new GVMRDataSave();
                    gvmrsave.Gvmrid = item.Gvmrid;
                    gvmrsave.NoteNo = item.NoteNo;
                    gvmrsave.ActualInRFIDCard = item.ActualInRFIDCard;
                    if (item.ActualTripInDate.Year != 0001) { gvmrsave.ActualTripInDate = item.ActualTripInDate; }
                    gvmrsave.ActualTripInTime = item.ActualTripInTime != null ? item.ActualTripInTime : null;
                    gvmrsave.ActualTripInKM = item.ActualTripInKM > 0 ? item.ActualTripInKM : 0;
                    gvmrsave.ActualOutRFIDCard = item.ActualOutRFIDCard;
                    if (item.ActualTripOutDate.Year != 0001) { gvmrsave.ActualTripOutDate = item.ActualTripOutDate; }
                    gvmrsave.ActualTripOutTime = item.ActualTripOutTime != null ? item.ActualTripOutTime : null;
                    gvmrsave.ActualTripOutKM = item.ActualTripOutKM > 0 ? item.ActualTripOutKM : 0;
                    gvmrsave.Remark = item.Remark != null ? item.Remark : "NA";
                    gvmrsave.CenterCode = user.CentreCode;
                    listsave.Add(gvmrsave);
                }
                if (_IGVMR.SetGVMRDetailsV2(listsave, ref pMsg))
                { result.bResponseBool = true;
                  result.sResponseString = "Data successfully updated.";
                }else
                { result.bResponseBool = false;}
            }
            catch (Exception ex)
            { pMsg = ex.ToString();}
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGVMRDetails(string NoteNumber)
        {
            GVMRHeader gvmrvm = new GVMRHeader();
            try
            {
                gvmrvm = _IGVMR.GetGVMRDetails(NoteNumber, user.CentreCode == 13 ? 0 : user.CentreCode, ref pMsg);
                //if (gvmrvm != null)
                //{
                //    if (Convert.ToBoolean(gvmrvm.gvmrDetailslist.Find(x => x.ActualTripInDate.Year == 0001).ActualTripInDate.Year))
                //    {
                //        gvmrvm.gvmrDetailslist = null;
                //    }
                //}
                //return Json(_IGVMR.GetGVMRDetailsWithPunchingDetails(NoteNumber, user.CentreCode, ref pMsg), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { 
                pMsg = ex.ToString();
            }
            return Json(gvmrvm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCenterWiseGVMRDetails(string NoteNumber)
        {
            return Json(_IGVMR.GetCenterWiseGVMRDetails(NoteNumber, user.CentreCode, ref pMsg), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
         string sSortDir_0, string sSearch)
        {
            List<GVMRNoteList> noteList = _IGVMR.getGVMRDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, ref pMsg);
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
        public ActionResult Details(string NoteNumber)
        {
            GVMRHeader model = new GVMRHeader();
            model = _IGVMR.getGVMRDetailsForView(NoteNumber, user.CentreCode == 13 ? 0 : user.CentreCode, ref pMsg);
            model.LocationName = user.CentreCode + "/" + user.CentreName;
            //var models = model.gvmrDetailslist.Where(x => x.NoteNo == NoteNumber).OrderByDescending(x => x.NoteNo).FirstOrDefault();
            //model.NoteNo = models.NoteNo;
            //model.VehicleNo = models.VehicleNo;
            //model.VehicleType = models.VehicleType;
            //model.DriverNo = models.DriverNo;
            //model.DriverName = models.DriverNo + "/" + models.DriverName;
            //model.ModelName = models.ModelName;
            //model.MonthYear = models.MonthYear;
            //model.LocationName = models.LocationName;
            //model.EntryDateDisplay = models.EntryDateDisplay;
            //model.EntryTime = models.EntryTime;
            return View(model);
        }
        //public JsonResult GetGVMRDetailsForView(string NoteNumber)
        //{
        //    GVMRDetailsVM model = new GVMRDetailsVM();
        //    model = _IGVMR.getGVMRDetailsForView(NoteNumber, user.CentreCode, ref pMsg);
        //    return Json(model.gvmrdetailsview, JsonRequestBehavior.AllowGet);
        //}
    }
}