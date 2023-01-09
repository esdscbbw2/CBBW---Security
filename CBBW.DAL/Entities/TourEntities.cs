using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Tour;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class TourEntities
    {
        DataTable dt = null;
        DataSet ds=null;
        RulesDBMapper _mapper;
        DBResponseMapper _dbResponseMapper;
        RulesDataSync _datasync;
        public TourEntities()
        {
            _mapper = new RulesDBMapper();
            _datasync = new RulesDataSync();
            _dbResponseMapper = new DBResponseMapper();
        }
        #region Version 2 Changes
        public List<TourRuleListData> GetTourRules(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, ref string pMsg)
        {
            List<TourRuleListData> TourRules = new List<TourRuleListData>();
            try
            {
                dt = _datasync.getTourRules(DisplayLength,DisplayStart,SortColumn,SortDirection,SearchText,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TourRules.Add(_mapper.Map_TourRuleListData(dt.Rows[i]));
                    }
                }
            }
            catch(Exception ex) { pMsg = ex.Message; }
            return TourRules;
        }
        public bool CreateNewTourRuleV2(TourRuleSaveInfo trd, ref string pMsg)
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.createNewTourRuleV2(trd, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public TourRuleServiceTypes getServiceTypesFromEffectiveDate(DateTime EffectiveDate, ref string pMsg) 
        {
            try
            {
                ds = _datasync.getServiceTypesFromEffectiveDate(EffectiveDate, ref pMsg);
                if (ds != null)
                {
                    DataTable dt0 = null; DataTable dt1 = null;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) { dt0 = ds.Tables[0]; }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0) { dt1 = ds.Tables[1]; }
                    return _mapper.Map_TourRuleServiceTypes(dt0, dt1);
                }
                else { return null; }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public TourRuleSaveInfo getLastTourInfoFromServiceTypeCodes(string serviceTypeCodes, int IsView, DateTime EffectiveDate, ref string pMsg) 
        {
            TourRuleSaveInfo result = new TourRuleSaveInfo();
            try
            {
                dt = _datasync.getLastTourInfoFromServiceTypeCodes(serviceTypeCodes,IsView, EffectiveDate, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = _mapper.Map_TourRuleSaveInfo(dt.Rows[0]);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return result;
        }
        public List<TourRuleSaveInfo> getTourInfoFromEffectiveDate(DateTime EffectiveDate, ref string pMsg) 
        {
            List<TourRuleSaveInfo> Result = new List<TourRuleSaveInfo>();
            try
            {
                dt = _datasync.getTourInfoFromEffectiveDate(EffectiveDate, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Result.Add(_mapper.Map_TourRuleSaveInfo(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return Result;
        }
        public bool FinalSubmitToursRuleV2(DateTime EffectiveDate, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.FinalSubmitToursRuleV2(EffectiveDate, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool RemoveToursRuleV2(DateTime EffectiveDate, string ServiceTypeCodes, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.RemoveToursRuleV2(EffectiveDate, ServiceTypeCodes, ref pMsg), ref pMsg, ref result);
            return result;
        }
        #endregion
        public IEnumerable<TourRule> GetTourRules(ref string pMsg) 
        {
            List<TourRule> TourRules = new List<TourRule>();
            try
            {
                dt = _datasync.getToursRules(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TourRules.Add(_mapper.Map_TourRule(dt.Rows[i], i + 1));
                    }
                }
            }
            catch { }
            return TourRules;
        }
        public TourRuleDetails GetLastTourRule(ref string pMsg)
        {
            TourRuleDetails result = new TourRuleDetails();
            try
            {
                ds = _datasync.getLastToursRule(ref pMsg);
                if (ds != null)
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        dt = ds.Tables[1];
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        result = _mapper.Map_TourRuleDetails(ds.Tables[0].Rows[0], dt);
                    }
                    else { result = _mapper.Map_TourRuleDetails(null, dt); }
                }
            }
            catch { }
            return result;
        }
        public TourRuleDetails GetTourRuleByID(int ID, ref string pMsg) 
        {
            TourRuleDetails result = new TourRuleDetails();
            try
            {
                ds = _datasync.getToursRuleByID(ID, ref pMsg);
                if (ds != null)
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        dt = ds.Tables[1];
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        result = _mapper.Map_TourRuleDetails(ds.Tables[0].Rows[0], dt);
                    }
                }
            }
            catch { }
            return result;
        }
        public bool RemoveTourRule(int ID, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.removeTourRule(ID, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool CreateNewTourRule(TourRuleDetails trd, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.createNewTourRule(trd, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool IsValidTourRule(TourRuleDetails trd, ref string pMsg)
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.IsValidTourRule(trd, ref pMsg), ref pMsg, ref result);
            return result;
        }
        
    }
}
