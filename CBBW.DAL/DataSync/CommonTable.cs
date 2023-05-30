using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.CTV;
using CBBW.BOL.CTV2;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;
using CBBW.BOL.Master;
using CBBW.BOL.MGP;
using CBBW.BOL.TADA;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DataSync
{
    public partial class CommonTable
    {
        public DataTable UDTable { get; set; }
        public CommonTable(List<CTVOtherTripDtls> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dFromDate", typeof(DateTime));
            UDTable.Columns.Add("sFromTime", typeof(string));
            UDTable.Columns.Add("dToDate", typeof(DateTime));
            UDTable.Columns.Add("iFromLocationType", typeof(int));
            UDTable.Columns.Add("sFromLocationTypeName", typeof(string));
            UDTable.Columns.Add("iFromLocation", typeof(int));
            UDTable.Columns.Add("sFromLocationName", typeof(string));
            UDTable.Columns.Add("sToLocationTypeCodes", typeof(string));
            UDTable.Columns.Add("sToLocationCodes", typeof(string));
            UDTable.Columns.Add("sToLocationTypeTexts", typeof(string));
            UDTable.Columns.Add("sToLocationTexts", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (CTVOtherTripDtls obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dFromDate"] = DateTime.Parse(obj.FromDate);
                    dr["sFromTime"] = obj.FromTime;
                    dr["dToDate"] = DateTime.ParseExact(obj.ToDate, "dd/MM/yyyy", null);
                    dr["iFromLocationType"] = obj.FromLocationTypeCode;
                    dr["sFromLocationTypeName"] = obj.FromLocationTypeText;
                    dr["iFromLocation"] = obj.FromLocationCode;
                    dr["sFromLocationName"] = obj.FromLocationText;
                    dr["sToLocationTypeCodes"] = obj.ToLocationTypeCodes;
                    dr["sToLocationCodes"] = obj.ToLocationCodes;
                    dr["sToLocationTypeTexts"] = obj.ToLocationTypeText;
                    dr["sToLocationTexts"] = obj.ToLocationText;                    
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<CustomCheckBoxOption> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iCommonID", typeof(Int32));
            UDTable.Columns.Add("bCommonStatus", typeof(Boolean));
            UDTable.Columns.Add("sCommonValue1", typeof(string));
            UDTable.Columns.Add("sCommonValue2", typeof(string));
            UDTable.Columns.Add("sCommonValue3", typeof(string));
            UDTable.Columns.Add("sCommonValue4", typeof(string));

            customoptions = customoptions.Where(o => o.IsSelected == true).ToList();
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (CustomCheckBoxOption obj in customoptions) 
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iCommonID"] = obj.ID;
                    UDTable.Rows.Add(dr);
                }
            }            
        }
        public CommonTable(List<TADAPubTransOption> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iCommonID", typeof(Int32));
            UDTable.Columns.Add("bCommonStatus", typeof(Boolean));
            UDTable.Columns.Add("sCommonValue1", typeof(string));
            UDTable.Columns.Add("sCommonValue2", typeof(string));
            UDTable.Columns.Add("sCommonValue3", typeof(string));
            UDTable.Columns.Add("sCommonValue4", typeof(string));

            customoptions = customoptions.Where(o => o.IsSelected == true).ToList();
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (CustomCheckBoxOption obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iCommonID"] = obj.ID;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<int> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iCommonID", typeof(Int32));
            UDTable.Columns.Add("bCommonStatus", typeof(Boolean));
            UDTable.Columns.Add("sCommonValue1", typeof(string));
            UDTable.Columns.Add("sCommonValue2", typeof(string));
            UDTable.Columns.Add("sCommonValue3", typeof(string));
            UDTable.Columns.Add("sCommonValue4", typeof(string));

            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (int obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iCommonID"] = obj;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EmpDate> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dPunchDate", typeof(DateTime));
            UDTable.Columns.Add("iEmployeeNumber", typeof(int));            
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (var obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dPunchDate"] = obj.PunchDate;
                    dr["iEmployeeNumber"] = obj.EmpNumber;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<OthTripTemp> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dFromDate", typeof(DateTime));
            UDTable.Columns.Add("sFromTime", typeof(string));
            UDTable.Columns.Add("iFromLocationType", typeof(int));
            UDTable.Columns.Add("iFromLocation", typeof(int));
            UDTable.Columns.Add("iToLocationType", typeof(int));
            UDTable.Columns.Add("iToLocation", typeof(int));
            UDTable.Columns.Add("dToDate", typeof(DateTime));
            UDTable.Columns.Add("iDrivercode", typeof(int));
            UDTable.Columns.Add("sDriverName", typeof(string));
            UDTable.Columns.Add("sToLocationType", typeof(string));
            UDTable.Columns.Add("sToLocation", typeof(string));
            UDTable.Columns.Add("sToLocationTypeStr", typeof(string));
            UDTable.Columns.Add("sToLocationStr", typeof(string));
            UDTable.Columns.Add("sVehicleNo", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (OthTripTemp obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dFromDate"] =DateTime.Parse(obj.FromDate);
                    dr["sFromTime"] = obj.FromTime;
                    dr["iFromLocationType"] = obj.FromCentreTypeCode;
                    dr["iFromLocation"] = obj.FromCentreCode;
                    dr["iToLocationType"] = obj.ToCentreTypeCode;
                    dr["iToLocation"] = obj.ToCentreCode;
                    dr["dToDate"] = DateTime.Parse(obj.ToDate);
                    dr["iDrivercode"] = obj.DriverCode;
                    dr["sDriverName"] = obj.DriverName;
                    dr["sToLocationType"] = obj.ToCentreTypeCodes;
                    dr["sToLocation"] = obj.ToCentreCodes;
                    dr["sToLocationTypeStr"] = obj.ToCentreTypeCodesStr;
                    dr["sToLocationStr"] = obj.ToCentreCodesStr;
                    dr["sVehicleNo"] = obj.VehicleNo;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<OthTripTemp> customoptions,bool IsEdit)
        {            
            UDTable = new DataTable();
            UDTable.Columns.Add("dFromDate", typeof(DateTime));
            UDTable.Columns.Add("sFromTime", typeof(string));
            UDTable.Columns.Add("iFromLocationType", typeof(int));
            UDTable.Columns.Add("iFromLocation", typeof(int));
            UDTable.Columns.Add("iToLocationType", typeof(int));
            UDTable.Columns.Add("iToLocation", typeof(int));
            UDTable.Columns.Add("dToDate", typeof(DateTime));
            UDTable.Columns.Add("iDrivercode", typeof(int));
            UDTable.Columns.Add("sDriverName", typeof(string));
            UDTable.Columns.Add("sToLocationType", typeof(string));
            UDTable.Columns.Add("sToLocation", typeof(string));
            UDTable.Columns.Add("sToLocationTypeStr", typeof(string));
            UDTable.Columns.Add("sToLocationStr", typeof(string));
            UDTable.Columns.Add("sVehicleNo", typeof(string));
            UDTable.Columns.Add("iEditDrivercode", typeof(int));
            UDTable.Columns.Add("sEditDriverName", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (OthTripTemp obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dFromDate"] = DateTime.Parse(obj.FromDate);
                    dr["sFromTime"] = obj.FromTime;
                    dr["iFromLocationType"] = obj.FromCentreTypeCode;
                    dr["iFromLocation"] = obj.FromCentreCode;
                    dr["iToLocationType"] = obj.ToCentreTypeCode;
                    dr["iToLocation"] = obj.ToCentreCode;
                    dr["dToDate"] = DateTime.Parse(obj.ToDate);
                    dr["iDrivercode"] = obj.DriverCode;
                    dr["sDriverName"] = obj.DriverName;
                    dr["sToLocationType"] = obj.ToCentreTypeCodes;
                    dr["sToLocation"] = obj.ToCentreCodes;
                    dr["sToLocationTypeStr"] = obj.ToCentreTypeCodesStr;
                    dr["sToLocationStr"] = obj.ToCentreCodesStr;
                    dr["sVehicleNo"] = obj.VehicleNo;
                    dr["iEditDrivercode"] = obj.EditDriverNo;
                    dr["sEditDriverName"] = obj.EditDriverName;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<LTSDriVerChange> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sNoteNo", typeof(string));
            UDTable.Columns.Add("sVehicleNo", typeof(string));
            UDTable.Columns.Add("dSchDate", typeof(DateTime));
            UDTable.Columns.Add("iDrivercode", typeof(int));
            UDTable.Columns.Add("sDriverName", typeof(string));
            UDTable.Columns.Add("sDriverNonName", typeof(string));            
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (LTSDriVerChange obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["sNoteNo"] = obj.NoteNo;
                    dr["sVehicleNo"] = obj.VehicleNo;
                    dr["dSchDate"] = obj.SchDate;
                    dr["iDrivercode"] = obj.DriverNo;
                    dr["sDriverName"] = obj.DriverName;
                    dr["sDriverNonName"] = obj.DriverNonName;                    
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<LocVehSchFromMat> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dFromDate", typeof(DateTime));
            UDTable.Columns.Add("iFromLocationType", typeof(int));
            UDTable.Columns.Add("sFromLocationTypeName", typeof(string));
            UDTable.Columns.Add("iFromLocation", typeof(int));            
            UDTable.Columns.Add("sFromLocationName", typeof(string));
            UDTable.Columns.Add("sToLocationTypes", typeof(string));
            UDTable.Columns.Add("sToLocations", typeof(string));
            UDTable.Columns.Add("dToDate", typeof(DateTime));
            UDTable.Columns.Add("iDrivercode", typeof(int));
            UDTable.Columns.Add("sDriverName", typeof(string));
            UDTable.Columns.Add("sVehicleNo", typeof(string));
            UDTable.Columns.Add("bCanSchedule", typeof(string));            
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (LocVehSchFromMat obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dFromDate"] = obj.FromDate;
                    dr["iFromLocationType"] = obj.FromCenterTypeCode;
                    dr["iFromLocation"] = obj.FromCentreCode;
                    dr["sFromLocationTypeName"] = obj.FromCenterTypeName;
                    dr["sFromLocationName"] = obj.FromCenterName;
                    dr["sToLocationTypes"] = obj.ToCenterTypeName;
                    dr["sToLocations"] = obj.ToCenterName;
                    dr["dToDate"] = obj.ToDate;
                    dr["iDrivercode"] = 0;
                    dr["sDriverName"] = obj.DriverCodenName;
                    dr["sVehicleNo"] = obj.VehicleNumber;
                    dr["bCanSchedule"] = obj.CanSchedule;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<MGPReferenceDCDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("sNoteNumber", typeof(string));
            UDTable.Columns.Add("dNoteDate", typeof(DateTime));
            UDTable.Columns.Add("iFromLocationCode", typeof(int));
            UDTable.Columns.Add("sFromLocationText", typeof(string));
            UDTable.Columns.Add("iToLocationCode", typeof(int));
            UDTable.Columns.Add("iToLocationText", typeof(string));
            UDTable.Columns.Add("sVehicleNo", typeof(string));
            UDTable.Columns.Add("sCheckFound", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (MGPReferenceDCDetails obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["sNoteNumber"] = obj.NoteNumber;
                    dr["dNoteDate"] = obj.NoteDate;
                    dr["iFromLocationCode"] = obj.FromLocationCode;
                    dr["sFromLocationText"] = obj.FromLocationText;
                    dr["iToLocationCode"] = obj.ToLocationCode;
                    dr["iToLocationText"] = obj.ToLocationText;
                    dr["sVehicleNo"] = obj.VehicleNo;
                    dr["sCheckFound"] = obj.FindOk;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<EHGTravelingPersondtls> customoptions,string AuthEmp)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iPersonType", typeof(int));
            UDTable.Columns.Add("iEmployeeNo", typeof(int));
            UDTable.Columns.Add("sDesignationCodenName", typeof(string));
            UDTable.Columns.Add("dFromDate", typeof(DateTime));
            UDTable.Columns.Add("sFromTime", typeof(string));
            UDTable.Columns.Add("dToDate", typeof(DateTime));
            UDTable.Columns.Add("sPurposeOfVisit", typeof(string));
            UDTable.Columns.Add("bTADADenied", typeof(bool));
            UDTable.Columns.Add("sEmployeeName", typeof(string));
            UDTable.Columns.Add("iDesignationCode", typeof(int));
            UDTable.Columns.Add("bIsAuthorised", typeof(bool));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EHGTravelingPersondtls obj in customoptions)
                {
                    if (obj.PersonType == 1 || obj.PersonType == 2)
                    { obj.EmployeeNonName = obj.EmployeeNonNamecmb; }
                    DataRow dr = UDTable.NewRow();
                    dr["iPersonType"] = obj.PersonType;
                    dr["iEmployeeNo"] = obj.EmployeeNo;
                    dr["sDesignationCodenName"] = obj.DesignationCodenName;
                    dr["dFromDate"] = obj.FromDate;
                    dr["sFromTime"] = obj.FromTime;
                    dr["dToDate"] = obj.ToDate;
                    dr["sPurposeOfVisit"] = obj.PurposeOfVisit;
                    dr["bTADADenied"] = obj.iTADADenied==1?true:false;
                    dr["sEmployeeName"] = obj.EmployeeNonName;
                    dr["iDesignationCode"] = MyDBLogic.getFirstIntegerFromString(obj.DesignationCodenName,'/');
                    dr["bIsAuthorised"] = obj.EmployeeNonName == AuthEmp ? true : false;                    
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<DateWiseTourDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dFromDate", typeof(DateTime));
            UDTable.Columns.Add("dToDate", typeof(DateTime));
            UDTable.Columns.Add("sTourCatCodes", typeof(string));
            UDTable.Columns.Add("sTourCatText", typeof(string));
            UDTable.Columns.Add("sCenterCodes", typeof(string));
            UDTable.Columns.Add("sCenterNames", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (DateWiseTourDetails obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["dFromDate"] = obj.FromDate;
                    dr["dToDate"] = obj.ToDate;
                    dr["sTourCatCodes"] = MyDBLogic.Change_ToComma(obj.TourCatCodes);
                    dr["sTourCatText"] = MyDBLogic.Change_ToComma(obj.TourCatText);
                    dr["sCenterCodes"] = MyDBLogic.Change_ToComma(obj.CenterCodes);
                    dr["sCenterNames"] = MyDBLogic.Change_ToComma(obj.CenterNames);
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<DWTourDetailsFromTable> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("dSchToDate", typeof(DateTime));
            UDTable.Columns.Add("dEditedTourToDate", typeof(DateTime));
            UDTable.Columns.Add("sTourCategoryIds", typeof(string));
            UDTable.Columns.Add("sTourCategoryNames", typeof(string));
            UDTable.Columns.Add("sTourCenterCodeIds", typeof(string));
            UDTable.Columns.Add("sTourCenterNames", typeof(string));
            UDTable.Columns.Add("sBranchCodes", typeof(string));
            UDTable.Columns.Add("sBranchNames", typeof(string));
            UDTable.Columns.Add("iSourceID", typeof(int));            
            UDTable.Columns.Add("bIsEdited", typeof(bool));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (DWTourDetailsFromTable obj in customoptions)
                {
                    string mCentreCodes = "";
                    string mCentreNames = "";
                    string mBranchCodes = "";
                    string mBranchNames = "";
                    if (string.IsNullOrEmpty(obj.CentreCodes) || obj.CentreCodes == "NA")
                    {
                        if (string.IsNullOrEmpty(obj.CentreCodesMulti))
                        {
                            if (string.IsNullOrEmpty(obj.CentreCodesDD))
                            {
                                mCentreCodes = "NA";
                            }
                            else { mCentreCodes = obj.CentreCodesDD; }
                        }
                        else { mCentreCodes = obj.CentreCodesMulti; }
                    }
                    else { mCentreCodes = obj.CentreCodes; }
                    if (string.IsNullOrEmpty(obj.CentreNames) || obj.CentreNames == "NA")
                    {
                        if (string.IsNullOrEmpty(obj.CentreNamesMulti))
                        {
                            if (string.IsNullOrEmpty(obj.CentreNamesDD))
                            {
                                mCentreNames = "NA";
                            }
                            else { mCentreNames = obj.CentreNamesDD; }
                        }
                        else { mCentreNames = obj.CentreNamesMulti; }
                    }
                    else { mCentreNames = obj.CentreNames; }
                    if (string.IsNullOrEmpty(obj.BranchCodes) || obj.BranchCodes == "NA")
                    {
                        if (string.IsNullOrEmpty(obj.BranchCodesDD))
                        {
                            mBranchCodes = "NA";
                        }
                        else { mBranchCodes = obj.BranchCodesDD; }
                    }
                    else { mBranchCodes = obj.BranchCodes; }
                    if (string.IsNullOrEmpty(obj.BranchNames) || obj.BranchNames == "NA")
                    {
                        if (string.IsNullOrEmpty(obj.BranchNamesDD))
                        {
                            mBranchNames = "NA";
                        }
                        else { mBranchNames = obj.BranchNamesDD; }
                    }
                    else { mBranchNames = obj.BranchNames; }                    
                    DataRow dr = UDTable.NewRow();
                    dr["dSchFromDate"] =DateTime.Parse(obj.FromDate);
                    dr["dSchToDate"] =DateTime.Parse(obj.ToDate=="-"?"01/01/0001": obj.ToDate);
                    dr["dEditedTourToDate"] =DateTime.Parse(obj.EditToDate=="-"?"01/01/0001": obj.EditToDate);
                    dr["sTourCategoryIds"] = obj.TourCategoryIDs;
                    dr["sTourCategoryNames"] = obj.TourCategoryNames.Replace("&amp;","&");
                    dr["sTourCenterCodeIds"] =obj.TourCategoryIDs=="6"?"13":mCentreCodes;
                    dr["sTourCenterNames"] = mCentreNames;
                    dr["sBranchCodes"] = mBranchCodes;
                    dr["sBranchNames"] = mBranchNames;
                    dr["iSourceID"] = obj.SourceID;                    
                    dr["bIsEdited"] = true;
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<SaveTPDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iPersonType", typeof(int));
            UDTable.Columns.Add("sPersonTypeText", typeof(string));
            UDTable.Columns.Add("iPersonID", typeof(int));
            UDTable.Columns.Add("sPersonIDName", typeof(string));
            UDTable.Columns.Add("iDesignationCode", typeof(int));
            UDTable.Columns.Add("sDesignationCodeText", typeof(string));
            UDTable.Columns.Add("iPersonCentre", typeof(int));
            UDTable.Columns.Add("sPersonCentreName", typeof(string));
            UDTable.Columns.Add("iAuthEmployeeCode", typeof(int));
            UDTable.Columns.Add("sAuthEmployeeCodeName", typeof(string));
            UDTable.Columns.Add("bIsVehicleProvided", typeof(bool));
            UDTable.Columns.Add("bTADADenied", typeof(bool));
            UDTable.Columns.Add("bIsDriver", typeof(int));
            UDTable.Columns.Add("sTourCategoryText", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (var obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iPersonType"] = obj.PersonType;
                    dr["sPersonTypeText"] = obj.PersonTypeText;
                    dr["iPersonID"] = obj.PersonID;
                    dr["sPersonIDName"] = obj.PersonIdnName;
                    dr["iDesignationCode"] = obj.DesignationCode;
                    dr["sDesignationCodeText"] = obj.DesignationCodenName;
                    dr["iPersonCentre"] = obj.PersonCenterCode;
                    dr["sPersonCentreName"] = obj.PersonCenterName;
                    dr["iAuthEmployeeCode"] = MyCodeHelper.GetEmpNoFromString(obj.AuthorisedEmpNoName);
                    dr["sAuthEmployeeCodeName"] = obj.AuthorisedEmpNoName;
                    dr["bIsVehicleProvided"] = obj.IsVehicleProvided;
                    dr["bTADADenied"] = obj.TADADenied;
                    dr["bIsDriver"] = obj.Isdriver;
                    dr["sTourCategoryText"] = !string.IsNullOrEmpty(obj.TourCategoryText)? obj.TourCategoryText.Replace("&amp;","&"):"";
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<SaveTPDWDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iPersonID", typeof(int));
            UDTable.Columns.Add("sTourCategoryCodes", typeof(string));
            UDTable.Columns.Add("sTourCategoryText", typeof(string));
            UDTable.Columns.Add("sCentreCodes", typeof(string));
            UDTable.Columns.Add("sCentreCodeName", typeof(string));
            UDTable.Columns.Add("sBranchCodes", typeof(string));
            UDTable.Columns.Add("sBranchNames", typeof(string));
            UDTable.Columns.Add("dSchFromDate", typeof(DateTime));
            UDTable.Columns.Add("dRequiredTourInDate", typeof(DateTime));
            UDTable.Columns.Add("sRequiredTourInTime", typeof(string));
            UDTable.Columns.Add("dActualTourInDate", typeof(DateTime));
            UDTable.Columns.Add("sActualTourInTime", typeof(string));
            UDTable.Columns.Add("dSchToDate", typeof(DateTime));
            UDTable.Columns.Add("dActualTourOutDate", typeof(DateTime));
            UDTable.Columns.Add("sActualTourOutTime", typeof(string));
            UDTable.Columns.Add("iStatus", typeof(int));
            UDTable.Columns.Add("sLNPunchStatus", typeof(string));
            UDTable.Columns.Add("sEMPunchStatus", typeof(string));
            UDTable.Columns.Add("sSchFromTime", typeof(string));
            UDTable.Columns.Add("sDWLNPunch", typeof(string));
            UDTable.Columns.Add("sDWEMPunch", typeof(string));
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (var obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iPersonID"] = obj.PersonID;
                    dr["sTourCategoryCodes"] = obj.DWTourCategoryIds;
                    dr["sTourCategoryText"] = !string.IsNullOrEmpty(obj.DWTourCategoryNames)? obj.DWTourCategoryNames.Replace("&amp;", "&"):"";
                    dr["sCentreCodes"] = obj.DWTourCenterCodeIds;
                    dr["sCentreCodeName"] = obj.DWTourCenterNames;
                    dr["sBranchCodes"] = obj.DWBranchCodes;
                    dr["sBranchNames"] = obj.DWBranchNames;
                    dr["dSchFromDate"] = obj.DWFromDate;
                    dr["dRequiredTourInDate"] = obj.RequiredTourInDate;
                    dr["sRequiredTourInTime"] = obj.RequiredTourInTime.Year==1?"": obj.RequiredTourInTime.ToString("hh:mm:ss tt");
                    dr["dActualTourInDate"] = obj.ActualTourInDate;
                    dr["sActualTourInTime"] = obj.ActualTourInTime.Year==1?"": obj.ActualTourInTime.ToString("hh:mm:ss tt");
                    dr["dSchToDate"] = obj.DWToDate;
                    dr["dActualTourOutDate"] = obj.ActualTourOutDate;
                    dr["sActualTourOutTime"] = obj.ActualTourOutTime.Year==1?"": obj.ActualTourOutTime.ToString("hh:mm:ss tt");
                    dr["iStatus"] = obj.TourStatus;
                    dr["sLNPunchStatus"] = obj.LNPunchStatus;
                    dr["sEMPunchStatus"] = obj.EMPunchStatus;
                    dr["sSchFromTime"] = obj.SchFromTime.Year==1?"":obj.SchFromTime.ToString("hh:mm:ss tt");
                    dr["sDWLNPunch"] = obj.DWLNPunch.Year==1?"": obj.DWLNPunch.ToString("hh:mm:ss tt");
                    dr["sDWEMPunch"] = obj.DWEMPunch.Year==1?"": obj.DWEMPunch.ToString("hh:mm:ss tt");
                    UDTable.Rows.Add(dr);
                }
            }
        }
        public CommonTable(List<SaveVehicleDetails> customoptions)
        {
            UDTable = new DataTable();
            UDTable.Columns.Add("iDriverNo", typeof(int));
            UDTable.Columns.Add("sDriverNoName", typeof(string));
            UDTable.Columns.Add("sVehicleNo", typeof(string));
            UDTable.Columns.Add("sVehicleType", typeof(string));
            UDTable.Columns.Add("sModelName", typeof(string));
            UDTable.Columns.Add("sActualTourInRFID", typeof(string));
            UDTable.Columns.Add("dActualTourInDate", typeof(DateTime));
            UDTable.Columns.Add("sActualTourInTime", typeof(string));
            UDTable.Columns.Add("iActualTourInKMIN", typeof(int));
            UDTable.Columns.Add("dActualTourOutDate", typeof(DateTime));
            UDTable.Columns.Add("sActualTourOutTime", typeof(string));
            UDTable.Columns.Add("iActualTourOutKMOUT", typeof(int));
            UDTable.Columns.Add("sRemarks", typeof(string));
            UDTable.Columns.Add("sActualTourOutRFID", typeof(string));
            UDTable.Columns.Add("iRequiredKMIn", typeof(int));
            UDTable.Columns.Add("bIsCarryingMaterialIn", typeof(bool));
            UDTable.Columns.Add("bIsCarryingMaterialOut", typeof(bool));            
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (var obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iDriverNo"] = obj.DriverID;
                    dr["sDriverNoName"] = obj.DriverName;
                    dr["sVehicleNo"] = obj.VehicleNumber;
                    dr["sVehicleType"] = obj.VehicleType;
                    dr["sModelName"] = obj.ModelName;
                    dr["sActualTourInRFID"] = obj.InRFIDCard;
                    dr["dActualTourInDate"] = obj.ActualTourInDate;
                    dr["sActualTourInTime"] = obj.ActualTourInTime.Year==1?"": obj.ActualTourInTime.ToString("hh:mm:ss tt");
                    dr["iActualTourInKMIN"] = obj.KMIn;
                    dr["dActualTourOutDate"] = obj.ActualTourOutDate;
                    dr["sActualTourOutTime"] = obj.ActualTourOutTime.Year==1?"": obj.ActualTourOutTime.ToString("hh:mm:ss tt");
                    dr["iActualTourOutKMOUT"] = obj.KMOut;
                    dr["sRemarks"] = obj.VRemarks;
                    dr["sActualTourOutRFID"] = obj.OutRFIDCard;
                    dr["iRequiredKMIn"] = obj.RequiredKMIn;
                    dr["bIsCarryingMaterialIn"] =string.IsNullOrEmpty(obj.IsCarryingMaterialIn)?false:obj.IsCarryingMaterialIn.ToUpper()=="YES"?true:false;
                    dr["bIsCarryingMaterialOut"] =string.IsNullOrEmpty(obj.IsCarryingMaterialOut)?false:obj.IsCarryingMaterialOut.ToUpper() == "YES" ? true : false;                     
                    UDTable.Rows.Add(dr);
                }
            }
        }

    }
}
