using System;
using System.Collections.Generic;
using System.Data;
using CBBW.BOL;
using CBBW.BOL.BIL;
using CBBW.BOL.EMC;
using CBBW.BOL.EMN;
using CBBW.BOL.ETS;
using CBBW.BOL.GVMR;
using CBBW.BOL.Modules;
using CBBW.BOL.Navigation;
using CBBW.BOL.Role;
using CBBW.BOL.SubModules;
using CBBW.BOL.Task;
using CBBW.BOL.TFD;

namespace CBBW.DAL.DataSync
{
    public partial class CommonTable
    {
       
        public CommonTable(List<ETSTravellingPerson> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sNoteNumber", typeof(string));
            UDTable.Columns.Add("iPersonType", typeof(int));
            UDTable.Columns.Add("sPersonTypeName", typeof(string));
            UDTable.Columns.Add("iEmployeeNo", typeof(int));
            UDTable.Columns.Add("sEmployeeNonName", typeof(string));
            UDTable.Columns.Add("sDesignationCodenName", typeof(string));
            UDTable.Columns.Add("iEligibleVehicleType", typeof(int));
            UDTable.Columns.Add("isEligibleVehicleTypeName", typeof(string));
            UDTable.Columns.Add("bTADADenied", typeof(bool));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (ETSTravellingPerson obj in customoptions)
                {
                    if (obj.PersonType == 1 || obj.PersonType == 2)
                    { obj.EmployeeNonName = obj.EmployeeNonNamecmb; }
                    if (obj.PersonType == 3 || obj.PersonType == 4)
                    { obj.EmployeeNo = 0; }

                    DataRow dr = UDTable.NewRow();
                    dr["sNoteNumber"] = obj.NoteNumber;
                    dr["iPersonType"] = obj.PersonType;
                    dr["sPersonTypeName"] = obj.PersonTypeName;
                    dr["iEmployeeNo"] = obj.EmployeeNo;
                    dr["sEmployeeNonName"] = obj.EmployeeNonName;
                    dr["sDesignationCodenName"] = obj.DesignationCodenName;
                    dr["iEligibleVehicleType"] = obj.EligibleVehicleType;
                    dr["isEligibleVehicleTypeName"] = obj.EligibleVehicleTypeName;
                    dr["bTADADenied"] = obj.TADADenied == 1 ? true : false;

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<ETSTravellingDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("bPublicTransport", typeof(bool));
            UDTable.Columns.Add("iVehicleType", typeof(int));
            UDTable.Columns.Add("sReasonVehicleReq", typeof(string));
            UDTable.Columns.Add("iVehicleTypeProvided", typeof(int));
            UDTable.Columns.Add("sReasonVehicleProvided", typeof(string));
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("sSchFromTime", typeof(string));
            UDTable.Columns.Add("dSchTourToDate", typeof(DateTime));
            UDTable.Columns.Add("sPurposeOfVisit", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (ETSTravellingDetails obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["bPublicTransport"] = obj.PublicTransport == 1 ? true : false;
                    dr["iVehicleType"] = obj.VehicleType != 0 ? obj.VehicleType : 0;
                    dr["sReasonVehicleReq"] = obj.ReasonVehicleReq != null ? obj.ReasonVehicleReq : "NA";
                    dr["iVehicleTypeProvided"] = obj.VehicleTypeProvided;
                    dr["sReasonVehicleProvided"] = obj.ReasonVehicleProvided != null ? obj.ReasonVehicleProvided : "NA";
                    dr["dSchFromDate"] = obj.SchFromDate;
                    dr["sSchFromTime"] = obj.SchFromTime != null ? obj.SchFromTime : "NA";
                    dr["dSchTourToDate"] = obj.SchTourToDate;
                    dr["sPurposeOfVisit"] = obj.PurposeOfVisit != null ? obj.PurposeOfVisit : "NA";

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<ETSDateWiseTour> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sSchFromDate", typeof(string));
            UDTable.Columns.Add("dSchToDate", typeof(DateTime));
            UDTable.Columns.Add("sTourCategoryId", typeof(string));
            UDTable.Columns.Add("sTourCategory", typeof(string));
            UDTable.Columns.Add("sCenterCode", typeof(string));
            UDTable.Columns.Add("sCenterCodeName", typeof(string));
            UDTable.Columns.Add("sBranchCode", typeof(string));
            UDTable.Columns.Add("sBranchCodeName", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (ETSDateWiseTour obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["sSchFromDate"] = obj.DDSchFromDate != null ? obj.DDSchFromDate : "NA";
                    dr["dSchToDate"] = obj.SchToDate;
                    dr["sTourCategoryId"] = obj.TourCategory != null ? obj.TourCategory : "NA";
                    dr["sTourCategory"] = obj.TourCatText != null ? obj.TourCatText : "NA";
                    dr["sCenterCode"] = obj.CenterCodeName != null ? obj.CenterCodeName : "NA";
                    dr["sCenterCodeName"] = obj.CenterCodeNametxt != null ? obj.CenterCodeNametxt : "NA";
                    dr["sBranchCode"] = obj.BranchCodeName != null ? obj.BranchCodeName : "NA";
                    dr["sBranchCodeName"] = obj.BranchCodeNametxt != null ? obj.BranchCodeNametxt : "NA";
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EMNTravellingPerson> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sNoteNumber", typeof(string));
            UDTable.Columns.Add("iPersonType", typeof(int));
            UDTable.Columns.Add("sPersonTypeName", typeof(string));
            UDTable.Columns.Add("iEmployeeNo", typeof(int));
            UDTable.Columns.Add("sEmployeeNonName", typeof(string));
            UDTable.Columns.Add("sDesignationCodenName", typeof(string));
            UDTable.Columns.Add("iEligibleVehicleType", typeof(int));
            UDTable.Columns.Add("isEligibleVehicleTypeName", typeof(string));
            UDTable.Columns.Add("bTADADenied", typeof(bool));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EMNTravellingPerson obj in customoptions)
                {
                    if (obj.PersonType == 1 || obj.PersonType == 2)
                    { obj.EmployeeNonName = obj.EmployeeNonNamecmb; }
                    if (obj.PersonType == 3 || obj.PersonType == 4)
                    { obj.EmployeeNo = 0; }

                    DataRow dr = UDTable.NewRow();
                    dr["sNoteNumber"] = obj.NoteNumber;
                    dr["iPersonType"] = obj.PersonType;
                    dr["sPersonTypeName"] = obj.PersonTypeName;
                    dr["iEmployeeNo"] = obj.EmployeeNo;
                    dr["sEmployeeNonName"] = obj.EmployeeNonName;
                    dr["sDesignationCodenName"] = obj.DesignationCodenName;
                    dr["iEligibleVehicleType"] = obj.EligibleVehicleType;
                    dr["isEligibleVehicleTypeName"] = obj.EligibleVehicleTypeName;
                    dr["bTADADenied"] = obj.TADADenied == 1 ? true : false;

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EMNTravellingDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("bPublicTransport", typeof(bool));
            UDTable.Columns.Add("iVehicleType", typeof(int));
            UDTable.Columns.Add("sReasonVehicleReq", typeof(string));
            UDTable.Columns.Add("iVehicleTypeProvided", typeof(int));
            UDTable.Columns.Add("sReasonVehicleProvided", typeof(string));
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("sSchFromTime", typeof(string));
            UDTable.Columns.Add("dSchTourToDate", typeof(DateTime));
            UDTable.Columns.Add("sPurposeOfVisit", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EMNTravellingDetails obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["bPublicTransport"] = obj.PublicTransport == 1 ? true : false;
                    dr["iVehicleType"] = obj.VehicleType != 0 ? obj.VehicleType : 0;
                    dr["sReasonVehicleReq"] = obj.ReasonVehicleReq != null ? obj.ReasonVehicleReq : "NA";
                    dr["iVehicleTypeProvided"] = obj.VehicleTypeProvided;
                    dr["sReasonVehicleProvided"] = obj.ReasonVehicleProvided != null ? obj.ReasonVehicleProvided : "NA";
                    dr["dSchFromDate"] = obj.SchFromDate;
                    dr["sSchFromTime"] = obj.SchFromTime != null ? obj.SchFromTime : "NA";
                    dr["dSchTourToDate"] = obj.SchTourToDate;
                    dr["sPurposeOfVisit"] = obj.PurposeOfVisit != null ? obj.PurposeOfVisit : "NA";

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EMNDateWiseTour> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("dSchToDate", typeof(DateTime));
            UDTable.Columns.Add("sTourCategoryId", typeof(string));
            UDTable.Columns.Add("sTourCategory", typeof(string));
            UDTable.Columns.Add("sCenterCode", typeof(string));
            UDTable.Columns.Add("sCenterCodeName", typeof(string));
            UDTable.Columns.Add("sBranchCode", typeof(string));
            UDTable.Columns.Add("sBranchCodeName", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EMNDateWiseTour obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dSchFromDate"] = obj.SchFromDate;
                    dr["dSchToDate"] = obj.SchToDate;
                    dr["sTourCategoryId"] = obj.TourCategory != null ? obj.TourCategory : "NA";
                    dr["sTourCategory"] = obj.TourCatText != null ? obj.TourCatText : "NA";
                    dr["sCenterCode"] = obj.CenterCodeName != null ? obj.CenterCodeName : "NA";
                    dr["sCenterCodeName"] = obj.CenterCodeNametxt != null ? obj.CenterCodeNametxt : "NA";
                    dr["sBranchCode"] = obj.BranchCodeName != null ? obj.BranchCodeName : "NA";
                    dr["sBranchCodeName"] = obj.BranchCodeNametxt != null ? obj.BranchCodeNametxt : "NA";
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EMCTravellingPerson> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sNoteNumber", typeof(string));
            UDTable.Columns.Add("iPersonType", typeof(int));
            UDTable.Columns.Add("sPersonTypeName", typeof(string));
            UDTable.Columns.Add("iEmployeeNo", typeof(int));
            UDTable.Columns.Add("sEmployeeNonName", typeof(string));
            UDTable.Columns.Add("sDesignationCodenName", typeof(string));
            UDTable.Columns.Add("iEligibleVehicleType", typeof(int));
            UDTable.Columns.Add("isEligibleVehicleTypeName", typeof(string));
            UDTable.Columns.Add("sEPNoteNumber", typeof(string));
            UDTable.Columns.Add("dNoteDate", typeof(DateTime));
            UDTable.Columns.Add("bTADADenied", typeof(bool));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EMCTravellingPerson obj in customoptions)
                {
                    if (obj.PersonType == 1 || obj.PersonType == 2)
                    { obj.EmployeeNonName = obj.EmployeeNonNamecmb; }
                    if (obj.PersonType == 3 || obj.PersonType == 4)
                    { obj.EmployeeNo = 0; }
                   //if (obj.NoteDate == null) { obj.NoteDate = DateTime.MinValue;  }
                    

                    DataRow dr = UDTable.NewRow();
                    dr["sNoteNumber"] = obj.NoteNumber;
                    dr["iPersonType"] = obj.PersonType;
                    dr["sPersonTypeName"] = obj.PersonTypeName;
                    dr["iEmployeeNo"] = obj.EmployeeNo;
                    dr["sEmployeeNonName"] = obj.EmployeeNonName;
                    dr["sDesignationCodenName"] = obj.DesignationCodenName;
                    dr["iEligibleVehicleType"] = obj.EligibleVehicleType;
                    dr["isEligibleVehicleTypeName"] = obj.EligibleVehicleTypeName;
                    dr["sEPNoteNumber"] = obj.EPNoteNumber;
                    if(obj.NoteDate.Year != 1)
                    {
                        dr["dNoteDate"] = obj.NoteDate;
                    }
                   
                    
                    dr["bTADADenied"] = obj.TADADenied == 1 ? true : false;

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EMCTravellingDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("bPublicTransport", typeof(bool));
            UDTable.Columns.Add("iVehicleType", typeof(int));
            UDTable.Columns.Add("sReasonVehicleReq", typeof(string));
            UDTable.Columns.Add("iVehicleTypeProvided", typeof(int));
            UDTable.Columns.Add("sReasonVehicleProvided", typeof(string));
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("sSchFromTime", typeof(string));
            UDTable.Columns.Add("dSchTourToDate", typeof(DateTime));
            UDTable.Columns.Add("sPurposeOfVisit", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EMCTravellingDetails obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["bPublicTransport"] = obj.PublicTransport == 1 ? true : false;
                    dr["iVehicleType"] = obj.VehicleType != 0 ? obj.VehicleType : 0;
                    dr["sReasonVehicleReq"] = obj.ReasonVehicleReq != null ? obj.ReasonVehicleReq : "NA";
                    dr["iVehicleTypeProvided"] = obj.VehicleTypeProvided;
                    dr["sReasonVehicleProvided"] = obj.ReasonVehicleProvided != null ? obj.ReasonVehicleProvided : "NA";
                    dr["dSchFromDate"] = obj.SchFromDate;
                    dr["sSchFromTime"] = obj.SchFromTime != null ? obj.SchFromTime : "NA";
                    dr["dSchTourToDate"] = obj.SchTourToDate;
                    dr["sPurposeOfVisit"] = obj.PurposeOfVisit != null ? obj.PurposeOfVisit : "NA";

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EMCDateWiseTour> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("dSchToDate", typeof(DateTime));
            UDTable.Columns.Add("sTourCategoryId", typeof(string));
            UDTable.Columns.Add("sTourCategory", typeof(string));
            UDTable.Columns.Add("sCenterCode", typeof(string));
            UDTable.Columns.Add("sCenterCodeName", typeof(string));
            UDTable.Columns.Add("sBranchCode", typeof(string));
            UDTable.Columns.Add("sBranchCodeName", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EMCDateWiseTour obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dSchFromDate"] = obj.SchFromDate;
                    dr["dSchToDate"] = obj.SchToDate;
                    dr["sTourCategoryId"] = obj.TourCategory != null ? obj.TourCategory : "NA";
                    dr["sTourCategory"] = obj.TourCatText != null ? obj.TourCatText : "NA";
                    dr["sCenterCode"] = obj.CenterCodeName != null ? obj.CenterCodeName : "NA";
                    dr["sCenterCodeName"] = obj.CenterCodeNametxt != null ? obj.CenterCodeNametxt : "NA";
                    dr["sBranchCode"] = obj.BranchCodeName != null ? obj.BranchCodeName : "NA";
                    dr["sBranchCodeName"] = obj.BranchCodeNametxt != null ? obj.BranchCodeNametxt : "NA";
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<TFDTourFeedBackDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sTourCategoryId", typeof(string));
            UDTable.Columns.Add("sTourCategory", typeof(string));
            UDTable.Columns.Add("sLocationCode", typeof(string));
            UDTable.Columns.Add("sLocationCodeName", typeof(string));
            UDTable.Columns.Add("sTourFeedBack", typeof(string));
            UDTable.Columns.Add("bActionTaken", typeof(bool));
            UDTable.Columns.Add("iConcernDeptCode", typeof(int));
            UDTable.Columns.Add("sConcernDeptName", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (TFDTourFeedBackDetails obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                  
                    dr["sTourCategoryId"] = obj.TourCategory != null ? obj.TourCategory : "NA";
                    dr["sTourCategory"] = obj.TourCategoryText != null ? obj.TourCategoryText : "NA";
                    dr["sLocationCode"] = obj.CenterCodeName != null ? obj.CenterCodeName : "NA";
                    dr["sLocationCodeName"] = obj.CenterCodeNametxt != null ? obj.CenterCodeNametxt : "NA";
                    dr["sTourFeedBack"] = obj.TourFeedBack != null ? obj.TourFeedBack : "NA";
                    dr["bActionTaken"] = obj.ActionTaken ==1 ? true : false;
                    dr["iConcernDeptCode"] = 0;
                    dr["sConcernDeptName"] = null;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<TFDTourFBApproval> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iID", typeof(int));
            UDTable.Columns.Add("sNoteNumber", typeof(string));
            UDTable.Columns.Add("iConcernDeptCode", typeof(int));
            UDTable.Columns.Add("sConcernDeptName", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (TFDTourFBApproval obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();

                    dr["iID"] = obj.ID != 0 ? obj.ID : 0;
                    dr["sNoteNumber"] = obj.NoteNumber != null ? obj.NoteNumber : "NA";
                    dr["iConcernDeptCode"] = obj.ConcernDeptCode != 0 ? obj.ConcernDeptCode : 0;
                    dr["sConcernDeptName"] = obj.ConcernDeptName != null ? obj.ConcernDeptName : "NA";
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<TFDDateWiseTourData> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iTourCategoryCodes", typeof(int));
            UDTable.Columns.Add("iCentreCode", typeof(int));
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("iPersonID", typeof(int));
            UDTable.Columns.Add("sCenterCodeName", typeof(string));
            UDTable.Columns.Add("sTourCategoryText", typeof(string));
            UDTable.Columns.Add("sActualTourInTime", typeof(string));
            UDTable.Columns.Add("sActualTourOutTime", typeof(string));
            UDTable.Columns.Add("bIsApproval", typeof(bool));
            UDTable.Columns.Add("sApprovalRemark", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (TFDDateWiseTourData obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iTourCategoryCodes"] = obj.TourCategoryCodes;
                    dr["iCentreCode"] = obj.CentreCode;
                    dr["dSchFromDate"] = obj.SchFromDate;
                    dr["iPersonID"] = obj.PersonID;
                    dr["sCenterCodeName"] = obj.CentreCodeName != null ? obj.CentreCodeName : "NA";
                    dr["sTourCategoryText"] = obj.TourCategoryText != null ? obj.TourCategoryText : "NA";
                    dr["sActualTourInTime"] = obj.ActualTourInTime != null ? obj.ActualTourInTime : "NA";
                    dr["sActualTourOutTime"] = obj.ActualTourOutTime != null ? obj.ActualTourOutTime : "NA";
                    dr["bIsApproval"] = obj.IsApprovals == "1" ? true : false;
                    dr["sApprovalRemark"] =obj.ApprovalRemark != null && obj.ApprovalRemark !=""? obj.ApprovalRemark : "NA";

                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<ApprovalNoteNo> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sNoteNumber", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (ApprovalNoteNo obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["sNoteNumber"] = obj.NoteNumbers;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<GVMRDataSave> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iGvmrid", typeof(int));
            UDTable.Columns.Add("sNoteNo", typeof(string));
            UDTable.Columns.Add("sActualInRFIDCard", typeof(string));
            UDTable.Columns.Add("dActualTripInDate", typeof(DateTime));
            UDTable.Columns.Add("sActualTripInTime", typeof(string));
            UDTable.Columns.Add("lActualTripInKM", typeof(long));
            UDTable.Columns.Add("sActualOutRFIDCard", typeof(string));
            UDTable.Columns.Add("dActualTripOutDate", typeof(DateTime));
            UDTable.Columns.Add("sActualTripOutTime", typeof(string));
            UDTable.Columns.Add("lActualTripOutKM", typeof(long));
            UDTable.Columns.Add("sRemark", typeof(string));
            UDTable.Columns.Add("iCenterCode", typeof(int));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (GVMRDataSave obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iGvmrid"] = obj.Gvmrid;
                    dr["sNoteNo"] = obj.NoteNo;
                    dr["sActualInRFIDCard"] = obj.ActualInRFIDCard;
                    dr["dActualTripInDate"] = obj.ActualTripInDate;
                    dr["sActualTripInTime"] = obj.ActualTripInTime;
                    dr["lActualTripInKM"] = obj.ActualTripInKM;
                    dr["sActualOutRFIDCard"] = obj.ActualOutRFIDCard;
                    dr["dActualTripOutDate"] = obj.ActualTripOutDate;
                    dr["sActualTripOutTime"] = obj.ActualTripOutTime;
                    dr["lActualTripOutKM"] = obj.ActualTripOutKM;
                    dr["sRemark"] = obj.Remark;
                    dr["iCenterCode"] = obj.CenterCode;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<Module> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iID", typeof(int));
            UDTable.Columns.Add("sModuleName", typeof(string));
            UDTable.Columns.Add("bIsActive", typeof(bool));
            UDTable.Columns.Add("iUserId", typeof(int));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (Module obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iID"] = obj.ID;
                    dr["sModuleName"] = obj.ModuleName;
                    dr["bIsActive"] = obj.IsActiveInt==1?true:false;
                    dr["iUserId"] = obj.UserId;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<SubModule> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iID", typeof(int));
            UDTable.Columns.Add("iModuleId", typeof(int));
            UDTable.Columns.Add("sSubModuleName", typeof(string));
            UDTable.Columns.Add("bIsActive", typeof(bool));
            UDTable.Columns.Add("iUserId", typeof(int));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (SubModule obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iID"] = obj.ID;
                    dr["iModuleId"] = obj.ModuleId;
                    dr["sSubModuleName"] = obj.SubModuleName;
                    dr["bIsActive"] = obj.IsActiveInt == 1 ? true : false;
                    dr["iUserId"] = obj.UserId;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<Navigations> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iID", typeof(int));
            UDTable.Columns.Add("iModuleId", typeof(int));
            UDTable.Columns.Add("iSubModuleId", typeof(int));
            UDTable.Columns.Add("sNavigationName", typeof(string));
            UDTable.Columns.Add("bIsActive", typeof(bool));
            UDTable.Columns.Add("iUserId", typeof(int));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (Navigations obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iID"] = obj.ID;
                    dr["iModuleId"] = obj.ModuleId;
                    dr["iSubModuleId"] = obj.SubModuleId;
                    dr["sNavigationName"] = obj.NavigationName;
                    dr["bIsActive"] = obj.IsActiveInt == 1 ? true : false;
                    dr["iUserId"] = obj.UserId;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<TaskMaster> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iID", typeof(int));
            UDTable.Columns.Add("iNavigationId", typeof(int));
            UDTable.Columns.Add("iTaskId", typeof(int));
            UDTable.Columns.Add("bIsActive", typeof(bool));
            UDTable.Columns.Add("iUserId", typeof(int));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (TaskMaster obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iID"] = obj.ID!=null?obj.ID:0;
                    dr["iNavigationId"] = obj.NavigationId;
                    dr["iTaskId"] = obj.TaskId;
                    dr["bIsActive"] = obj.IsActiveInt == 1 ? true : false;
                    dr["iUserId"] = obj.UserId;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<Actions> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iID", typeof(int));
            UDTable.Columns.Add("sTaskName", typeof(string));
            UDTable.Columns.Add("iTaskId", typeof(int));
            UDTable.Columns.Add("sActionID", typeof(string));
            UDTable.Columns.Add("iNavigationId", typeof(int));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (Actions obj in customoptions)
                {
                    obj.ActionIDs.Split(',');
                    foreach(var item in obj.ActionIDs.Split(','))
                    {
                        DataRow dr = UDTable.NewRow();
                        dr["iID"] = obj.ID != null ? obj.ID : 0;
                        dr["sTaskName"] = obj.TaskName;
                        dr["iTaskId"] = obj.TaskId;
                        dr["sActionID"] = item;
                        dr["iNavigationId"] = obj.NavigationId;
                        UDTable.Rows.Add(dr);
                    }
                }
            }
        }

        public CommonTable(List<PageInformation> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sControllerName", typeof(string));
            UDTable.Columns.Add("sActionName", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (PageInformation obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["sControllerName"] = obj.ControllerName;
                    dr["sActionName"] = obj.ActionName;
                    UDTable.Rows.Add(dr);
                }
            }
        }

    }
}
