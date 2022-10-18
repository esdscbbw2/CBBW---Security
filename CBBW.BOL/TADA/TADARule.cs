using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CBBW.BOL.TADA
{
    public class TADARule
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime EffectiveDate { get; set; }
        public bool IsApplied { get; set; }
        public string ConnectingID { get; set; }
        public string NewConnectingID { get; set; }
        public int SL { get; set; }
        public DateTime LastEffectiveDate { get; set; }
        public int IsBtn { get; set; }
        public int IsParamBtn { get; set; }
        public int IsSubmitBtn { get; set; }
    }
}
