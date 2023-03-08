using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.BIL;

namespace CBBW.Areas.Security.ViewModel.BIL
{
    public class BILHdrVM
    {
        public BILHdrVM()
        {
            TADABill = new TADABillGeneration();
            TADARuledata = new TADARuleData();
        }
        public string RefNoteNumber { get; set; }
        public string NoteNumber { get; set; }
        public int submitcount { get; set; }
        public List<NoteNo> Notelist { get; set; }
        public TADABillGeneration TADABill { get; set; }
        public TADARuleData TADARuledata { get; set; }
    }
}