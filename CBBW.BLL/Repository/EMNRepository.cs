using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMN;
using CBBW.BOL.Tour;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
   public class EMNRepository:IEMNRepository
    {
        string NotePattern = "200003-EMN-" + DateTime.Today.ToString("yyyyMMdd") + "-";
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        EMNEntities _EMNEntities;
        TourEntities _tourEntities;
        public EMNRepository()
        {
            _tourEntities = new TourEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            _EMNEntities = new EMNEntities();
            user = _user.getLoggedInUser();
        }
        public EMNHeader getNewEMNHeader(ref string pMsg)
        {
            EMNHeader obj = new EMNHeader();
            obj.NoteNumber = _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterCode = user.CentreCode;
            obj.CenterName = user.CentreName;
            obj.CenterCodeName = obj.CenterCode + " / " + obj.CenterName;
            return obj;
        }
        public List<EMNNoteList> GetEMNNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _EMNEntities.GetEMNNZBDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getCenterCodeList(int center, ref string pMsg)
        {
            return _EMNEntities.getCenterCodeList(center, ref pMsg);
        }
        public bool SetEMNTravellingPerson(string NoteNumber, int CenterCode, string CenterCodeName, List<EMNTravellingPerson> dtldata, ref string pMsg)
        {
            return _EMNEntities.SetEMNTravellingPerson(NoteNumber, CenterCode, CenterCodeName, dtldata, ref pMsg);
        }
        public bool setEMNTravDetailsNTourDetails(string NoteNumber, List<EMNTravellingDetails> TDdata, List<EMNDateWiseTour> DWTdata, ref string pMsg)
        {
            return _EMNEntities.setEMNTravDetailsNTourDetails(NoteNumber, TDdata, DWTdata, ref pMsg);
        }
        public List<EMNTravellingPerson> GetEMNTravellingPerson(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            return _EMNEntities.GetEMNTravellingPerson(Notenumber, CenterCode, status, ref pMsg);
        }
        public bool SetEMNDetailsFinalSubmit(EMNHeader hdrmodel, ref string pMsg)
        {
            return _EMNEntities.SetEMNDetailsFinalSubmit(hdrmodel, ref pMsg);
        }
        public EMNHeader GetEMNHdrEntry(string Notenumber, ref string pMsg)
        {
            return _EMNEntities.GetEMNHdrEntry(Notenumber, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getCenterCodeListFromTravellingPerson(string NoteNumber, int status, ref string pMsg)
        {
            return _EMNEntities.getCenterCodeListFromTravellingPerson(NoteNumber, status, ref pMsg);
        }
        public EMNTravellingDetails GetEMNTravellingDetails(string Notenumber, ref string pMsg)
        {
            return _EMNEntities.GetEMNTravellingDetails(Notenumber, ref pMsg);
        }
        public List<EMNDateWiseTour> GetEMNDateWiseTour(string Notenumber, ref string pMsg)
        {
            return _EMNEntities.GetEMNDateWiseTour(Notenumber, ref pMsg);
        }
        public bool RemoveEMNNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _EMNEntities.RemoveEMNNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg);
        }
        public List<EMNNote> GetEMNNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            return _EMNEntities.GetEMNNoteListToBeApproved(CentreCode, status, ref pMsg);
        }
        public bool SetEMNApprovalData(EMNApproveTravDetails model, ref string pMsg)
        {
            return _EMNEntities.SetEMNApprovalData(model, ref pMsg);
        }
        public bool SetEMNRatifiedData(EMNRatified model, ref string pMsg)
        {
            return _EMNEntities.SetEMNRatifiedData(model, ref pMsg);
        }

        public TourRuleSaveInfo GetTourInfoForServiceType(string ServiceTypes, ref string pMsg)
        {
            return _tourEntities.GetTourInfoForServiceType(ServiceTypes, ref pMsg);
        }
    }
}
