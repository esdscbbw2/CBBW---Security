using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.TFD;
using CBBW.DAL.DBLogic;

namespace CBBW.DAL.DBMapper
{
   public class TFDDBMapper
    {
        public TFDHdr Map_TFDHeader(DataRow dr)
        {
            TFDHdr result = new TFDHdr();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntEntryDate"]))
                        result.EntEntryDate = DateTime.Parse(dr["EntEntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntEntryTime"]))
                        result.EntEntryTime = dr["EntEntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["TourFromDate"]))
                        result.TourFromDate = DateTime.Parse(dr["TourFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourToDate"]))
                        result.TourToDate = DateTime.Parse(dr["TourToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["CenterCode"]))
                        result.CenterCode = int.Parse(dr["CenterCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    result.EntEntryDatestr = MyDBLogic.ConvertDateToString(result.EntEntryDate);
                    result.TourFromDatestr = MyDBLogic.ConvertDateToString(result.TourFromDate);
                    result.TourToDatestr = MyDBLogic.ConvertDateToString(result.TourToDate);
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public TFDTravellingPerson Map_TFDTravellingPerson(DataRow dr)
        {
            TFDTravellingPerson result = new TFDTravellingPerson();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonIDName"]))
                        result.PersonIDName = dr["PersonIDName"].ToString();
                    if (!DBNull.Value.Equals(dr["DesignationCode"]))
                        result.DesignationCode = int.Parse(dr["DesignationCode"].ToString());
                    if (!DBNull.Value.Equals(dr["DesignationCodeText"]))
                        result.DesignationCodeText = dr["DesignationCodeText"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonCentre"]))
                        result.PersonCentre = int.Parse(dr["PersonCentre"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonCentreName"]))
                        result.PersonCentreName = dr["PersonCentreName"].ToString();
                    if (!DBNull.Value.Equals(dr["AuthEmployeeCode"]))
                        result.AuthEmployeeCode = int.Parse(dr["AuthEmployeeCode"].ToString());
                    if (!DBNull.Value.Equals(dr["AuthEmployeeCodeName"]))
                        result.AuthEmployeeCodeName = dr["AuthEmployeeCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CentreCode = int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreCodeName"]))
                        result.CentreCodeName = dr["CentreCodeName"].ToString();
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public TFDDateWiseTourData Map_TFDDateWiseTourData(DataRow dr)
        {
            TFDDateWiseTourData result = new TFDDateWiseTourData();
            try
            {
                if (dr != null)
                { 
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryCodes"]))
                        result.TourCategoryCodes = dr["TourCategoryCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryText"]))
                        result.TourCategoryText = dr["TourCategoryText"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CentreCode = int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreCodeName"]))
                        result.CentreCodeName = dr["CentreCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInTime"]))
                        result.ActualTourInTime = dr["ActualTourInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourOutTime"]))
                        result.ActualTourOutTime = dr["ActualTourOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonType"]))
                        result.PersonType = int.Parse(dr["PersonType"].ToString());
                    if (!DBNull.Value.Equals(dr["PersonIDName"]))
                        result.PersonIDName = dr["PersonIDName"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    result.SchFromDatestr = MyDBLogic.ConvertDateToString(result.SchFromDate);

                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public TFDNoteList Map_TFDNoteList(DataRow dr)
        {
            TFDNoteList result = new TFDNoteList();
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
                    if (!DBNull.Value.Equals(dr["CenterCodeName"]))
                        result.CenterCodeName = dr["CenterCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    result.EntryDateDisplay = MyDBLogic.ConvertDateToString(result.EntryDate);
                    if (!result.IsApproved.HasValue && result.EntryDate == DateTime.Today)
                    { result.CanDelete = true; }

                }
            }
            catch { }
            return result;
        }

        public TFDTourFeedBackDetails Map_TFDTourFeedBackDetails(DataRow dr)
        {
            TFDTourFeedBackDetails result = new TFDTourFeedBackDetails();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["ID"]))
                        result.ID = int.Parse(dr["ID"].ToString());
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryID"]))
                        result.TourCategory = dr["TourCategoryID"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategory"]))
                        result.TourCategoryText = dr["TourCategory"].ToString();
                    if (!DBNull.Value.Equals(dr["LocationCode"]))
                        result.CenterCodeName = dr["LocationCode"].ToString();
                    if (!DBNull.Value.Equals(dr["LocationCodeName"]))
                        result.CenterCodeNametxt = dr["LocationCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["TourFeedBack"]))
                        result.TourFeedBack = dr["TourFeedBack"].ToString();
                    if (!DBNull.Value.Equals(dr["ActionTaken"]))
                        result.ActionTakens = bool.Parse(dr["ActionTaken"].ToString());
                    if (!DBNull.Value.Equals(dr["ConcernDeptCode"]))
                        result.ConcernDeptCode = int.Parse(dr["ConcernDeptCode"].ToString());
                    if (!DBNull.Value.Equals(dr["ConcernDeptName"]))
                        result.ConcernDeptName = dr["ConcernDeptName"].ToString();

                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        public TFDHdr Map_TFDHeaderDetails(DataRow dr)
        {
            TFDHdr result = new TFDHdr();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["RefNoteNumber"]))
                        result.RefNoteNumber = dr["RefNoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["EntryDate"]))
                        result.EntryDate = DateTime.Parse(dr["EntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntryTime"]))
                        result.EntryTime = dr["EntryTime"].ToString();

                    if (!DBNull.Value.Equals(dr["EntEntryDate"]))
                        result.EntEntryDate = DateTime.Parse(dr["EntEntryDate"].ToString());
                    if (!DBNull.Value.Equals(dr["EntEntryTime"]))
                        result.EntEntryTime = dr["EntEntryTime"].ToString();
                    if (!DBNull.Value.Equals(dr["TourFromDate"]))
                        result.TourFromDate = DateTime.Parse(dr["TourFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["TourToDate"]))
                        result.TourToDate = DateTime.Parse(dr["TourToDate"].ToString());
                    if (!DBNull.Value.Equals(dr["PurposeOfVisit"]))
                        result.PurposeOfVisit = dr["PurposeOfVisit"].ToString();
                    if (!DBNull.Value.Equals(dr["AuthEmployeeName"]))
                        result.AuthEmployeeName = dr["AuthEmployeeName"].ToString();
                    if (!DBNull.Value.Equals(dr["AuthEmployeeCode"]))
                        result.AuthEmployeeCode =int.Parse(dr["AuthEmployeeCode"].ToString());

                    if (!DBNull.Value.Equals(dr["IsApproved"]))
                        result.IsApproved = bool.Parse(dr["IsApproved"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovalDT"]))
                        result.ApprovalDT = DateTime.Parse(dr["ApprovalDT"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovalTime"]))
                        result.ApprovalTime = dr["ApprovalTime"].ToString();
                    if (!DBNull.Value.Equals(dr["Remark"]))
                        result.Remark = dr["Remark"].ToString();
                    result.EntEntryDatestr = MyDBLogic.ConvertDateToString(result.EntEntryDate);
                    result.TourFromDatestr = MyDBLogic.ConvertDateToString(result.TourFromDate);
                    result.TourToDatestr = MyDBLogic.ConvertDateToString(result.TourToDate);
                    result.EntryDatestr = MyDBLogic.ConvertDateToString(result.EntryDate);
                    result.ApprovalDTstr = MyDBLogic.ConvertDateToString(result.EntryDate);
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
        public TFDDateWiseTourData Map_TFDDateWiseTourDataView(DataRow dr)
        {
            TFDDateWiseTourData result = new TFDDateWiseTourData();
            try
            {
                if (dr != null)
                {
                    if (!DBNull.Value.Equals(dr["NoteNumber"]))
                        result.NoteNumber = dr["NoteNumber"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryCodes"]))
                        result.TourCategoryCodes = dr["TourCategoryCodes"].ToString();
                    if (!DBNull.Value.Equals(dr["TourCategoryText"]))
                        result.TourCategoryText = dr["TourCategoryText"].ToString();
                    if (!DBNull.Value.Equals(dr["CentreCode"]))
                        result.CentreCode = int.Parse(dr["CentreCode"].ToString());
                    if (!DBNull.Value.Equals(dr["CentreCodeName"]))
                        result.CentreCodeName = dr["CentreCodeName"].ToString();
                    if (!DBNull.Value.Equals(dr["SchFromDate"]))
                        result.SchFromDate = DateTime.Parse(dr["SchFromDate"].ToString());
                    if (!DBNull.Value.Equals(dr["ActualTourInTime"]))
                        result.ActualTourInTime = dr["ActualTourInTime"].ToString();
                    if (!DBNull.Value.Equals(dr["ActualTourOutTime"]))
                        result.ActualTourOutTime = dr["ActualTourOutTime"].ToString();
                    if (!DBNull.Value.Equals(dr["PersonID"]))
                        result.PersonID = int.Parse(dr["PersonID"].ToString());
                    if (!DBNull.Value.Equals(dr["IsApproval"]))
                        result.IsApproval = bool.Parse(dr["IsApproval"].ToString());
                    if (!DBNull.Value.Equals(dr["ApprovalRemark"]))
                        result.ApprovalRemark = dr["ApprovalRemark"].ToString();
                    
                    result.SchFromDatestr = MyDBLogic.ConvertDateToString(result.SchFromDate);

                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }
    }
}
