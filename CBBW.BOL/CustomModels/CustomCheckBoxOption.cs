using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CustomModels
{
    public class CustomCheckBoxOption : CustomComboOptions
    {        
        public bool IsSelected { get; set; }
        public bool IsActive { get; set; }
    }
    
}
