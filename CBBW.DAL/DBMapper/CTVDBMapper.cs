using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
    public class CTVDBMapper
    {
        public VehicleInfo Map_VehicleInfo(DataRow dr)
        {
            VehicleInfo result = new VehicleInfo();
            try
            {
                if (dr != null)
                {                    
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["Nature"]))
                        result.VehicleType = dr["Nature"].ToString();
                    if (!DBNull.Value.Equals(dr["Manufacturer"]))
                        result.ModelName = dr["Manufacturer"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo =int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleStatus"]))
                        result.VehicleStatus = dr["VehicleStatus"].ToString();
                    if (!DBNull.Value.Equals(dr["ServiceDuaration"]))
                        result.ServiceDuaration = int.Parse(dr["ServiceDuaration"].ToString());
                    if (!DBNull.Value.Equals(dr["IsSuccess"]))
                        result.IsSuccess = bool.Parse(dr["IsSuccess"].ToString());
                    if (!DBNull.Value.Equals(dr["Msg"]))
                        result.Msg = dr["Msg"].ToString();
                    if (!DBNull.Value.Equals(dr["LocalTripRecords"]))
                        result.LocalTripRecords = int.Parse(dr["LocalTripRecords"].ToString());
                    if (!DBNull.Value.Equals(dr["SloatAVBL"]))
                        result.IsSlotAvbl = bool.Parse(dr["SloatAVBL"].ToString());

                    result.IsActive = result.VehicleStatus == "ACTIVE" ? true : false;
                    if (result.DriverName!=null && result.DriverName.IndexOf("-") > 0) 
                    { 
                        String[] spearator = {"-"};
                        result.DriverName = result.DriverName.Split(spearator, StringSplitOptions.RemoveEmptyEntries)[1];
                    }                    
                    result.DriverNonName = result.DriverNo + "/" + result.DriverName;
                }
            }
            catch { }
            return result;
        }
        public UserInfo Map_UserInfo(DataRow dr) 
        {
            UserInfo result = new UserInfo();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CentreCode = int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreName"]))
                        result.CentreName = dr["CentreName"].ToString();
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.EmployeeNumber =int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeName"]))
                        result.EmployeeName = dr["EmployeeName"].ToString();
                    if (!DBNull.Value.Equals(dr["UserName"]))
                        result.UserName = dr["UserName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsOffline"]))
                        result.IsOffline =bool.Parse(dr["IsOffline"].ToString());
                }
            }
            catch { }
            return result;
        }
        public LocVehSchFromMat Map_LocVehSchFromMat(DataRow dr) 
        {
            LocVehSchFromMat result = new LocVehSchFromMat();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["SchDate"]))
                        result.FromDate =DateTime.Parse(dr["SchDate"].ToString());
                    if (!DBNull.Value.Equals(dr["FromCentreTypeCode"]))
                        result.FromCenterTypeCode = int.Parse(dr["FromCentreTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromCentreCode"]))
                        result.FromCentreCode = int.Parse(dr["FromCentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromCenterName"]))
                        result.FromCenterName = dr["FromCenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["ToCentreTypeCode"]))
                        result.ToCentreTypeCode = int.Parse(dr["ToCentreTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ToCentreTypeCodeName"]))
                        result.ToCenterTypeName = dr["ToCentreTypeCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["ToCentreCode"]))
                        result.ToCentreCode = int.Parse(dr["ToCentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ToCentreCodeName"]))
                        result.ToCenterName = dr["ToCentreCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["Distance"]))
                        result.Distance =float.Parse(dr["Distance"].ToString());
                    if (!DBNull.Value.Equals(dr["CanSchedule"]))
                        result.CanSchedule = bool.Parse(dr["CanSchedule"].ToString());
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.ToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["CalcDays"]))
                        result.CalcDays = float.Parse(dr["CalcDays"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverCodenName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["EditDriverCode"]))
                        result.EditDriverNo = int.Parse(dr["EditDriverCode"].ToString());
                    if (!DBNull.Value.Equals(dr["EditDriverName"]))
                        result.EditDriverName = dr["EditDriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["CurrentDriverCode"]))
                        result.CurrentDriverCode = int.Parse(dr["CurrentDriverCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CurrentDriverName"]))
                        result.CurrentDriverName = dr["CurrentDriverName"].ToString();
                    result.FromCenterTypeName = "Centre";
                    result.IsActivetoEdit = result.FromDate >= DateTime.Today ? 1 : 0;
                    //result.ToDate = result.FromDate.AddDays(MyDBLogic.ReturnDaysFromDistance(result.Distance));
                    result.FromDateStr = result.FromDate.ToString("dd-MM-yyyy");
                    result.ToDateStr = result.ToDate.ToString("dd-MM-yyyy");
                }
            }
            catch { }
            return result;
        }
        public CTVHdrDtl Map_CTVHdrDtl(DataRow dr, DataTable dt)
        {
            CTVHdrDtl result = new CTVHdrDtl();
            try
            {
                if (dr != null)
                {
                    TripScheduleHdr hdr = new TripScheduleHdr();
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        hdr.NoteNo = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        hdr.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        hdr.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        hdr.CenterCode=int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["centername"]))
                        hdr.CenterName = dr["centername"].ToString();
                    if (!DBNull.Value.Equals(dr["FortheMonth"]))
                        hdr.FortheMonth = int.Parse(dr["FortheMonth"].ToString());
                    if (!DBNull.Value.Equals(dr["FortheYear"]))
                        hdr.FortheYear = int.Parse(dr["FortheYear"].ToString());
                    if (!DBNull.Value.Equals(dr["FromDate"]))
                        hdr.FromDate = DateTime.Parse(dr["FromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ToDate"]))
                        hdr.ToDate = DateTime.Parse(dr["ToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["Vehicleno"]))
                        hdr.Vehicleno = dr["Vehicleno"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        hdr.DriverNo = int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        hdr.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        hdr.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["TripPurpose"]))
                        hdr.TripPurpose = dr["TripPurpose"].ToString();
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        hdr.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovedDateTime"]))
                        hdr.ApprovedDateTime = DateTime.Parse(dr["ApprovedDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonForDisApproval"]))
                        hdr.ReasonForDisApproval = dr["ReasonForDisApproval"].ToString();
                    if (!DBNull.Value.Equals(dr["Employeenumber"]))
                        hdr.EmployeeNumber = int.Parse(dr["Employeenumber"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovalFor"]))
                        hdr.ApprovalFor = int.Parse(dr["ApprovalFor"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        hdr.VehicleType = dr["VehicleType"].ToString();
                    if (!DBNull.Value.Equals(dr["ModelName"]))
                        hdr.ModelName = dr["ModelName"].ToString();
                    if (hdr.FortheMonth > 0) 
                    {
                        hdr.FortheMonthnYear = new DateTime(2022, hdr.FortheMonth, 1).ToString("MMM") + " " + hdr.FortheYear;
                    }
                    hdr.CentreCodenName = hdr.CenterCode + " / " + hdr.CenterName;                     
                    hdr.DriverNonName = hdr.DriverNo.ToString() + " / " + hdr.DriverName;

                    hdr.FromDateStr = hdr.FromDate.ToString("dd-MM-yyyy");
                    hdr.ToDateStr = hdr.ToDate.ToString("dd-MM-yyyy");
                    hdr.EntryDatestr = MyDBLogic.ConvertDateToString(hdr.EntryDate);
                    result.SchHdrData = hdr;
                }
                if (dt != null && dt.Rows.Count > 0) 
                {
                    List<LocVehSchFromMat> dtl = new List<LocVehSchFromMat>();
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        LocVehSchFromMat x = new LocVehSchFromMat();
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromDate"]))
                            x.FromDate =DateTime.Parse(dt.Rows[i]["FromDate"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromLocationType"]))
                            x.FromCenterTypeCode = int.Parse(dt.Rows[i]["FromLocationType"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromLocation"]))
                            x.FromCentreCode = int.Parse(dt.Rows[i]["FromLocation"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToLocationType"]))
                            x.ToCentreTypeCode = int.Parse(dt.Rows[i]["ToLocationType"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToLocation"]))
                            x.ToCentreCode = int.Parse(dt.Rows[i]["ToLocation"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToDate"]))
                            x.ToDate = DateTime.Parse(dt.Rows[i]["ToDate"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromTime"]))
                            x.FromTime = dt.Rows[i]["FromTime"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["DriverCodenName"]))
                            x.DriverCodenName = dt.Rows[i]["DriverCodenName"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromLocationName"]))
                            x.FromCenterName = dt.Rows[i]["FromLocationName"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromLocationTypeName"]))
                            x.FromCenterTypeName = dt.Rows[i]["FromLocationTypeName"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToLocationTypes"]))
                            x.ToCenterTypeName = dt.Rows[i]["ToLocationTypes"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToLocations"]))
                            x.ToCenterName = dt.Rows[i]["ToLocations"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToLocationTypeCodes"]))
                            x.ToCentreTypeCodes = dt.Rows[i]["ToLocationTypeCodes"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToLocationCodes"]))
                            x.ToCentreCodes = dt.Rows[i]["ToLocationCodes"].ToString();
                        if (!DBNull.Value.Equals(dt.Rows[i]["EditDriverNo"]))
                            x.EditDriverNo = int.Parse(dt.Rows[i]["EditDriverNo"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["EditDriverName"]))
                            x.EditDriverName = dt.Rows[i]["EditDriverName"].ToString();
                        
                        if (!DBNull.Value.Equals(dt.Rows[i]["CurrentDriverCode"]))
                            x.CurrentDriverCode = int.Parse(dt.Rows[i]["CurrentDriverCode"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["CurrentDriverName"]))
                            x.CurrentDriverName = dt.Rows[i]["CurrentDriverName"].ToString();

                        x.IsActivetoEdit = x.FromDate >= DateTime.Today ? 1 : 0;
                        x.FromDateStr = x.FromDate.ToString("dd-MM-yyyy");
                        x.ToDateStr = x.ToDate.ToString("dd-MM-yyyy");
                        x.FromDateStrYMD = x.FromDate.ToString("yyyy-MM-dd");
                        if (result.SchHdrData != null && result.SchHdrData.TripPurpose!=null)
                        { x.TripPurpose = result.SchHdrData.TripPurpose; }
                        //Otherlocation yes no options will come here.

                        dtl.Add(x);
                    }
                    result.SchDetailList = dtl.OrderBy(o=>o.FromDate).ToList();
                }
            }
            catch(Exception ex) { string errmsg = ex.Message; }
            return result;
        }
        public VehicleAvblInfo Map_VehicleAvblInfo(DataRow dr, DataTable bookedslots,DataTable avblslots) 
        {
            VehicleAvblInfo result = new VehicleAvblInfo();
            try
            {
                if (dr != null)
                {                    
                    if (!DBNull.Value.Equals(dr["IsSuccess"]))
                        result.IsSlotAvbl =bool.Parse(dr["IsSuccess"].ToString());
                    if (!DBNull.Value.Equals(dr["Msg"]))
                        result.Msg = dr["Msg"].ToString(); 
                    
                }
                if (bookedslots != null && bookedslots.Rows.Count > 0)
                {
                    List<CustomDateRange> dtl = new List<CustomDateRange>();
                    for (int i = 0; i < bookedslots.Rows.Count; i++)
                    {
                        CustomDateRange x = new CustomDateRange();
                        if (!DBNull.Value.Equals(bookedslots.Rows[i]["FromDate"]))
                            x.FromDate = DateTime.Parse(bookedslots.Rows[i]["FromDate"].ToString());
                        if (!DBNull.Value.Equals(bookedslots.Rows[i]["ToDate"]))
                            x.ToDate = DateTime.Parse(bookedslots.Rows[i]["ToDate"].ToString());

                        dtl.Add(x);
                    }
                    result.SlotsBooked = dtl;
                }
                if (avblslots != null && avblslots.Rows.Count > 0)
                {
                    List<CustomDateRange> dtl2 = new List<CustomDateRange>();
                    for (int i = 0; i < avblslots.Rows.Count; i++)
                    {
                        CustomDateRange x = new CustomDateRange();
                        if (!DBNull.Value.Equals(avblslots.Rows[i]["FromDate"]))
                            x.FromDate = DateTime.Parse(avblslots.Rows[i]["FromDate"].ToString());
                        if (!DBNull.Value.Equals(avblslots.Rows[i]["ToDate"]))
                            x.ToDate = DateTime.Parse(avblslots.Rows[i]["ToDate"].ToString());

                        dtl2.Add(x);
                    }
                    result.SlotsAvailable = dtl2;
                }
            }
            catch { }
            return result;
        }

        public TripScheduleHdr Map_CtvSchedule(DataRow dr, int SL)
        {
            TripScheduleHdr result = new TripScheduleHdr();
            try
            {
                if (dr != null)
                {
                    result.SL = SL;
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNo = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["Vehicleno"]))
                        result.Vehicleno = dr["Vehicleno"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["IsLocked"]))
                        result.IsLocked = int.Parse(dr["IsLocked"].ToString());
                    if (!DBNull.Value.Equals(dr["mResult"]))
                        result.mResult = int.Parse(dr["mResult"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["FortheMonth"]))
                        result.FortheMonth = int.Parse(dr["FortheMonth"].ToString());
                    if (!DBNull.Value.Equals(dr["FortheYear"]))
                        result.FortheYear = int.Parse(dr["FortheYear"].ToString());
                    if (!DBNull.Value.Equals(dr["DeleteLock"]))
                        result.IsDeleteBtn = int.Parse(dr["DeleteLock"].ToString());
                    //if (!DBNull.Value.Equals(dr["ForMonthYear"]))
                    //    result.FortheMonthnYear = dr["ForMonthYear"].ToString();
                    result.FortheMonthnYear = new DateTime(result.FortheYear, result.FortheMonth, 1).ToString("MMM yyyy");
                }
            }
            catch { }
            return result;
        }
    }
}
