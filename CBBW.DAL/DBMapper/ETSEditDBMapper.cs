using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;
using System.Globalization;
using CBBW.BOL.CustomModels;

namespace CBBW.DAL.DBMapper
{
    public class ETSEditDBMapper
    {
        public EditNoteNumber Map_EditNoteNumber(DataRow dr)
        {
            EditNoteNumber result = new EditNoteNumber();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                }
            }
            catch { }
            return result;
        }
        public EditNoteDetails Map_EditNoteDetails(DataRow dr,int mtag=0)
        {
            EditNoteDetails result = new EditNoteDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate =DateTime.Parse(dr["SchFromDate"].ToString()).ToString("yyyy-MM-dd");
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.SchToDate =DateTime.Parse(dr["SchToDate"].ToString()).ToString("yyyy-MM-dd");
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate =DateTime.Parse( dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["PurposeOfAllotment"]))
                        result.POA =int.Parse(dr["PurposeOfAllotment"].ToString());
                    if (!DBNull.Value.Equals(dr["EPTour"]))
                        result.EPTour = int.Parse(dr["EPTour"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["AppDateTime"]))
                        result.AppDateTime =DateTime.Parse(dr["AppDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonForDisApproval"]))
                        result.NotAppReason = dr["ReasonForDisApproval"].ToString();
                    if (!DBNull.Value.Equals(dr["IsRatified"]))
                        result.IsRatified = bool.Parse(dr["IsRatified"].ToString());
                    if (!DBNull.Value.Equals(dr["RetDateTime"]))
                        result.RetDateTime =DateTime.Parse(dr["RetDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["RetReason"]))
                        result.RetReason = dr["RetReason"].ToString();
                    if (!DBNull.Value.Equals(dr["IsIndividualEdit"]))
                        result.IsIndividualEdit = int.Parse(dr["IsIndividualEdit"].ToString());
                    if (!DBNull.Value.Equals(dr["IsCancelled"]))
                        result.IsCancelled = int.Parse(dr["IsCancelled"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpNonName"]))
                        result.AuthorisedEmpNonName = dr["AuthorisedEmpNonName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesgCodenNameOfAE"]))
                        result.DesgCodenNameOfAE = dr["DesgCodenNameOfAE"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType = int.Parse(dr["VehicleType"].ToString());
                    if (result.POA == 0)
                        result.POAText = "NA";
                    else if (result.POA == 1)
                        result.POAText = "For Management";
                    else if (result.POA==2)
                        result.POAText = "For Office Work";
                    
                    result.EPTourText =result.EPTour==1?"Yes":result.NoteNumber.Substring(7,3)=="EMC"?"No":"NA";
                    //Else part is pending for the required module.
                    result.EntryDateDisplay = result.EntryDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (mtag == 1) 
                    {
                        result.EntryTime = result.EntryDate.ToString("hh:mm:ss tt");
                    }
                    result.AppDateTimeDisplay =result.AppDateTime.Year==1?"-":result.AppDateTime.ToString("dd/MM/yyyy hh:mm ss tt",CultureInfo.InvariantCulture);
                    result.RetDateTimeDisplay = result.RetDateTime.Year == 1 ? "-" : result.RetDateTime.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //result.AppDateTimeDisplay = result.AppDateTimeDisplay.Trim() == "01/01/0001 12:00:00 AM" ? "-" : result.AppDateTimeDisplay;
                    //result.RetDateTimeDisplay = result.RetDateTimeDisplay.Trim() == "01/01/0001 12:00:00 AM" ? "-" : result.RetDateTimeDisplay;
                    result.NotAppReason = string.IsNullOrEmpty(result.NotAppReason)||result.NotAppReason.Trim() == "NA"  ? "-" : result.NotAppReason;
                    result.RetReason = string.IsNullOrEmpty(result.RetReason)||result.RetReason.Trim() == "NA" ? "-" : result.RetReason;
                    result.IsApprovedDisplay = result.IsApproved ? "Yes" : "No";
                    result.IsRatifiedDisplay = result.IsRatified ? "Yes":result.RetDateTime.Year==1?"-":"No";
                }
            }
            catch { }
            return result;
        }
        public EditTPDetails Map_EditTPDetails(DataRow dr)
        {
            EditTPDetails result = new EditTPDetails();
            try
            {
                if (dr != null)
                {
                    EHGMaster _EHGMaster = EHGMaster.GetInstance;
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                    { 
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                        result.PersonTypeText = _EHGMaster.PersonType.Where(o => o.ID == result.PersonType).FirstOrDefault().DisplayText;
                    }
                    if (!DBNull.Value.Equals(dr["EmployeeNo"]))
                        result.EmployeeNo = int.Parse(dr["EmployeeNo"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeNonName"]))
                        result.EmployeeNonName = dr["EmployeeNonName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode =int.Parse( dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName = dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["EligibleVehicleType"]))
                        result.EligibleVehicleType = int.Parse(dr["EligibleVehicleType"].ToString());
                    if (!DBNull.Value.Equals(dr["EligibleVehicleTypeName"]))
                        result.EligibleVehicleTypeName = dr["EligibleVehicleTypeName"].ToString();
                    else
                        result.EligibleVehicleTypeName = "-";
                    if (!DBNull.Value.Equals(dr["EPNoteNumber"]))
                    {
                        result.EPNoteNumber = dr["EPNoteNumber"].ToString();
                        if (!DBNull.Value.Equals(dr["EPNoteDate"]))
                            result.EPNoteDate = DateTime.Parse(dr["EPNoteDate"].ToString());
                    }
                    else { result.EPNoteNumber = "-"; }
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied =bool.Parse(dr["TADADenied"].ToString());
                    if (!DBNull.Value.Equals(dr["Isdriver"]))
                        result.Isdriver = int.Parse(dr["Isdriver"].ToString());
                }
            }
            catch { }
            return result;
        }
        public EditDWTDetails Map_EditDWTDetails(DataRow dr) 
        {
            EHGMaster master = EHGMaster.GetInstance;
            EditDWTDetails result = new EditDWTDetails();
            if (dr != null)
            {
                if (!DBNull.Value.Equals(dr["NoteNumber"]))
                    result.NoteNumber = dr["NoteNumber"].ToString();
                if (!DBNull.Value.Equals(dr["SchFromDate"]))
                    result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                if (!DBNull.Value.Equals(dr["SchToDate"]))
                    result.SchToDate = DateTime.Parse(dr["SchToDate"].ToString());
                if (!DBNull.Value.Equals(dr["EditedTourToDate"]))
                    result.EditedTourToDate = DateTime.Parse(dr["EditedTourToDate"].ToString());
                if (!DBNull.Value.Equals(dr["TourCategoryIds"]))
                    result.TourCategoryIds = dr["TourCategoryIds"].ToString();
                if (!DBNull.Value.Equals(dr["TourCategoryNames"]))
                    result.TourCategoryNames = dr["TourCategoryNames"].ToString();
                if (!DBNull.Value.Equals(dr["TourCenterCodeIds"]))
                    result.TourCenterCodeIds = dr["TourCenterCodeIds"].ToString();
                if (!DBNull.Value.Equals(dr["TourCenterNames"]))
                    result.TourCenterNames = dr["TourCenterNames"].ToString();
                else
                    result.TourCenterNames = "NA";
                if (!DBNull.Value.Equals(dr["BranchCodes"]))
                    result.BranchCodes = dr["BranchCodes"].ToString();
                if (!DBNull.Value.Equals(dr["BranchNames"]))
                    result.BranchNames = dr["BranchNames"].ToString();
                else
                    result.BranchNames = "NA";
                if (!DBNull.Value.Equals(dr["EditSL"]))
                    result.EditSL = int.Parse(dr["EditSL"].ToString());
                if (!DBNull.Value.Equals(dr["SourceID"]))
                    result.SourceID = int.Parse(dr["SourceID"].ToString());
                if (!DBNull.Value.Equals(dr["EditTag"])) 
                {
                    result.EditTag = int.Parse(dr["EditTag"].ToString());
                    result.EditTagText = master.EditTag.Where(o => o.ID == result.EditTag).FirstOrDefault().DisplayText;
                }                    
                if (!DBNull.Value.Equals(dr["ReasonForEdit"]))
                    result.EditReason = dr["ReasonForEdit"].ToString();

                result.SchFromDateDisplay = result.SchFromDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                result.SchToDateDisplay = result.SchToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                result.EditedTourToDateDisplay = result.EditedTourToDate.Year==1?"-":result.EditedTourToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                result.EditedTourToDateStr = result.EditedTourToDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                List<CustomComboOptions> mTC = new List<CustomComboOptions>();
                if (result.TourCategoryIds.IndexOf("1") >= 0) 
                {
                    mTC.Add(new CustomComboOptions { ID = 2, DisplayText = "Branch & Center Visit" });
                }
                if (result.TourCategoryIds.IndexOf("2") >= 0)
                {
                    mTC.Add(new CustomComboOptions { ID = 1, DisplayText = "Center Visit" });
                }
                if (result.TourCategoryIds.IndexOf("3") >= 0 || result.TourCategoryIds.IndexOf("4") >= 0)
                {
                    mTC.Add(new CustomComboOptions { ID = 1, DisplayText = "Center Visit" });
                    mTC.Add(new CustomComboOptions { ID = 2, DisplayText = "Branch & Center Visit" });
                }
                if (result.TourCategoryIds.IndexOf("5") >= 0)
                {
                    mTC.Add(new CustomComboOptions { ID = 5, DisplayText = "Unknown Destination" });
                }
                if (result.TourCategoryIds.IndexOf("6") >= 0)
                {
                    mTC.Add(new CustomComboOptions { ID = 6, DisplayText = "E.P. Tour" });
                }
                if (result.TourCategoryIds.IndexOf("7") >= 0)
                {
                    mTC.Add(new CustomComboOptions { ID = 7, DisplayText = "NA" });
                }
                result.TourCategories = mTC.GroupBy(o=>o.ID).Select(o=>o.FirstOrDefault()).ToList();
                TimeSpan timeLimit = TimeSpan.Parse("16:00");
                TimeSpan now = DateTime.Now.TimeOfDay;

                if (result.SchFromDate > DateTime.Today)
                {
                    result.IsEditable = 1;
                    result.TourCancelMinDate = result.SchFromDate;
                }
                else if (result.SchFromDate == DateTime.Today && now <= timeLimit)
                {
                    result.IsEditable = 1;
                    result.TourCancelMinDate = result.SchFromDate;
                }
                else 
                {
                    if(now <= timeLimit)
                        result.TourCancelMinDate = DateTime.Today;
                    else
                        result.TourCancelMinDate = DateTime.Today.AddDays(1);
                }
                result.TourCancelMaxDate = result.SchToDate.AddDays(-1);
                result.TourCancelMinDateStr = result.TourCancelMinDate.ToString("yyyy-MM-dd");
                result.TourCancelMaxDateStr = result.TourCancelMaxDate.ToString("yyyy-MM-dd");
            }                        
            return result;
        }
        public EditNoteList Map_EditNoteList(DataRow dr) 
        {
            EditNoteList result = new EditNoteList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalRecords"]))
                        result.TotalRecord = int.Parse(dr["TotalRecords"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EditDate"]))
                        result.EntryDate =DateTime.Parse(dr["EditDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EditDateDispaly"]))
                        result.EntryDateDisplay = dr["EditDateDispaly"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CenterCode =int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmpNo"]))
                        result.EmployeeNumber = int.Parse(dr["EmpNo"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["IsLocked"]))
                        result.IsLocked = bool.Parse(dr["IsLocked"].ToString());
                    result.CanDelete = result.EntryDate.ToString("dd-MM-yyyy") == DateTime.Today.ToString("dd-MM-yyyy") ? result.IsApproved?0:result.IsLocked?0:1 : 0;
                }
            }
            catch { }
            return result;
        }
        public EntryINoteList Map_EntryINoteList(DataRow dr)
        {
            EntryINoteList result = new EntryINoteList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalRecords"]))
                        result.TotalRecord = int.Parse(dr["TotalRecords"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDateDispaly"]))
                        result.EntryDateDisplay = dr["EntryDateDispaly"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CenterCode = int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();
                    
                    result.CanDelete= result.EntryDateDisplay == DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)?true:false;
                }
            }
            catch { }
            return result;
        }
        public EntryITDetails Map_EntryITDetails(DataRow dr) 
        {
            EntryITDetails result = new EntryITDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["PublicTransport"]))
                        result.PublicTransport = bool.Parse(dr["PublicTransport"].ToString());
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
                        result.SchToDate = DateTime.Parse(dr["SchTourToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    result.SchFromDateDisplay = result.SchFromDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
                    result.SchToDateDisplay = result.SchToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.PublicTransportText = result.PublicTransport ? "Yes" : "No";
                    EHGMaster _master = new EHGMaster();                    
                    result.VehicleTypeText = result.VehicleType>0?_master.VehicleTypes.Where(o => o.ID == result.VehicleType).FirstOrDefault().DisplayText:"NA";
                    result.VehicleTypeProvidedText= result.VehicleTypeProvided>0? _master.VehicleTypes.Where(o => o.ID == result.VehicleTypeProvided).FirstOrDefault().DisplayText:"NA";
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public EntryIDWDetails Map_EntryIDWDetails(DataRow dr) 
        {
            EntryIDWDetails result = new EntryIDWDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.SchToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourCategoryIds"]))
                        result.TourCategoryIds = dr["TourCategoryIds"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryNames"]))
                        result.TourCategoryNames = dr["TourCategoryNames"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCenterCodeIds"]))
                        result.TourCenterCodeIds = dr["TourCenterCodeIds"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCenterNames"]))
                        result.TourCenterNames = dr["TourCenterNames"].ToString();
                    else
                        result.TourCenterNames = "NA";
                    if (!DBNull.Value.Equals(dr["BranchCodes"]))
                        result.BranchCodes = dr["BranchCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["BranchNames"]))
                        result.BranchNames = dr["BranchNames"].ToString();
                    else
                        result.BranchNames = "NA";                    
                    result.SchFromDateDisplay = result.SchFromDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.SchToDateDisplay = result.SchToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);                    
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public IEnumerable<NoteDriver> Map_DriverDetails(DataTable dt)
        {
            List<NoteDriver> results = new List<NoteDriver>();
            try
            {
                if (dt != null && dt.Rows.Count > 0) 
                {
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        DataRow dr = dt.Rows[i];
                        NoteDriver result = new NoteDriver();
                        if (dr != null)
                        {
                            if (!DBNull.Value.Equals(dr["PersonType"]))
                                result.PersonType = int.Parse(dr["PersonType"].ToString());
                            if (!DBNull.Value.Equals(dr["EmployeeNo"]))
                                result.PersonID = int.Parse(dr["EmployeeNo"].ToString());
                            if (!DBNull.Value.Equals(dr["EmployeeNonName"]))
                                result.PersonName = dr["EmployeeNonName"].ToString();                           
                        }
                        results.Add(result);
                    }
                }
                int x = results.Where(o => o.PersonType == 2).Count();
                if (x > 0)
                {
                    results = results.Where(o => o.PersonType == 2).ToList();
                }
            }
            catch { }            
            return results;
        }


    }
}
