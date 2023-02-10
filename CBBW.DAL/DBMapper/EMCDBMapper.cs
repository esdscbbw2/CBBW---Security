using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EMC;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
   public  class EMCDBMapper
    {
        public EMCNoteList Map_EMCNoteList(DataRow dr)
        {
            EMCNoteList result = new EMCNoteList();
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
        public EMCHeader Map_EMCHeader(DataRow dr)
        {
            EMCHeader result = new EMCHeader();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["AttachFile"]))
                        result.AttachFile = dr["AttachFile"].ToString();
                    if (!DBNull.Value.Equals(dr["IsEPTour"]))
                        result.IsEPTour = bool.Parse(dr["IsEPTour"].ToString());
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovedReason"]))
                        result.ApprovedReason = dr["ApprovedReason"].ToString();
                    if (!DBNull.Value.Equals(dr["ApproveDate"]))
                        result.ApproveDate = DateTime.Parse(dr["ApproveDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ApproveTime"]))
                        result.ApproveTime = dr["ApproveTime"].ToString();
                   
                    result.EntryDateDisplay = MyDBLogic.ConvertDateToString(result.EntryDate);
                    result.ApproveDatestr = MyDBLogic.ConvertDateToString(result.ApproveDate);
                 
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public EMCTravellingPerson Map_EMCTravellingPerson(DataRow dr)
        {
            EMCTravellingPerson result = new EMCTravellingPerson();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonTypeName"]))
                        result.PersonTypeName = dr["PersonTypeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmployeeNo"]))
                        result.EmployeeNo = int.Parse(dr["EmployeeNo"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeNonName"]))
                        result.EmployeeNonName = dr["EmployeeNonName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName = dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["EligibleVehicleType"]))
                        result.EligibleVehicleType = int.Parse(dr["EligibleVehicleType"].ToString());
                    if (!DBNull.Value.Equals(dr["EligibleVehicleTypeName"]))
                        result.EligibleVehicleTypeName = dr["EligibleVehicleTypeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EPNoteNumber"]))
                        result.EPNoteNumber = dr["EPNoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["NoteDate"]))
                        result.NoteDate = DateTime.Parse(dr["NoteDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenieds = bool.Parse(dr["TADADenied"].ToString());
                    if (result.NoteDate.Year == 1)
                    {
                        result.NoteDatestr = "NA";
                    }
                    else
                    {
                        result.NoteDatestr = MyDBLogic.ConvertDateToString(result.NoteDate);
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public EMCTravellingDetails Map_EMCTravellingDetails(DataRow dr)
        {
            EMCTravellingDetails result = new EMCTravellingDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["PublicTransport"]))
                        result.PublicTransports = bool.Parse(dr["PublicTransport"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType = int.Parse(dr["VehicleType"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonVehicleReq"]))
                        result.ReasonVehicleReq = dr["ReasonVehicleReq"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleTypeProvided"]))
                        result.VehicleTypeProvided = int.Parse(dr["VehicleTypeProvided"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonVehicleProvided"]))
                        result.ReasonVehicleProvided = dr["ReasonVehicleProvided"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["SchFromTime"]))
                        result.SchFromTime = dr["SchFromTime"].ToString();
                    if (!DBNull.Value.Equals(dr["SchTourToDate"]))
                        result.SchTourToDate = DateTime.Parse(dr["SchTourToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["EmpNoName"]))
                        result.EmpNoName = dr["EmpNoName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    result.SchFromDateDisplay = MyDBLogic.ConvertDateToString(result.SchFromDate);
                    result.SchTourToDateDisplay = MyDBLogic.ConvertDateToString(result.SchTourToDate);
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public EMCDateWiseTour Map_EMCDateWiseTour(DataRow dr)
        {
            EMCDateWiseTour result = new EMCDateWiseTour();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.SchToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourCategoryId"]))
                        result.TourCategoryId = dr["TourCategoryId"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategory"]))
                        result.TourCategory = dr["TourCategory"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = dr["CenterCode"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["BranchCode"]))
                        result.BranchCode = dr["BranchCode"].ToString();
                    if (!DBNull.Value.Equals(dr["BranchCodeName"]))
                        result.BranchCodeName = dr["BranchCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    result.DDSchToDate = MyDBLogic.ConvertDateToString(result.SchToDate);
                    result.SchToDatestr = result.SchToDate.ToString("yyyy-MM-dd");
                    result.DDSchFromDate = MyDBLogic.ConvertDateToString(result.SchFromDate);
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        public TPEPNote Map_TPEPNote(DataRow dr)
        {
            TPEPNote result = new TPEPNote();
            if (dr != null)
            {
                if (!DBNull.Value.Equals(dr["EPNoteNumber"]))
                    result.EPNoteNumber = dr["EPNoteNumber"].ToString();
                if (!DBNull.Value.Equals(dr["NoteDate"]))
                    result.NoteDate = DateTime.Parse(dr["NoteDate"].ToString());
                if (result.NoteDate.Year == 1)
                {
                    result.NoteDatestr = "NA";
                }
                else
                {
                    result.NoteDatestr = MyDBLogic.ConvertDateToString(result.NoteDate);
                }

            }
            return result;
        }
    }
}
