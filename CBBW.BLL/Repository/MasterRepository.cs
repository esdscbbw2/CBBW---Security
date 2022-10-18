using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.Master;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class MasterRepository : IMasterRepository
    {
        MasterEntities _entities;
        public MasterRepository()
        {
            _entities = new MasterEntities();
        }

        public IEnumerable<ServiceType> getAllServiceTypes(ref string pMsg)
        {
            return _entities.getServiceTypes(0,ref pMsg);
        }

        public ServiceType getServiceType(int ID, ref string pMsg)
        {
            return _entities.getServiceTypes(ID, ref pMsg).FirstOrDefault();
        }
    }
}
