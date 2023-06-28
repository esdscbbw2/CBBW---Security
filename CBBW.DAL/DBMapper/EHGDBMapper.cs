using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using System.Globalization;
using CBBW.BOL;

namespace CBBW.DAL.DBMapper
{
    public class EHGDBMapper
    {
        public EHGTravelingPersondtlsForManagement Map_EHGTravelingPersondtlsForManagement(DataRow dr)
        {
            EHGTravelingPersondtlsForManagement result = new EHGTravelingPersondtlsForManagement();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType =int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeNo"]))
                        result.EmployeeNo = int.Parse(dr["EmployeeNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode =int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeNonName"]))
                        result.EmployeeNonName = dr["EmployeeNonName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName = dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["FromDate"]))
                        result.FromDate = DateTime.Parse(dr["FromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["FromTime"]))
                        result.FromTime = dr["FromTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ToDate"]))
                        result.ToDate = DateTime.Parse(dr["ToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit =dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied =bool.Parse(dr["TADADenied"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutDate"]))
                        result.ActualTourOutDate =DateTime.Parse(dr["ActualTourOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutTime"]))
                        result.ActualTourOutTime = dr["ActualTourOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["RequiredTourInDate"]))
                        result.RequiredTourInDate =DateTime.Parse( dr["RequiredTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["RequiredTourInTime"]))
                        result.RequiredTourInTime = dr["RequiredTourInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourInDate"]))
                        result.ActualTourInDate =DateTime.Parse(dr["ActualTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInTime"]))
                        result.ActualTourInTime = dr["ActualTourInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["TourStatus"]))
                        result.TourStatus = int.Parse(dr["TourStatus"].ToString());
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["IsAuthorised"]))
                        result.IsAuthorised = bool.Parse(dr["IsAuthorised"].ToString());
                    result.FromDateStr = result.FromDate.ToString("yyyy-MM-dd");
                    result.ToDateStr = result.ToDate.ToString("yyyy-MM-dd");
                    result.FromDateStrDisplay=result.FromDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.ToDateStrDisplay = result.ToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.ActualTourOutDateDisplay= result.ActualTourOutDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.RequiredTourInDateDisplay = result.RequiredTourInDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.ActualTourInDateDisplay = result.ActualTourInDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.ActualTourOutDateDisplay = result.ActualTourOutDateDisplay == "01/01/0001" ? "" : result.ActualTourOutDateDisplay;
                    result.RequiredTourInDateDisplay = result.RequiredTourInDateDisplay == "01/01/0001" ? "" : result.RequiredTourInDateDisplay;
                    result.ActualTourInDateDisplay = result.ActualTourInDateDisplay == "01/01/0001" ? "" : result.ActualTourInDateDisplay;
                    result.TourStatusText = result.TourStatus == 0 ? "Continue" : "Completed";
                }
            }
            catch(Exception ex) { MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex); }
            return result;
        }
        public VehicleAllotmentDetails Map_VehicleAllotmentDetails(DataRow dr)
        {
            VehicleAllotmentDetails result = new VehicleAllotmentDetails();
            try
            {
                if (dr != null)
                {                    
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpNumber"]))
                        result.AuthorisedEmpNumber = int.Parse(dr["AuthorisedEmpNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpName"]))
                        result.AuthorisedEmpName = dr["AuthorisedEmpName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationText"]))
                        result.DesignationText = dr["DesignationText"].ToString();
                    if (!DBNull.Value.Equals(dr["MaterialStatus"]))
                        result.MaterialStatus = bool.Parse(dr["MaterialStatus"].ToString())?1:0;
                    if (!DBNull.Value.Equals(dr["VehicleBelongsTo"]))
                        result.VehicleBelongsTo = int.Parse(dr["VehicleBelongsTo"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType = dr["VehicleType"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["ModelName"]))
                        result.ModelName = dr["ModelName"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNumber"]))
                        result.DriverNumber = int.Parse(dr["DriverNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["KMOut"]))
                        result.KMOut =int.Parse(dr["KMOut"].ToString());
                    if (!DBNull.Value.Equals(dr["KMIn"]))
                        result.KMIn = int.Parse(dr["KMIn"].ToString());
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive =bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDateTime"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    else
                        result.EntryTime = result.EntryDate.ToString("hh:mm ss");
                    
                    if (!DBNull.Value.Equals(dr["RFIDCardIn"]))
                        result.RFIDCardIn = dr["RFIDCardIn"].ToString();
                    if (!DBNull.Value.Equals(dr["RFIDCardOut"]))
                        result.RFIDCardOut = dr["RFIDCardOut"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourInTime"]))
                        result.ActualTourInTime = dr["ActualTourInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourOutTime"]))
                        result.ActualTourOutTime = dr["ActualTourOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualKmIn"]))
                        result.ActualKmIn =int.Parse(dr["ActualKmIn"].ToString());
                    if (!DBNull.Value.Equals(dr["UserRemarks"]))
                        result.UserRemarks = dr["UserRemarks"].ToString();
                }
            }
            catch(Exception ex) { MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex); }
            return result;
        }
        public EHGHeader Map_EHGHeader(DataRow dr)
        {
            EHGHeader result = new EHGHeader();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                    {
                        result.CenterName = dr["CenterName"].ToString();
                        result.CenterCodenName = result.CenterCode + " / " + result.CenterName;
                    }                        
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType = int.Parse(dr["VehicleType"].ToString());
                    if (!DBNull.Value.Equals(dr["MaterialStatus"]))
                        result.MaterialStatus = bool.Parse(dr["MaterialStatus"].ToString())?1:0;
                    if (!DBNull.Value.Equals(dr["Initiator"]))
                        result.Initiator =int.Parse(dr["Initiator"].ToString());
                    if (!DBNull.Value.Equals(dr["Instructor"]))
                        result.Instructor =int.Parse(dr["Instructor"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpNo"]))
                        result.AuthorisedEmpNo =int.Parse(dr["AuthorisedEmpNo"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfAllotment"]))
                        result.PurposeOfAllotment = int.Parse(dr["PurposeOfAllotment"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpName"]))
                        result.AuthorisedEmployeeName = dr["AuthorisedEmpName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = int.Parse(dr["IsActive"].ToString())==1?true:false;
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["AppDateTime"]))
                        result.AppDateTime = DateTime.Parse(dr["AppDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonForDisApproval"]))
                        result.ReasonForDisApproval = dr["ReasonForDisApproval"].ToString();
                    if (!DBNull.Value.Equals(dr["InitiatorName"]))
                        result.InitiatorCodenName = dr["InitiatorName"].ToString();
                    if (!DBNull.Value.Equals(dr["InstructorName"]))
                        result.InstructorName = dr["InstructorName"].ToString();
                    if (!DBNull.Value.Equals(dr["ImageFile"]))
                        result.DocFileName = dr["ImageFile"].ToString();
                }
            }
            catch(Exception ex) { MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex); }
            return result;
        }
        public DateWiseTourDetails Map_DateWiseTourDetails(DataRow dr)
        {
            DateWiseTourDetails result = new DateWiseTourDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["FromDate"]))
                        result.FromDate = DateTime.Parse(dr["FromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ToDate"]))
                        result.ToDate =DateTime.Parse(dr["ToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourCatCodes"]))
                        result.TourCatCodes = dr["TourCatCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCatText"]))
                        result.TourCatText = dr["TourCatText"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCodes"]))
                        result.CenterCodes = dr["CenterCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterNames"]))
                        result.CenterNames = dr["CenterNames"].ToString();
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    result.FromDateStrDisplay = result.FromDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
                    result.ToDateStrDisplay = result.ToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.ToDateStr = result.ToDate.ToString("yyyy-MM-dd");
                }
            }
            catch(Exception ex) { MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex); }
            return result;
        }
        public EHGNoteList Map_EHGNoteList(DataRow dr)
        {
            EHGNoteList result = new EHGNoteList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber =int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate =DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["PurposeOfAllotment"]))
                        result.PurposeOfAllotment = dr["PurposeOfAllotment"].ToString();
                    if (!DBNull.Value.Equals(dr["Instructor"]))
                        result.Instructor =int.Parse(dr["Instructor"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved =bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["IsLocked"]))
                        result.IsLockedForDelete = bool.Parse(dr["IsLocked"].ToString());
                    result.EntryDateDisplay = result.EntryDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.NoteNumber2 = result.NoteNumber;
                    if (!result.IsLockedForDelete && !result.IsApproved && result.EntryDate == DateTime.Today)
                    { result.CanDelete = true; }
                    //result.CanDelete = true;
                }
            }
            catch(Exception ex) { MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex); }
            return result;
        }
    }
}
