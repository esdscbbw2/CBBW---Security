using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.DAL.Entities;

namespace CBBW.Areas.Security.ViewModel.EHG
{
    public class EHGHeaderEntryVM
    {
        MasterEntities _master;
        public EHGHeaderEntryVM(int centerCode)
        {
            EHGMaster master = EHGMaster.GetInstance;
            this.VehicleTypes = master.VehicleTypes;
            this.PurposeOfAllotment = master.PurposeOfAllotment;
            this.PersonType = master.PersonType;
            this.TourCategory = master.TourCategory;
            this.VehicleBelongsTo = master.VehicleBelongsTo;
            string pMsg = "";
            _master = new MasterEntities();
            this.DriverList = _master.getDriverList(ref pMsg);
            this.StaffList = _master.getEmployeeList(centerCode, 99, 0, ref pMsg);
            this.MDDICList = _master.getEmployeeList(centerCode, 0, 0, ref pMsg);
            this.OtherStaffList = _master.getEmployeeList(centerCode, 99, 1, ref pMsg);
        }        
        public EHGHeader ehgHeader { get; set; }
        public List<CustomComboOptions> VehicleTypes { get; set; }
        public List<CustomComboOptions> PurposeOfAllotment { get; set; }
        public List<CustomComboOptions> PersonType { get; set; }
        public List<CustomComboOptions> TourCategory { get; set; }
        public List<CustomComboOptions> VehicleBelongsTo { get; set; }
        public IEnumerable<CustomComboOptions> DriverList { get; set; }
        public IEnumerable<CustomComboOptions> StaffList { get; set; }
        public IEnumerable<CustomComboOptions> MDDICList { get; set; }
        public IEnumerable<CustomComboOptions> OtherStaffList { get; set; }        
    }
}