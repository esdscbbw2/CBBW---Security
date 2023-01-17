using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.ETS;

namespace CBBW.BLL.IRepository
{
    public interface IETSRepository
    {
        ETSHeader getNewETSHeader(ref string pMsg);
        bool SetETSTravellingPerson(string NoteNumber, List<ETSTravellingPerson> dtldata, ref string pMsg);
        bool setETSTravDetailsNTourDetails(string NoteNumber, List<ETSTravellingDetails> TDdata, List<ETSDateWiseTour> DWTdata, ref string pMsg);
        List<ETSTravellingPerson> GetETSTravellingPerson(string NoteNumber, ref string pMsg);
        bool SetETSDetailsFinalSubmit(ETSHeader hdrmodel, ref string pMsg);
        List<ETSNoteList> GetETSNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode,int status, ref string pMsg);
        ETSHeader GetETSHdrEntry(string Notenumber, ref string pMsg);
        ETSTravellingDetails GetETSTravellingDetails(string Notenumber, ref string pMsg);
        List<ETSDateWiseTour> GetETSDateWiseTour(string Notenumber, ref string pMsg);
        bool RemoveETSNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg);
         List<ETSNote> GetETSNoteListToBeApproved(int CentreCode, int status, ref string pMsg);
        bool SetETSApprovalData(ETSApproveTravDetails model, ref string pMsg);
        bool SetETSRatifiedData(ETSRatified model, ref string pMsg);
    }
}
