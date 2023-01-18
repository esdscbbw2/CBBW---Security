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
    }
}
