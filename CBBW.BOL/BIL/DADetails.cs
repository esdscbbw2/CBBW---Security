using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.BIL
{
    public class DADetails
    {
        public DateTime ActualTourInDate { get; set; }
        public string ActualTourInDatestr { get; set; }
        public double DAAmount { get; set; }
	    public double DADeducted { get; set; }
        public double EAmount { get; set; }
        public int TotalHours { get; set; }
        public int FullDayHours { get; set; }
        public int HalfDayHours { get; set; }
}
}
