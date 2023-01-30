using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETSEdit
{
    public class DWTTourDetailsForEdit 
    {
        public string NoteNumber { get; set; }
        public int EditTag { get; set; }
        public string ReasonForEdit { get; set; }
        public List<DWTourDetailsFromTable> DWTDetails { get; set; }
    }
    public class DWTIndTourDetailsForEdit : DWTTourDetailsForEdit
    {        
        public int PersonType { get; set; }
        public int PersonID { get; set; }
        public string PersonName { get; set; }
    }
    public class DWTTourDetailsForDB : DWTIndTourDetailsForEdit
    {
        public bool IsIndividualEdit { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }        
    }
    public class DWTourDetailsFromTable
    {        
        public int EditRowTag { get; set; }
        public int SourceID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string EditToDate { get; set; }
        public string TourCategoryIDs { get; set; }
        public string TourCategoryNames { get; set; }
        public string CentreCodes { get; set; }
        public string CentreNames { get; set; }
        public string CentreCodesMulti { get; set; }
        public string CentreNamesMulti { get; set; }
        public string CentreCodesDD { get; set; }
        public string CentreNamesDD { get; set; }
        public string BranchCodes { get; set; }
        public string BranchNames { get; set; }
        public string BranchCodesDD { get; set; }
        public string BranchNamesDD { get; set; }
        
    }
}
