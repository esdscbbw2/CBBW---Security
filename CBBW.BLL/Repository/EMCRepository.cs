using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EMC;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
   public class EMCRepository:IEMCRepository
    {
        string NotePattern = "200003-EMC-" + DateTime.Today.ToString("yyyyMMdd") + "-";
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        EMCEntities _EMCEntities;
        public EMCRepository()
        {
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            _EMCEntities = new EMCEntities();
            user = _user.getLoggedInUser();
        }
        public EMCHeader getNewEMCHeader(ref string pMsg)
        {
            EMCHeader obj = new EMCHeader();
            obj.NoteNumber = _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterCode = user.CentreCode;
            obj.CenterName = user.CentreName;
            obj.CenterCodeName = obj.CenterCode + " / " + obj.CenterName;
            return obj;
        }
        public List<EMCNoteList> GetEMCNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _EMCEntities.GetEMCNZBDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }
  
        public bool SetEMCTravellingPerson(string NoteNumber, int CenterCode, string CenterCodeName, List<EMCTravellingPerson> dtldata, ref string pMsg)
        {
            return _EMCEntities.SetEMCTravellingPerson(NoteNumber, CenterCode, CenterCodeName, dtldata, ref pMsg);
        }
        public bool setEMCTravDetailsNTourDetails(string NoteNumber, List<EMCTravellingDetails> TDdata, List<EMCDateWiseTour> DWTdata, ref string pMsg)
        {
            return _EMCEntities.setEMCTravDetailsNTourDetails(NoteNumber, TDdata, DWTdata, ref pMsg);
        }
        public List<EMCTravellingPerson> GetEMCTravellingPerson(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            return _EMCEntities.GetEMCTravellingPerson(Notenumber, CenterCode, status, ref pMsg);
        }
        public bool SetEMCDetailsFinalSubmit(EMCHeader hdrmodel, ref string pMsg)
        {
            return _EMCEntities.SetEMCDetailsFinalSubmit(hdrmodel, ref pMsg);
        }
        public EMCHeader GetEMCHdrEntry(string Notenumber, ref string pMsg)
        {
            return _EMCEntities.GetEMCHdrEntry(Notenumber, ref pMsg);
        }
        public EMCTravellingDetails GetEMCTravellingDetails(string Notenumber, ref string pMsg)
        {
            return _EMCEntities.GetEMCTravellingDetails(Notenumber, ref pMsg);
        }
        public List<EMCDateWiseTour> GetEMCDateWiseTour(string Notenumber, ref string pMsg)
        {
            return _EMCEntities.GetEMCDateWiseTour(Notenumber, ref pMsg);
        }
        public bool RemoveEMCNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _EMCEntities.RemoveEMCNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg);
        }
        public List<EMCNote> GetEMCNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            return _EMCEntities.GetEMCNoteListToBeApproved(CentreCode, status, ref pMsg);
        }
        public bool SetEMCApprovalData(EMCApproveTravDetails model, ref string pMsg)
        {
            return _EMCEntities.SetEMCApprovalData(model, ref pMsg);
        }

        public IEnumerable<TPEPNote> GetEPTourNoteNumber(int EmployeeNumber, int CentreCode, ref string pMsg)
        {
            return _EMCEntities.GetEPTourNoteNumber(EmployeeNumber, CentreCode, ref pMsg);
        }
    }
}
