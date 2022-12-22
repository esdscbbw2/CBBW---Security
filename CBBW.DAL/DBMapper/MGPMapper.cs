using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.MGP;

namespace CBBW.DAL.DBMapper
{
    public class MGPMapper
    {
        #region For Out Details
        public MGPNotes Map_MGPNotes(DataRow dr)
        {
            MGPNotes result = new MGPNotes();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNo = dr["NoteNo"].ToString();
                    //if (!DBNull.Value.Equals(dr["CenterCode"]))
                    //    result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    //if (!DBNull.Value.Equals(dr["CreateEmployeeNo"]))
                    //    result.CreateEmployeeNo = int.Parse(dr["CreateEmployeeNo"].ToString());
                }
            }
            catch { }
            return result;
        }
        public MGPOutInDetails Map_MGPOutInDetails(DataRow dr)
        {
            MGPOutInDetails result = new MGPOutInDetails();
            try
            {
                if (dr != null)
                {

                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = long.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNo = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo = long.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();

                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationText"]))
                        result.DesignationText = dr["DesignationText"].ToString();
                    if (!DBNull.Value.Equals(dr["TripType"]))
                        result.TripType = int.Parse(dr["TripType"].ToString());
                    if (!DBNull.Value.Equals(dr["ToLocationCodenName"]))
                        result.ToLocationCodenName = dr["ToLocationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["CarryingOutMaterial"]))
                        result.CarryingOutMaterial = bool.Parse(dr["CarryingOutMaterial"].ToString());
                    if (!DBNull.Value.Equals(dr["LoadPercentage"]))
                        result.LoadPercentage = float.Parse(dr["LoadPercentage"].ToString());
                    if (!DBNull.Value.Equals(dr["RFIDOut"]))
                        result.RFIDOut = dr["RFIDOut"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripOutDate"]))
                        result.ActualTripOutDate = DateTime.Parse(dr["ActualTripOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripOutTime"]))
                        result.ActualTripOutTime = dr["ActualTripOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["KMSOut"]))
                        result.KMSOut = long.Parse(dr["KMSOut"].ToString());
                    if (!DBNull.Value.Equals(dr["OutRemarks"]))
                        result.OutRemarks = dr["OutRemarks"].ToString();
                    if (!DBNull.Value.Equals(dr["OutActive"]))
                        result.OutActive = bool.Parse(dr["OutActive"].ToString());
                    if (!DBNull.Value.Equals(dr["InActive"]))
                        result.InActive = bool.Parse(dr["InActive"].ToString());

                    result.FromDate = result.SchFromDate.ToString("dd-MM-yyyy");
                    result.ATripOutDate = result.ActualTripOutDate.ToString("dd-MM-yyyy");
                    //For Existing In details
                    if (!DBNull.Value.Equals(dr["RFIDCardIn"]))
                        result.RFIDCardIn = dr["RFIDCardIn"].ToString();
                    if (!DBNull.Value.Equals(dr["FromLocationType"]))
                        result.FromLocationType = int.Parse(dr["FromLocationType"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationCode"]))
                        result.FromLocationCode = int.Parse(dr["FromLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationName"]))
                        result.FromLocationName = dr["FromLocationName"].ToString();

                    if (!DBNull.Value.Equals(dr["CarryingInMaterial"]))
                        result.CarryingInMaterial = bool.Parse(dr["CarryingInMaterial"].ToString());
                    if (!DBNull.Value.Equals(dr["LoadPercentageIn"]))
                        result.LoadPercentageIn = float.Parse(dr["LoadPercentageIn"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripInDate"]))
                        result.ActualTripInDate = DateTime.Parse(dr["ActualTripInDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripInTime"]))
                        result.ActualTripInTime = dr["ActualTripInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["RequiredKmIn"]))
                        result.RequiredKmIn = int.Parse(dr["RequiredKmIn"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualKmIn"]))
                        result.ActualKmIn = int.Parse(dr["ActualKmIn"].ToString());
                    if (!DBNull.Value.Equals(dr["KMRunInTrip"]))
                        result.KMRunInTrip = int.Parse(dr["KMRunInTrip"].ToString());
                    if (!DBNull.Value.Equals(dr["RemarkIn"]))
                        result.RemarkIn = dr["RemarkIn"].ToString();
                    result.ActualTripInD = result.ActualTripInDate.ToString("dd-MM-yyyy");

                }
            }
            catch { }
            return result;
        }
        // Getting data for Out details in Item Wise Details using NoteNo(For New Data insert)
        public MGPItemWiseDetails Map_MGPItemWiseDetails(DataRow dr)
        {
            MGPItemWiseDetails result = new MGPItemWiseDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["ItemTypeCode"]))
                        result.ItemTypeCode = int.Parse(dr["ItemTypeCode"].ToString());

                    if (!DBNull.Value.Equals(dr["ItemTypeText"]))
                        result.ItemTypeText = dr["ItemTypeText"].ToString();
                    if (!DBNull.Value.Equals(dr["ItemCode"]))
                        result.ItemCode = int.Parse(dr["ItemCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ItemText"]))
                        result.ItemText = dr["ItemText"].ToString();
                    if (!DBNull.Value.Equals(dr["UOMCode"]))
                        result.UOMCode = int.Parse(dr["UOMCode"].ToString());
                    if (!DBNull.Value.Equals(dr["UOMText"]))
                        result.UOMText = dr["UOMText"].ToString();
                    if (!DBNull.Value.Equals(dr["QuantityBag"]))
                        result.QuantityBag = int.Parse(dr["QuantityBag"].ToString());
                    if (!DBNull.Value.Equals(dr["QuantityKg"]))
                        result.QuantityKg = int.Parse(dr["QuantityKg"].ToString());
                }
            }
            catch { }
            return result;
        }
        // Getting data for Out details in Reference DC Details using NoteNo(For New Data insert)
        public MGPReferenceDCDetails Map_MGPReferenceDCDetails(DataRow dr)
        {
            MGPReferenceDCDetails result = new MGPReferenceDCDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["RefNoteNumber"]))
                        result.RefNoteNumber = dr["RefNoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["NoteDate"]))
                        result.NoteDate = DateTime.Parse(dr["NoteDate"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationCode"]))
                        result.FromLocationCode = int.Parse(dr["FromLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationText"]))
                        result.FromLocationText = dr["FromLocationText"].ToString();
                    if (!DBNull.Value.Equals(dr["ToLocationCode"]))
                        result.ToLocationCode = int.Parse(dr["ToLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ToLocationText"]))
                        result.ToLocationText = dr["ToLocationText"].ToString();
                    result.NoteDatestr = result.NoteDate.ToString("dd-MM-yyyy");

                }
            }
            catch { }
            return result;
        }
        public MGPVehicleOutDetails Map_MGPVehicleOutDetails(DataRow dr)
        {
            MGPVehicleOutDetails result = new MGPVehicleOutDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo = int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["Drivername"]))
                        result.Drivername = dr["Drivername"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationName"]))
                        result.DesignationName = dr["DesignationName"].ToString();
                    if (!DBNull.Value.Equals(dr["TripType"]))
                        result.TripType = int.Parse(dr["TripType"].ToString());
                    if (!DBNull.Value.Equals(dr["TripTypeStr"]))
                        result.TripTypeStr = dr["TripTypeStr"].ToString();
                    if (!DBNull.Value.Equals(dr["ToLocationCodeName"]))
                        result.ToLocationCodeName = dr["ToLocationCodeName"].ToString();
                    //if (!DBNull.Value.Equals(dr["ToLocationCode"]))
                    //    result.ToLocationCode =int.Parse(dr["ToLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CarryingOutMat"]))
                        result.CarryingOutMat = bool.Parse(dr["CarryingOutMat"].ToString());
                    if (!DBNull.Value.Equals(dr["LoadPercentage"]))
                        result.LoadPercentage = int.Parse(dr["LoadPercentage"].ToString());
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["KMOUT"]))
                        result.KMOUT = int.Parse(dr["KMOUT"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleNumber"]))
                        result.VehicleNumber = dr["VehicleNumber"].ToString();


                    result.SchFromDatestr = result.SchFromDate.ToString("dd-MM-yyyy");
                    //if (!DBNull.Value.Equals(dr["FromCentreCode"]))
                    //    result.FromCentreCode = int.Parse(dr["FromCentreCode"].ToString());
                    //if (!DBNull.Value.Equals(dr["FromCenterName"]))
                    //    result.FromCenterName = dr["FromCenterName"].ToString();
                    //if (!DBNull.Value.Equals(dr["ToCentreCodeName"]))
                    //    result.ToCentreCodeName = dr["ToCentreCodeName"].ToString();
                    //if (!DBNull.Value.Equals(dr["Distance"]))
                    //    result.Distance = int.Parse(dr["Distance"].ToString()); 
                    //if (!DBNull.Value.Equals(dr["SCHFromDate"]))
                    //    result.SCHFromDate =DateTime.Parse(dr["SCHFromDate"].ToString());
                    //if (!DBNull.Value.Equals(dr["SCHTodate"]))
                    //    result.SCHTodate = DateTime.Parse(dr["SCHTodate"].ToString());
                    //if (!DBNull.Value.Equals(dr["CarryingOutMaterialStat"]))
                    //    result.CarryingOutMaterialStat =bool.Parse(dr["CarryingOutMaterialStat"].ToString());
                    //if (!DBNull.Value.Equals(dr["LoadPercentage"]))
                    //    result.LoadPercentage =int.Parse(dr["LoadPercentage"].ToString());
                    //if (!DBNull.Value.Equals(dr["KMOut"]))
                    //    result.KMOut = int.Parse(dr["KMOut"].ToString());
                    //if (!DBNull.Value.Equals(dr["EditDriverNo"]))
                    //    result.EditDriverNo = int.Parse(dr["EditDriverNo"].ToString());
                    //if (!DBNull.Value.Equals(dr["EditDriverName"]))
                    //    result.EditDriverName = dr["EditDriverName"].ToString();

                }
            }
            catch { }
            return result;
        }
        public MGPHistoryDCDetails Map_MGPHistoryDCDetails(DataRow dr)
        {
            MGPHistoryDCDetails result = new MGPHistoryDCDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["NoteDate"]))
                        result.NoteDate = DateTime.Parse(dr["NoteDate"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationCode"]))
                        result.FromLocationCode = int.Parse(dr["FromLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationText"]))
                        result.FromLocationText = dr["FromLocationText"].ToString();
                    if (!DBNull.Value.Equals(dr["ToLocationCode"]))
                        result.ToLocationCode = int.Parse(dr["ToLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ToLocationText"]))
                        result.ToLocationText = dr["ToLocationText"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["CheckFound"]))
                        result.CheckFound = dr["CheckFound"].ToString();
                    result.NoteDatestr = result.NoteDate.ToString("dd-MM-yyyy");


                }
            }
            catch { }
            return result;
        }
        #endregion

        #region For In Details
        public MGPCurrentInDetails Map_MGPCurrentInDetails(DataRow dr)
        {
            MGPCurrentInDetails result = new MGPCurrentInDetails();
            try
            {
                if (dr != null)
                {

                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = long.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNo = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo = long.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationText"]))
                        result.DesignationText = dr["DesignationText"].ToString();
                    if (!DBNull.Value.Equals(dr["TripType"]))
                        result.TripType = int.Parse(dr["TripType"].ToString());
                    if (!DBNull.Value.Equals(dr["TripTypeStr"]))
                        result.TripTypeStr = dr["TripTypeStr"].ToString();
                    if (!DBNull.Value.Equals(dr["FromLocationType"]))
                        result.FromLocationType = int.Parse(dr["FromLocationType"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationCode"]))
                        result.FromLocationCode = int.Parse(dr["FromLocationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["FromLocationName"]))
                        result.FromLocationName = dr["FromLocationName"].ToString();
                    if (!DBNull.Value.Equals(dr["CarryingInMaterial"]))
                        result.CarryingInMaterial = bool.Parse(dr["CarryingInMaterial"].ToString());
                    if (!DBNull.Value.Equals(dr["LoadPercentageIn"]))
                        result.LoadPercentageIn = int.Parse(dr["LoadPercentageIn"].ToString());
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["KMSOut"]))
                        result.KMSOut = int.Parse(dr["KMSOut"].ToString());
                    if (!DBNull.Value.Equals(dr["OutActive"]))
                        result.OutActive = bool.Parse(dr["OutActive"].ToString());
                    if (!DBNull.Value.Equals(dr["InActive"]))
                        result.InActive = bool.Parse(dr["InActive"].ToString());
                    if (!DBNull.Value.Equals(dr["RequiredKmIn"]))
                        result.RequiredKmIn = int.Parse(dr["RequiredKmIn"].ToString());
                    if (!DBNull.Value.Equals(dr["ActKmIn"]))
                        result.ActKmIn = int.Parse(dr["ActKmIn"].ToString());
                    if (!DBNull.Value.Equals(dr["RunningKm"]))
                        result.RunningKm = int.Parse(dr["RunningKm"].ToString());
                    result.FromschDates = result.SchFromDate.ToString("dd-MM-yyyy");



                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        #endregion

        #region For List Page(Index page)
  


        public MGPNoteList Map_MGPNoteList(DataRow dr)
        {
            MGPNoteList result = new MGPNoteList();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["RowNum"]))
                        result.RowNumber = int.Parse(dr["RowNum"].ToString());
                    if (!DBNull.Value.Equals(dr["TotalCount"]))
                        result.TotalCount = int.Parse(dr["TotalCount"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["MonthYear"]))
                        result.MonthYear =dr["MonthYear"].ToString();
                   // result.NoteNumber2 = result.NoteNumber;
                }
            }
            catch { }
            return result;
        }
        #endregion


        #region In/Out Button Active
        public ButtonActive Map_ButtonActive(DataRow dr)
        {
            ButtonActive result = new ButtonActive();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["OutButtonActive"]))
                        result.OutButtonActive = bool.Parse(dr["OutButtonActive"].ToString());
                    if (!DBNull.Value.Equals(dr["InButtonActive"]))
                        result.InButtonActive = bool.Parse(dr["InButtonActive"].ToString());

                }
            }
            catch { }
            return result;
        }
        #endregion
    }
}

