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
        public IEnumerable<MGPNotes> getApprovedNoteNumbers(int Centercode, ref string pMsg)
        {
            return _MGPEntities.getApprovedNoteNumbers(Centercode, ref pMsg);
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


        public List<MGPHistoryDCDetails> getMGPHistoryDCDetails(long ID, ref string pMsg)
        {
            return _MGPEntities.getMGPHistoryDCDetails(ID, ref pMsg);
        }

        public bool setMGPOutDetails(MGPOutSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg)
        {
            return _MGPEntities.setMGPOutDetails(mgpouthdr, mgprefdcdetails, ref pMsg);
        }

        public bool spUpdateOutDetailsflag(string NoteNumber, long ID, ref string pMsg)
        {
            return _MGPEntities.spUpdateOutDetailsflag(NoteNumber, ID, ref pMsg);
        }

    }
}
