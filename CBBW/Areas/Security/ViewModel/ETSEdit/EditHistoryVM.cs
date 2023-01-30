using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.ViewModel.ETSEdit
{
    public class EditHistoryVM
    {
        public List<EditDWTDetails> DWTDetailsHistory { get; set; }
        public List<int> EditSequence { get; set; }
        public string EmpNoNName { get; set; }
        public string PersonType { get; set; }
    }
}