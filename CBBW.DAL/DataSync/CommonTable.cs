using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;

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
                    UDTable.Rows.Add(dr);
                }
            }
        }
    }
}
