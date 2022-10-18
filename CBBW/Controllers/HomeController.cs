using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
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
            return View();
        }

    }
}