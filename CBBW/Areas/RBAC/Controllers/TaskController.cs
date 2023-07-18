using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels; 
using CBBW.BOL.Modules;
using CBBW.BOL.Task;

namespace CBBW.Areas.RBAC.Controllers
{
    public class TaskController : Controller
    {
        // GET: RBAC/Module
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        ITaskRepository _iModule;
        ICTVRepository _iCTV;
        public TaskController(ITaskRepository iModule, ICTVRepository iCTV, IUserRepository iUser, IMyHelperRepository myHelper, IMasterRepository master)
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
            List<TaskList> noteList = _iModule.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        public JsonResult Create(int NavigationId, List<TaskMaster> modulelist, string SubmitType)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                    modulelist = modulelist.Where(x => x.IsActiveInt == 1).ToList();
                    if (_iModule.SetAddTaskModule(SubmitType == "Final" ? 4 : 1, user.CentreCode, NavigationId, modulelist, ref pMsg))
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
        //[HttpGet]
        //public ActionResult Details(int ID, int CanDelete, int CBUID = 0)
        //{
        //    Module module = new Module();
        //    //try
        //    //{
        //    //    module = _iModule.GetModuleDetails(ID, ref pMsg);
        //    //}
        //    //catch (Exception ex) { ex.ToString(); }

        //    return View(module);
        //}
        [HttpGet]
        public ActionResult Edit(string Module,string SubModule,string NavModule, int ID,int CanDelete, int CBUID = 0)
        {
            Header module = new Header();
            try
            {
                //module = _iModule.GetNavigationDetails(ID, ref pMsg);
                module.NavigationId = ID;
                module.ModuleName = Module;
                module.SubModuleName = SubModule;
                module.NavigationName = NavModule;
                module.CanDelete = CanDelete == 1 ? true : false;
                module.HeaderText = CanDelete == 1 ? "Delete" : "Edit";
            }
            catch (Exception ex) { ex.ToString(); }


            return View(module);
        }
        [HttpPost]
        public ActionResult Edit(int NavigationId, string SubmitType,List<TaskMaster> modulelist)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                    modulelist = modulelist.Where(x => x.IsActiveInt == 1).ToList();
                    if (_iModule.SetAddTaskModule(SubmitType == "Final" ? 2 : 3, user.CentreCode, NavigationId, modulelist, ref pMsg))
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

        #region Json Function
        public JsonResult GetTaskData(int Id)
        {
            Header result = new Header();
            try
            {
                result = _iModule.GetTaskMasterDetails(Id, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTaskNameList()
        {
            IEnumerable<CustomComboOptions> result = null ;
            try
            {
                result = _iModule.GetTaskNameList(0, ref pMsg);
            }
            catch (Exception ex) { ex.ToString(); }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}