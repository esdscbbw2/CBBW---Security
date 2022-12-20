using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EHG
{
    public class ApprovalStatus: EHGNote
    {
        public int ID { get; set; }
        public bool IsApproved { get; set; }
        public DateTime AppDateTime { get; set; }
        public string ReasonForDisApproval { get; set; }
        public int ApproverID { get; set; }

    }
}
