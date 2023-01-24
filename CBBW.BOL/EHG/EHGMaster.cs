using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;

namespace CBBW.BOL.EHG
{
    public sealed class EHGMaster
    {        
        private static readonly Lazy<EHGMaster> instance = new Lazy<EHGMaster>();
        public static EHGMaster GetInstance 
        {
            get { return instance.Value; }
        }
        public EHGMaster()
        {
            getVehicletypes();
            getPurposeOfAllotment();
            getPersonType();
            getTourCategory();
            getVehicleBelongsTo();
            getEditTag();
        }
        private void getVehicleBelongsTo()
        {
            VehicleBelongsTo = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 0, DisplayText = "Select Vehicle Belongs To" },
                new CustomComboOptions{ ID = 1, DisplayText = "Company Vehicle" },
                new CustomComboOptions{ ID = 2, DisplayText = "Other Vehicle" }
            };
        }
        private void getTourCategory()
        {
            TourCategory = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 1, DisplayText = "Center Visit" },
                new CustomComboOptions{ ID = 2, DisplayText = "Branch & Center Visit" },
                new CustomComboOptions{ ID = 3, DisplayText = "Others" }
            };
            TourCategoryForNZB = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 1, DisplayText = "Center Visit" },
                new CustomComboOptions{ ID = 2, DisplayText = "Branch & Center Visit" },
                new CustomComboOptions{ ID = 4, DisplayText = "Branch Visit" },
                new CustomComboOptions{ ID = 5, DisplayText = "Unknown Visit" },
                new CustomComboOptions{ ID = 3, DisplayText = "Others" }
            };
            TourCategoryForEdit = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 1, DisplayText = "Center Visit" },
                new CustomComboOptions{ ID = 2, DisplayText = "Branch & Center Visit" },
                new CustomComboOptions{ ID = 4, DisplayText = "Branch Visit" },
                new CustomComboOptions{ ID = 5, DisplayText = "Unknown Destination" },
                new CustomComboOptions{ ID = 3, DisplayText = "Others" },
                new CustomComboOptions{ ID = 6, DisplayText = "E.P. Tour" }
            };
        }
        private void getPersonType()
        {
            PersonType = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 1, DisplayText = "Staff" },
                new CustomComboOptions{ ID = 2, DisplayText = "Driver " },                                
                new CustomComboOptions{ ID = 3, DisplayText = "Others " },
                new CustomComboOptions{ ID = 4, DisplayText = "Management " }
            };
        }
        private void getPurposeOfAllotment()
        {
            PurposeOfAllotment = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 1, DisplayText = "For Management" },
                new CustomComboOptions{ ID = 2, DisplayText = "For Office Work" }
            };
        }
        private void getVehicletypes()
        {
            VehicleTypes = new List<CustomComboOptions>() 
            {                
                new CustomComboOptions{ ID = 1, DisplayText = "LV" },
                new CustomComboOptions{ ID = 2, DisplayText = "2 Wheeler" },
                new CustomComboOptions{ ID = 3, DisplayText = "Public Transport" }
            };
            VehicleTypesForHg = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 1, DisplayText = "LV" },
                new CustomComboOptions{ ID = 2, DisplayText = "2 Wheeler" }
            };
        }
        private void getEditTag() 
        {
            EditTag = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 0, DisplayText = "Select Required EDIT" },
                new CustomComboOptions{ ID = 1, DisplayText = "Tour Cancellation" },
                new CustomComboOptions{ ID = 3, DisplayText = "Tour Extension" },
                new CustomComboOptions{ ID = 2, DisplayText = "Other EDIT " }
            };
            IndividualEditTag = new List<CustomComboOptions>()
            {
                new CustomComboOptions{ ID = 0, DisplayText = "Select Required EDIT" },
                new CustomComboOptions{ ID = 1, DisplayText = "Tour Cancellation" },
                new CustomComboOptions{ ID = 2, DisplayText = "Other EDIT " }
            };
        }
        public List<CustomComboOptions> VehicleTypes { get; set; }
        public List<CustomComboOptions> VehicleTypesForHg { get; set; }
        public List<CustomComboOptions> PurposeOfAllotment { get; set; }
        public List<CustomComboOptions> PersonType { get; set; }
        public List<CustomComboOptions> TourCategory { get; set; }
        public List<CustomComboOptions> TourCategoryForNZB { get; set; }
        public List<CustomComboOptions> TourCategoryForEdit { get; set; }
        public List<CustomComboOptions> VehicleBelongsTo { get; set; }
        public List<CustomComboOptions> EditTag { get; set; }
        public List<CustomComboOptions> IndividualEditTag { get; set; }
    }

}
