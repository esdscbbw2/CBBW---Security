using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.MGP;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class MGPEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        MGPDataSync _datasync;
        DBResponseMapper _DBResponseMapper;
        MGPMapper _datamapper;
        public MGPEntities()
        {
            _datasync = new MGPDataSync();
            _datamapper = new MGPMapper();
            _DBResponseMapper = new DBResponseMapper();
        }
        public List<MGPNotes> getApprovedNoteNumbers(int Centercode, ref string pMsg) 
        {
            List<MGPNotes> result = new List<MGPNotes>();
            dt= _datasync.getNoteNumbersForMatGatePass(Centercode, ref pMsg);
            if (dt != null && dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    result.Add(_datamapper.Map_MGPNotes(dt.Rows[i]));
                }
            }
            return result;
        }
        public List<MGPOutInDetails> getMGPOutDetails(string NoteNumber, ref string pMsg) 
        {
            List<MGPOutInDetails> result = new List<MGPOutInDetails>();
            try
            {
                ds = _datasync.getMGPOutDetails(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPOutInDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<RFID> getRFIDCards(ref string pMsg) 
        {
            List<RFID> result = new List<RFID>();
            try
            {
                dt = _datasync.getRFIDCards(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RFID obj = new RFID();
                        if (!DBNull.Value.Equals(dt.Rows[i]["RFIDNumber"]))
                            obj.RFIDCardNo = dt.Rows[i]["RFIDNumber"].ToString();
                        result.Add(obj);
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        // Getting data for Out details in Item Wise Details using NoteNo(For New Data insert)
        public List<MGPItemWiseDetails> getItemWiseDetails(string NoteNumber, ref string pMsg)
        {
            List<MGPItemWiseDetails> result = new List<MGPItemWiseDetails>();
            try
            {
                ds = _datasync.getItemWiseDetails(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPItemWiseDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }



        // Getting data for Out details in Reference DC Details using NoteNo(For New Data insert)
        public List<MGPReferenceDCDetails> getReferenceDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            List<MGPReferenceDCDetails> result = new List<MGPReferenceDCDetails>();
            try
            {
                ds = _datasync.getReferenceDCDetails( VehicleNo,  FromDT,  ToDT, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPReferenceDCDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public List<MGPVehicleOutDetails> getSchDtlsForMGP(string NoteNumber, ref string pMsg)
        {
            List<MGPVehicleOutDetails> result = new List<MGPVehicleOutDetails>();
            try
            {
                ds = _datasync.getSchDtlsForMGP(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPVehicleOutDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }



        public List<MGPHistoryDCDetails> getMGPHistoryDCDetails(long ID, ref string pMsg)
        {
            List< MGPHistoryDCDetails> result = new List<MGPHistoryDCDetails> ();
            try
            {
                ds = _datasync.getMGPHistoryDCDetails(ID, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPHistoryDCDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public bool setMGPOutDetails(MGPOutSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setMGPOutDetails(mgpouthdr, mgprefdcdetails, ref pMsg), ref pMsg, ref result);
            return result;
        }

        public bool spUpdateOutDetailsflag(string NoteNumber, long ID, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.spUpdateOutDetailsflag(NoteNumber, ID, ref pMsg), ref pMsg, ref result);
            return result;
        }

    }
}
