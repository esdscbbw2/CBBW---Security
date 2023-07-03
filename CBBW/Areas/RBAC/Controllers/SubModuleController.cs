using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Modules;
using CBBW.BOL.SubModules;

namespace CBBW.Areas.RBAC.Controllers
{
    public class SubModuleController : Controller
    {
        // GET: RBAC/Module
        UserInfo user;
        string pMsg;
        IUserRepository _iUser;
        IMyHelperRepository _myHelper;
        IMasterRepository _master;
        ISubModuleRepository _iModule;
        ICTVRepository _iCTV;
        public SubModuleController(ISubModuleRepository iModule, ICTVRepository iCTV, IUserRepository iUser, IMyHelperRepository myHelper, IMasterRepository master)
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
            List<SubModuleList> noteList = _iModule.GetIndexListPage(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, 1, ref pMsg);

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
        public JsonResult Create(int ModuleId,List<SubModule> modulelist,string SubmitType)
        {
            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                    if (_iModule.SetAddSubModule(SubmitType=="Final"?4:1, user.CentreCode, ModuleId, modulelist, ref pMsg))
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
            //try
            //{
            //    module = _iModule.GetModuleDetails(ID, ref pMsg);
            //}
            //catch (Exception ex) { ex.ToString(); }

            return View(module);
        }

        [HttpGet]
        public ActionResult Edit(int ID, int CanDelete, int CBUID = 0)
        {
            Header module = new Header();
            try
            {
                //module = _iModule.GetSubModuleDetails(ID, ref pMsg);

                //foreach (var item  in module.sublist)
                //{
                //SubModule obj = new SubModule();
                //    obj.IsActiveInt = item.IsActive == true ? 1 : 0;
                //}
                module.ModuleId = ID;


                module.CanDelete = CanDelete == 1 ? true : false;
                module.HeaderText = CanDelete == 1 ? "Delete" : "Edit";
            }
            catch (Exception ex) { ex.ToString(); }

            return View(module);
        }
        [HttpPost]
        public ActionResult Edit(int ModuleId, List<SubModule> modulelist, string ButtonType)
        {

            CustomAjaxResponse result = new CustomAjaxResponse();
            try
            {
                if (modulelist != null)
                {
                    if (_iModule.SetAddSubModule(ButtonType== "Save"?2:3, user.CentreCode,ModuleId, modulelist, ref pMsg))
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
       public JsonResult GetSubmoduleData(int Id)
        {

            Header result = new Header();
            try
            {
                result = _iModule.GetSubModuleDetails(Id, ref pMsg);

                //foreach (var item in result.sublist)
                //{
                //    SubModule obj = new SubModule();
                //    obj.IsActiveInt = item.IsActive == true ? 1 : 0;
                //}

            }
            catch (Exception ex) { ex.ToString(); }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}