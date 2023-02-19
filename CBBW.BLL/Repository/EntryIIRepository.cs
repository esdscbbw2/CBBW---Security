using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;
using CBBW.BOL.MGP;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class EntryIIRepository : IEntryIIRepository
    {
        MGPEntities _MGPEntities;
        EntryIIEntities _EntryIIEntities;
        ETSEditEntities _ETSEditEntities;
        public EntryIIRepository()
        {
            _MGPEntities = new MGPEntities();
            _EntryIIEntities = new EntryIIEntities();
            _ETSEditEntities = new ETSEditEntities();
        }
        public List<MGPReferenceDCDetails> GetMatOutDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            return _MGPEntities.getReferenceDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
        }
        public List<MGPReferenceDCDetails> GetMatInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            return _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
        }
        public IEnumerable<EntryIINote> GetDCNotes(string VehicleNo, DateTime FromDT, DateTime ToDT, bool IsMatOut, ref string pMsg)
        {
            List<EntryIINote> result = new List<EntryIINote>();
            try
            {
                List<MGPReferenceDCDetails> objList = new List<MGPReferenceDCDetails>();
                if (IsMatOut)
                {
                    objList = _MGPEntities.getReferenceDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                else
                {
                    objList = _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                if (objList != null && objList.Count > 0)
                {
                    foreach (var obj in objList)
                    {
                        EntryIINote note = new EntryIINote();
                        note.NoteNumber = obj.NoteNumber;
                        result.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public MGPReferenceDCDetails GetDCDetails(string NoteNumber, string VehicleNo, DateTime FromDT, DateTime ToDT, bool IsMatOut, ref string pMsg)
        {
            MGPReferenceDCDetails result = new MGPReferenceDCDetails();
            try
            {
                List<MGPReferenceDCDetails> objList = new List<MGPReferenceDCDetails>();
                if (IsMatOut)
                {
                    objList = _MGPEntities.getReferenceDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                else
                {
                    objList = _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                result = objList.Where(o => o.NoteNumber == NoteNumber).FirstOrDefault();
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public List<MGPItemWiseDetails> GetMatOutItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getItemWiseDetails(NoteNumber, ref pMsg);
        }
        public List<MGPItemWiseDetails> GetMatInItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getItemWiseInDetails(NoteNumber, ref pMsg);
        }
        public List<EntryIINote> GetEntryIINotes(int CentreCode, bool IsMainLocation, ref string pMsg) 
        {
            return _EntryIIEntities.GetEntryIINotes(CentreCode, IsMainLocation, ref pMsg);
        }
        public List<EntryIIList> GetEntryIINoteList(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIINoteList(DisplayLength,DisplayStart,SortColumn,SortDirection,SearchText,CentreCode,IsMainLocation,ref pMsg);
        }
        public EditNoteDetails GetEditNoteHdr(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.getEditNoteHdr(NoteNumber, ref pMsg);
        }
        public List<EntryIITravelingDetails> GetEntryIITravellingDetails(string NoteNumber, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIITravellingDetails(NoteNumber, ref pMsg);
        }
        public VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string Notenumber, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIIVehicleAllotmentDetails(Notenumber,ref pMsg);
        }
    }
}
