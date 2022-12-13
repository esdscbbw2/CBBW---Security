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

        public bool SetEHGTravellingPersonDetails(string NoteNumber, List<EHGTravelingPersondtls> dtldata, ref string pMsg)
        {
           return _EHGEntities.SetEHGTravellingPersonDetails(NoteNumber, dtldata, ref pMsg);
        }
    }
}
