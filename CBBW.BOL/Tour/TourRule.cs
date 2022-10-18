using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;

namespace CBBW.BOL.Tour
{
    public class TourRule
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime EffectiveDate { get; set; }
        public bool IsApplied { get; set; }
        public int SL { get; set; }
        public int isBtn { get; set; }
    }
    
    
}
