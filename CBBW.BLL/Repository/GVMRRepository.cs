using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.GVMR;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class GVMRRepository : IGVMRRepository
    {

        GVMREntities _GVMREntities;
        MasterEntities _MasterEntities;
        UserRepository _user;
        public GVMRRepository()
        {
            _GVMREntities = new GVMREntities();
        }
        public IEnumerable<GVMRNoteNumber> GetNoteNumbers(int CenterCode, int status, ref string pMsg)
        {
            return _GVMREntities.GetNoteNumbers(CenterCode, status, ref pMsg);
        }
        public List<GVMRDetails> GetCenterWiseGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg)
        {
            return _GVMREntities.GetGVMRDetails(NoteNumber, CenterCode, ref pMsg);
        }
        public GVMRHeader GetGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg)
        {
            List<PunchingDetails> objpunch = new List<PunchingDetails>();
            GVMRHeader obj = new GVMRHeader();
            obj.gvmrDetailslist = _GVMREntities.GetGVMRDetails(NoteNumber, CenterCode, ref pMsg);
            if (obj.gvmrDetailslist != null)
            {
                foreach (var item in obj.gvmrDetailslist.Select(x => new { x.NoteNo, x.VehicleNo, x.VehicleType, x.ModelName, x.EntryDate, x.EntryDateDisplay, x.EntryTime, x.MonthYear, x.DriverNo, x.DriverName, x.LocationName, x.SchFromDate, x.SchToDate }).Distinct().ToList())
                {
                    obj.NoteNo = item.NoteNo;
                    obj.VehicleNo = item.VehicleNo;
                    obj.VehicleType = item.VehicleType;
                    obj.ModelName = item.ModelName;
                    obj.EntryDate = item.EntryDate;
                    obj.EntryDateDisplay = item.EntryDateDisplay;
                    obj.EntryTime = item.EntryTime;
                    obj.MonthYear = item.MonthYear;
                    obj.DriverNo = item.DriverNo;
                    obj.DriverName = item.DriverName;
                    obj.LocationName = item.LocationName;
                    obj.FromDate = item.SchFromDate;
                    obj.ToDate = item.SchToDate;
                }
                obj.LocationCodes = string.Join(",", obj.gvmrDetailslist.Select(x => x.LocationCode).Distinct().ToList());
                objpunch = _GVMREntities.GetPunchingDetails(obj.LocationCodes, obj.FromDate, obj.ToDate, obj.DriverNo, ref pMsg);

                foreach (var items in obj.gvmrDetailslist)
                {
                    PunchingDetails data = objpunch.Where(x => x.LocationCode == items.LocationCode).FirstOrDefault();
                    if (data != null)
                    {
                        obj.NoData = true;
                        if (data.IsRFIDCentre == false)
                        {
                            items.ActualTripOutDate = data.PunchOutDate;
                            items.ActualTripOutDateDisplay = data.PunchOutDate.Year == 0001 ? "NA" : data.PunchOutDatestr;
                            items.ActualTripInDateDisplay = data.PunchInDate.Year == 0001 ? "NA" : data.PunchInDatestr;
                            items.ActualTripOutTime = data.PunchOutTime;
                            items.ActualTripInDate = data.PunchInDate;
                            items.ActualTripInTime = data.PunchinTime;
                            items.IsRFIDCentres = data.IsRFIDCentre;
                            items.ActualOutRFIDCard = "NA";
                            items.ActualInRFIDCard = "NA";
                        }
                        else
                        {
                            items.ActualTripOutDate = items.ActualTripOutDate;
                            items.ActualTripOutDateDisplay = items.ActualTripOutDate.Year == 0001 ? null : items.ActualTripOutDateDisplay;
                            items.ActualTripInDateDisplay = items.ActualTripInDate.Year == 0001 ? null : items.ActualTripInDateDisplay;
                            items.ActualTripOutTime = items.ActualTripOutTime;
                            items.ActualTripInDate = items.ActualTripInDate;
                            items.ActualTripInTime = items.ActualTripInTime;
                            items.IsRFIDCentres = data.IsRFIDCentre;
                            items.ActualOutRFIDCard = items.ActualOutRFIDCard;
                            items.ActualInRFIDCard = items.ActualInRFIDCard;
                        }
                    }
                    else
                    {
                        obj.NoData = false;
                    }
                }
            }
            return obj;

        }
        public bool setGVMRDetails(GVMRDataSave gvmrdata, ref string pMsg)
        {
            return _GVMREntities.setGVMRDetails(gvmrdata, ref pMsg);
        }
        public List<GVMRNoteList> getGVMRDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CenterCode, ref string pMsg)
        {
            return _GVMREntities.getGVMRDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, ref pMsg);
        }
        public GVMRHeader getGVMRDetailsForView(string NoteNumber, int CenterCode, ref string pMsg)
        {
            GVMRHeader obj = new GVMRHeader();
            obj.gvmrDetailslist = _GVMREntities.getGVMRDetailsForView(NoteNumber, CenterCode, ref pMsg);

            if (obj.gvmrDetailslist != null)
            {
                foreach (var item in obj.gvmrDetailslist.Select(x => new { x.NoteNo, x.VehicleNo, x.VehicleType, x.ModelName, x.EntryDate, x.EntryDateDisplay, x.EntryTime, x.MonthYear, x.DriverNo, x.DriverName, x.LocationName, x.CenterCode }).Distinct().ToList())
                {
                    obj.NoteNo = item.NoteNo;
                    obj.VehicleNo = item.VehicleNo;
                    obj.VehicleType = item.VehicleType;
                    obj.ModelName = item.ModelName;
                    obj.EntryDate = item.EntryDate;
                    obj.EntryDateDisplay = item.EntryDateDisplay;
                    obj.EntryTime = item.EntryTime;
                    obj.MonthYear = item.MonthYear;
                    obj.DriverNo = item.DriverNo;
                    obj.DriverName = item.DriverName;

                }
            }
            return obj;

        }

        public List<PunchingDetails> GetPunchingDetails(string CentreCode, DateTime FromDate, DateTime ToDate, int UserID, ref string pMsg)
        {
            return _GVMREntities.GetPunchingDetails(CentreCode, FromDate, ToDate, UserID, ref pMsg);
        }

        public bool SetGVMRDetailsV2(List<GVMRDataSave> dtldata, ref string pMsg)
        {
            return _GVMREntities.SetGVMRDetailsV2(dtldata, ref pMsg);
        }

        //public GVMRHeader GetGVMRDetailsWithPunchingDetails(string NoteNumber, int CenterCode, ref string pMsg)
        //{

        //    GVMRHeader obj = new GVMRHeader();
        //    obj.GVMRpunching = _GVMREntities.GetGVMRDetailsWithPunchingDetails(NoteNumber, CenterCode, ref pMsg);
        //    if (obj.GVMRpunching.gvmrdetails != null)
        //    {
        //        foreach (var item in obj.GVMRpunching.gvmrdetails.Select(x => new { x.NoteNo, x.VehicleNo, x.VehicleType, x.ModelName, x.EntryDate, x.EntryDateDisplay, x.EntryTime, x.MonthYear, x.DriverNo, x.DriverName, x.LocationName }).Distinct().ToList())
        //        {
        //            obj.NoteNo = item.NoteNo;
        //            obj.VehicleNo = item.VehicleNo;
        //            obj.VehicleType = item.VehicleType;
        //            obj.ModelName = item.ModelName;
        //            obj.EntryDate = item.EntryDate;
        //            obj.EntryDateDisplay = item.EntryDateDisplay;
        //            obj.EntryTime = item.EntryTime;
        //            obj.MonthYear = item.MonthYear;
        //            obj.DriverNo = item.DriverNo;
        //            obj.DriverName = item.DriverName;
        //            obj.LocationName = item.LocationName;
        //        }
        //    }
        //    return obj;
        //    //return _GVMREntities.GetGVMRDetailsWithPunchingDetails(NoteNumber,CenterCode,ref pMsg);
        //}
    }
}
