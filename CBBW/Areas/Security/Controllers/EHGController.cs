using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CBBW.Areas.Security.Controllers
{
    public class EHGController : Controller
    {
        // GET: Security/EHG
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() 
        {

            return View();
        }
        public ActionResult DateWiseTourDetails() 
        {

            return View();
        }
        [HttpPost]
        public ActionResult TrialPick(trl trlList) 
        {

            return RedirectToAction("DateWiseTourDetails");
        }
        public class trl 
        {
            public List<trialclass> triallist { get; set; }
        }
        public class trialclass 
        {
            public DateTime Fromdate { get; set; }
            public string ToDate { get; set; }
            public string TourCategory { get; set; }
            public string CenterCodes { get; set; }

        }
    }
}