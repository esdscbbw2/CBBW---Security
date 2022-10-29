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
                    if (!DBNull.Value.Equals(dr["FromCentreCode"]))
                        result.FromCentreCode = int.Parse(dr["FromCentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromCenterName"]))
                        result.FromCenterName = dr["FromCenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["ToCentreCode"]))
                        result.ToCentreCode = int.Parse(dr["ToCentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ToCenterName"]))
                        result.ToCenterName = dr["ToCenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["Distance"]))
                        result.Distance =float.Parse(dr["Distance"].ToString());
                    result.FromCenterTypeCode = 2;
                    result.ToCentreTypeCode = 2;
                    result.FromCenterTypeName = "Centre";
                    result.ToCenterTypeName = "Centre";
                    result.ToDate = result.FromDate.AddDays(MyDBLogic.ReturnDaysFromDistance(result.Distance));
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

                        x.FromDateStr = x.FromDate.ToString("dd-MM-yyyy");
                        x.ToDateStr = x.ToDate.ToString("dd-MM-yyyy");
                        dtl.Add(x);
                    }
                    result.SchDetailList = dtl;
                }
            }
            catch { }
            return result;
        }
        public VehicleAvblInfo Map_VehicleAvblInfo(DataRow dr, DataTable dt) 
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
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<CustomDateRange> dtl = new List<CustomDateRange>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CustomDateRange x = new CustomDateRange();
                        if (!DBNull.Value.Equals(dt.Rows[i]["FromDate"]))
                            x.FromDate = DateTime.Parse(dt.Rows[i]["FromDate"].ToString());
                        if (!DBNull.Value.Equals(dt.Rows[i]["ToDate"]))
                            x.ToDate = DateTime.Parse(dt.Rows[i]["ToDate"].ToString());

                        dtl.Add(x);
                    }
                    result.SlotsBooked = dtl;
                }
            }
            catch { }
            return result;
        }

    }
}
