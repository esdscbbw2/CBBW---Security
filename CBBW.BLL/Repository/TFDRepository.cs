using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TFD;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class TFDRepository : ITFDRepository
    {
        string NotePattern = "200001-TFD-" + DateTime.Today.ToString("yyyyMMdd") + "-";
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
       TFDEntities _TFDEntities;
        TourEntities _tourEntities;
        public TFDRepository()
        {
            _tourEntities = new TourEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            _TFDEntities = new TFDEntities();
            user = _user.getLoggedInUser();
        }
        public TFDHdr getNewTFDNoteNumber(ref string pMsg)
        {
            TFDHdr obj = new TFDHdr();
            obj.NoteNumber = _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterCode = user.CentreCode;
            obj.CenterName = user.CentreName;
            obj.CenterCodeName = obj.CenterCode + " / " + obj.CenterName;
            return obj;
        }
        public List<TFDNote> GetNoteNumberList(int CentreCode, int status, ref string pMsg)
        {
            return _TFDEntities.GetNoteNumberList(CentreCode,status,ref pMsg);
            
        }
       public  List<TFDTravellingPerson> GetENTTravellingPerson(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            return _TFDEntities.GetENTTravellingPerson(Notenumber, CenterCode, status,ref pMsg);
        }
        public List<TFDDateWiseTourData> GetENTDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre,int status, ref string pMsg)
        {
            return _TFDEntities.GetENTDateWiseTourData(NoteNumber, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
        }
        public IEnumerable<CustomCheckBoxOption> GetENTAuthEmployeeList(string Notenumber, int CentreCode, ref string pMsg)
        {
            return _TFDEntities.GetENTAuthEmployeeList(Notenumber, CentreCode, ref pMsg);
        }
        public TFDHdr GetTFDHeaderData(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            return _TFDEntities.GetTFDHeaderData(Notenumber, CenterCode, status, ref pMsg);
        }

        public bool SetTFDFeedBackDetails(string NoteNumber, List<TFDTourFeedBackDetails> dtldata, ref string pMsg)
        {
            return _TFDEntities.SetTFDFeedBackDetails(NoteNumber, dtldata, ref pMsg);
        }

        public bool SetTFDetailsFinalSubmit(TFDHdr hdrmodel, ref string pMsg)
        {
            return _TFDEntities.SetTFDetailsFinalSubmit(hdrmodel, ref pMsg);
        }

        public List<TFDNoteList> GetTFDDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, int status, ref string pMsg)
        {
            return _TFDEntities.GetTFDDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }

        public List<TFDTourFeedBackDetails> GetTFDTourFeedBackDetails(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            return _TFDEntities.GetTFDTourFeedBackDetails(Notenumber, CenterCode, status, ref pMsg);
        }

        public TFDHdr GetTFDHeaderDetails(string Notenumber, int CenterCode, int status, ref string pMsg)
        {
            return _TFDEntities.GetTFDHeaderDetails(Notenumber, CenterCode, status, ref pMsg);

        }

        public bool RemoveTFDNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _TFDEntities.RemoveTFDNoteNumber(NoteNumber, RemoveTag, ActiveTag,ref pMsg);
        }

        public IEnumerable<CustomComboOptions> GetENTConcernDeptList(string Notenumber, int CentreCode, ref string pMsg)
        {
            return _TFDEntities.GetENTConcernDeptList(Notenumber, CentreCode, ref pMsg);
        }

        public bool SetTFDFeedBackApproval(string NoteNumber, List<TFDTourFBApproval> dtldata, ref string pMsg)
        {
            return _TFDEntities.SetTFDFeedBackApproval(NoteNumber, dtldata, ref pMsg);
        }
        public bool SetTFDDateWiseTourData(string NoteNumber, bool IsApprove, string ApproveReason, List<TFDDateWiseTourData> dtldata, ref string pMsg)
        {
            return _TFDEntities.SetTFDDateWiseTourData(NoteNumber, IsApprove, ApproveReason, dtldata, ref pMsg);
        }

        public List<TFDDateWiseTourData> GetTFDDateWiseTourData(string NoteNumber, int PersonType, int EmployeeNo, int PersonCentre, int status, ref string pMsg)
        {
            return _TFDEntities.GetTFDDateWiseTourData(NoteNumber, PersonType, EmployeeNo, PersonCentre, status, ref pMsg);
        }

        public string GetENTTourCategroy(string NoteNumber, ref string pMsg)
        {
            return _TFDEntities.GetENTTourCategroy(NoteNumber,ref pMsg);
        }
    
    }
}
