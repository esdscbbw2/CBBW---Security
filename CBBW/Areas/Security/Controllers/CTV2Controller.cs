using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CTV2;

namespace CBBW.Areas.Security.Controllers
{
    public class CTV2Controller : Controller
    {
        IUserRepository _iUser;
        string pMsg="";
        ICTVRepository _iCTV;
        UserInfo user;
        public CTV2Controller(ICTVRepository iCTV, IUserRepository iUser)
        {
            _iCTV = iCTV;
            _iUser = iUser;
            user = iUser.getLoggedInUser();
            ViewBag.LogInUser = user.UserName;
        }
        // GET: Security/CTV2
        public ActionResult Index()
        {
            return View();
        }


        #region Ajax Calling
        public JsonResult GetEntryNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<CTVNoteList4DT> noteList = _iCTV.GetNoteListForDataTable(iDisplayLength,iDisplayStart, iSortCol_0, sSortDir_0,sSearch,user.CentreCode,false, ref pMsg); 
            var result = new
            {
                iTotalRecords = noteList.Count==0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iTotalDisplayRecords = noteList.Count == 0 ? 0 : noteList.FirstOrDefault().TotalCount,
                iDisplayLength = iDisplayLength,
                iDisplayStart = iDisplayStart,
                aaData = noteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetApprovedNoteList(int iDisplayLength, int iDisplayStart, int iSortCol_0,
            string sSortDir_0, string sSearch)
        {
            List<CTVNoteList4DT> noteList = _iCTV.GetNoteListForDataTable(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, user.CentreCode, true, ref pMsg);
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





        #endregion Ajax Calling


    }
}