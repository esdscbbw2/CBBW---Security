using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CBBW.Areas.Security.ViewModel.ETSEdit;
using CBBW.BLL.Repository;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.Helpers
{
    public static class HtmlHelperExtensions
    {        
        public static MvcHtmlString GetHistoryData(this HtmlHelper html,string view,
            string NoteNumber,int FieldTag,int PersonType,int PersonID,string PersonName) 
        {
            string pMsg="";
            EditHistoryVM model = new EditHistoryVM();
            ETSEditRepository _editRepo = new ETSEditRepository();
            model.DWTDetailsHistory=_editRepo.getDateWiseTourHistory(NoteNumber, FieldTag, PersonType, PersonID, PersonName, ref pMsg,true);
            if (model.DWTDetailsHistory != null && model.DWTDetailsHistory.Count > 0)
            {
                model.EditSequence = model.DWTDetailsHistory.Select(o => o.EditSL).Distinct().ToList();                
            }
            return html.Partial(view, model);
        } 
    }
}