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

        public List<MGPMatOut> getMGPOutDetails(string NoteNumber, ref string pMsg)
        {
           return _MGPEntities.getMGPOutDetails(NoteNumber, ref pMsg);
        }

        public List<RFID> getRFIDCards(ref string pMsg)
        {
            return _MGPEntities.getRFIDCards(ref pMsg);
        }
    }
}
