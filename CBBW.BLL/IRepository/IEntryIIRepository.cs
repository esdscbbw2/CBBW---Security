using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;
using CBBW.BOL.MGP;

namespace CBBW.BLL.IRepository
{
    public interface IEntryIIRepository
    {
        List<RFID> GetRFIDCards(ref string pMsg);
        List<MGPReferenceDCDetails> GetMatOutDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg);
        List<MGPReferenceDCDetails> GetMatInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg);
        IEnumerable<EntryIINote>GetDCNotes(string VehicleNo, DateTime FromDT, DateTime ToDT,bool IsMatOut, ref string pMsg);
        MGPReferenceDCDetails GetDCDetails(string NoteNumber, string VehicleNo, DateTime FromDT, DateTime ToDT, bool IsMatOut, ref string pMsg);
        List<MGPItemWiseDetails> GetMatOutItemWiseDetails(string NoteNumber, ref string pMsg);
        List<MGPItemWiseDetails> GetMatInItemWiseDetails(string NoteNumber, ref string pMsg);
        List<EntryIINote> GetEntryIINotes(int CentreCode, bool IsMainLocation, ref string pMsg);
        List<EntryIIList> GetEntryIINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode,bool IsMainLocation, ref string pMsg);
        EditNoteDetails GetEditNoteHdr(string NoteNumber, ref string pMsg);
        List<EntryIITravelingDetails> GetEntryIITravellingDetails(string NoteNumber, ref string pMsg);
        VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string Notenumber, ref string pMsg);
        VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string NoteNumber, DateTime FromDate, DateTime ToDate, int CentreCode, bool IsMainLocation, ref string pMsg);
        List<MainLocationPersons> GetMainLocationTPs(string NoteNumber, ref string pMsg);
        LocationWiseTPDetails GetLocationWiseTPs(string NoteNumber, int CentreCode, ref string pMsg);
        bool SetEntryIIData(string NoteNumber, bool IsMainLocation,
                int CentreCode,bool IsOffline, List<SaveTPDetails> Persons, List<SaveTPDWDetails> DWTour, 
                List<SaveVehicleDetails> VAData, ref string pMsg);
        bool UpdateEntryIIData(string NoteNumber, int CentreCode, string CentreName, bool IsEPTour,
            bool IsMainLocation, ref string pMsg);
        PunchInDetails GetPunchingDetails(int EmployeeNumber, DateTime PunchDate, int CentreCode, string RFIDNumber, ref string pMsg);
        int GetTravelKmsOfANote(string NoteNumber, DateTime TillDate, int FromLocation, ref string pMsg);
        EntryIIInnerView GetEntryIIData(string NoteNumber, int CentreCode, bool IsMainlocation, ref string pMsg);
    }
}
