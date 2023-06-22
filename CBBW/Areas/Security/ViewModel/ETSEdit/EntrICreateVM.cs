using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;

namespace CBBW.Areas.Security.ViewModel.ETSEdit
{
    public class EntrICreateVM
    {
        public EntrICreateVM()
        {
            EHGMaster master = EHGMaster.GetInstance;
            this.VehicleBelongsTo = master.VehicleBelongsTo;
        }
        public string NoteNumber { get; set; }
        public int VehicleType { get; set; }
        public string AuthorisedEmpNonName { get; set; }
        public string DesgCodenNameOfAE { get; set; }
        public List<CustomComboOptions> VehicleBelongsTo { get; set; }
        public IEnumerable<EditNoteNumber> DropDownNoteList { get; set; }
        public List<VehicleNo> VehicleList { get; set; }
        public IEnumerable<NoteDriver> DriverList { get; set; }
        public VehicleAllotmentDetails VADetails { get; set; }
        public int VABackBtnActive { get; set; }
        public int IsBtn { get; set; }
        public int VASubmitBtnActive { get; set; }
        public int IsVABtnEnabled { get; set; }
        public int CanDelete { get; set; }
        public string EntryDateDisplay { get; set; }
        public string EntryTime { get; set; }
        public int MaterialStatus { get; set; }
        public int VehicleBelongsTo2 { get; set; }
        public string VehicleNumber { get; set; }
        public string OtherVehicleNumber { get; set; }
        public string DriverName { get; set; }
        public int IsDriverCtrlEnable { get; set; }
        public string AuthorisedEmpNonName2 { get; set; }
        public string DesgCodenNameOfAE2 { get; set; }
        public string SchFromDate { get; set; }
        public string SchToDate { get; set; }
    }
}