using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;
using CBBW.BOL.Tour;
using CBBW.DAL.Entities;
using CBBW.Models;

namespace CBBW.Controllers
{
    public class HomeController : Controller
    {
        IMasterRepository _masterrepo;
        public HomeController(IMasterRepository masterrepo)
        {
            _masterrepo = masterrepo;
        }
        public ActionResult Index()
        {
            string pMsg = "";
            TourEntities x = new TourEntities();
            DateTime fromdate = new DateTime(2022, 11, 2);

            bool result = x.RemoveTourRule(22,ref pMsg);

            Object obj=Json(result, JsonRequestBehavior.AllowGet);

            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
    }
}