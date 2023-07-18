using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.Alerts;
using CBBW.BOL.CTV;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
   public class AlertRepository: IAlertRepository
    {
        UserRepository _user;
        UserInfo user;
        AlertEntities _AlertEntities;
        MasterEntities _MasterEntities;
        public AlertRepository()
        {
            _AlertEntities = new AlertEntities();
            _MasterEntities = new MasterEntities();
            _user = new UserRepository();
        }

        public List<AlertDetail> GetAlertDetails(int ID, ref string pMsg)
        {
            return _AlertEntities.GetAlertDetails(ID, ref pMsg);
        }

        public List<AlertMaster> GetAlertMasterDetails(int USerId, ref string pMsg)
        {
            return _AlertEntities.GetAlertMasterDetails(USerId, ref pMsg);
        }
    }
}
