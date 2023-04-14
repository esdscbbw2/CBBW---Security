using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.BIL;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBLogic;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
   public class BILEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        BILDataSync _datasync;
        BILDBMapper _DBMapper;
        DBResponseMapper _DBResponseMapper;
        public BILEntities()
        {
            _datasync = new BILDataSync();
            _DBMapper = new BILDBMapper();
            _DBResponseMapper = new DBResponseMapper();

        }
        public TADARuleData GetTAdARuleData(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            TADARuleData result = new TADARuleData();
            try
            {
                dt = _datasync.GetTAdARuleData(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = _DBMapper.Map_TADARuleData(dt.Rows[0]);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<IndexList> GetIndexListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            List<IndexList> result = new List<IndexList>();
            try
            {
                dt = _datasync.GetIndexListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBMapper.Map_IndexList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<NoteNo> GetNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            List<NoteNo> result = new List<NoteNo>();
            try
            {
                dt = _datasync.GetNoteNumberList(CentreCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        NoteNo x = new NoteNo();
                        if (!DBNull.Value.Equals(dt.Rows[i]["NoteNumber"]))
                        {
                            x.NoteNumber = dt.Rows[i]["NoteNumber"].ToString();
                        }
                        result.Add(x);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomCheckBoxOption> GetEmployeeList(string Notenumber, int CentreCode, ref string pMsg)
        {
            List<CustomCheckBoxOption> result = new List<CustomCheckBoxOption>();
            try
            {
                dt = _datasync.GetEmployeeList(Notenumber, CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomCheckBoxOption(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomComboOptions> GetDeptWiseEmployeeList(int DeptId, int CentreCode, ref string pMsg)
        {
            List<CustomComboOptions> result = new List<CustomComboOptions>();
            try
            {
                dt = _datasync.GetDeptWiseEmployeeList(DeptId, CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomComboOptions(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public IEnumerable<CustomOptionsWithString> GetBILNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            List<CustomOptionsWithString> result = new List<CustomOptionsWithString>();
            try
            {
                dt = _datasync.GetBILNoteNumberList(CentreCode, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_DBResponseMapper.Map_CustomOptionsWithString(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public TADABillGeneration GetNoteHdr(string Notenumber, ref string pMsg)
        {
            TADABillGeneration result = new TADABillGeneration();
            try
            {
                dt = _datasync.GetNoteHdr(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["EntryDate"]))
                        result.RefEntryDate = DateTime.Parse(dt.Rows[0]["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dt.Rows[0]["EntryTime"]))
                        result.RefEntryTime =dt.Rows[0]["EntryTime"].ToString();
                    result.RefEntryDatestr= MyDBLogic.ConvertDateToString(result.RefEntryDate);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<DADetails> GetDADetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            List<DADetails> result = new List<DADetails>();
            try
            {
                dt = _datasync.GetDADetails(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            result.Add(_DBMapper.Map_DADetails(dt.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetSetTADABillGeneration(TADABillGeneration hdrmodel, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetSetTADABillGeneration(hdrmodel, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetBillGenerationFinalSubmit(string NoteNumber, int status, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetBillGenerationFinalSubmit(NoteNumber, status, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public TADABillGeneration GetTADABillGenerationData(string NoteNumber, string RefNoteNumber, int EmployeeNo, int status, ref string pMsg)
        {
            TADABillGeneration result = new TADABillGeneration();
            try
            {
                dt = _datasync.GetTADABillGenerationData(NoteNumber, RefNoteNumber, EmployeeNo, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _DBMapper.Map_TADABillGeneration(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool RemoveBILNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.RemoveBILNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetApprovalTADABillGeneration(TADABillGeneration model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetApprovalTADABillGeneration(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetTADABillGenerationApprovalData(TADABillGeneration model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetTADABillGenerationApprovalData(model, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool SetAnPFinalSubmit(int status, List<ApprovalNoteNo> dtldata, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetAnPFinalSubmit(status, dtldata, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<TravellingDetails> GetTraveelingDetails(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            List<TravellingDetails> result = new List<TravellingDetails>();
            try
            {
                dt = _datasync.GetTraveelingDetails(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            result.Add(_DBMapper.Map_TravellingDetails(dt.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public bool SetDeductionFormDA(TADABillGeneration model, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.SetDeductionFormDA(model, ref pMsg), ref pMsg, ref result);
            return result;
        }


        public List<TADAReport> GetTADACalculationDateWiseForReport(int EmployeeNumber, int CentreCode, string NoteNumber, ref string pMsg)
        {
            List<TADAReport> result = new List<TADAReport>();
            try
            {
                dt = _datasync.GetTADACalculationDateWiseForReport(EmployeeNumber, CentreCode, NoteNumber, ref pMsg);
                if (dt != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            result.Add(_DBMapper.Map_TADAReport(dt.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
    }
}
