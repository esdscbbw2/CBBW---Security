using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.MGP;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class MGPRepository : IMGPRepository
    {
        MGPEntities _MGPEntities;
        public MGPRepository()
        {
            _MGPEntities = new MGPEntities();
        }

        #region For Out Details
        public IEnumerable<MGPNotes> getApprovedNoteNumbers(int Centercode, ref string pMsg)
        {
            return _MGPEntities.getApprovedNoteNumbers(Centercode, ref pMsg);
        }
        public IEnumerable<MGPNotes> GetNoteNumbersfromMGP(int Centercode, ref string pMsg)
        {
            return _MGPEntities.GetNoteNumbersfromMGP(Centercode, ref pMsg);
        }
        public List<MGPOutInDetails> getMGPOutDetails(string NoteNumber, ref string pMsg)
        {
           return _MGPEntities.getMGPOutDetails(NoteNumber, ref pMsg);
        }
        public List<RFID> getRFIDCards(ref string pMsg)
        {
            return _MGPEntities.getRFIDCards(ref pMsg);
        }

        // Getting data for Out details in Item Wise Details using NoteNo(For New Data insert)
        public List<MGPItemWiseDetails> getItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getItemWiseDetails(NoteNumber, ref pMsg);
        }
        // Getting data for Out details in Reference DC Details using NoteNo(For New Data insert)
        public List<MGPReferenceDCDetails> getReferenceDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            return _MGPEntities.getReferenceDCDetails( VehicleNo,  FromDT,  ToDT, ref pMsg);
        }
        public List<MGPVehicleOutDetails> getSchDtlsForMGP(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getSchDtlsForMGP( NoteNumber, ref  pMsg);
        }
        public List<MGPHistoryDCDetails> getMGPHistoryDCDetails(long ID,int status, ref string pMsg)
        {
            return _MGPEntities.getMGPHistoryDCDetails(ID, status, ref pMsg);
        }
        public bool setMGPOutDetails(MGPOutSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg)
        {
            return _MGPEntities.setMGPOutDetails(mgpouthdr, mgprefdcdetails, ref pMsg);
        }
        public bool spUpdateOutDetailsflag(string NoteNumber, long ID,int status, ref string pMsg)
        {
            return _MGPEntities.spUpdateOutDetailsflag(NoteNumber, ID,status, ref pMsg);
        }
        #endregion
        #region For In Details
        public List<MGPCurrentInDetails> getMGPCurrentOutDetailsForIn(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getMGPCurrentOutDetailsForIn(NoteNumber, ref pMsg);
        }
        public List<MGPReferenceDCDetails> getReferenceInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            return _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
        }
        public List<MGPItemWiseDetails> getItemWiseInDetails(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getItemWiseInDetails(NoteNumber, ref pMsg);
        }
        public bool setMGPInDetails(MGPInSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg)
        {
            return _MGPEntities.setMGPInDetails(mgpouthdr, mgprefdcdetails, ref pMsg);
        }
        #endregion

        #region For List Page (Index Page)
      
        public List<MGPNoteList> getMGPDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, ref string pMsg)
        {
            return _MGPEntities.getMGPDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, ref pMsg);
        }
        #endregion

        #region In/Out Button Active
        public ButtonActive getMGPButtonStatus(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getMGPButtonStatus(NoteNumber,ref pMsg);
        }

        public PrintHeader GetMGPDetailsForPrint(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.GetMGPDetailsForPrint(NoteNumber, ref pMsg);
        }

        public PrintHeader GetMGPDetailsForPrintV2(string NoteNumber, DateTime SchFromDate, ref string pMsg)
        {
            return _MGPEntities.GetMGPDetailsForPrintV2(NoteNumber, SchFromDate, ref pMsg);
        }
        #endregion
    }
}
