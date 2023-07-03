using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Modules;

namespace CBBW.Areas.RBAC.Controllers
{
    public class ModuleController : Controller
    {
        // GET: RBAC/Module
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        IModuleRepository _iModule;
        ICTVRepository _iCTV;
        public ModuleController(IModuleRepository iModule, ICTVRepository iCTV, IUserRepository iUser, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
                _iModule = iModule;
                _iCTV = iCTV;
                user = iUser.getLoggedInUser();
                ViewBag.LogInUser = user.UserName;
            }
            catch (Exception ex) { pMsg = ex.ToString(); }

        }



        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetIndexListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
    string sSortDir_0, string sSearch)
        {
            List<ModuleList> noteList = _iModule.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public JsonResult Create(List<Module> modulelist)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                    if (_iModule.SetAddModule(1, modulelist,user.CentreCode, ref pMsg))
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
                else
                {
                    result.bResponseBool = false;
                    result.sResponseString = "Data Not Available";
                }
            }
            catch (Exception ex)
            {
                result.bResponseBool = false;
                result.sResponseString = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Details(int ID, int CanDelete, int CBUID = 0)
        {
            Module module = new Module();
            try
            {
                module = _iModule.GetModuleDetails(ID, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }

            return View(module);
        }

        [HttpGet]
        public ActionResult Edit(int ID, int CanDelete, int CBUID = 0)
        {
            Module module = new Module();
            try
            {
                module = _iModule.GetModuleDetails(ID, ref pMsg);
                module.IsActiveInt = module.IsActive == true ? 1 : 0;
                module.CanDelete = CanDelete==1?true:false;
                module.HeaderText = CanDelete == 1 ? "Delete" : "Edit";
            }
            catch (Exception ex) { ex.ToString(); }

            return View(module);
        }
        [HttpPost]
        public ActionResult Edit(Module module, string Submit)
        {
            List<Module> obj = new List<Module>();
            try
            {
                if (module != null)
                {
                    obj.Add(module);
                    if (Submit == "Save")
                    {
                        if (_iModule.SetAddModule(2, obj, user.CentreCode, ref pMsg))
                        {
                            ViewBag.Msg = "Module Updated Successfully.";

                        }
                        else
                        {
                            ViewBag.ErrMsg = "Module Updation Failed.";
                        }
                    }
                    else if (Submit == "Delete")
                    {
                        if (_iModule.SetAddModule(3, obj, user.CentreCode, ref pMsg))
                        {
                            ViewBag.Msg = "Module Deleted Successfully.";

                        }
                        else
                        {
                            ViewBag.ErrMsg = "Module Deleted Failed.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                pMsg=ex.ToString();
            }
            return View(module);
        }
    }
}