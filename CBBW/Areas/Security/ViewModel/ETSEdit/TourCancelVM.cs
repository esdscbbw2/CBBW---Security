using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.ViewModel.ETSEdit
{
    public class TourCancelVM: EditHistoryVM
    {
        public List<EditDWTDetails> DWTDetailsCurrent { get; set; }
    }
}