using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CBBW.BOL.TADA
{
    public class TADARuleListData
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int SL { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateDisplay { get; set; }
        public bool IsApplied { get; set; }
        public string IsActiveText { get; set; }
        public bool IsActive { get; set; }
    }
    public class TADARule: TADARuleListData
    {
        public int ID { get; set; }        
        public string EntryTime { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string ConnectingID { get; set; }
        public string NewConnectingID { get; set; }
        public DateTime LastEffectiveDate { get; set; }
        public int IsBtn { get; set; }
        public int IsParamBtn { get; set; }
        public int IsSubmitBtn { get; set; }
        public string MaxDate { get; set; }
        public string MinDate { get; set; }
    }
}
