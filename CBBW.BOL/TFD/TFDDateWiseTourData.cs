using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.TFD
{
    public class TFDDateWiseTourData
    {
        public string NoteNumber { get; set; }
        public string TourCategoryCodes { get; set; }
        public string TourCategoryText{ get; set; }
        public int CentreCode { get; set; }
        public string CentreCodeName { get; set; }
        public DateTime SchFromDate { get; set; }
        public string ActualTourInTime { get; set; }
        public string ActualTourOutTime { get; set; }
        public int PersonType { get; set; }
        public string PersonIDName { get; set; }
        public int PersonID { get; set; }
        public string SchFromDatestr { get; set; }
        public bool IsApproval { get; set; }
        public string IsApprovals { get; set; }
        public string ApprovalRemark { get; set; }
    }
}
