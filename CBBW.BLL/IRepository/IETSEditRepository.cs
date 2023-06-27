using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;

namespace CBBW.BLL.IRepository
{
    public interface IETSEditRepository
    {
        int getEditSL(string NoteNumber, ref string pMsg);
        List<EditNoteNumber> getETSNoteListToBeEdited(int CentreCode, ref string pMsg);
        List<EditNoteNumber> getETSEditNoteListForDropDown(int CentreCode, int mStatus, ref string pMsg);
        EditNoteDetails getEditNoteHdr(string NoteNumber, ref string pMsg);
        IEnumerable<EditTPDetails> getEditTPDetails(string NoteNumber, ref string pMsg);
        //List<EditDWTDetails> getCurrentDateWiseTour(string NoteNumber, int FieldTag, ref string pMsg);
        List<EditDWTDetails> getDateWiseTourHistory(string NoteNumber, int FieldTag, int PersonType, int PersonID, string PersonName, ref string pMsg, bool IsActive);
        bool SetETSTourEdit(DWTTourDetailsForDB obj, int CentreCode, string CentreName, ref string pMsg);
        bool UpdateETSTourEdit(string NoteNumber, ref string pMsg);
        List<EditNoteList> GetETSEditNoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, int IsApprovedList, ref string pMsg);
        bool RemoveETSEditNote(string NoteNumber, int ActiveTag, ref string pMsg);
        bool SetETSEditRatificationStatus(string NoteNumber, bool IsRatified, string RatReason, int ApproverID, ref string pMsg);
        bool SetETSEditAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval, int ApproverID, ref string pMsg);
        EditNoteDetails getETSEditHdr(string NoteNumber, int LockStatus, ref string pMsg);
        bool SetETSVehicleAllotmentDetails(VehicleAllotmentDetails mData, int CentreCode, string CentreName, ref string pMsg);
        VehicleAllotmentDetails GetVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg);
        EntryITourDetails GetEntryITourData(string Notenumber, int IsActive, ref string pMsg);
        EditNoteDetails GetNoteHdrForEntryI(string NoteNumber, int LockStatus, ref string pMsg);
        List<EditNoteNumber> GetNoteListForEntryI(int CentreCode, ref string pMsg);
        List<EntryINoteList> GetEntryINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, ref string pMsg);
        bool RemoveEntryINote(string NoteNumber, bool ActiveTag, ref string pMsg);
        IEnumerable<NoteDriver> GETDriverList(string NoteNumber, ref string pMsg);
        bool UpdateETSVehicleAllotmentDetails(string NoteNumber, ref string pMsg);
        bool IsTourStarted(string NoteNumber, ref string pMsg);
    }
}
