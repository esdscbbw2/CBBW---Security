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

        #region For Out Details
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
        public List<MGPNotes> GetNoteNumbersfromMGP(int Centercode, ref string pMsg)
        {
            List<MGPNotes> result = new List<MGPNotes>();
            dt = _datasync.GetNoteNumbersfromMGP(Centercode, ref pMsg);
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
        public List<MGPHistoryDCDetails> getMGPHistoryDCDetails(long ID, int status, ref string pMsg)
        {
            List< MGPHistoryDCDetails> result = new List<MGPHistoryDCDetails> ();
            try
            {
                ds = _datasync.getMGPHistoryDCDetails(ID, status, ref pMsg);
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
        public bool spUpdateOutDetailsflag(string NoteNumber, long ID,int status, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.spUpdateOutDetailsflag(NoteNumber, ID, status, ref pMsg), ref pMsg, ref result);
            return result;
        }
        #endregion
        #region For In Details
        public List<MGPCurrentInDetails> getMGPCurrentOutDetailsForIn(string NoteNumber, ref string pMsg)
        {
            List<MGPCurrentInDetails> result = new List<MGPCurrentInDetails>();
            try
            {
                ds = _datasync.getMGPCurrentOutDetailsForIn(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dtl = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtl = ds.Tables[0];
                        for (int i = 0; i < dtl.Rows.Count; i++)
                        {
                            result.Add(_datamapper.Map_MGPCurrentInDetails(dtl.Rows[i]));
                        }
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<MGPReferenceDCDetails> getReferenceInDCDetails(string VehicleNo, DateTime FromDT, DateTime ToDT, ref string pMsg)
        {
            List<MGPReferenceDCDetails> result = new List<MGPReferenceDCDetails>();
            try
            {
                ds = _datasync.getReferenceInDCDetails(VehicleNo, FromDT, ToDT, ref pMsg);
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
        public List<MGPItemWiseDetails> getItemWiseInDetails(string NoteNumber, ref string pMsg)
        {
            List<MGPItemWiseDetails> result = new List<MGPItemWiseDetails>();
            try
            {
                ds = _datasync.getItemWiseInDetails(NoteNumber, ref pMsg);
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
        public bool setMGPInDetails(MGPInSave mgpouthdr, List<MGPReferenceDCDetails> mgprefdcdetails, ref string pMsg)
        {
            bool result = false;
            _DBResponseMapper.Map_DBResponse(_datasync.setMGPInDetails(mgpouthdr, mgprefdcdetails, ref pMsg), ref pMsg, ref result);
            return result;
        }
        #endregion
        #region For List Page (Index page)
        public List<MGPNoteList> getMGPDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText,int CentreCode, ref string pMsg)
        {
            List<MGPNoteList> result = new List<MGPNoteList>();
            try
            {
                dt = _datasync.getMGPDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CentreCode, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_datamapper.Map_MGPNoteList(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        #endregion
        #region In/Out Button Active
        public ButtonActive getMGPButtonStatus(string NoteNumber, ref string pMsg)
        {
            ButtonActive result = new ButtonActive();
            try
            {
                dt = _datasync.getMGPButtonStatus(NoteNumber, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _datamapper.Map_ButtonActive(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        #endregion

        #region For Rport
        public PrintHeader GetMGPDetailsForPrint(string NoteNumber, ref string pMsg)
        {
            PrintHeader result = new PrintHeader();
            try
            {
                ds = _datasync.GetMGPDetailsForPrint(NoteNumber, ref pMsg);
                if (ds != null)
                {
                    DataTable dt = null;
                    dt = ds.Tables[0];
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                       
                        result.ReportHdr = _datamapper.Map_ReportHdr(dt.Rows[0]); 
                    }
                    List<ReportInOutDetails> INOuT = new List<ReportInOutDetails>();
                    dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            INOuT.Add(_datamapper.Map_ReportInOutDetails(dt.Rows[i]));
                        }
                    }
                    result.reportInOutdetails = INOuT;
                    List<ReportDCDetails> DC = new List<ReportDCDetails>();
                    dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DC.Add(_datamapper.Map_ReportDCDetails(dt.Rows[i]));
                        }
                    }
                    result.reportDCdetails = DC;
                    List<MGPItemWiseDetails> ItemWise = new List<MGPItemWiseDetails>();
                    dt = ds.Tables[3];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ItemWise.Add(_datamapper.Map_MGPItemWiseDetails(dt.Rows[i]));
                        }
                    }
                    result.reportItemWisedetails = ItemWise;

                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }

        public PrintHeader GetMGPDetailsForPrintV2(string NoteNumber,DateTime SchFromDate, ref string pMsg)
        {
            PrintHeader result = new PrintHeader();
            try
            {
                ds = _datasync.GetMGPDetailsForPrintV2(NoteNumber, SchFromDate, ref pMsg);
                if (ds != null)
                {
                    DataTable dt = null;
                    dt = ds.Tables[0];
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        result.ReportHdr = _datamapper.Map_ReportHdrV2(dt.Rows[0]);
                    }
                    List<ReportDCDetails> DC = new List<ReportDCDetails>();
                    dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DC.Add(_datamapper.Map_ReportDCDetails(dt.Rows[i]));
                        }
                    }
                    result.reportDCdetails = DC;
                    List<MGPItemWiseDetails> ItemWise = new List<MGPItemWiseDetails>();
                    dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ItemWise.Add(_datamapper.Map_MGPItemWiseDetails(dt.Rows[i]));
                        }
                    }
                    result.reportItemWisedetails = ItemWise;

                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        #endregion

        public Percentage getMaterialPercent(string VehicleNo, DateTime FromDT, int status, ref string pMsg)
        {
            Percentage result = new Percentage();
            try
            {
                dt = _datasync.getMaterialPercent(VehicleNo, FromDT, status, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _datamapper.Map_Percentage(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
    }
}
