using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;

namespace CBBW.BLL.IRepository
{
    public interface IEHGRepository
    {
        EHGHeader getNewEHGHeader(ref string pMsg);
    }
}
