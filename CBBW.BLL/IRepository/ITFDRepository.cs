using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TFD;

namespace CBBW.BLL.IRepository
{
    public interface ITFDRepository
    {
        TFDHdr getNewTFDNoteNumber(ref string pMsg);
        List<TFDNote> GetNoteNumberList(int CentreCode, int status, ref string pMsg);
        List<TFDTravellingPerson> GetENTTravellingPerson(string Notenumber, int CenterCode, int status, ref string pMsg);
        List<TFDDateWiseTourData> GetENTDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre,int status, ref string pMsg);
        IEnumerable<CustomCheckBoxOption> GetENTAuthEmployeeList(string Notenumber, int CentreCode, ref string pMsg);
        TFDHdr GetTFDHeaderData(string Notenumber, int CenterCode, int status, ref string pMsg);
        bool SetTFDFeedBackDetails(string NoteNumber, List<TFDTourFeedBackDetails> dtldata, ref string pMsg);
        bool SetTFDetailsFinalSubmit(TFDHdr hdrmodel, ref string pMsg);
        List<TFDNoteList> GetTFDDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        List<TFDTourFeedBackDetails> GetTFDTourFeedBackDetails(string Notenumber, int CenterCode, int status, ref string pMsg);
        TFDHdr GetTFDHeaderDetails(string Notenumber, int CenterCode, int status, ref string pMsg);
        bool RemoveTFDNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg);
         IEnumerable<CustomComboOptions> GetENTConcernDeptList(string Notenumber, int CentreCode, ref string pMsg);
        bool SetTFDFeedBackApproval(string NoteNumber, List<TFDTourFBApproval> dtldata, ref string pMsg);
         bool SetTFDDateWiseTourData(string NoteNumber, bool IsApprove, string ApproveReason, List<TFDDateWiseTourData> dtldata, ref string pMsg);
         List<TFDDateWiseTourData> GetTFDDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre, int status, ref string pMsg);
        string GetENTTourCategroy(string NoteNumber, ref string pMsg);
    }
}
