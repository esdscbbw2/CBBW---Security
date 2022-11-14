﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.CustomModels;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class CTVRepository : ICTVRepository
    {
        CTVEntities _CTVEntities;
        MasterEntities _MasterEntities;
        public CTVRepository()
        {
            _CTVEntities = new CTVEntities();
            _MasterEntities = new MasterEntities();
        }

        public bool CheckScheduleDateAvailibility(string VehicleNo, DateTime ScheduleDate, ref string pMsg)
        {
            bool result = false;
            try
            {
                VehicleAvblInfo obj = _CTVEntities.getVehicleSlot(VehicleNo, 0, ref pMsg);
                if (obj.SlotsAvailable != null)
                {
                    int avbl = obj.SlotsAvailable.Where(o => o.FromDate <= ScheduleDate && o.ToDate >= ScheduleDate).ToList().Count;
                    if (avbl > 0) { result = true; } 
                    else { pMsg = ScheduleDate.ToString("dd-MM-yyyy")+" is not available for vehicle no "+ VehicleNo; }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public bool CreateNewCTVHdr(TripScheduleHdr model, ref string pMsg)
        {
            return _CTVEntities.CreateCTVHdr(model, ref pMsg);
        }

        public List<VehicleNo> getLCVMCVVehicleList(ref string pMsg)
        {
            return _CTVEntities.getLCVMCVVehicleList(ref pMsg);
        }
        public List<LocVehSchFromMat> getLocalVehicleSChedules(string VehicleNo, DateTime FromDate, DateTime ToDate, ref string pMsg)
        {
            return _CTVEntities.getLocalVehicleSchedule(VehicleNo, FromDate, ToDate, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getLocationsFromType(string LocationTypeIDs, ref string pMsg)
        {
            return _MasterEntities.getLocationsFromType(LocationTypeIDs, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getLocationsFromType(int LocationTypeID, ref string pMsg)
        {
           return _MasterEntities.getLocationsFromType(LocationTypeID, ref pMsg);
        }
        public IEnumerable<CustomComboOptions> getLocationTypes(ref string pMsg)
        {
           return _MasterEntities.getLocationTypes(ref pMsg);
        }
        public string getNewTripScheduleNo(string SchPattern, ref string pMsg)
        {
           return _CTVEntities.getNewCTVNoteNo(SchPattern, ref pMsg);
        }

        public List<NoteNumber> GetNoteNumbersTobeApproved(int EmpNo, int CenterCode,ref string pMsg)
        {
            return _CTVEntities.getNotenumbersTobeApproved(EmpNo, CenterCode,ref pMsg);
        }

        public CTVHdrDtl getSchDetailsFromNote(string NoteNumber, ref string pMsg)
        {
            return _CTVEntities.getCTVSchDetailsFromNote(NoteNumber, ref pMsg);
        }
        public DateTime getSchToDateFromMultiLocation(string VehicleNo, DateTime FromSchDt, 
            int FromLocation, string ToLocationType,string ToLocation, ref string pMsg)
        {
            DateTime result = _MasterEntities.GetToSchDateFromMultiLocation(FromSchDt, FromLocation, ToLocationType,
                ToLocation, ref pMsg);
            VehicleAvblInfo obj = _CTVEntities.getVehicleSlot(VehicleNo, 0, ref pMsg);
            if (obj.SlotsAvailable != null)
            {
                int avbl = obj.SlotsAvailable.Where(o => o.FromDate <= result && o.ToDate >= result).ToList().Count;
                if (avbl <= 0) { result = new DateTime(1, 1, 1); }
            }
            return result;
        }
        public DateTime getSchToDate(string VehicleNo,DateTime FromSchDt, int FromLocation, int ToLocationType, 
            int ToLocation, int IsCalculateHourly, ref string pMsg)
        {
            DateTime result = _MasterEntities.GetToSchDate(FromSchDt, FromLocation, ToLocationType,
                ToLocation, IsCalculateHourly, ref pMsg);
            VehicleAvblInfo obj =_CTVEntities.getVehicleSlot(VehicleNo, 0, ref pMsg);
            if (obj.SlotsAvailable != null) 
            {
                int avbl=obj.SlotsAvailable.Where(o => o.FromDate <= result && o.ToDate >= result).ToList().Count;
                if (avbl <= 0) { result = new DateTime(1,1,1); }
            }
            return result;
        }

        public UserInfo getUserInfo(string UserName, ref string pMsg)
        {
            return _CTVEntities.getLogInUserInfo(UserName, ref pMsg);
        }

        public VehicleAvblInfo getVehicleDateSlots(string VehicleNo, int IncludeOTVSch, ref string pMsg)
        {
            return _CTVEntities.getVehicleSlot(VehicleNo, IncludeOTVSch, ref pMsg);
        }

        public VehicleInfo getVehicleInfo(string VehicleNo, ref string pMsg)
        {
            VehicleInfo result = _CTVEntities.getVehicleInfo(VehicleNo, ref pMsg);
            result.IsSlotAvbl = _CTVEntities.getVehicleSlot(VehicleNo, 0,ref pMsg).IsSlotAvbl;
            return result;
        }

        public TripScheduleHdr NewTripScheduleNo(string SchPattern, ref string pMsg)
        {
            TripScheduleHdr result = new TripScheduleHdr();
            try
            {                
                result.NoteNo = _CTVEntities.getNewCTVNoteNo(SchPattern, ref pMsg);
                result.EntryDate = DateTime.Today;
                result.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
                result.FortheMonth = DateTime.Today.Month;
                result.FortheYear = DateTime.Today.Year;
                result.FortheMonthnYear = DateTime.Today.ToString("MMM yyyy");
                result.CenterCode = 13;
                result.CenterName = "Nizamabad";
                result.CentreCodenName = result.CenterCode + "/" + result.CenterName;
                if (DateTime.Today.Day <= 15)
                {
                    result.FromDate = new DateTime(result.FortheYear, result.FortheMonth, 1);
                    result.ToDate = new DateTime(result.FortheYear, result.FortheMonth, 15);
                }
                else
                {
                    result.FromDate = new DateTime(result.FortheYear, result.FortheMonth, 16);
                    result.ToDate = new DateTime(result.FortheYear, result.FortheMonth, 1).AddMonths(1).AddDays(-1);
                }
                result.ListofVehicles = _CTVEntities.getLCVMCVVehicleList(ref pMsg);
            }
            catch { }
            return result;
        }

        public bool RemoveNote(string NoteNumber,int OnlyDtl, ref string pMsg)
        {
            return _CTVEntities.RemoveNote(NoteNumber, OnlyDtl, ref pMsg);
        }

        public bool UpdateOthTripSchDtl(string Notenumber,string TripPurpose, List<OthTripTemp> dtldata, ref string pMsg)
        {
            return _CTVEntities.InsertOthTripSchDtl(Notenumber, TripPurpose, dtldata, ref pMsg);
        }
        public IEnumerable<TripScheduleHdr> getCtvSchedule(int PageSize, int PageNumber, int SortCol, string SortDirection,
            string SearchText, int centercode, ref string pMsg)
        {
            return _CTVEntities.getCtvSchedule(PageSize, PageNumber, SortCol, SortDirection,
                SearchText,centercode, ref pMsg);
        }
        public IEnumerable<TripScheduleHdr> getApprovedCtvSchedule(int PageSize, int PageNumber, int SortCol, string SortDirection,
            string SearchText, int centercode, ref string pMsg)
        {
            return _CTVEntities.getCtvSchedule(PageSize, PageNumber, SortCol, SortDirection,
                SearchText, centercode, ref pMsg).Where(o => o.IsApproved).ToList();
        }
        public bool setCTVApproval(string Notenumber, int EmployeeNumber, bool Isapproved, 
            DateTime ApprovalDatetime, string DisApprovalReason, ref string pMsg)
        {
            if (DisApprovalReason == null) { DisApprovalReason = " "; }
            return _CTVEntities.setCTVApproval(Notenumber,EmployeeNumber,Isapproved,
                ApprovalDatetime,DisApprovalReason,ref pMsg);
        }
    }
}
