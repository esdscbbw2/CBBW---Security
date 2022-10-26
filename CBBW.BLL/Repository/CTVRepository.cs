using System;
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
            return _CTVEntities.CheckAvailibiltyofSchDate(VehicleNo, ScheduleDate, ref pMsg);
        }

        public bool CreateNewCTVHdr(TripScheduleHdr model, ref string pMsg)
        {
            return _CTVEntities.CreateCTVHdr(model, ref pMsg);
        }

        public List<VehicleNo> getLCVMCVVehicleList(ref string pMsg)
        {
            return _CTVEntities.getLCVMCVVehicleList(ref pMsg);
        }
        public IEnumerable<LocVehSchFromMat> getLocalVehicleSChedules(string VehicleNo, DateTime FromDate, DateTime ToDate, ref string pMsg)
        {
            return _CTVEntities.getLocalVehicleSchedule(VehicleNo, FromDate, ToDate, ref pMsg);
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

        public CTVHdrDtl getSchDetailsFromNote(string NoteNumber, ref string pMsg)
        {
            return _CTVEntities.getCTVSchDetailsFromNote(NoteNumber, ref pMsg);
        }

        public DateTime getSchToDate(DateTime FromSchDt, int FromLocation, int ToLocationType, 
            int ToLocation, int IsCalculateHourly, ref string pMsg)
        {
            return _MasterEntities.GetToSchDate(FromSchDt, FromLocation, ToLocationType,
                ToLocation, IsCalculateHourly, ref pMsg);
        }

        public UserInfo getUserInfo(string UserName, ref string pMsg)
        {
            return _CTVEntities.getLogInUserInfo(UserName, ref pMsg);
        }

        public VehicleInfo getVehicleInfo(string VehicleNo, ref string pMsg)
        {
           return _CTVEntities.getVehicleInfo(VehicleNo, ref pMsg);
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

        public bool RemoveNote(string NoteNumber, ref string pMsg)
        {
            return _CTVEntities.RemoveNote(NoteNumber, ref pMsg);
        }

        public bool UpdateOthTripSchDtl(string Notenumber, List<OthTripTemp> dtldata, ref string pMsg)
        {
            return _CTVEntities.InsertOthTripSchDtl(Notenumber, dtldata, ref pMsg);
        }
    }
}
