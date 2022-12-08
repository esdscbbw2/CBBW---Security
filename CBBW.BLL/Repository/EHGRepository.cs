using System;
using System.Collections.Generic;
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
        public EHGRepository()
        {
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
            user = _user.getLoggedInUser();
        }

        public EHGHeader getNewEHGHeader(ref string pMsg)
        {
            EHGHeader obj= new EHGHeader();
            obj.NoteNumber= _MasterEntities.getNewNoteNumber(NotePattern, ref pMsg);
            obj.EntryDate = DateTime.Today;
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
        
    }
}
