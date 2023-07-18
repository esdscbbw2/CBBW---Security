using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.Alerts
{
    public class AlertMaster
    {
        public int ID { get; set; }
        public string AlertName { get; set; }
        public int TaskID { get; set; }
        public string MTag { get; set; }
        public bool IsActive { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateStr { get; set; }
        public string UserName { get; set; }
    }
}
