using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EntryII
{
    public class LastCentrePunchOut 
    {
        public int LocationCode { get; set; }
        public DateTime PunchDate { get; set; }
        public DateTime PunchOut { get; set; }
    }
    public class LastCentrePunchOutWithEmpNo : LastCentrePunchOut
    {
        public int EmployeeNumber { get; set; }
    }
    public class PunchInDetails: LastCentrePunchOutWithEmpNo
    {        
        public int LocationTypeCode { get; set; }
        public DateTime PunchIn { get; set; }        
        public DateTime EarlyMorningPunch { get; set; }
        public DateTime LateNightPunch { get; set; }
    }
    public class LastCentrePunchOutWithDistance : PunchInDetails 
    {
        public int DistanceInKm { get; set; }
        public int ComVehicleMinutes { get; set; }
        public int PubTransMinutes { get; set; }
        public DateTime ComVehRequiredPunchIn { get; set; }
        public DateTime PubTransRequiredPunchIn { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsEarlyMorningPunchNotRequired { get; set; }
    }
}
