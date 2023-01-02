using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;

namespace CBBW.BOL.Tour
{
    public class TourRuleListData 
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int SL { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateDisplay { get; set; }
        public bool IsApplied { get; set; }
    }
    public class TourRule: TourRuleListData
    {
        public int ID { get; set; }       
        public string EntryTime { get; set; }
        public int isBtn { get; set; }
    }
    

}
