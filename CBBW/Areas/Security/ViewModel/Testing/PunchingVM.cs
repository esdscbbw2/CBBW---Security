using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;

namespace CBBW.Areas.Security.ViewModel.Testing
{
    public class PunchingVM
    {
        public int CentreCode { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime PunchDate { get; set; }
        public string PunchTime { get; set; }
        public IEnumerable<LocationMaster> CentreList { get; set; }
    }
}