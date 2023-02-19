using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.EHG;
using CBBW.BOL.EntryII;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;
namespace CBBW.DAL.Entities
{
    public class EntryIIEntities
    {
        EntryIIDataSync _EntryIIDataSync;
        EntryIIDBMapper _EntryIIDBMapper;
        DataTable dt;
        public EntryIIEntities()
        {
            _EntryIIDataSync = new EntryIIDataSync();
            _EntryIIDBMapper = new EntryIIDBMapper();
        }
        public List<EntryIINote> GetEntryIINotes(int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            List<EntryIINote> result = new List<EntryIINote>();
            try
            {
                dt = _EntryIIDataSync.GetEntryIINotes(CentreCode, IsMainLocation, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_EntryIINote(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EntryIIList> GetEntryIINoteList(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, int CentreCode, bool IsMainLocation, ref string pMsg)
        {
            List<EntryIIList> result = new List<EntryIIList>();
            try
            {
                dt = _EntryIIDataSync.GetEntryIINoteList(DisplayLength,DisplayStart,SortColumn,SortDirection,SearchText, CentreCode, IsMainLocation, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_EntryIIList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<EntryIITravelingDetails> GetEntryIITravellingDetails(string NoteNumber, ref string pMsg)
        {
            List<EntryIITravelingDetails> result = new List<EntryIITravelingDetails>();
            try
            {
                dt = _EntryIIDataSync.GetEntryIITravellingDetails(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_EntryIIDBMapper.Map_EntryIITravelingDetails(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public VehicleAllotmentDetails GetEntryIIVehicleAllotmentDetails(string Notenumber, ref string pMsg)
        {
            try
            {
                dt = _EntryIIDataSync.GetEntryIIVehicleAllotmentDetails(Notenumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    EHGDBMapper _EHGDBMapper = new EHGDBMapper();
                    return _EHGDBMapper.Map_VehicleAllotmentDetails(dt.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            { pMsg = ex.Message; return null; }
        }




    }
}
