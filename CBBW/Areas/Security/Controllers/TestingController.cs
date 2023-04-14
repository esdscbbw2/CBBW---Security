using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.Testing;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;

namespace CBBW.Areas.Security.Controllers
{
    public class TestingController : Controller
    {
        IEMNRepository _iEMN;
        IMasterRepository _iMaster;
        string pMsg = "";
        public TestingController(IEMNRepository iEMN, IMasterRepository iMaster)
        {
            _iEMN = iEMN;
            _iMaster = iMaster;

        }
        // GET: Security/Testing
        public ActionResult Index()
        {
            PunchingVM model = new PunchingVM();
            // / EMN / GetStaffList
            model.CentreList=_iMaster.GetCentresFromTourCategory("1", ref pMsg);
            //CustomComboOptions c1 = new CustomComboOptions() { ID = 13, DisplayText = "13/ Nizamabad" };
            //model.CentreList = .getCenterCodeList(0, ref pMsg);
            //model.CentreList.Append(c1);
            model.PunchDate = DateTime.Today;
            model.PunchTime = DateTime.Now.ToString("hh:mm tt");
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(PunchingVM model)
        {
            //ViewBag.ErrMsg            
            if (model.CentreCode == 0)
                ViewBag.ErrMsg = "Select A Centre To Proceed.";
            else if (model.EmployeeNumber == 0)
                ViewBag.ErrMsg = "Enter Valid Employee Number To Proceed.";
            else if (model.PunchDate.Year == 1)
                ViewBag.ErrMsg = "Invalid Punch Date.";
            else if (string.IsNullOrEmpty(model.PunchTime))
                ViewBag.ErrMsg = "Invalid Punch Time.";
            else 
            {
                if (_iMaster.SetPunchIN(model.CentreCode, model.EmployeeNumber, model.PunchDate, model.PunchTime, ref pMsg))
                {
                    ViewBag.ErrMsg = "";
                    ViewBag.Msg = "Punching Details Updated Successfully.";
                }
                else { ViewBag.ErrMsg = "Failed To Update Punching Details."; }
            }
            model.CentreList = _iMaster.GetCentresFromTourCategory("1", ref pMsg);
            return View(model);
        }
    }
}