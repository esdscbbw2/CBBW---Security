using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.EntryII;
using CBBW.BOL.MGP;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class EntryIIRepository : IEntryIIRepository
    {
        MGPEntities _MGPEntities;
        public EntryIIRepository()
        {
            _MGPEntities = new MGPEntities();
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



    }
}
