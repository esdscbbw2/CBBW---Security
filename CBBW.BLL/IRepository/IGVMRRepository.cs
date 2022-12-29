using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.GVMR;

namespace CBBW.BLL.IRepository
{
   public interface IGVMRRepository
    {
        IEnumerable<GVMRNoteNumber> GetNoteNumbers(int CenterCode,int status, ref string pMsg);
        List<GVMRDetails> GetGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg);

        bool setGVMRDetails(GVMRDataSave gvmrdata, ref string pMsg);
        List<GVMRNoteList> getGVMRDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText,int CenterCode, ref string pMsg);
        List<GVMRDetails> getGVMRDetailsForView(string NoteNumber, int CenterCode, ref string pMsg);
    }
}
