using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class EHGRepository : IEHGRepository
    {
        string NotePattern = "200002-EHG-" + DateTime.Today.ToString("yyyyMMdd") + "-";
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        EHGEntities _EHGEntities;
        public EHGRepository()
        {
            _MasterEntities = new MasterEntities();
            _EHGEntities = new EHGEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }
        public List<DateWiseTourDetails> getDateWiseTourDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            return _EHGEntities.getDateWiseTourDetails(Notenumber, IsActive, ref pMsg);
        }
        public EHGHeader getEHGNoteHdr(string Notenumber, ref string pMsg, int isLocked = 0, int UserID=0)
        {
            EHGHeader result= _EHGEntities.getEHGNoteHdr(Notenumber, UserID, ref pMsg, isLocked);
            try
            {
                EHGMaster m = EHGMaster.GetInstance;
                if (result.PurposeOfAllotment > 0)
                { result.POAText = m.PurposeOfAllotment.Where(o => o.ID == result.PurposeOfAllotment).FirstOrDefault().DisplayText; }
                else { result.POAText = "NA"; }
                if (result.VehicleType > 0)
                { result.VehicleTypeText = m.VehicleTypes.Where(o => o.ID == result.VehicleType).FirstOrDefault().DisplayText; }
            }
            catch { }
            return result;
        }
        public List<EHGNoteList> GetEHGNoteList(int DisplayLength, int DisplayStart, int SortColumn, 
            string SortDirection,  string SearchText, int CentreCode,bool IsApprovedList, ref string pMsg)
        {
            return _EHGEntities.GetEHGNoteList(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText,CentreCode, IsApprovedList, ref pMsg);
        }
        public EHGHeader getNewEHGHeader(ref string pMsg)
        {
            EHGHeader obj= new EHGHeader();
            obj.NoteNumber= _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            //obj.EntryDateStr = DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterCode = user.CentreCode;
            obj.CenterName = user.CentreName;
            obj.CenterCodenName = obj.CenterCode + " / " + obj.CenterName;
            obj.Initiator = user.EmployeeNumber;
            obj.InitiatorName = user.EmployeeName;
            obj.InitiatorCodenName = user.EmployeeNumber + " / " + user.EmployeeName;
            obj.MaterialStatus = -1;
            return obj;
        }
        public List<EHGNote> getNoteListToBeApproved(int CentreCode,ref string pMsg)
        {
            return _EHGEntities.getNoteListToBeApproved(CentreCode, ref pMsg);
        }
        public List<EHGTravelingPersondtlsForManagement> getTravelingPersonDetails(string Notenumber,  int IsActive, ref string pMsg)
        {
            EHGMaster m = EHGMaster.GetInstance;
            List <EHGTravelingPersondtlsForManagement> result= _EHGEntities.getTravelingPersonDetails(Notenumber, IsActive, ref pMsg);
            if (result != null && result.Count > 0) 
            {
                foreach (var item in result) 
                {
                    item.PersonTypeText = m.PersonType.Where(o => o.ID == item.PersonType).FirstOrDefault().DisplayText;
                }
            }
            return result;
        }
        public VehicleAllotmentDetails getVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            VehicleAllotmentDetails result = _EHGEntities.getVehicleAllotmentDetails(Notenumber,IsActive, ref pMsg);
            try
            {
                EHGMaster m = EHGMaster.GetInstance;
                result.VehicleBelongsToText = m.VehicleBelongsTo.Where(o => o.ID == result.VehicleBelongsTo).FirstOrDefault().DisplayText;
                if (result.VehicleBelongsTo == 2) 
                {
                    result.OtherVehicleNumber = result.VehicleNumber;
                    result.OtherVehicleModelName = "NA";
                }
            }
            catch { }
            return result;
        }
        public bool RemoveEHGNote(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _EHGEntities.RemoveEHGNote(NoteNumber, RemoveTag, ActiveTag,ref pMsg);
        }
        public bool SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg)
        {
            return _EHGEntities.SetDateWiseTourDetails(NoteNumber, dtldata, ref pMsg);
        }
        public bool SetEHGHdrAppStatus(string NoteNumber, bool IsApproved, string ReasonForDisApproval, int ApproverID, ref string pMsg)
        {
            if (ReasonForDisApproval == null) { ReasonForDisApproval = " "; }
            return _EHGEntities.SetEHGHdrAppStatus(NoteNumber, IsApproved, ReasonForDisApproval, ApproverID, ref pMsg);
        }
        public bool SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtlsForManagement dtl, ref string pMsg)
        {
            header.CenterCode = user.CentreCode;
            header.CenterName = user.CentreName;
            header.Initiator = user.EmployeeNumber;
            header.InitiatorName = user.EmployeeName;
            header.EntryDate = DateTime.Today;
            header.EntryTime = DateTime.Now.ToString("hh:mm tt");
            return _EHGEntities.SetEHGHdrForManagement(header, dtl, ref pMsg);
        }
        public bool SetEHGTravellingPersonDetails(string NoteNumber, string AuthEmp, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
           return _EHGEntities.SetEHGTravellingPersonDetails(NoteNumber,AuthEmp, dtldata, ref pMsg);
        }
        public bool SetEHGVehicleAllotmentDetails(VehicleAllotmentDetails mData, ref string pMsg)
        {
            if (mData.VehicleBelongsTo == 2) 
            { 
                mData.VehicleNumber = mData.OtherVehicleNumber;
                //mData.ModelName = mData.OtherVehicleModelName;
                mData.ModelName = "NA";
            }
            return _EHGEntities.SetEHGVehicleAllotmentDetails(mData, ref pMsg);
        }
        public bool UpdateEHGHdr(EHGHeader header, ref string pMsg)
        {
            header.EntryDate = DateTime.Today;
            header.EntryTime = DateTime.Now.ToString("hh:mm tt");
            return _EHGEntities.UpdateEHGHdr(header, ref pMsg);
        }
        public List<CustomComboOptions> getDriverListForOfficeWork(string Notenumber, ref string pMsg) 
        {
            return _EHGEntities.getDriverListForOfficeWork(Notenumber, ref pMsg);
        }
    }
}
