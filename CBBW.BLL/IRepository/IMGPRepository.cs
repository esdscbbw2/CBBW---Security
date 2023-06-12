using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.MGP;

namespace CBBW.BLL.IRepository
{
    public interface IMGPRepository
    {
        #region For Out Details
        IEnumerable<MGPNotes> getApprovedNoteNumbers(int Centercode, ref string pMsg);
        IEnumerable<MGPNotes> GetNoteNumbersfromMGP(int Centercode, ref string pMsg);
        List<MGPOutInDetails> getMGPOutDetails(string NoteNumber, ref string pMsg);
        List<RFID> getRFIDCards(ref string pMsg);

        // Getting data for Out details in Item Wise Details using NoteNo(For New Data insert)
        List<MGPItemWiseDetails> getItemWiseDetails(string NoteNumber, ref string pMsg);

        // Getting data for Out details in Reference DC Details using NoteNo(For New Data insert)
        List<MGPReferenceDCDetails> getReferenceDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg);

        List<MGPVehicleOutDetails> getSchDtlsForMGP(string NoteNumber, ref string pMsg);

        List<MGPHistoryDCDetails> getMGPHistoryDCDetails(long ID,int status, ref string pMsg);

        bool setMGPOutDetails(MGPOutSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg);
        bool spUpdateOutDetailsflag(string NoteNumber, long ID,int status, ref string pMsg);
        #endregion

        #region For In Details
        List<MGPCurrentInDetails> getMGPCurrentOutDetailsForIn(string NoteNumber, ref string pMsg);
        List<MGPReferenceDCDetails> getReferenceInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg);
        List<MGPItemWiseDetails> getItemWiseInDetails(string NoteNumber, ref string pMsg);
        bool setMGPInDetails(MGPInSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg);
        #endregion

        #region For List Page (Index page)
        List<MGPNoteList> getMGPDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText,int CentreCode, ref string pMsg);
       
        #endregion

        #region IN/Out Button Active
        ButtonActive getMGPButtonStatus(string NoteNumber, ref string pMsg);
        #endregion

        PrintHeader GetMGPDetailsForPrint(string NoteNumber, ref string pMsg);
        PrintHeader GetMGPDetailsForPrintV2(string NoteNumber, DateTime SchFromDate, ref string pMsg);
        Percentage getMaterialPercent(string VehicleNo, DateTime FromDT, int status, ref string pMsg);


    }
}
