using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Role
{
    public class Actions
    {
        public int? ID { get; set; }
        public string TaskName { get; set; }
        public int ActionID { get; set; }
        public string ActionIDs { get; set; }
        public string ActionName { get; set; }
        public int TaskId { get; set; }
        public int IsActiveInt { get; set; }
        public int NavigationId { get; set; }
    }

    
}
