using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.BIL;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
   public class BILDBMapper
    {
        public IndexList Map_IndexList(DataRow dr)
        {
            IndexList result = new IndexList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    result.EntryDateDisplay = MyDBLogic.ConvertDateToString(result.EntryDate);
                    if (!result.IsApproved.HasValue && result.EntryDate == DateTime.Today)
                    { result.CanDelete = true; }

                }
            }
            catch { }
            return result;
        }
        public TADARuleData Map_TADARuleData(DataRow dr)
        {
            TADARuleData result = new TADARuleData();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CentreCode = int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ZoneCode"]))
                        result.ZoneCode = int.Parse(dr["ZoneCode"].ToString());
                    if (!DBNull.Value.Equals(dr["MinHoursForHalfDA"]))
                        result.MinHoursForHalfDA = int.Parse(dr["MinHoursForHalfDA"].ToString());
                    if (!DBNull.Value.Equals(dr["MinKmsForDA"]))
                        result.MinKmsForDA = int.Parse(dr["MinKmsForDA"].ToString());
                    if (!DBNull.Value.Equals(dr["LodgingExpOnCompAcco"]))
                        result.LodgingExpOnCompAcco = bool.Parse(dr["LodgingExpOnCompAcco"].ToString());
                    if (!DBNull.Value.Equals(dr["LocalConvEligibility"]))
                        result.LocalConvEligibility = bool.Parse(dr["LocalConvEligibility"].ToString());
                    if (!DBNull.Value.Equals(dr["DepuStaffDAEligibility"]))
                        result.DepuStaffDAEligibility = bool.Parse(dr["DepuStaffDAEligibility"].ToString());
                    if (!DBNull.Value.Equals(dr["ExtraDAApplicability"]))
                        result.ExtraDAApplicability = bool.Parse(dr["ExtraDAApplicability"].ToString());
                    if (!DBNull.Value.Equals(dr["IsDAActualSpend"]))
                        result.IsDAActualSpend = bool.Parse(dr["IsDAActualSpend"].ToString());
                    if (!DBNull.Value.Equals(dr["IsLodgingAllowed"]))
                        result.IsLodgingAllowed = bool.Parse(dr["IsLodgingAllowed"].ToString());
                    if (!DBNull.Value.Equals(dr["DAPerDay"]))
                        result.DAPerDay = int.Parse(dr["DAPerDay"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxLodgingExp"]))
                        result.MaxLodgingExp = int.Parse(dr["MaxLodgingExp"].ToString());
                    if (!DBNull.Value.Equals(dr["MaxLocalConv"]))
                        result.MaxLocalConv = int.Parse(dr["MaxLocalConv"].ToString());
                   
                    
                    if (!DBNull.Value.Equals(dr["ActualTourInDate"]))
                        result.ActualTourInDate = DateTime.Parse(dr["ActualTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutDate"]))
                        result.ActualTourOutDate = DateTime.Parse(dr["ActualTourOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalNoOfDays"]))
                        result.TotalNoOfDays = int.Parse(dr["TotalNoOfDays"].ToString());
                    if (!DBNull.Value.Equals(dr["DAAmount"]))
                        result.DAAmount = int.Parse(dr["DAAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["DADeducted"]))
                        result.DADeducted = int.Parse(dr["DADeducted"].ToString());
                    if (!DBNull.Value.Equals(dr["EAmount"]))
                        result.EAmount = int.Parse(dr["EAmount"].ToString());

                    if (!DBNull.Value.Equals(dr["DesginationCodeName"]))
                        result.DesginationCodeName = dr["DesginationCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = dr["PersonType"].ToString();
                    result.ActualTourInDatestr = MyDBLogic.ConvertDateToString(result.ActualTourInDate);
                    result.ActualTourOutDatestr = MyDBLogic.ConvertDateToString(result.ActualTourOutDate);

                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public DADetails Map_DADetails(DataRow dr)
        {
            DADetails result = new DADetails();
            try
            {
                if (dr != null)
                {
                    
                    if (!DBNull.Value.Equals(dr["ActualTourInDate"]))
                        result.ActualTourInDate = DateTime.Parse(dr["ActualTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["DAAmount"]))
                        result.DAAmount = int.Parse(dr["DAAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["DADeducted"]))
                        result.DADeducted = int.Parse(dr["DADeducted"].ToString());
                    if (!DBNull.Value.Equals(dr["EAmount"]))
                        result.EAmount = int.Parse(dr["EAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalHours"]))
                        result.TotalHours = int.Parse(dr["TotalHours"].ToString());
                    result.ActualTourInDatestr = MyDBLogic.ConvertDateToString(result.ActualTourInDate);
                   

                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public TADABillGeneration Map_TADABillGeneration(DataRow dr)
        {
            TADABillGeneration result = new TADABillGeneration();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["RefNoteNumber"]))
                        result.RefNoteNumber = dr["RefNoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["EmployeeCode"]))
                        result.EmployeeNo = int.Parse(dr["EmployeeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeCodeName"]))
                        result.EmployeeCodeName = dr["EmployeeCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["RefEntryDate"]))
                        result.RefEntryDate = DateTime.Parse(dr["RefEntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["RefEntryTime"]))
                        result.RefEntryTime = dr["RefEntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonTypetxt"]))
                        result.PersonTypetxt = dr["PersonTypetxt"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesigCode"]))
                        result.DesigCode = int.Parse(dr["DesigCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesigCodeName"]))
                        result.DesigCodeName = dr["DesigCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["DAAmount"]))
                        result.DAAmount = int.Parse(dr["DAAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["DADeducted"]))
                        result.DADeducted = int.Parse(dr["DADeducted"].ToString());
                    if (!DBNull.Value.Equals(dr["EDAllowance"]))
                        result.EDAllowance = int.Parse(dr["EDAllowance"].ToString());
                    if (!DBNull.Value.Equals(dr["TAAmount"]))
                        result.TAAmount = int.Parse(dr["TAAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["LocalConveyance"]))
                        result.LocalConveyance = int.Parse(dr["LocalConveyance"].ToString());
                    if (!DBNull.Value.Equals(dr["Lodging"]))
                        result.Lodging = int.Parse(dr["Lodging"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalExpenses"]))
                        result.TotalExpenses = int.Parse(dr["TotalExpenses"].ToString());
                    if (!DBNull.Value.Equals(dr["TourFromDate"]))
                        result.TourFromDate = DateTime.Parse(dr["TourFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourToDate"]))
                        result.TourToDate = DateTime.Parse(dr["TourToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["NoOfDays"]))
                        result.NoOfDays = int.Parse(dr["NoOfDays"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovalDate"]))
                        result.ApprovalDate = DateTime.Parse(dr["ApprovalDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovalTime"]))
                        result.ApprovalTime = dr["ApprovalTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ApprovalReason"]))
                        result.ApprovalReason = dr["ApprovalReason"].ToString();


                    if (!DBNull.Value.Equals(dr["AEDAmount"]))
                        result.AEDAmount = float.Parse(dr["AEDAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ATAAmount"]))
                        result.ATAAmount = float.Parse(dr["ATAAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ALocAmount"]))
                        result.ALocAmount = float.Parse(dr["ALocAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ALodAmount"]))
                        result.ALodAmount = float.Parse(dr["ALodAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ATotalAmount"]))
                        result.ATotalAmount = float.Parse(dr["ATotalAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["AReamrk"]))
                        result.AReamrk = dr["AReamrk"].ToString();
                    if (!DBNull.Value.Equals(dr["EEDAmount"]))
                        result.EEDAmount = float.Parse(dr["EEDAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ETAAmount"]))
                        result.ETAAmount = float.Parse(dr["ETAAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ELocAmount"]))
                        result.ELocAmount = float.Parse(dr["ELocAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ELodAmount"]))
                        result.ELodAmount = float.Parse(dr["ELodAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["ETotalAmount"]))
                        result.ETotalAmount = float.Parse(dr["ETotalAmount"].ToString());
                    if (!DBNull.Value.Equals(dr["EReamrk"]))
                        result.EReamrk = dr["EReamrk"].ToString();
                    //For DA Deduction Page
                    if (!DBNull.Value.Equals(dr["DeptCode"]))
                        result.DeptCode = int.Parse(dr["DeptCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DeptName"]))
                        result.DeptName = dr["DeptName"].ToString();
                    if (!DBNull.Value.Equals(dr["RequisitionNo"]))
                        result.RequisitionNo = int.Parse(dr["RequisitionNo"].ToString());
                    if (!DBNull.Value.Equals(dr["RequisitionDate"]))
                        result.RequisitionDate = DateTime.Parse(dr["RequisitionDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PreparedEmpNo"]))
                        result.PreparedEmpNo = int.Parse(dr["PreparedEmpNo"].ToString());
                    if (!DBNull.Value.Equals(dr["PreparedEmpName"]))
                        result.PreparedEmpName = dr["PreparedEmpName"].ToString();
                    if (!DBNull.Value.Equals(dr["RequisitionAmt"]))
                        result.RequisitionAmt = float.Parse(dr["RequisitionAmt"].ToString());
                    if (!DBNull.Value.Equals(dr["Remark"]))
                        result.Remark = dr["Remark"].ToString();


                    result.RequisitionDatestr = MyDBLogic.ConvertDateToString(result.RequisitionDate);
                    result.TourFromDateNTime = MyDBLogic.ConvertDateToString(result.TourFromDate);
                    result.TourToDateNTime = MyDBLogic.ConvertDateToString(result.TourToDate);
                    result.EntryDatestr = MyDBLogic.ConvertDateToString(result.EntryDate);
                    result.RefEntryDatestr = MyDBLogic.ConvertDateToString(result.RefEntryDate);
                    result.ApprovalDatestr = MyDBLogic.ConvertDateToString(result.ApprovalDate);
                    result.ApprovalDatestr = MyDBLogic.ConvertDateToString(result.ApprovalDate);
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        public TravellingDetails Map_TravellingDetails(DataRow dr)
        {
            TravellingDetails result = new TravellingDetails();
            try
            {
                if (dr != null)
                {

                    if (!DBNull.Value.Equals(dr["ActualTourInDate"]))
                        result.ActualTourInDate = DateTime.Parse(dr["ActualTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeCodeName"]))
                        result.EmployeeCodeName = dr["EmployeeCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmployeeName"]))
                        result.EmployeeName = dr["EmployeeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmployeeNo"]))
                        result.EmployeeNo = int.Parse(dr["EmployeeNo"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["BASInTime"]))
                        result.BASInTime = dr["BASInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["BASOutTime"]))
                        result.BASOutTime = dr["BASOutTime"].ToString();
                    result.ActualTourInDatestr = MyDBLogic.ConvertDateToString(result.ActualTourInDate);


                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
    }
}
