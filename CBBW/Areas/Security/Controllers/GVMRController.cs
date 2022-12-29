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
            return View();
        }

        public ActionResult Create()
        {
            GVMRDetailsVM gvmrvm = new GVMRDetailsVM();
            gvmrvm.listnotenumber = _IGVMR.GetNoteNumbers(user.CentreCode,1, ref pMsg);

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
                    gvmrsave.ActualTripInTime = item.ActualTripInTime;
                    gvmrsave.ActualTripInKM = item.ActualTripInKM > 0 ? item.ActualTripInKM : 0;
                    gvmrsave.ActualOutRFIDCard = item.ActualOutRFIDCard;
                    gvmrsave.ActualTripOutDate = item.ActualTripOutDate;
                    gvmrsave.ActualTripOutTime = item.ActualTripOutTime;
                    gvmrsave.ActualTripOutKM = item.ActualTripOutKM > 0 ? item.ActualTripOutKM : 0;
                    gvmrsave.Remark = item.Remark!=null? item.Remark:"NA";
                }




                if (_IGVMR.setGVMRDetails(gvmrsave, ref pMsg))
                {
                    result.bResponseBool = true;
                    result.sResponseString = "Data successfully updated.";
                }
                else
                {
                    result.bResponseBool = false;

                }


            }
            catch (Exception ex)
            {
                pMsg = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGVMRDetails(string NoteNumber)
        {
            GVMRDetailsVM model = new GVMRDetailsVM();
            model.gvmrdetails = _IGVMR.GetGVMRDetails(NoteNumber, user.CentreCode, ref pMsg);
            return Json(model.gvmrdetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
         string sSortDir_0, string sSearch)
        {
            List<GVMRNoteList> noteList = _IGVMR.getGVMRDetailsforListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, ref pMsg);

            var result = new
            {
                iTotalRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = noteList.Count(),
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(string NoteNumber)
        {
            GVMRDetailsVM model = new GVMRDetailsVM();
            model.gvmrdetailsview = _IGVMR.getGVMRDetailsForView(NoteNumber, user.CentreCode, ref pMsg);
           var models = model.gvmrdetailsview.Where(x => x.NoteNo == NoteNumber).OrderByDescending(x => x.NoteNo).FirstOrDefault();
            model.NoteNo = models.NoteNo;
            model.VehicleNo = models.VehicleNo;
            model.VehicleType = models.VehicleType;
            model.DriverNo = models.DriverNo;
            model.DriverName = models.DriverNo+"/"+ models.DriverName;
            model.ModelName = models.ModelName;
            model.MonthYear = models.MonthYear;
            model.CenterName = models.LocationName;
            model.EntryDate = models.EntryDate;
            model.EntryTime = models.EntryTime;
            return View(model);
        }
        public JsonResult GetGVMRDetailsForView(string NoteNumber)
        {
            GVMRDetailsVM model = new GVMRDetailsVM();
            model.gvmrdetailsview = _IGVMR.getGVMRDetailsForView(NoteNumber, user.CentreCode, ref pMsg);

            return Json(model.gvmrdetailsview, JsonRequestBehavior.AllowGet);
        }



    }
}