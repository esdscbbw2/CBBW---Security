using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.Areas.Security.ViewModel
{
    public class TADATransViewModel
    {
        public string SelectedpubTranOptions { get; set; }
        public List<TADAPubTransOption> PubTranOptions { get; set; }
        public List<CustomCheckBoxOption> CompTranOptions { get; set; }
        public int mRuleID { get; set; }
        public bool mDelete { get; set; }
        public int IsBtnSubmit { get; set; }
    }
}