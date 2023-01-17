using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.ETS;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    
    public class ETSRepository: IETSRepository
    {
        string NotePattern = "200003-EZB-" + DateTime.Today.ToString("yyyyMMdd") + "-";
        MasterEntities _MasterEntities;
        UserRepository _user;
        UserInfo user;
        ETSEntities _ETSEntities;
        public ETSRepository()
        {
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            _ETSEntities = new ETSEntities();
            user = _user.getLoggedInUser();
        }

        public ETSHeader getNewETSHeader(ref string pMsg)
        {
            ETSHeader obj = new ETSHeader();
            obj.NoteNumber = _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
            obj.EntryTime = DateTime.Now.ToString("hh:mm tt");
            obj.CenterCode = user.CentreCode;
            obj.CenterName = user.CentreName;
            obj.CenterCodeName = obj.CenterCode + " / " + obj.CenterName;
            return obj;
        }
        public bool SetETSTravellingPerson(string NoteNumber, List<ETSTravellingPerson> dtldata, ref string pMsg)
        {
            return _ETSEntities.SetETSTravellingPerson(NoteNumber, dtldata, ref pMsg);
        }
        public bool setETSTravDetailsNTourDetails(string NoteNumber, List<ETSTravellingDetails> TDdata, List<ETSDateWiseTour> DWTdata, ref string pMsg)
        {
            return _ETSEntities.setETSTravDetailsNTourDetails(NoteNumber, TDdata, DWTdata, ref pMsg);
        }

        public List<ETSTravellingPerson> GetETSTravellingPerson(string NoteNumber, ref string pMsg)
        {
            return _ETSEntities.GetETSTravellingPerson(NoteNumber, ref pMsg);
        }
        public bool SetETSDetailsFinalSubmit(ETSHeader hdrmodel, ref string pMsg)
        {
            return _ETSEntities.SetETSDetailsFinalSubmit(hdrmodel, ref pMsg);
        }
        public List<ETSNoteList> GetETSNZBDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode,int status, ref string pMsg)
        {
            return _ETSEntities.GetETSNZBDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, status, ref pMsg);
        }

        public ETSHeader GetETSHdrEntry(string Notenumber, ref string pMsg)
        {
            return _ETSEntities.GetETSHdrEntry(Notenumber,ref pMsg);
        }
        public ETSTravellingDetails GetETSTravellingDetails(string Notenumber, ref string pMsg)
        {
            return _ETSEntities.GetETSTravellingDetails(Notenumber, ref pMsg);
        }
        public List<ETSDateWiseTour> GetETSDateWiseTour(string Notenumber, ref string pMsg)
        {
            return _ETSEntities.GetETSDateWiseTour(Notenumber, ref pMsg);
        }
        public bool RemoveETSNoteNumber(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _ETSEntities.RemoveETSNoteNumber(NoteNumber, RemoveTag, ActiveTag, ref pMsg);
        }

        public List<ETSNote> GetETSNoteListToBeApproved(int CentreCode, int status, ref string pMsg)
        {
            return _ETSEntities.GetETSNoteListToBeApproved(CentreCode, status, ref pMsg);
        }

        public bool SetETSApprovalData(ETSApproveTravDetails model, ref string pMsg)
        {
            return _ETSEntities.SetETSApprovalData(model, ref pMsg);
        }
        public bool SetETSRatifiedData(ETSRatified model, ref string pMsg)
        {
            return _ETSEntities.SetETSRatifiedData(model, ref pMsg);
        }
    }
}
