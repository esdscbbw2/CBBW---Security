using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.ViewModel
{
    public class MGPOutInVM
    {
        public string NoteNumber { get; set; }
        public LocVehSchFromMat HDREntry { get; set; }
        //public IEnumerable<LocVehSchFromMat> ListHDREntry { get; set; }
        public IEnumerable<MGPOutInDetails> ListofMatOut { get; set; }
        public List<MGPVehicleOutDetails>ListCurrentOutDetails{ get; set; } 
       //public IEnumerable<MGPVehicleOutDetails> ListCurrentOutDetails { get; set; }
        public IEnumerable<RFID> ListofRFID { get; set; }
        public List<MGPReferenceDCDetails> ListofMGPReferenceDCDetails { get; set; }
        public List<MGPHistoryDCDetails> ListMGPHistoryDCDetails { get; set; }
        public IEnumerable<MGPItemWiseDetails> ListofMGPItemWiseDetails { get; set; }
    }

    public class MGPSaveOutDetailsVM
    {
        public List<MGPOutSave> ListCurrentOutData { get; set; }
        public List<MGPReferenceDCDetails> ListofMGPReferenceDCData { get; set; }
    }
}