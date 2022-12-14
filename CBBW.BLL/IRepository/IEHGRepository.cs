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
        bool SetEHGHdrForManagement(EHGHeader header, EHGTravelingPersondtls dtl, ref string pMsg);
        bool SetEHGTravellingPersonDetails(string NoteNumber, List<EHGTravelingPersondtls> dtldata, ref string pMsg);
        bool SetDateWiseTourDetails(string NoteNumber, List<DateWiseTourDetails> dtldata, ref string pMsg);
        bool SetEHGVehicleAllotmentDetails(VehicleAllotmentDetails mData, ref string pMsg);
    }
}
