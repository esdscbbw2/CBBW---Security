using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.BIL;
using CBBW.BOL.CustomModels;

namespace CBBW.Areas.Security.ViewModel.BIL
{
    public class DaDetailsVM
    {
        public string RefNoteNumber { get; set; }
        public string NoteNumber { get; set; }
        public int  EmployeeNo { get; set; }
        public double TotalDA { get; set; }
        public double TotalDaDect { get; set; }
        public double TotalElg { get; set; }
        public string Remark { get; set; }
        public TADABillGeneration TadabollGen { get; set; }
        public IEnumerable<CustomComboOptions> Deptlist { get; set; }
        public List<DADetails> DADetailslist { get; set; }
        public List<TravellingDetails> TravDetails { get; set; }
    }
}