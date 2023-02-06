using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.ETSEdit
{
    public class EntryITourDetails
    {
        public string NoteNumber { get; set; }
        public EntryITDetails TravellingDetails { get; set; }
        public List<EntryIDWDetails> DateWiseDetails { get; set; }
    }
    public class EntryITDetails 
    {
        public bool PublicTransport { get; set; }
        public int VehicleType { get; set; }
        public string ReasonVehicleReq { get; set; }
        public int VehicleTypeProvided { get; set; }
        public string ReasonVehicleProvided { get; set; }
        public DateTime SchFromDate { get; set; }
        public string SchFromDateDisplay { get; set; }
        public string SchFromTime { get; set; }
        public DateTime SchToDate { get; set; }
        public string SchToDateDisplay { get; set; }
        public string PurposeOfVisit { get; set; }
        public bool IsActive { get; set; }
        public string PublicTransportText { get; set; }
        public string VehicleTypeText { get; set; }
        public string VehicleTypeProvidedText { get; set; }

    }
    public class EntryIDWDetails 
    {        
        public DateTime SchFromDate { get; set; }
        public string SchFromDateDisplay { get; set; }
        public DateTime SchToDate { get; set; }
        public string SchToDateDisplay { get; set; }
        public string TourCategoryIds { get; set; }
        public string TourCategoryNames { get; set; }
        public string TourCenterCodeIds { get; set; }
        public string TourCenterNames { get; set; }
        public string BranchCodes { get; set; }
        public string BranchNames { get; set; }
        public bool IsActive { get; set; }
    }
    public class EntryINoteList 
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecord { get; set; }
        public string NoteNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public int CenterCode { get; set; }
        public string CenterName { get; set; }
        public string VehicleNumber { get; set; }
        public bool CanDelete { get; set; }
    }
}
