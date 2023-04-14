using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.GVMR;
using CBBW.BOL.CustomModels;
using System.Data;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
    public class GVMRDBMapper
    {
        public GVMRDetails Map_GVMRDetails(DataRow dr)
        {
            GVMRDetails result = new GVMRDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["Gvmrid"]))
                        result.Gvmrid = long.Parse(dr["Gvmrid"].ToString());
                    if (!DBNull.Value.Equals(dr["MGPId"]))
                        result.MGPId = long.Parse(dr["MGPId"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNo = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate =DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleType"]))
                        result.VehicleType = dr["VehicleType"].ToString();
                    if (!DBNull.Value.Equals(dr["ModelName"]))
                        result.ModelName = dr["ModelName"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();                    
                    if (!DBNull.Value.Equals(dr["MonthYear"]))
                        result.MonthYear = dr["MonthYear"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo = int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["LocationType"]))
                        result.LocationType = int.Parse(dr["LocationType"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationCode"]))
                        result.LocationCode = int.Parse(dr["LocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationName"]))
                        result.LocationName = dr["LocationName"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualInRFIDCard"]))
                        result.ActualInRFIDCard = dr["ActualInRFIDCard"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTripInDate"]))
                        result.ActualTripInDate = DateTime.Parse(dr["ActualTripInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripInTime"]))
                        result.ActualTripInTime =dr["ActualTripInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTripInKM"]))
                        result.ActualTripInKM = long.Parse(dr["ActualTripInKM"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualOutRFIDCard"]))
                        result.ActualOutRFIDCard = dr["ActualOutRFIDCard"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTripOutDate"]))
                        result.ActualTripOutDate = DateTime.Parse(dr["ActualTripOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripOutTime"]))
                        result.ActualTripOutTime = dr["ActualTripOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTripOutKM"]))
                        result.ActualTripOutKM =long.Parse(dr["ActualTripOutKM"].ToString());
                    if (!DBNull.Value.Equals(dr["Remark"]))
                        result.Remark = dr["Remark"].ToString();
                    if (!DBNull.Value.Equals(dr["IsActive"]))
                        result.IsActive = bool.Parse(dr["IsActive"].ToString());
                    if (!DBNull.Value.Equals(dr["SchToDate"]))
                        result.SchToDate = DateTime.Parse(dr["SchToDate"].ToString());
                    result.EntryDateDisplay = MyDBLogic.ConvertDateToString(result.EntryDate);
                    result.ActualTripInDateDisplay = MyDBLogic.ConvertDateToString(result.ActualTripInDate);
                    result.ActualTripOutDateDisplay = MyDBLogic.ConvertDateToString(result.ActualTripOutDate);

                    //result.SchFromDateDisplay = MyDBLogic.ConvertDateToString(result.SchFromDate);
                    //result.ToDateVal = MyDBLogic.ConvertDateToString(result.SchToDate);
                    result.SchFromDateDisplay = result.SchFromDate.ToString("yyyy-MM-dd");
                    result.ToDateVal = result.SchToDate.ToString("yyyy-MM-dd");
                }
            }
            catch(Exception ex) { ex.ToString(); }
            return result;
        }


        public GVMRNoteList Map_GVMRNoteList(DataRow dr)
        {
            GVMRNoteList result = new GVMRNoteList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNo = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["LocationName"]))
                        result.LocationName = dr["LocationName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["MonthYear"]))
                        result.MonthYear = dr["MonthYear"].ToString();
                    
                }
            }
            catch { }
            return result;
        }
    }
}
