using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.EHG
{
    public class EHGNote
    {
        public string NoteNumber { get; set; }
    }
    public class EHGHeader: EHGNote
    {
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string CenterCodenName { get; set; }
        public int VehicleType { get; set; }
        public int PurposeOfAllotment { get; set; }
        public int MaterialStatus { get; set; }
        public int Initiator { get; set; }
        public int Instructor { get; set; }
        public string InstructorName { get; set; }
        public int AuthorisedEmpNo { get; set; }
        public string AuthorisedEmployeeName { get; set; }
        public string InitiatorName { get; set; }
        public string InitiatorCodenName { get; set; }
        public bool IsActive { get; set; }
        public string POA2 { get; set; }
        public bool IsApproved { get; set; }
        public DateTime AppDateTime { get; set; }
        public string ReasonForDisApproval { get; set; }
        public string POAText { get; set; }
        public string VehicleTypeText { get; set; }
        public string DocFileName { get; set; }
    }
}
