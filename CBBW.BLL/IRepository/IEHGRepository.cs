using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;

namespace CBBW.BLL.IRepository
{
    public interface IEHGRepository
    {
        EHGHeader getNewEHGHeader(ref string pMsg);
        bool SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtlsForManagement dtl, ref string pMsg);
        bool SetEHGTravellingPersonDetails(string NoteNumber, string AuthEmp, List<EHGTravelingPersondtls> dtldata, ref string pMsg);
        bool SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg);
        bool SetEHGVehicleAllotmentDetails(VehicleAllotmentDetails mData, ref string pMsg);
        bool UpdateEHGHdr(EHGHeader header, ref string pMsg);
        List<DateWiseTourDetails> getDateWiseTourDetails(string Notenumber, int IsActive, ref string pMsg);
        List<EHGTravelingPersondtlsForManagement> getTravelingPersonDetails(string Notenumber, int IsActive, ref string pMsg);
        VehicleAllotmentDetails getVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg);
        EHGHeader getEHGNoteHdr(string Notenumber, ref string pMsg);
        bool RemoveEHGNote(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg);
        List<EHGNoteList> GetEHGNoteList(int DisplayLength, int DisplayStart, int SortColumn, 
            string SortDirection, string SearchText, int CentreCode,bool IsApprovedList, ref string pMsg);
        List<EHGNote> getNoteListToBeApproved(int CentreCode,ref string pMsg);
        bool SetEHGHdrAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval,
            int ApproverID, ref string pMsg);
    }
}
