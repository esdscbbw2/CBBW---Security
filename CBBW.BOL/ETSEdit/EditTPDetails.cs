using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETSEdit
{
    public class EditTPDetails: EditNoteNumber
    {
        public int PersonType { get; set; }
        public string PersonTypeText { get; set; }
        public int EmployeeNo { get; set; }
        public string EmployeeNonName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationCodenName { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public int EligibleVehicleType { get; set; }
        public string EligibleVehicleTypeName { get; set; }
        public string EPNoteNumber { get; set; }
        public DateTime EPNoteDate { get; set; }
        public bool TADADenied { get; set; }
        public int Isdriver { get; set; }
    }
}
