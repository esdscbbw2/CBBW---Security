using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.BIL;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class BILRepository : IBILRepository
    {
        BILEntities _BILEntities;
        MasterEntities _MasterEntities;
        string NotePattern = "200001-BIL-" + DateTime.Today.ToString("yyyyMMdd") + "-";
       
        UserRepository _user;
        UserInfo user;
        TFDEntities _TFDEntities;
        TourEntities _tourEntities;
        public BILRepository()
        {
            _BILEntities = new BILEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            _TFDEntities = new TFDEntities();
            user = _user.getLoggedInUser();
        }

        public IEnumerable<CustomOptionsWithString> GetBILNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            return _BILEntities.GetBILNoteNumberList(CentreCode, status, ref pMsg);
        }

        public List<DADetails> GetDADetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            return _BILEntities.GetDADetails(EmployeeNumber, CentreCode, NoteNumber,ref pMsg);
        }

        public IEnumerable<CustomComboOptions> GetDeptWiseEmployeeList(int DeptId, int CentreCode, ref string pMsg)
        {
            return _BILEntities.GetDeptWiseEmployeeList(DeptId, CentreCode, ref pMsg);
        }

        public IEnumerable<CustomCheckBoxOption> GetEmployeeList(string Notenumber, int CentreCode, ref string pMsg)
        {
            return _BILEntities.GetEmployeeList(Notenumber, CentreCode, ref pMsg);
        }

        public List<IndexList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _BILEntities.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }

        public TADABillGeneration getNewNoteNumber(ref string pMsg)
        {
            TADABillGeneration obj = new TADABillGeneration();
            obj.NoteNumber = _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterCode = user.CentreCode;
            obj.CenterName = user.CentreName;
            obj.CenterCodeName = obj.CenterCode + " / " + obj.CenterName;
            return obj;
        }

        public TADABillGeneration GetNoteHdr(string Notenumber, ref string pMsg)
        {
            return _BILEntities.GetNoteHdr(Notenumber,ref pMsg);
        }

        public List<NoteNo> GetNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            return _BILEntities.GetNoteNumberList(CentreCode, status, ref pMsg);
        }

        public TADABillGeneration GetTADABillGenerationData(string NoteNumber, string RefNoteNumber, int EmployeeNo, int status, ref string pMsg)
        {
            return _BILEntities.GetTADABillGenerationData(NoteNumber, RefNoteNumber, EmployeeNo, status, ref pMsg);
        }

        public List<TADAReport> GetTADACalculationDateWiseForReport(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            return _BILEntities.GetTADACalculationDateWiseForReport(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
        }

        public TADARuleData GetTAdARuleData(int EmployeeNumber, int CentreCode,string NoteNumber, ref string pMsg)
        {
            return _BILEntities.GetTAdARuleData(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
        }

        public List<TravellingDetails> GetTraveelingDetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            return _BILEntities.GetTraveelingDetails(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
        }

        public bool RemoveBILNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _BILEntities.RemoveBILNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg);
        }

        public bool SetAnPFinalSubmit(int status, List<ApprovalNoteNo> dtldata, ref string pMsg)
        {
            return _BILEntities.SetAnPFinalSubmit(status, dtldata, ref pMsg);
        }

        public bool SetApprovalTADABillGeneration(TADABillGeneration model, ref string pMsg)
        {
            return _BILEntities.SetApprovalTADABillGeneration(model, ref pMsg);
        }

        public bool SetBillGenerationFinalSubmit(string NoteNumber, int status, ref string pMsg)
        {
            return _BILEntities.SetBillGenerationFinalSubmit(NoteNumber, status, ref pMsg);
        }

        public bool SetDeductionFormDA(TADABillGeneration model, ref string pMsg)
        {
            return _BILEntities.SetDeductionFormDA(model, ref pMsg);
        }

        public bool SetSetTADABillGeneration(TADABillGeneration hdrmodel, ref string pMsg)
        {
            return _BILEntities.SetSetTADABillGeneration( hdrmodel, ref  pMsg);
        }
    }
}
