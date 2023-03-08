using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;
using System.Globalization;
using CBBW.BOL.EHG;
using CBBW.DAL.Entities;

namespace CBBW.DAL.DBMapper
{
    public class EntryIIDBMapper
    {
        EHGMaster master = EHGMaster.GetInstance;
        //EntryIIEntities x = new EntryIIEntities();
        public EntryIINote Map_EntryIINote(DataRow dr)
        {
            EntryIINote result = new EntryIINote();
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
        public EntryIIList Map_EntryIIList(DataRow dr)
        {
            EntryIIList result = new EntryIIList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate =DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDateDispaly"]))
                        result.EntryDateDisplay = dr["EntryDateDispaly"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CentreCode =int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreCodeName"]))
                        result.CentreCodeName = dr["CentreCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmpNo"]))
                        result.EmpNo = dr["EmpNo"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["TotalRecords"]))
                        result.TotalRecord = int.Parse(dr["TotalRecords"].ToString());
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                }
            }
            catch { }
            return result;
        }
        public EntryIITravelingDetails Map_EntryIITravelingDetails(DataRow dr) 
        {
            EntryIITravelingDetails result = new EntryIITravelingDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["PublicTransport"]))
                        result.PublicTransport = bool.Parse(dr["PublicTransport"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType =int.Parse(dr["VehicleType"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonVehicleReq"]))
                        result.ReasonVehicleReq = dr["ReasonVehicleReq"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleTypeProvided"]))
                        result.VehicleTypeProvided =int.Parse(dr["VehicleTypeProvided"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonVehicleProvided"]))
                        result.ReasonVehicleProvided = dr["ReasonVehicleProvided"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate =DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["SchFromTime"]))
                        result.SchFromTime = dr["SchFromTime"].ToString();
                    if (!DBNull.Value.Equals(dr["SchTourToDate"]))
                        result.SchTourToDate =DateTime.Parse(dr["SchTourToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["EmpNoName"]))
                        result.EmpNoName = dr["EmpNoName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    result.SchFromDateDisplay = result.SchFromDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
                    result.SchTourToDateDisplay = result.SchTourToDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    result.VehicleTypeText = master.VehicleTypesForEntryII.Where(o => o.ID == result.VehicleType).FirstOrDefault().DisplayText;
                    result.VehicleTypeProvidedText = master.VehicleTypesForEntryII.Where(o => o.ID == result.VehicleTypeProvided).FirstOrDefault().DisplayText;

                }
            }
            catch { }
            return result;
        }
        public MainLocationPersons Map_MainLocationPersons(DataRow dr) 
        {
            MainLocationPersons result = new MainLocationPersons();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["MainLocationCode"]))
                        result.MainLocationCode = int.Parse(dr["MainLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID =int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonIdnName"]))
                        result.PersonIdnName = dr["PersonIdnName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode =int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName =dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonCenterCode"]))
                        result.PersonCenterCode = int.Parse(dr["PersonCenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonCenterName"]))
                        result.PersonCenterName = dr["PersonCenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied =bool.Parse(dr["TADADenied"].ToString());
                    if (!DBNull.Value.Equals(dr["Isdriver"]))
                        result.Isdriver = int.Parse(dr["Isdriver"].ToString());
                    if (!DBNull.Value.Equals(dr["IsVehicleProvided"]))
                        result.IsVehicleProvided = bool.Parse(dr["IsVehicleProvided"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpNoName"]))
                        result.AuthorisedEmpNoName = dr["AuthorisedEmpNoName"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["SchFromTime"]))
                        result.SchFromTime = dr["SchFromTime"].ToString();
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.SchToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourCategoryText"]))
                        result.TourCategoryText = dr["TourCategoryText"].ToString();
                    if (!DBNull.Value.Equals(dr["RequiredTimeinMinutes"]))
                        result.RequiredTimeinMinutes =int.Parse(dr["RequiredTimeinMinutes"].ToString());
                    if (!DBNull.Value.Equals(dr["MainLocationGenTimeIn"]))
                        result.MainLocationGenTimeIn = DateTime.Parse(dr["MainLocationGenTimeIn"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreTimeIn"]))
                        result.CentreTimeIn = DateTime.Parse(dr["CentreTimeIn"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeIDs"]))
                        result.EmployeeIDs = dr["EmployeeIDs"].ToString();

                    result.PersonTypeText = master.PersonType.Where(o => o.ID == result.PersonType).FirstOrDefault().DisplayText;

                }
            }
            catch { }
            return result;
        }
        public PunchInDetails Map_PunchInDetails(DataRow dr) 
        {
            PunchInDetails result = new PunchInDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber =int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationCode"]))
                        result.LocationCode = int.Parse(dr["LocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationTypeCode"]))
                        result.LocationTypeCode = int.Parse(dr["LocationTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchDate"]))
                        result.PunchDate = DateTime.Parse(dr["PunchDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchIn"]))
                        result.PunchIn =DateTime.Parse(dr["PunchIn"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchOut"]))
                        result.PunchOut = DateTime.Parse(dr["PunchOut"].ToString());
                    if (!DBNull.Value.Equals(dr["EarlyMorningPunch"]))
                        result.EarlyMorningPunch =DateTime.Parse(dr["EarlyMorningPunch"].ToString());
                    if (!DBNull.Value.Equals(dr["LateNightPunch"]))
                        result.LateNightPunch = DateTime.Parse(dr["LateNightPunch"].ToString());
                    result.PunchInStr = result.PunchIn.Year==0?"-":result.PunchIn.ToString("hh:mm tt");
                    result.PunchOutStr = result.PunchOut.Year==0?"-":result.PunchOut.ToString("hh:mm tt");
                }
            }
            catch { }
            return result;
        }
        public LastCentrePunchOut Map_LastCentrePunchOut(DataRow dr)
        {
            LastCentrePunchOut result = new LastCentrePunchOut();
            try
            {
                if (dr != null)
                {
                    //if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                    //    result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationCode"]))
                        result.LocationCode = int.Parse(dr["LocationCode"].ToString());
                    //if (!DBNull.Value.Equals(dr["LocationTypeCode"]))
                    //    result.LocationTypeCode = int.Parse(dr["LocationTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchDate"]))
                        result.PunchDate = DateTime.Parse(dr["PunchDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchOut"]))
                        result.PunchOut = DateTime.Parse(dr["PunchOut"].ToString());
                }
            }
            catch { }
            return result;
        }
        public LastCentrePunchOutWithEmpNo Map_LastCentrePunchOutWithEmpNo(DataRow dr)
        {
            LastCentrePunchOutWithEmpNo result = new LastCentrePunchOutWithEmpNo();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationCode"]))
                        result.LocationCode = int.Parse(dr["LocationCode"].ToString());
                    //if (!DBNull.Value.Equals(dr["LocationTypeCode"]))
                    //    result.LocationTypeCode = int.Parse(dr["LocationTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchDate"]))
                        result.PunchDate = DateTime.Parse(dr["PunchDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchOut"]))
                        result.PunchOut = DateTime.Parse(dr["PunchOut"].ToString());
                }
            }
            catch { }
            return result;
        }
        public LastCentrePunchOutWithDistance Map_LastCentrePunchOutWithDistance(DataRow dr)
        {
            LastCentrePunchOutWithDistance result = new LastCentrePunchOutWithDistance();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationCode"])) 
                    {
                        result.LocationCode = int.Parse(dr["LocationCode"].ToString());
                        result.ErrorMsg = "Last Punch Out Location Is Unknown";
                    }                        
                    //if (!DBNull.Value.Equals(dr["LocationTypeCode"]))
                    //    result.LocationTypeCode = int.Parse(dr["LocationTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchDate"]))
                        result.PunchDate = DateTime.Parse(dr["PunchDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchOut"]))
                        result.PunchOut = DateTime.Parse(dr["PunchOut"].ToString());
                    if (!DBNull.Value.Equals(dr["DistanceInKm"]))
                        result.DistanceInKm = int.Parse(dr["DistanceInKm"].ToString());
                    if (!DBNull.Value.Equals(dr["ComVehicleMinutes"]))
                        result.ComVehicleMinutes = int.Parse(dr["ComVehicleMinutes"].ToString());
                    if (!DBNull.Value.Equals(dr["PubTransMinutes"]))
                        result.PubTransMinutes = int.Parse(dr["PubTransMinutes"].ToString());
                    result.IsEarlyMorningPunchNotRequired = true;
                    result.ComVehRequiredPunchIn = result.PunchOut.AddMinutes(result.ComVehicleMinutes);
                    result.PubTransRequiredPunchIn = result.PunchOut.AddMinutes(result.PubTransMinutes);
                }
            }
            catch { }
            return result;
        }
        public LocationWisePersons Map_LocationWisePersons(DataRow dr) 
        {
            LocationWisePersons result = new LocationWisePersons();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["MainLocationCode"]))
                        result.MainLocationCode = int.Parse(dr["MainLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonIdnName"]))
                        result.PersonIdnName = dr["PersonIdnName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName = dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonCenterCode"]))
                        result.PersonCenterCode = int.Parse(dr["PersonCenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonCenterName"]))
                        result.PersonCenterName = dr["PersonCenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied = bool.Parse(dr["TADADenied"].ToString());
                    if (!DBNull.Value.Equals(dr["Isdriver"]))
                        result.Isdriver = int.Parse(dr["Isdriver"].ToString());
                    if (!DBNull.Value.Equals(dr["IsVehicleProvided"]))
                        result.IsVehicleProvided = bool.Parse(dr["IsVehicleProvided"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpNoName"]))
                        result.AuthorisedEmpNoName = dr["AuthorisedEmpNoName"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["SchFromTime"]))
                        result.SchFromTime = dr["SchFromTime"].ToString();
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.SchToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["DWFromDate"]))
                        result.DWFromDate = DateTime.Parse(dr["DWFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["DWToDate"]))
                        result.DWToDate = DateTime.Parse(dr["DWToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["DWTourCategoryIds"]))
                        result.DWTourCategoryIds =dr["DWTourCategoryIds"].ToString();
                    if (!DBNull.Value.Equals(dr["DWTourCategoryNames"]))
                        result.DWTourCategoryNames = dr["DWTourCategoryNames"].ToString();
                    if (!DBNull.Value.Equals(dr["DWTourCenterCodeIds"]))
                        result.DWTourCenterCodeIds = dr["DWTourCenterCodeIds"].ToString();
                    if (!DBNull.Value.Equals(dr["DWTourCenterNames"]))
                        result.DWTourCenterNames = dr["DWTourCenterNames"].ToString();
                    if (!DBNull.Value.Equals(dr["DWBranchCodes"]))
                        result.DWBranchCodes = dr["DWBranchCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["DWBranchNames"]))
                        result.DWBranchNames = dr["DWBranchNames"].ToString();
                    if (!DBNull.Value.Equals(dr["MCurDate"]))
                        result.MCurDate = DateTime.Parse(dr["MCurDate"].ToString());
                    if (!DBNull.Value.Equals(dr["MainLocationGenTimeIn"]))
                        result.MainLocationGenTimeIn = DateTime.Parse(dr["MainLocationGenTimeIn"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreTimeIn"]))
                        result.CentreTimeIn = DateTime.Parse(dr["CentreTimeIn"].ToString());

                    result.PersonTypeText = master.PersonType.Where(o => o.ID == result.PersonType).FirstOrDefault().DisplayText;

                }
            }
            catch { }
            return result;
        }
        public EntryIIPersons Map_EntryIIPersons(DataRow dr) 
        {
            EntryIIPersons result = new EntryIIPersons();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["MainLocationCode"]))
                        result.MainLocationCode = int.Parse(dr["MainLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonIdnName"]))
                        result.PersonIdnName = dr["PersonIdnName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName = dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonCenterCode"]))
                        result.PersonCenterCode = int.Parse(dr["PersonCenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonCenterName"]))
                        result.PersonCenterName = dr["PersonCenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied = bool.Parse(dr["TADADenied"].ToString());
                    if (!DBNull.Value.Equals(dr["Isdriver"]))
                        result.Isdriver = int.Parse(dr["Isdriver"].ToString());
                    if (!DBNull.Value.Equals(dr["IsVehicleProvided"]))
                        result.IsVehicleProvided = bool.Parse(dr["IsVehicleProvided"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthorisedEmpNoName"]))
                        result.AuthorisedEmpNoName = dr["AuthorisedEmpNoName"].ToString();
                    
                    result.PersonTypeText = master.PersonType.Where(o => o.ID == result.PersonType).FirstOrDefault().DisplayText;

                }
            }
            catch { }
            return result;
        }
        public LocationWiseTPDetails Map_LocationWiseTPDetails(DataSet ds) 
        {
            LocationWiseTPDetails result = new LocationWiseTPDetails();
            List<EntryIIPersons> resultPersons = new List<EntryIIPersons>();
            List<LocationWisePersons> resultDateWise = new List<LocationWisePersons>();
            List<EmpDate> forPunching = new List<EmpDate>();
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        resultPersons.Add(Map_EntryIIPersons(dt.Rows[i]));
                    }
                }
                
                dt = ds.Tables[1];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LocationWisePersons personinfo = Map_LocationWisePersons(dt.Rows[i]);
                        EmpDate forobj1 = new EmpDate() { PunchDate = personinfo.MCurDate, EmpNumber = personinfo.PersonID };
                        resultDateWise.Add(personinfo);
                        forPunching.Add(forobj1);
                    }
                }
            }
            forPunching = forPunching.Distinct().ToList();
            result.PersonDetails = resultPersons;
            result.PersonDateWiseDetails = resultDateWise;
            result.EmpDatesForPunching = forPunching;
            return result;
        }
        public SaveTPDetails Map_SaveTPDetails(DataRow dr) 
        {
            SaveTPDetails result = new SaveTPDetails();
            try
            {
                if (dr != null)
                {                    
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonTypeText"]))
                        result.PersonTypeText = dr["PersonTypeText"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonIDName"]))
                        result.PersonIdnName = dr["PersonIDName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodeText"]))
                        result.DesignationCodenName = dr["DesignationCodeText"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonCentre"]))
                        result.PersonCenterCode = int.Parse(dr["PersonCentre"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonCentreName"]))
                        result.PersonCenterName = dr["PersonCentreName"].ToString();
                    if (!DBNull.Value.Equals(dr["AuthEmployeeCode"]))
                        result.AuthorisedEmpNo = int.Parse(dr["AuthEmployeeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthEmployeeCodeName"]))
                        result.AuthorisedEmpNoName = dr["AuthEmployeeCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsVehicleProvided"]))
                        result.IsVehicleProvided = bool.Parse(dr["IsVehicleProvided"].ToString());
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied = bool.Parse(dr["TADADenied"].ToString());
                    if (!DBNull.Value.Equals(dr["IsDriver"]))
                        result.Isdriver = int.Parse(dr["IsDriver"].ToString());
                    if (!DBNull.Value.Equals(dr["TourCategoryText"]))
                        result.TourCategoryText = dr["TourCategoryText"].ToString();
                }
            }
            catch { }
            return result;
        }
        public SaveTPDWDetails Map_SaveTPDWDetails(DataRow dr) 
        {
            SaveTPDWDetails result = new SaveTPDWDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["TourCategoryCodes"]))
                        result.DWTourCategoryIds = dr["TourCategoryCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryText"]))
                        result.DWTourCategoryNames = dr["TourCategoryText"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCodes"]))
                        result.DWTourCenterCodeIds = dr["CentreCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCodeName"]))
                        result.DWTourCenterNames = dr["CentreCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["BranchCodes"]))
                        result.DWBranchCodes = dr["BranchCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["BranchNames"]))
                        result.DWBranchNames = dr["BranchNames"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.DWFromDate =DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["RequiredTourInDate"]))
                        result.RequiredTourInDate = DateTime.Parse(dr["RequiredTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["RequiredTourInTime"]))
                        result.RequiredTourInTime = DateTime.Parse(dr["RequiredTourInTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInDate"]))
                        result.ActualTourInDate = DateTime.Parse(dr["ActualTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInTime"]))
                        result.ActualTourInTime = DateTime.Parse(dr["ActualTourInTime"].ToString());
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.DWToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutDate"]))
                        result.ActualTourOutDate = DateTime.Parse(dr["ActualTourOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutTime"]))
                        result.ActualTourOutTime = DateTime.Parse(dr["ActualTourOutTime"].ToString());
                    if (!DBNull.Value.Equals(dr["Status"]))
                        result.TourStatus = int.Parse(dr["Status"].ToString());
                    if (!DBNull.Value.Equals(dr["LNPunchStatus"]))
                        result.LNPunchStatus = dr["LNPunchStatus"].ToString();
                    if (!DBNull.Value.Equals(dr["EMPunchStatus"]))
                        result.EMPunchStatus = dr["EMPunchStatus"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromTime"]))
                        result.SchFromTime =DateTime.Parse(dr["SchFromTime"].ToString());
                }
            }
            catch { }
            return result;
        }
        public SaveVehicleDetails Map_SaveVehicleDetails(DataRow dr) 
        {
            SaveVehicleDetails result = new SaveVehicleDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverID = int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverNoName"]))
                        result.DriverName = dr["DriverNoName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNumber = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType = dr["VehicleType"].ToString();
                    if (!DBNull.Value.Equals(dr["ModelName"]))
                        result.ModelName = dr["ModelName"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourInRFID"]))
                        result.InRFIDCard = dr["ActualTourInRFID"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourInDate"]))
                        result.ActualTourInDate =DateTime.Parse(dr["ActualTourInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInTime"]))
                        result.ActualTourInTime = DateTime.Parse(dr["ActualTourInTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInKMIN"]))
                        result.KMIn = int.Parse(dr["ActualTourInKMIN"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutRFID"]))
                        result.OutRFIDCard = dr["ActualTourOutRFID"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourOutDate"]))
                        result.ActualTourOutDate = DateTime.Parse(dr["ActualTourOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutTime"]))
                        result.ActualTourOutTime = DateTime.Parse(dr["ActualTourOutTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourOutKMOUT"]))
                        result.KMOut = int.Parse(dr["ActualTourOutKMOUT"].ToString());
                    if (!DBNull.Value.Equals(dr["Remarks"]))
                        result.VRemarks = dr["Remarks"].ToString();
                    if (!DBNull.Value.Equals(dr["RequiredKMIn"]))
                        result.RequiredKMIn =int.Parse(dr["RequiredKMIn"].ToString());
                    if (!DBNull.Value.Equals(dr["IsCarryingMaterialIn"]))
                        result.IsCarryingMaterialIn = bool.Parse(dr["IsCarryingMaterialIn"].ToString())?"Yes":"No";
                    if (!DBNull.Value.Equals(dr["IsCarryingMaterialOut"]))
                        result.IsCarryingMaterialOut = bool.Parse(dr["IsCarryingMaterialOut"].ToString())?"Yes":"No";
                }
            }
            catch { }
            return result;
        }
        public EntryIIInnerView Map_EntryIIInnerView(DataSet ds) 
        {
            EntryIIInnerView result = new EntryIIInnerView();
            if (ds != null) 
            {   
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<SaveTPDetails> TPersons = new List<SaveTPDetails>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TPersons.Add(Map_SaveTPDetails(dt.Rows[i]));
                    }
                    result.Persons = TPersons;
                }
                dt = ds.Tables[1];
                if (dt != null && dt.Rows.Count > 0) 
                {
                    List<SaveTPDWDetails> TDWDetails = new List<SaveTPDWDetails>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TDWDetails.Add(Map_SaveTPDWDetails(dt.Rows[i]));
                    }
                    result.DWDetails = TDWDetails;
                }
                dt = ds.Tables[2];
                if (dt != null && dt.Rows.Count > 0) 
                {
                    result.VehicleDetail= Map_SaveVehicleDetails(dt.Rows[0]);                    
                }
            }
            return result;
        }


    }
}
