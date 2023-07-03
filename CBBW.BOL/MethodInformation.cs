using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL
{
    public class MethodInformation
    {
        public string MethodName { get; set; }
        public string MethodPath { get; set; }
        public string MethodParams { get; set; }
        public string MethodSignature { get; set; }
    }
    public class PageInformation 
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
    
}
