using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.GVMR;

namespace CBBW.Areas.Security.ViewModel
{
    public class GVMRDetailsVM
    {
         public string NoteNo { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public string EntryTime { get; set; }
        public string MonthYear { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public IEnumerable<GVMRNoteNumber> listnotenumber { get; set; }
        public List<GVMRDetails> gvmrdetails { get; set; }
        public List<GVMRDetails> gvmrdetailsview { get; set; }
        public List<GVMRDataSave> gvmrdatasave { get; set; }
        public GVMRDataSave InPutData { get; set; }

    }
}