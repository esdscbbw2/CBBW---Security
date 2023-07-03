using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;

namespace CBBW.BOL.Role
{
   public class TaskNames
    {
        public string TaskName { get; set; }

    }

    public class TaskControl
    {
        public List<TaskNames> taskname { get; set; }
        public List<Actions> actions { get; set; }
    }
}
