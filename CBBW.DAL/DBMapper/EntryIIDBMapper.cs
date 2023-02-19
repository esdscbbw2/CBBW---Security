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

namespace CBBW.DAL.DBMapper
{
    public class EntryIIDBMapper
    {
        EHGMaster master = EHGMaster.GetInstance;
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





    }
}
