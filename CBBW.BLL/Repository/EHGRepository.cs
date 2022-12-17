using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
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
        public EHGHeader getEHGNoteHdr(string Notenumber, ref string pMsg)
        {
            return _EHGEntities.getEHGNoteHdr(Notenumber, ref pMsg);
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
        public List<EHGTravelingPersondtlsForManagement> getTravelingPersonDetails(string Notenumber,  int IsActive, ref string pMsg)
        {
            return _EHGEntities.getTravelingPersonDetails(Notenumber,IsActive, ref pMsg);
        }
        public VehicleAllotmentDetails getVehicleAllotmentDetails(string Notenumber, int IsActive, ref string pMsg)
        {
            return _EHGEntities.getVehicleAllotmentDetails(Notenumber,IsActive, ref pMsg);
        }
        public bool RemoveEHGNote(string NoteNumber, int RemoveTag, int ActiveTag, ref string pMsg)
        {
            return _EHGEntities.RemoveEHGNote(NoteNumber, RemoveTag, ActiveTag,ref pMsg);
        }
        public bool SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg)
        {
            return _EHGEntities.SetDateWiseTourDetails(NoteNumber, dtldata, ref pMsg);
        }
        public bool SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtls dtl, ref string pMsg)
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
            return _EHGEntities.SetEHGVehicleAllotmentDetails(mData, ref pMsg);
        }
        public bool UpdateEHGHdr(EHGHeader header, ref string pMsg)
        {
            return _EHGEntities.UpdateEHGHdr(header, ref pMsg);
        }
    }
}
