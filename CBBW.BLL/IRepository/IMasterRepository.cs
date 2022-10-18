using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;

namespace CBBW.BLL.IRepository
{
    public interface IMasterRepository
    {
        IEnumerable<ServiceType> getAllServiceTypes(ref string pMsg);
        ServiceType getServiceType(int ID,ref string pMsg);
    }
}
