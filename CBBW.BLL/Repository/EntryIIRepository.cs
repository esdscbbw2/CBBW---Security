using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.BOL.ETSEdit;
using CBBW.BOL.MGP;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class EntryIIRepository : IEntryIIRepository
    {
        MGPEntities _MGPEntities;
        EntryIIEntities _EntryIIEntities;
        ETSEditEntities _ETSEditEntities;
        public EntryIIRepository()
        {
            _MGPEntities = new MGPEntities();
            _EntryIIEntities = new EntryIIEntities();
            _ETSEditEntities = new ETSEditEntities();
        }
        public List<RFID> GetRFIDCards(ref string pMsg)
        {
            return _MGPEntities.getRFIDCards(ref pMsg);
        }
        public List<MGPReferenceDCDetails> GetMatOutDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            return _MGPEntities.getReferenceDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
        }
        public List<MGPReferenceDCDetails> GetMatInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            return _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
        }
        public IEnumerable<EntryIINote> GetDCNotes(string VehicleNo, DateTime FromDT, DateTime ToDT, bool IsMatOut, ref string pMsg)
        {
            List<EntryIINote> result = new List<EntryIINote>();
            try
            {
                List<MGPReferenceDCDetails> objList = new List<MGPReferenceDCDetails>();
                if (IsMatOut)
                {
                    objList = _MGPEntities.getReferenceDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                else
                {
                    objList = _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                if (objList != null && objList.Count > 0)
                {
                    foreach (var obj in objList)
                    {
                        EntryIINote note = new EntryIINote();
                        note.NoteNumber = obj.NoteNumber;
                        result.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public MGPReferenceDCDetails GetDCDetails(string NoteNumber, string VehicleNo, DateTime FromDT, DateTime ToDT, bool IsMatOut, ref string pMsg)
        {
            MGPReferenceDCDetails result = new MGPReferenceDCDetails();
            try
            {
                List<MGPReferenceDCDetails> objList = new List<MGPReferenceDCDetails>();
                if (IsMatOut)
                {
                    objList = _MGPEntities.getReferenceDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                else
                {
                    objList = _MGPEntities.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
                }
                result = objList.Where(o => o.NoteNumber == NoteNumber).FirstOrDefault();
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public List<MGPItemWiseDetails> GetMatOutItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getItemWiseDetails(NoteNumber, ref pMsg);
        }
        public List<MGPItemWiseDetails> GetMatInItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            return _MGPEntities.getItemWiseInDetails(NoteNumber, ref pMsg);
        }
        public List<EntryIINote> GetEntryIINotes(int CentreCode, bool IsMainLocation, ref string pMsg) 
        {
            return _EntryIIEntities.GetEntryIINotes(CentreCode, IsMainLocation, ref pMsg);
        }
        public List<EntryIIList> GetEntryIINoteList(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText, int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIINoteList(DisplayLength,DisplayStart,SortColumn,SortDirection,SearchText,CentreCode,IsMainLocation,ref pMsg);
        }
        public EditNoteDetails GetEditNoteHdr(string NoteNumber, ref string pMsg)
        {
            return _ETSEditEntities.getEditNoteHdr(NoteNumber, ref pMsg);
        }
        public List<EntryIITravelingDetails> GetEntryIITravellingDetails(string NoteNumber, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIITravellingDetails(NoteNumber, ref pMsg);
        }
        public VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string Notenumber, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIIVehicleAllotmentDetails(Notenumber,ref pMsg);
        }
        public List<MainLocationPersons> GetMainLocationTPs(string NoteNumber, ref string pMsg)
        {
            int CentreCode;
            List<PunchInDetails> Punchings=new List<PunchInDetails>();
            List<LastCentrePunchOutWithDistance> LastPunchings=new List<LastCentrePunchOutWithDistance>();
            MLPersonsInfo result = _EntryIIEntities.GetMainLocationTPs(NoteNumber,ref pMsg);
            if (result != null && result.PersonInfo!=null) 
            {
                CentreCode = result.PersonInfo.FirstOrDefault().MainLocationCode;
                if (result.EmpDatesForPunching != null) 
                {
                    Punchings = _EntryIIEntities.GetPunchingsV3(CentreCode, result.EmpDatesForPunching, ref pMsg);
                }
                if (result.EmpDatesForReq != null)
                {
                    LastPunchings = _EntryIIEntities.GetLastPunchingCentresV3(CentreCode, result.EmpDatesForReq, ref pMsg);
                }
                foreach (var item in result.PersonInfo) 
                {
                    PunchInDetails PunchOut = Punchings.Where(o => o.EmployeeNumber == item.PersonID && o.PunchDate == item.SchFromDate).FirstOrDefault();
                    PunchInDetails PunchIn = Punchings.Where(o => o.EmployeeNumber == item.PersonID && o.PunchDate == item.SchToDate).FirstOrDefault();
                    LastCentrePunchOutWithDistance LastPunching= LastPunchings.Where(o=>o.EmployeeNumber==item.PersonID && o.PunchDate==item.SchToDate).FirstOrDefault();
                    item.RequiredTourInDate = item.SchToDate;
                    if (LastPunching != null) 
                        item.RequiredTourInTime = item.IsVehicleProvided ? LastPunching.ComVehRequiredPunchIn:LastPunching.PubTransRequiredPunchIn;
                    if (PunchOut != null)
                    {
                        if (item.Isdriver != 1)
                            PunchOut.PunchOut = PunchOut.LateNightPunch.Hour>0 ? PunchOut.LateNightPunch : PunchOut.PunchOut;
                        item.ActualTourOutDate = PunchOut.PunchDate == null || PunchOut.PunchDate.Year == 1 ? item.SchFromDate : PunchOut.PunchDate;
                        item.ActualTourOutTime = PunchOut.PunchOut.Hour>0 ? PunchOut.PunchOut :DateTime.Parse(item.SchFromTime);
                    }
                    else 
                    { 
                        item.ActualTourOutDate = item.SchFromDate;
                        item.ActualTourOutTime = DateTime.Parse(item.SchFromTime);
                    }
                    if (PunchIn != null)
                    {
                        if (item.Isdriver != 1)
                            PunchIn.PunchIn = PunchIn.EarlyMorningPunch != null ? PunchIn.EarlyMorningPunch : PunchIn.PunchIn;
                        item.ActualTourInDate = PunchIn.PunchDate == null || PunchIn.PunchDate.Year == 1 ? item.SchToDate : PunchIn.PunchDate;
                        item.ActualTourInTime = PunchIn.PunchIn;
                    }
                    else
                        item.ActualTourInDate = item.SchToDate;
                    item.TourStatus = item.SchToDate <= DateTime.Today ? 1 : 0;
                }
            }
            
            return result.PersonInfo;
        }
        public LocationWiseTPDetails GetLocationWiseTPs(string NoteNumber, int CentreCode, ref string pMsg)
        {
            //Dummy Code
            CentreCode = 7;
            //Dummy Code END
            List<PunchInDetails> Punchings = new List<PunchInDetails>();
            List<LastCentrePunchOutWithDistance> LastPunchings = new List<LastCentrePunchOutWithDistance>();
            LocationWiseTPDetails result = _EntryIIEntities.GetLocationWiseTPs(NoteNumber, CentreCode, ref pMsg);
            if (result != null && result.PersonDetails != null && result.PersonDateWiseDetails!=null) 
            {
                if (result.EmpDatesForPunching != null)
                {
                    Punchings = _EntryIIEntities.GetPunchingsV3(CentreCode, result.EmpDatesForPunching, ref pMsg);
                    LastPunchings = _EntryIIEntities.GetLastPunchingCentresV3(CentreCode, result.EmpDatesForPunching, ref pMsg);
                }
                foreach (var item in result.PersonDateWiseDetails) 
                {
                    PunchInDetails Punching = Punchings.Where(o => o.EmployeeNumber == item.PersonID && o.PunchDate == item.MCurDate).FirstOrDefault();
                    LastCentrePunchOutWithDistance LastPunching = LastPunchings.Where(o => o.EmployeeNumber == item.PersonID && o.PunchDate == item.MCurDate).FirstOrDefault();
                    if (Punching != null)
                    {
                        if (item.Isdriver != 1)
                        {
                            Punching.PunchOut = Punching.LateNightPunch.Hour >0 ? Punching.LateNightPunch : Punching.PunchOut;
                            Punching.PunchIn = Punching.EarlyMorningPunch.Hour>0 ? Punching.EarlyMorningPunch : Punching.PunchIn;
                        }
                        else 
                        {
                            item.LNPunchTime = Punching.LateNightPunch;
                            item.EMPunchTime = Punching.EarlyMorningPunch;
                        }
                        if (Punching.PunchIn != Punching.PunchOut)
                            item.ActualTourOutTime = Punching.PunchOut;                        
                        item.ActualTourInTime = Punching.PunchIn;
                    }
                    item.ActualTourOutDate = item.MCurDate;
                    item.ActualTourInDate = item.MCurDate;
                    item.RequiredTourInDate= item.MCurDate;
                    if (LastPunching != null)
                    {
                        item.RequiredTourInTime = item.IsVehicleProvided ? LastPunching.ComVehRequiredPunchIn : LastPunching.PubTransRequiredPunchIn;
                    }
                    else 
                    {
                        if (item.DWTourCategoryIds.IndexOf("3") > 0)
                            item.RequiredTourInTime = item.MainLocationGenTimeIn;
                        else if (item.DWTourCategoryIds.IndexOf("4") > 0)
                            item.RequiredTourInTime = item.ActualTourInTime;
                        else
                            item.RequiredTourInTime = item.CentreTimeIn;
                        item.EMPunchRequired =item.Isdriver==1?1:0;
                    }
                    item.TourStatus = item.MCurDate < item.DWToDate ? 0 : 1;
                    item.LNPunchRequired = item.Isdriver == 1?item.TourStatus == 0 ? 1 : 0:0;
                    item.EMPunchStatus =item.Isdriver==1?item.EMPunchRequired == 1 ? item.EMPunchTime.Hour>0?"Yes": "Required But Not Entered" : "NR":"No";
                    item.LNPunchStatus = item.Isdriver == 1 ? item.LNPunchRequired == 1 ? item.LNPunchTime.Hour>0 ? "Yes" : "Required But Not Entered" : "NR":"No";
                
                }
            }
            return result;
        }
        public bool SetEntryIIData(string NoteNumber, bool IsMainLocation, int CentreCode,bool IsOffline, List<SaveTPDetails> Persons, List<SaveTPDWDetails> DWTour, List<SaveVehicleDetails> VAData, ref string pMsg)
        {
            return _EntryIIEntities.SetEntryIIData(NoteNumber, IsMainLocation, CentreCode,IsOffline, Persons, DWTour,VAData, ref pMsg);
        }
        public bool UpdateEntryIIData(string NoteNumber, int CentreCode, string CentreName, bool IsEPTour, bool IsMainLocation, ref string pMsg)
        {
            return _EntryIIEntities.UpdateEntryIIData(NoteNumber,CentreCode,CentreName,IsEPTour,IsMainLocation,ref pMsg);
        }
        public VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string NoteNumber, DateTime FromDate, DateTime ToDate, int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            return _EntryIIEntities.GetEntryIIVehicleAllotmentDetails(NoteNumber,FromDate,ToDate,CentreCode,IsMainLocation, ref pMsg);
        }
        public PunchInDetails GetPunchingDetails(int EmployeeNumber, DateTime PunchDate, int CentreCode, string RFIDNumber, ref string pMsg)
        {
            return _EntryIIEntities.GetPunchingDetails(EmployeeNumber, PunchDate, CentreCode, RFIDNumber,ref pMsg);
        }
        public int GetTravelKmsOfANote(string NoteNumber, DateTime TillDate, int FromLocation, ref string pMsg)
        {
            return _EntryIIEntities.GetTravelKmsOfANote(NoteNumber, TillDate, FromLocation,ref pMsg);
        }
    }
}
