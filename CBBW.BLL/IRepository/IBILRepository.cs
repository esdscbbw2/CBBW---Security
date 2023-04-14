using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.BIL;
using CBBW.BOL.CustomModels;

namespace CBBW.BLL.IRepository
{
   public interface IBILRepository
    {
        List<IndexList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg);
        TADABillGeneration getNewNoteNumber(ref string pMsg);
        TADARuleData GetTAdARuleData(int EmployeeNumber, int CentreCode,string NoteNumber, ref string pMsg);
        List<NoteNo> GetNoteNumberList(int CentreCode, int status, ref string pMsg);
        IEnumerable<CustomCheckBoxOption> GetEmployeeList(string Notenumber, int CentreCode, ref string pMsg);
        TADABillGeneration GetNoteHdr(string Notenumber, ref string pMsg);
        List<DADetails> GetDADetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg);
        bool SetSetTADABillGeneration(TADABillGeneration hdrmodel, ref string pMsg);
         bool SetBillGenerationFinalSubmit(string NoteNumber,int status, ref string pMsg);
        TADABillGeneration GetTADABillGenerationData(string NoteNumber, string RefNoteNumber, int EmployeeNo, int status, ref string pMsg);
        bool RemoveBILNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg);
        IEnumerable<CustomOptionsWithString> GetBILNoteNumberList(int CentreCode, int status, ref string pMsg);
        bool SetApprovalTADABillGeneration(TADABillGeneration model, ref string pMsg);
        bool SetApprovalTADABillApprovalData(TADABillGeneration model, ref string pMsg);
        bool SetAnPFinalSubmit(int status, List<ApprovalNoteNo> dtldata, ref string pMsg);
        IEnumerable<CustomComboOptions> GetDeptWiseEmployeeList(int DeptId, int CentreCode, ref string pMsg);
        List<TravellingDetails> GetTraveelingDetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg);
         bool SetDeductionFormDA(TADABillGeneration model, ref string pMsg);
        List<TADAReport> GetTADACalculationDateWiseForReport(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg);
    }
}
