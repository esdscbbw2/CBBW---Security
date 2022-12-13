using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.BOL.Master;
using CBBW.BOL.MGP;
using CBBW.BOL.TADA;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DataSync
{
    public class CommonTable
    {
        public DataTable UDTable { get; set; }
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
        public CommonTable(List<EHGTravelingPersondtls> customoptions)
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
            if (customoptions != null && customoptions.Count > 0)
            {
                foreach (EHGTravelingPersondtls obj in customoptions)
                {
                    DataRow dr = UDTable.NewRow();
                    dr["iPersonType"] = obj.PersonType;
                    dr["iEmployeeNo"] = obj.EmployeeNo;
                    dr["sDesignationCodenName"] = obj.DesignationCodenName;
                    dr["dFromDate"] = obj.FromDate;
                    dr["sFromTime"] = obj.FromTime;
                    dr["dToDate"] = obj.ToDate;
                    dr["sPurposeOfVisit"] = obj.PurposeOfVisit;
                    dr["bTADADenied"] = obj.TADADenied;
                    dr["sEmployeeName"] = obj.EmployeeNonName;
                    dr["iDesignationCode"] = MyDBLogic.getFirstIntegerFromString(obj.DesignationCodenName,'/');
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

    }
}
