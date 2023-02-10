using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMC;

namespace CBBW.BLL.IRepository
{
    public interface IEMCRepository
    {
        EMCHeader getNewEMCHeader(ref string pMsg);
        List<EMCNoteList> GetEMCNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        bool SetEMCTravellingPerson(string NoteNumber, int CenterCode, string CenterCodeName, List<EMCTravellingPerson> dtldata, ref string pMsg);
        bool setEMCTravDetailsNTourDetails(string NoteNumber, List<EMCTravellingDetails> TDdata, List<EMCDateWiseTour> DWTdata, ref string pMsg);
        List<EMCTravellingPerson> GetEMCTravellingPerson(string Notenumber, int CenterCode,int status, ref string pMsg);
        bool SetEMCDetailsFinalSubmit(EMCHeader hdrmodel, ref string pMsg);
        EMCHeader GetEMCHdrEntry(string Notenumber, ref string pMsg);
        EMCTravellingDetails GetEMCTravellingDetails(string Notenumber, ref string pMsg);
        List<EMCDateWiseTour> GetEMCDateWiseTour(string Notenumber, ref string pMsg);
        bool RemoveEMCNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg);
        List<EMCNote> GetEMCNoteListToBeApproved(int CentreCode, int status, ref string pMsg);
        bool SetEMCApprovalData(EMCApproveTravDetails model, ref string pMsg);
        
        IEnumerable<TPEPNote> GetEPTourNoteNumber(int EmployeeNumber, int CentreCode, ref string pMsg);
    }
}
