using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.ETSEdit;

namespace CBBW.BLL.IRepository
{
    public interface IETSEditRepository
    {
        int getEditSL(string NoteNumber, ref string pMsg);
        List<EditNoteNumber> getETSNoteListToBeEdited(int CentreCode, ref string pMsg);
        EditNoteDetails getEditNoteHdr(string NoteNumber, ref string pMsg);
    }
}
