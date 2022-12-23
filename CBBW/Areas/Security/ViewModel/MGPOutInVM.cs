using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.CTV;
using CBBW.BOL.MGP;

namespace CBBW.Areas.Security.ViewModel
{
    #region For Out Details
    public class MGPOutInVM
    {
        public string NoteNumber { get; set; }
        public LocVehSchFromMat HDREntry { get; set; }
        //public IEnumerable<LocVehSchFromMat> ListHDREntry { get; set; }
        public IEnumerable<MGPOutInDetails> ListofMatOut { get; set; }
        public List<MGPVehicleOutDetails> ListCurrentOutDetails { get; set; }
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
    #endregion
    //Create VM for Material In Details For 
    #region VM for In Details 
    public class MGPInDetailsVM
    {
        public string NoteNumber { get; set; }
        public IEnumerable<MGPCurrentInDetails> mgpoutindetails { get; set; }
        public List<MGPReferenceDCDetails> ListofMGPReferenceInDCDetails { get; set; }
        public IEnumerable<MGPItemWiseDetails> ListofMGPItemWiseInDetails { get; set; }
        
       public IEnumerable<MGPOutInDetails> ListInDetails { get; set; }

        public string RFIDCard { get; set; }
        public DateTime ActualTripInDate { get; set; }
        public int ActualKmIn { get; set; }
        public int KMRunInTrip { get; set; }
        public string InRemark { get; set; }
        public string ActualTime { get; set; }

    }

    public class MGPSaveInDetailsVM
    {
        public List<MGPInSave> ListCurrentInData { get; set; }
        public List<MGPReferenceDCDetails> ListofMGPReferenceInDCData { get; set; }
    } 

    #endregion


}