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
                    if (!DBNull.Value.Equals(dr["IsRFIDCentres"]))
                        result.IsRFIDCentres = bool.Parse(dr["IsRFIDCentres"].ToString());
                    result.EntryDateDisplay = MyDBLogic.ConvertDateToString(result.EntryDate);
                    result.ActualTripInDateDisplay = MyDBLogic.ConvertDateToString(result.ActualTripInDate);
                    result.ActualTripOutDateDisplay = MyDBLogic.ConvertDateToString(result.ActualTripOutDate);

                    result.SchFromDateEntry = MyDBLogic.ConvertDateToString(result.SchFromDate);
                    //result.ToDateVal = MyDBLogic.ConvertDateToString(result.SchToDate);
                    result.SchFromDateDisplay = result.SchFromDate.ToString("yyyy-MM-dd");
                    result.ToDateVal = result.SchToDate.ToString("yyyy-MM-dd");
                }
            }
            catch(Exception ex) { ex.ToString(); }
            return result;
        }
        public PunchingDetails Map_PunchingDetails(DataRow dr)
        {
            PunchingDetails result = new PunchingDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["LocationCode"]))
                        result.LocationCode = int.Parse(dr["LocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["LocationTypeCode"]))
                        result.LocationTypeCode = int.Parse(dr["LocationTypeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchIn"]))
                        result.PunchIn = DateTime.Parse(dr["PunchIn"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchOut"]))
                        result.PunchOut = DateTime.Parse(dr["PunchOut"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchInDate"]))
                        result.PunchInDate = DateTime.Parse(dr["PunchInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchinTime"]))
                        result.PunchinTime = dr["PunchinTime"].ToString();
                    if (!DBNull.Value.Equals(dr["PunchOutDate"]))
                        result.PunchOutDate = DateTime.Parse(dr["PunchOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PunchOutTime"]))
                        result.PunchOutTime = dr["PunchOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["IsRFIDCentre"]))
                        result.IsRFIDCentre = bool.Parse(dr["IsRFIDCentre"].ToString());
                    result.PunchOutDatestr = MyDBLogic.ConvertDateToString(result.PunchOutDate);
                    result.PunchInDatestr = MyDBLogic.ConvertDateToString(result.PunchInDate);
                }
            }
            catch (Exception ex) { ex.ToString(); }
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
        public GetGVMRDetailsWithPunching Map_GetGVMRDetailsWithPunching(DataTable sts, DataTable cco)
        {
            GetGVMRDetailsWithPunching result = new GetGVMRDetailsWithPunching();
            try
            {
                List<PunchingDetails> objlist1 = new List<PunchingDetails>();
                List<GVMRDetails> objlist2 = new List<GVMRDetails>();

                if (sts != null && sts.Rows.Count > 0)
                {
                    for (int i = 0; i < sts.Rows.Count; i++)
                    {
                        PunchingDetails obj1 = new PunchingDetails();
                        if (!DBNull.Value.Equals(sts.Rows[i]["LocationCode"]))
                            obj1.LocationCode = int.Parse(sts.Rows[i]["LocationCode"].ToString());
                        if (!DBNull.Value.Equals(sts.Rows[i]["LocationTypeCode"]))
                            obj1.LocationTypeCode = int.Parse(sts.Rows[i]["LocationTypeCode"].ToString());
                        if (!DBNull.Value.Equals(sts.Rows[i]["PunchIn"]))
                            obj1.PunchIn = DateTime.Parse(sts.Rows[i]["PunchIn"].ToString());
                        if (!DBNull.Value.Equals(sts.Rows[i]["PunchOut"]))
                            obj1.PunchOut = DateTime.Parse(sts.Rows[i]["PunchOut"].ToString());
                        if (!DBNull.Value.Equals(sts.Rows[i]["PunchInDate"]))
                            obj1.PunchInDate = DateTime.Parse(sts.Rows[i]["PunchInDate"].ToString());
                        if (!DBNull.Value.Equals(sts.Rows[i]["PunchinTime"]))
                            obj1.PunchinTime = sts.Rows[i]["PunchinTime"].ToString();
                        if (!DBNull.Value.Equals(sts.Rows[i]["PunchOutDate"]))
                            obj1.PunchOutDate = DateTime.Parse(sts.Rows[i]["PunchOutDate"].ToString());
                        if (!DBNull.Value.Equals(sts.Rows[i]["PunchOutTime"]))
                            obj1.PunchOutTime = sts.Rows[i]["PunchOutTime"].ToString();
                        if (!DBNull.Value.Equals(sts.Rows[i]["IsRFIDCentre"]))
                            obj1.IsRFIDCentre = bool.Parse(sts.Rows[i]["IsRFIDCentre"].ToString());
                        objlist1.Add(obj1);
                    }
                }
                if (cco != null && cco.Rows.Count > 0)
                {
                    for (int i = 0; i < cco.Rows.Count; i++)
                    {
                        objlist2.Add(Map_GVMRDetails(cco.Rows[i]));
                    }
                }
                result.punchingdetails = objlist1;
                result.gvmrdetails = objlist2;
            }
            catch { }
            return result;
        }
    }
}
