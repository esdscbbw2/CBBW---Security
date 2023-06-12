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
        List<GVMRDetails> GetCenterWiseGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg);
        GVMRHeader GetGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg);
        List<PunchingDetails> GetPunchingDetails(string CentreCode, DateTime FromDate, DateTime ToDate, int UserID, ref string pMsg);
        bool setGVMRDetails(GVMRDataSave gvmrdata, ref string pMsg);
        List<GVMRNoteList> getGVMRDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText,int CenterCode, ref string pMsg);
        GVMRHeader getGVMRDetailsForView(string NoteNumber, int CenterCode, ref string pMsg);
        bool SetGVMRDetailsV2(List<GVMRDataSave> dtldata, ref string pMsg);
        // GVMRHeader GetGVMRDetailsWithPunchingDetails(string NoteNumber, int CenterCode, ref string pMsg);


    }
}
