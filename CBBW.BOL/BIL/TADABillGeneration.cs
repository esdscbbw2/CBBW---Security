using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;

namespace CBBW.BOL.BIL
{
    public class NoteNo
    {
        public string NoteNumber { get; set; }
    }
    public class ApprovalNoteNo
    {
        public string NoteNumbers { get; set; }
    }

    public class TADABillGeneration
    {
        public int status { get; set; }
        public int CBUID { get; set; }
        public string HeaderText { get; set; }
        public int CanDelete { get; set; }
        public int SubmitCount { get; set; }
        public IEnumerable<NoteNo> RNotelist { get; set; }
        public IEnumerable<CustomOptionsWithString> customlist{ get; set; }
        public List<ApprovalNoteNo> NoteList { get; set; }
        public int EmpNo { get; set; }
        public int EmployeeNo { get; set; }
        public string EmployeeCodeName { get; set; }
        public string NoteNumber { get; set; }
        public string RefNoteNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDatestr { get; set; }
        public string EntryTime { get; set; }
        public DateTime RefEntryDate { get; set; }
        public string RefEntryDatestr { get; set; }

        public string RefEntryTime { get; set; }
        public int PersonType { get; set; }
        public string PersonTypetxt { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string CenterCodeName { get; set; }
        public int DesigCode { get; set; }
        public string DesigName { get; set; }
        public string DesigCodeName { get; set; }
        public long DAAmount { get; set; }
        public long DADeducted { get; set; }
        public long EDAllowance { get; set; }
        public long TAAmount { get; set; }
        public long LocalConveyance { get; set; }
        public long Lodging { get; set; }
        public long TotalExpenses { get; set; }
        public string TourFromDateNTime { get; set; }
        public DateTime TourFromDate { get; set; }
        public string TourFromTime { get; set; }
        public string TourToDateNTime { get; set; }
        public DateTime TourToDate { get; set; }
        public string TourToTime { get; set; }
        public int NoOfDays { get; set; }
        public string PurposeOfVisit { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ApprovalDatestr { get; set; }
        public string ApprovalTime { get; set; }
        public string ApprovalReason { get; set; }
        public bool IsActive { get; set; }
        //For Approval and Payment Details
	    public DateTime AEntryDT { get; set; }
        public float AEDAmount { get; set; }
        public float ATAAmount { get; set; }
        public float ALocAmount { get; set; }
        public float ALodAmount { get; set; }
        public float ATotalAmount { get; set; }
        public string AReamrk { get; set; }
        public DateTime EEntryDT { get; set; }
        public float EEDAmount { get; set; }
        public float ETAAmount { get; set; }
        public float ELocAmount { get; set; }
        public float ELodAmount { get; set; }
        public float ETotalAmount { get; set; }
        public string EReamrk { get; set; }

        public int DeptCode { get; set; }
        public string DeptName { get; set; }
        public int RequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public string RequisitionDatestr { get; set; }
        public string RequisitionDateMin { get; set; }
        public string RequisitionDateMax { get; set; }
        public int PreparedEmpNo { get; set; }
        public string PreparedEmpName { get; set; }
        public float RequisitionAmt { get; set; }
        public string Remark { get; set; }
        public float NetAmount { get; set; }


    }

    public class IndexList
    {
        public string NoteNumber { get; set; }
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string CenterCodeName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public bool? IsApproved { get; set; }
        public string IsApproveds { get; set; }
        public bool CanDelete { get; set; }

    }
}
