using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMN;

namespace CBBW.BLL.IRepository
{
    public interface IEMNRepository
    {
        EMNHeader getNewEMNHeader(ref string pMsg);
        List<EMNNoteList> GetEMNNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        IEnumerable<CustomComboOptions> getCenterCodeList(int center, ref string pMsg);
        IEnumerable<CustomComboOptions> getCenterCodeListFromTravellingPerson(string NoteNumber,int status, ref string pMsg);
        bool SetEMNTravellingPerson(string NoteNumber, int CenterCode, string CenterCodeName, List<EMNTravellingPerson> dtldata, ref string pMsg);
        bool setEMNTravDetailsNTourDetails(string NoteNumber, List<EMNTravellingDetails> TDdata, List<EMNDateWiseTour> DWTdata, ref string pMsg);
        List<EMNTravellingPerson> GetEMNTravellingPerson(string Notenumber, int CenterCode, ref string pMsg);
        bool SetEMNDetailsFinalSubmit(EMNHeader hdrmodel, ref string pMsg);
        EMNHeader GetEMNHdrEntry(string Notenumber, ref string pMsg);
        EMNTravellingDetails GetEMNTravellingDetails(string Notenumber, ref string pMsg);
        List<EMNDateWiseTour> GetEMNDateWiseTour(string Notenumber, ref string pMsg);
        bool RemoveEMNNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg);
        List<EMNNote> GetEMNNoteListToBeApproved(int CentreCode, int status, ref string pMsg);
        bool SetEMNApprovalData(EMNApproveTravDetails model, ref string pMsg);
        bool SetEMNRatifiedData(EMNRatified model, ref string pMsg);
    }
}
