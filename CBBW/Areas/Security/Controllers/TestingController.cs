using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CBBW.Areas.Security.ViewModel.Testing;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CustomModels;
using CBBW.BOL.RBACUsers;

namespace CBBW.Areas.Security.Controllers
{
    public class TestingController : Controller
    {
        IEMNRepository _iEMN;
        IMasterRepository _iMaster;
        IRoleRepository _iRole;
        IUserRepository _iUser;
        string pMsg = "";
        public TestingController(IUserRepository iUser,IRoleRepository iRole,IEMNRepository iEMN, IMasterRepository iMaster)
        {
            _iEMN = iEMN;
            _iMaster = iMaster;
            _iRole = iRole;
            _iUser = iUser;
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
        public ActionResult GetRBAC() 
        {
            List<PageInformation> model = new List<PageInformation>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> controllerTypes = assembly.GetTypes();
            controllerTypes = controllerTypes.Where(t => t.BaseType.Name == "Controller");
            foreach (Type controllerType in controllerTypes)
            {
                string controllerName = controllerType.Name.Replace("Controller", "");
                MethodInfo[] methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in methods)
                {
                    if (method.IsPublic && !method.IsConstructor && method.ReturnType.Name == "ActionResult")
                        model.Add(new PageInformation
                        {
                            ControllerName = controllerName,
                            ActionName = method.Name
                        });
                }
            }
            List<PageInformation> model2 = model.Distinct(new PageInformationEqualityComparer()).ToList();

            if (_iRole.SetRBACMVC(model2.OrderBy(o=>o.ControllerName).ToList(), ref pMsg))
            {
                @ViewBag.Msg = "Done";
            }
            else
            {
                @ViewBag.ErrMsg = pMsg;
            }
            return View(model2.OrderBy(o => o.ControllerName).ToList());
        }
        public ActionResult ValidationTesting() 
        {
            return View();
        }
        public ActionResult GetUserMenu() 
        {
            List<UserMenu> model = _iUser.GetUserMenu(38682, 13,9, ref pMsg);
            ViewData["MyMenu"] = model;
            return View(model);
        }
    }
}