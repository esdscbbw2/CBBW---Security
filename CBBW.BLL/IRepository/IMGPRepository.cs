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
        IEnumerable<MGPNotes> getApprovedNoteNumbers(int Centercode, ref string pMsg);
        List<MGPMatOut> getMGPOutDetails(string NoteNumber, ref string pMsg);
        List<RFID> getRFIDCards(ref string pMsg);
    }
}
