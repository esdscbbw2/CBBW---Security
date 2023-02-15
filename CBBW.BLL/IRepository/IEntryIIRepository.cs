using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EntryII;
using CBBW.BOL.MGP;

namespace CBBW.BLL.IRepository
{
    public interface IEntryIIRepository
    {
        List<MGPReferenceDCDetails> GetMatOutDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg);
        List<MGPReferenceDCDetails> GetMatInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg);
        IEnumerable<EntryIINote>GetDCNotes(string VehicleNo, DateTime FromDT, DateTime ToDT,bool IsMatOut, ref string pMsg);
        MGPReferenceDCDetails GetDCDetails(string NoteNumber, string VehicleNo, DateTime FromDT, DateTime ToDT, bool IsMatOut, ref string pMsg);
        List<MGPItemWiseDetails> GetMatOutItemWiseDetails(string NoteNumber, ref string pMsg);
        List<MGPItemWiseDetails> GetMatInItemWiseDetails(string NoteNumber, ref string pMsg);


    }
}
