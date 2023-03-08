﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CustomModels
{
    public class CustomComboOptionsWithString
    {
        public string ID { get; set; }
        public string DisplayText { get; set; }
    }
    public class CustomOptionsWithString : CustomComboOptionsWithString
    {
        public bool IsSelected { get; set; }
    }
}
