using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL;
using CBBW.BOL.EHG;
using CBBW.BOL.ETSEdit;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class ETSEditRepository : IETSEditRepository
    {
        ETSEditEntities _ETSEditEntities;
        public ETSEditRepository()
        {
            _ETSEditEntities = new ETSEditEntities();
        }
        public EditNoteDetails getEditNoteHdr(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.getEditNoteHdr(NoteNumber, ref pMsg);
        }
        public int getEditSL(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.getEditSL(NoteNumber, ref pMsg);
        }
        public List<EditNoteNumber> getETSNoteListToBeEdited(int CentreCode, ref string pMsg)
        {
            return _ETSEditEntities.getETSNoteListToBeEdited(CentreCode, ref pMsg);
        }
        public List<EditNoteNumber> getETSEditNoteListForDropDown(int CentreCode, int mStatus, ref string pMsg) 
        {
            return _ETSEditEntities.getETSEditNoteListForDropDown(CentreCode, mStatus, ref pMsg);
        }
        public IEnumerable<EditTPDetails> getEditTPDetails(string NoteNumber, ref string pMsg) 
        {
            return _ETSEditEntities.getEditTPDetails(NoteNumber, ref pMsg);
        }
        //public List<EditDWTDetails> getCurrentDateWiseTour(string NoteNumber, int FieldTag, ref string pMsg)
        //{
        //    int maxrowid = 0;
        //    List<EditDWTDetails> objlist = _ETSEditEntities.getCurrentDateWiseTour(NoteNumber, FieldTag, ref pMsg);
        //    if (objlist != null) { maxrowid = objlist.Max(o => o.EditSL); }
        //    List<EditDWTDetails> result = objlist.Where(o => o.EditSL == maxrowid).ToList();
        //    return result;
        //}
        public List<EditDWTDetails> getDateWiseTourHistory(string NoteNumber, int FieldTag, int PersonType, int PersonID, string PersonName, ref string pMsg, bool IsActive)
        {
            List<EditDWTDetails> objlist = _ETSEditEntities.getCurrentDateWiseTour(NoteNumber, FieldTag,PersonType,PersonID,PersonName,ref pMsg,IsActive);
            return objlist;
        }
        public bool SetETSTourEdit(DWTTourDetailsForDB obj, int CentreCode, string CentreName, ref string pMsg) 
        {
            return _ETSEditEntities.SetETSTourEdit(obj,CentreCode,CentreName, ref pMsg);
        }
        public bool UpdateETSTourEdit(string NoteNumber, ref string pMsg) 
        {
            return _ETSEditEntities.UpdateETSTourEdit(NoteNumber, ref pMsg);
        }
        public List<EditNoteList> GetETSEditNoteList(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CentreCode, int IsApprovedList, ref string pMsg)
        {
            return _ETSEditEntities.GetETSEditNoteList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CentreCode, IsApprovedList, ref pMsg);
        }
        public bool RemoveETSEditNote(string NoteNumber, int ActiveTag, ref string pMsg)
        {
            return _ETSEditEntities.RemoveETSEditNote(NoteNumber, ActiveTag, ref pMsg);
        }
        public bool SetETSEditRatificationStatus(string NoteNumber, bool IsRatified, string RatReason, int ApproverID, ref string pMsg)
        {
            return _ETSEditEntities.SetETSEditRatificationStatus(NoteNumber, IsRatified, RatReason, ApproverID,ref pMsg);
        }
        public bool SetETSEditAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval, int ApproverID, ref string pMsg)
        {
            return _ETSEditEntities.SetETSEditAppStatus(NoteNumber, IsApproved, ReasonForDisApproval, ApproverID,ref pMsg);
        }
        public EditNoteDetails getETSEditHdr(string NoteNumber, int LockStatus, ref string pMsg)
        {
            return _ETSEditEntities.getETSEditHdr(NoteNumber, LockStatus, ref pMsg);
        }
        public bool SetETSVehicleAllotmentDetails(VehicleAllotmentDetails mData, int CentreCode, string CentreName, ref string pMsg)
        {
            mData.AuthorisedEmpNumber = MyCodeHelper.GetEmpNoFromString(mData.AuthorisedEmpName);
            mData.DesignationCode = MyCodeHelper.GetEmpNoFromString(mData.DesignationText);
            mData.VehicleNumber = mData.VehicleBelongsTo==1 ? mData.VehicleNumber : mData.OtherVehicleNumber;
            mData.ModelName = mData.VehicleBelongsTo == 1 ? mData.ModelName : "NA";
            return _ETSEditEntities.SetETSVehicleAllotmentDetails(mData, CentreCode, CentreName, ref pMsg);
        }
        public VehicleAllotmentDetails GetVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            return _ETSEditEntities.GetVehicleAllotmentDetails(Notenumber, IsActive, ref pMsg);
        }
        public EntryITourDetails GetEntryITourData(string Notenumber, int IsActive, ref string pMsg)
        {
            return _ETSEditEntities.GetEntryITourData(Notenumber, IsActive,ref pMsg);
        }
        public EditNoteDetails GetNoteHdrForEntryI(string NoteNumber, int LockStatus, ref string pMsg)
        {
            return _ETSEditEntities.GetNoteHdrForEntryI(NoteNumber, LockStatus, ref pMsg);
        }
        public List<EditNoteNumber> GetNoteListForEntryI(int CentreCode, ref string pMsg)
        {
            return _ETSEditEntities.GetNoteListForEntryI(CentreCode, ref pMsg);
        }
        public List<EntryINoteList> GetEntryINoteList(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CentreCode, ref string pMsg)
        {
            return _ETSEditEntities.GetEntryINoteList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CentreCode, ref pMsg);
        }
        public bool RemoveEntryINote(string NoteNumber, bool ActiveTag, ref string pMsg)
        {
            return _ETSEditEntities.RemoveEntryINote(NoteNumber, ActiveTag, ref pMsg);
        }
        public IEnumerable<NoteDriver> GETDriverList(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.GETDriverList(NoteNumber, ref pMsg);
        }
        public bool UpdateETSVehicleAllotmentDetails(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.UpdateETSVehicleAllotmentDetails(NoteNumber, ref pMsg);
        }
        public bool IsTourStarted(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.IsTourStarted(NoteNumber, ref pMsg);
        }
    }
}
