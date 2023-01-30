using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.ETSEdit;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class ETSEditRepository : IETSEditRepository
    {
        ETSEditEntities _ETSEditEntities;
        public ETSEditRepository()
        {
            _ETSEditEntities = new ETSEditEntities();
        }
        public EditNoteDetails getEditNoteHdr(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.getEditNoteHdr(NoteNumber, ref pMsg);
        }
        public int getEditSL(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.getEditSL(NoteNumber, ref pMsg);
        }
        public List<EditNoteNumber> getETSNoteListToBeEdited(int CentreCode, ref string pMsg)
        {
            return _ETSEditEntities.getETSNoteListToBeEdited(CentreCode, ref pMsg);
        }
        public IEnumerable<EditTPDetails> getEditTPDetails(string NoteNumber, ref string pMsg) 
        {
            return _ETSEditEntities.getEditTPDetails(NoteNumber, ref pMsg);
        }
        //public List<EditDWTDetails> getCurrentDateWiseTour(string NoteNumber, int FieldTag, ref string pMsg)
        //{
        //    int maxrowid = 0;
        //    List<EditDWTDetails> objlist = _ETSEditEntities.getCurrentDateWiseTour(NoteNumber, FieldTag, ref pMsg);
        //    if (objlist != null) { maxrowid = objlist.Max(o => o.EditSL); }
        //    List<EditDWTDetails> result = objlist.Where(o => o.EditSL == maxrowid).ToList();
        //    return result;
        //}
        public List<EditDWTDetails> getDateWiseTourHistory(string NoteNumber, int FieldTag, int PersonType, int PersonID, string PersonName, ref string pMsg)
        {
            List<EditDWTDetails> objlist = _ETSEditEntities.getCurrentDateWiseTour(NoteNumber, FieldTag,PersonType,PersonID,PersonName,ref pMsg);
            return objlist;
        }
        public bool SetETSTourEdit(DWTTourDetailsForDB obj, int CentreCode, string CentreName, ref string pMsg) 
        {
            return _ETSEditEntities.SetETSTourEdit(obj,CentreCode,CentreName, ref pMsg);
        }
        public bool UpdateETSTourEdit(string NoteNumber, ref string pMsg) 
        {
            return _ETSEditEntities.UpdateETSTourEdit(NoteNumber, ref pMsg);
        }
        public List<EditNoteList> GetETSEditNoteList(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CentreCode, bool IsApprovedList, ref string pMsg)
        {
            return _ETSEditEntities.GetETSEditNoteList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CentreCode, IsApprovedList, ref pMsg);
        }


    }
}
