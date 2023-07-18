using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Alerts;

namespace CBBW.BLL.IRepository
{
    public interface IAlertRepository
    {
        List<AlertMaster> GetAlertMasterDetails(int USerId, ref string pMsg);
        List<AlertDetail> GetAlertDetails(int ID, ref string pMsg);
    }
}
