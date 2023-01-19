﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;

namespace CBBW.DAL.DBMapper
{
    public class ETSEditDBMapper
    {
        public EditNoteNumber Map_EditNoteNumber(DataRow dr)
        {
            EditNoteNumber result = new EditNoteNumber();
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
        public EditNoteDetails Map_EditNoteDetails(DataRow dr)
        {
            EditNoteDetails result = new EditNoteDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate =DateTime.Parse( dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["PurposeOfAllotment"]))
                        result.POA =int.Parse(dr["PurposeOfAllotment"].ToString());
                    if (!DBNull.Value.Equals(dr["EPTour"]))
                        result.EPTour = int.Parse(dr["EPTour"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["AppDateTime"]))
                        result.AppDateTime =DateTime.Parse(dr["AppDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["ReasonForDisApproval"]))
                        result.NotAppReason = dr["ReasonForDisApproval"].ToString();
                    if (!DBNull.Value.Equals(dr["IsRatified"]))
                        result.IsRatified = bool.Parse(dr["IsRatified"].ToString());
                    if (!DBNull.Value.Equals(dr["RetDateTime"]))
                        result.RetDateTime =DateTime.Parse(dr["RetDateTime"].ToString());
                    if (!DBNull.Value.Equals(dr["RetReason"]))
                        result.RetReason = dr["RetReason"].ToString();
                    if (result.POA == 0)
                        result.POAText = "NA";
                    else if (result.POA == 1)
                        result.POAText = "For Management";
                    else if (result.POA==2)
                        result.POAText = "For Office Work";
                    if (result.EPTour == 0)
                        result.EPTourText = "NA";
                    //Else part is pending for the required module.

                }
            }
            catch { }
            return result;
        }
        public EditTPDetails Map_EditTPDetails(DataRow dr)
        {
            EditTPDetails result = new EditTPDetails();
            try
            {
                if (dr != null)
                {
                    EHGMaster _EHGMaster = EHGMaster.GetInstance;
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                    { 
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                        result.PersonTypeText = _EHGMaster.PersonType.Where(o => o.ID == result.PersonType).FirstOrDefault().DisplayText;
                    }
                    if (!DBNull.Value.Equals(dr["EmployeeNo"]))
                        result.EmployeeNo = int.Parse(dr["EmployeeNo"].ToString());
                    if (!DBNull.Value.Equals(dr["EmployeeNonName"]))
                        result.EmployeeNonName = dr["EmployeeNonName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode =int.Parse( dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodenName"]))
                        result.DesignationCodenName = dr["DesignationCodenName"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterName"]))
                        result.CenterName = dr["CenterName"].ToString();
                    if (!DBNull.Value.Equals(dr["EligibleVehicleType"]))
                        result.EligibleVehicleType =int.Parse(dr["EligibleVehicleType"].ToString());
                    if (!DBNull.Value.Equals(dr["EligibleVehicleTypeName"]))
                        result.EligibleVehicleTypeName = dr["EligibleVehicleTypeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EPNoteNumber"]))
                        result.EPNoteNumber = dr["EPNoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EPNoteDate"]))
                        result.EPNoteDate =DateTime.Parse(dr["EPNoteDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TADADenied"]))
                        result.TADADenied =bool.Parse(dr["TADADenied"].ToString());
                    
                }
            }
            catch { }
            return result;
        }
    }
}
