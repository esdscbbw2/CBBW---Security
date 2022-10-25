using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
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
                    result.IsActive = result.VehicleStatus == "ACTIVE" ? true : false;
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
                    result.ToDate = result.FromDate.AddDays(MyDBLogic.ReturnDaysFromDistance(result.Distance));
                    
                }
            }
            catch { }
            return result;
        }

        
    }
}
