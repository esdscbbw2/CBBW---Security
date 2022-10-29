﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;

namespace CBBW.Areas.Security.ViewModel
{
    public class LVTSFromMatVM
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime SCHDates { get; set; }
        public List<LocVehSchFromMat> LVSDataList { get; set; }
    }
}