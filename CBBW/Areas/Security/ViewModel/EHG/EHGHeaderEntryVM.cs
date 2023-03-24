using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.BOL.EHG;
using CBBW.DAL.Entities;

namespace CBBW.Areas.Security.ViewModel.EHG
{
    public class EHGHeaderEntryVM
    {
        MasterEntities _master;
        TourEntities _tour;
        string pMsg;
        public EHGHeaderEntryVM(int centerCode)
        {
            EHGMaster master = EHGMaster.GetInstance;
            this.VehicleTypes = master.VehicleTypesForHg;
            this.PurposeOfAllotment = master.PurposeOfAllotment;
            this.PersonType = master.PersonType;
            this.TourCategory = master.TourCategory;
            this.VehicleBelongsTo = master.VehicleBelongsTo;
            pMsg = "";
            _master = new MasterEntities();
            this.DriverList = _master.getCenterWiseDriverList(centerCode,ref pMsg);
            this.StaffList = _master.getEmployeeList(centerCode, 99, 0, ref pMsg);
            this.MDDICList = _master.getEmployeeList(centerCode, 0, 0, ref pMsg);
            this.OtherStaffList = _master.getEmployeeList(centerCode, 99, 1, ref pMsg);
            _tour = new TourEntities();
            this.MaxDaysOfTourForEmp = _tour.GetMaxDaysOfTour(1, ref pMsg);
            
        }
        public EHGHeaderEntryVM()
        {
            EHGMaster master = EHGMaster.GetInstance;
            this.VehicleTypes = master.VehicleTypes;
            this.PurposeOfAllotment = master.PurposeOfAllotment;
            this.PersonType = master.PersonType;
            this.TourCategory = master.TourCategory;
            this.VehicleBelongsTo = master.VehicleBelongsTo;
            pMsg = "";
            _master = new MasterEntities();
            _tour = new TourEntities();
            this.MaxDaysOfTourForEmp = _tour.GetMaxDaysOfTour(1, ref pMsg);

        }
        public EHGHeaderEntryVM(bool IsForList)
        {
            pMsg = "";
            _master = new MasterEntities();
            _tour = new TourEntities();
            this.MaxDaysOfTourForEmp = _tour.GetMaxDaysOfTour(1, ref pMsg);

        }
        public IEnumerable<CustomComboOptions> getDriverList(int centerCode) 
        {
            return _master.getCenterWiseDriverList(centerCode, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getStaffList(int centerCode)
        {
            return _master.getEmployeeList(centerCode, 99, 0, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getMDDICList(int centerCode)
        {           
            return _master.getEmployeeList(centerCode, 0, 0, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getOtherStaffList(int centerCode)
        {
            return _master.getEmployeeList(centerCode, 99, 1, ref pMsg);
        }
        public EHGHeader ehgHeader { get; set; }
        public VehicleAllotmentDetails VADetails { get; set; }
        public List<EHGTravelingPersondtls> PersonDtls { get; set; }
        public List<CustomComboOptions> VehicleTypes { get; set; }
        public List<CustomComboOptions> PurposeOfAllotment { get; set; }
        public List<CustomComboOptions> PersonType { get; set; }
        public List<CustomComboOptions> TourCategory { get; set; }
        public List<CustomComboOptions> VehicleBelongsTo { get; set; }
        public IEnumerable<CustomComboOptions> DriverList { get; set; }
        public IEnumerable<CustomComboOptions> StaffList { get; set; }
        public IEnumerable<CustomComboOptions> MDDICList { get; set; }
        public IEnumerable<CustomComboOptions> OtherStaffList { get; set; }
        public List<VehicleNo> VehicleList { get; set; }

        public int AuthorisedEmpNoForManagement { get; set; }
        public string AuthorisedEmpNameForManagement { get; set; }
        public int DriverNoForManagement { get; set; }
        public DateTime FromdateForMang { get; set; }
        public string FromTimeForMang { get; set; }
        public DateTime ToDateForMang { get; set; }
        public string PurposeOfVisitFoeMang { get; set; }
        public int TADADeniedForManagement { get; set; }
        public string MaxFromDate { get; set; }
        public string MinFromDate { get; set; }
        public string DriverNameForManagement { get; set; }
        public string DesgCodeNNameForManagement { get; set; }
        public string FromdateStrForDisplay { get; set; }
        public string FromdateStr { get; set; }
        public string TodateStr { get; set; }
        public int BackBtnActive { get; set; }
        public int SubmitBtnActive { get; set; }
        public int VABackBtnActive { get; set; }
        public int VASubmitBtnActive { get; set; }
        public int DWBackBtnActive { get; set; }
        public int DWSubmitBtnActive { get; set; }
        public int VehicleType { get; set; }
        public int MaterialStatus { get; set; }
        public int Instructor { get; set; }
        public int AuthorisedEmpNo { get; set; }
        public int POA { get; set; }
        public string OthVehNo { get; set; }
        public int IsBtn { get; set; }
        public string MaxToDate { get; set; }
        public int MaxDaysOfTourForEmp { get; set; }
    }
}