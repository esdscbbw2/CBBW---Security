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
        public MGPNotes Map_MGPNotes(DataRow dr) 
        {
            MGPNotes result = new MGPNotes();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNumber = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CreateEmployeeNo"]))
                        result.CreateEmployeeNo = int.Parse(dr["CreateEmployeeNo"].ToString());
                }
            }
            catch { }
            return result;
        }
        public MGPMatOut Map_MGPMatOut(DataRow dr)
        {
            MGPMatOut result = new MGPMatOut();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNo"]))
                        result.NoteNumber = dr["NoteNo"].ToString();
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["VehicleNo"]))
                        result.VehicleNo = dr["VehicleNo"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate =DateTime.Parse( dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["DriverNo"]))
                        result.DriverNo =int.Parse(dr["DriverNo"].ToString());
                    if (!DBNull.Value.Equals(dr["DriverName"]))
                        result.DriverName = dr["DriverName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode =int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationText"]))
                        result.DesignationText = dr["DesignationText"].ToString();
                    if (!DBNull.Value.Equals(dr["TripType"]))
                        result.TripType =int.Parse(dr["TripType"].ToString());
                    if (!DBNull.Value.Equals(dr["ToLocationCodenName"]))
                        result.ToLocationCodenName = dr["ToLocationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["CarryingOutMaterial"]))
                        result.CarryingOutMaterial =bool.Parse( dr["CarryingOutMaterial"].ToString());
                    if (!DBNull.Value.Equals(dr["LoadPercentage"]))
                        result.LoadPercentage =float.Parse(dr["LoadPercentage"].ToString());
                    if (!DBNull.Value.Equals(dr["RFID"]))
                        result.RFID = dr["RFID"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate =DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripOutDate"]))
                        result.ActualTripOutDate =DateTime.Parse(dr["ActualTripOutDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTripOutTime"]))
                        result.ActualTripOutTime =DateTime.Parse(dr["ActualTripOutTime"].ToString());
                    if (!DBNull.Value.Equals(dr["KMSOut"]))
                        result.KMSOut =int.Parse(dr["KMSOut"].ToString());
                    if (!DBNull.Value.Equals(dr["Remarks"]))
                        result.Remarks = dr["Remarks"].ToString();
                    if (!DBNull.Value.Equals(dr["CreateID"]))
                        result.CreateID =int.Parse(dr["CreateID"].ToString());
                }
            }
            catch { }
            return result;
        }
    }
}
