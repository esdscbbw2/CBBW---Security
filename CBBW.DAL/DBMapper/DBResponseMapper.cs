using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;

namespace CBBW.DAL.DBMapper
{
    public class DBResponseMapper
    {
        public void Map_DBResponse(DataTable dt,ref string pMsg,ref bool IsSuccess) 
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dt.Rows[0]["IsSuccess"]))
                        IsSuccess = bool.Parse(dt.Rows[0]["IsSuccess"].ToString());
                    if (!DBNull.Value.Equals(dt.Rows[0]["Msg"]))
                        pMsg = dt.Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
        }
        public CustomComboOptions Map_CustomComboOptions(DataRow dr)
        {
            CustomComboOptions result = new CustomComboOptions();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["DisplayText"]))
                        result.DisplayText = result.ID + " / " + dr["DisplayText"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public CustomComboOptions Map_CustomComboOptionsForEmployees(DataRow dr)
        {
            CustomComboOptions result = new CustomComboOptions();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["EmployeeNumber"]))
                        result.ID = int.Parse(dr["EmployeeNumber"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeName"]))
                        result.DisplayText = result.ID + " / " + dr["EmployeeName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public CustomComboOptions Map_CustomComboOptionsForDrivers(DataRow dr)
        {
            CustomComboOptions result = new CustomComboOptions();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["DisplayText"]))
                        result.DisplayText = dr["DisplayText"].ToString();
                    if (result.DisplayText.IndexOf("/") < 0)
                    {
                        result.DisplayText = result.ID + " / " + dr["DisplayText"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public CustomCheckBoxOption Map_CustomCheckBoxOption(DataRow dr) 
        {
            CustomCheckBoxOption result = new CustomCheckBoxOption();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["DisplayText"]))
                        result.DisplayText = dr["DisplayText"].ToString();
                    if (!DBNull.Value.Equals(dr["IsSelected"]))
                        result.IsSelected = bool.Parse(dr["IsSelected"].ToString());
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public TADAPubTransOption Map_TADAPubTransOption(DataRow dr)
        {
            TADAPubTransOption result = new TADAPubTransOption();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["TransTypeID"]))
                        result.TransTypeID = int.Parse(dr["TransTypeID"].ToString());
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["DisplayText"]))
                        result.DisplayText = dr["DisplayText"].ToString();
                    if (!DBNull.Value.Equals(dr["IsSelected"]))
                        result.IsSelected = bool.Parse(dr["IsSelected"].ToString());
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public CustomOptionsWithString Map_CustomOptionsWithString(DataRow dr)
        {
            CustomOptionsWithString result = new CustomOptionsWithString();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = dr["ID"].ToString();
                    if (!DBNull.Value.Equals(dr["DisplayText"]))
                        result.DisplayText = dr["DisplayText"].ToString();
                    if (!DBNull.Value.Equals(dr["IsSelected"]))
                        result.IsSelected = bool.Parse(dr["IsSelected"].ToString());
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }
            return result;
        }
        public CustomComboOptions Map_CustomComboOptionsWithoutID(DataRow dr)
        {
            CustomComboOptions result = new CustomComboOptions();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["DisplayText"]))
                        result.DisplayText = dr["DisplayText"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyCodeHelper.WriteErrorLog(MyCodeHelper.GetMethodInfo().MethodSignature, ex);
            }            
            return result;
        }


    }
}
