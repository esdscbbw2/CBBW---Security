using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CTV;

namespace CBBW.BLL.IRepository
{
    public interface ICTVRepository
    {
        string getNewTripScheduleNo(string SchPattern, ref string pMsg);
        TripScheduleHdr NewTripScheduleNo(string SchPattern, ref string pMsg);
    }
}
