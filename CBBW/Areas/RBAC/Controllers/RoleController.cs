using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Role;

namespace CBBW.Areas.RBAC.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        UserInfo user;
        string pMsg;
        int Status;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        IRoleRepository _iRole;
        ICTVRepository _iCTV;
        public RoleController(IRoleRepository iRole, ICTVRepository iCTV, IUserRepository iUser, IMyHelperRepository myHelper, IMasterRepository master)
        {
            try
            {
                _iUser = iUser;
                _myHelper = myHelper;
                _master = master;
                _iRole = iRole;
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
        public ActionResult Create()
        {
            RBACRoles model = new RBACRoles();
            model= _iRole.getNewNoteNumber(ref pMsg);
            model.RoleIds = model.RoleId;
            return View(model);
        }
        [HttpPost]
        public JsonResult Create(string RoleId, string RoleName,int NavigationId, List<Actions> modulelist, string SubmitType)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                    modulelist = modulelist.Where(x => x.IsActiveInt == 1).ToList();
                    if (modulelist.Count > 0) { 
                    if (_iRole.SetRoleModule(RoleId, RoleName, NavigationId,SubmitType == "Final" ? 4 : 1, user.CentreCode, modulelist, ref pMsg))
                    {
                        result.bResponseBool = true;
                        if (SubmitType == "Final")
                        {
                            result.sResponseString = "Data successfully Submit.";
                        }
                        else
                        {
                            result.sResponseString = "Data successfully Save.";
                        }
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
                        result.sResponseString = "Please Select Task Name";

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
        public JsonResult GetModule(int ID=0)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iRole.GetModuleList(ID, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubModule(int Id)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iRole.GetSubModuleList(Id, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNavigation(int Id)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iRole.GetNavigationList(Id, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult TaskDetails(int NavigationId)
        {
            TaskControl result = new TaskControl();
            result = _iRole.GetTaskDetails(NavigationId, ref pMsg);
            return View("~/Areas/RBAC/Views/Role/_Task.cshtml", result);
        }
        public JsonResult GetIndexListPage(int iDisplayLength, int iDisplayStart, int iSortCol_0,
   string sSortDir_0, string sSearch)
        {
            List<RoleList> noteList = _iRole.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        public ActionResult Edit(string ID, int CanDelete, int CBUID = 0)
        {
            Header model = new Header();
            model.RoleId = ID;
            return View(model);
        }
        [HttpPost]
        public JsonResult Edit(string RoleId, string RoleName, List<Actions> modulelist)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                   
                    if (modulelist.Count > 0)
                    {
                        modulelist = modulelist.Where(x => x.NavigationId != 0 && x.TaskId!=0 && x.ActionIDs!=null).ToList();
                        if (_iRole.SetRoleModule(RoleId,RoleName,0,2, user.CentreCode, modulelist, ref pMsg))
                        {
                            result.bResponseBool = true;
                          
                                result.sResponseString = "Data Update successfully.";
                            
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
                        result.sResponseString = "Please Select Task Name";

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
        public JsonResult GetRoleDetails(string RoleId)
        {
            Header result = new Header();
            result = _iRole.GetRoleDetails(RoleId, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTaskName(int Id)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iRole.GetTaskList(Id, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetActionsList(int Id)
        {
            IEnumerable<CustomComboOptions> result;
            result = _iRole.GetActionList(Id, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(string ID, int CanDelete, int CBUID = 0)
        {
            Header model = new Header();
            model.RoleId = ID;
            model.CanDelete = CanDelete == 1 ? true : false;
            model.HeaderText = CanDelete == 1 ? "Delete" : "View";
            return View(model);
        }
        public JsonResult GetRoleDetailsForView(string RoleId)
        {
            Header result = new Header();
            result = _iRole.GetRoleDetailsForView(RoleId, ref pMsg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string RoleId,int NavigationId, int TaskId)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (RoleId != null && NavigationId!=0 && TaskId!=0)
                {
                        if (_iRole.SetDeleteRoleModule(RoleId,NavigationId, TaskId, ref pMsg))
                        {
                            result.bResponseBool = true;
                            result.sResponseString = "Data Deleted successfully.";
                        }
                        else
                        {
                          result.iRespinseInteger = Status;
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
        public ActionResult InjectMVC()
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
           
            if (_iRole.SetRBACMVC(model2,ref pMsg))
            {
                @ViewBag.Msg = "Done";
            }
            else
            {
                @ViewBag.ErrMsg = pMsg;
            }
            return View();
        }


    }
}