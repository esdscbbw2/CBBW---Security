using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TFD
{
   public class TFDTravellingPerson
    {
        public string NoteNumber { get; set; }
        public int PersonType { get; set; }
        public int PersonID { get; set; }
        public string PersonIDName { get; set; }
        public int DesignationCode { get; set; }
        public string DesignationCodeText { get; set; }
        public int PersonCentre { get; set; }
        public string PersonCentreName { get; set; }
        public int AuthEmployeeCode { get; set; }
        public string AuthEmployeeCodeName { get; set; }
        public int CentreCode { get; set; }
        public string CentreCodeName { get; set; }
        public string PersonTypeText { get; set; }
    }
}
