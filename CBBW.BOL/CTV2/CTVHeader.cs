using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CTV2
{
    public class CTVHeaderToSet
    {
        public string NoteNumber { get; set; }
        public int CentreCode { get; set; }
        public string CentreName { get; set; }
        public int FortheMonth { get; set; }
        public int FortheYear { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public int DriverNo { get; set; }
        public string DriverName { get; set; }
        public int EmployeeNumber { get; set; }

    }
}
