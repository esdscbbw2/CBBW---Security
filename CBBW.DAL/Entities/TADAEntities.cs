using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.CustomModels;
using CBBW.BOL.TADA;
using CBBW.DAL.DataSync;
using CBBW.DAL.DBMapper;

namespace CBBW.DAL.Entities
{
    public class TADAEntities
    {
        DataTable dt = null;
        DataSet ds = null;
        RulesDBMapper _mapper;
        DBResponseMapper _dbResponseMapper;
        RulesDataSync _datasync;
        public TADAEntities()
        {
            _mapper = new RulesDBMapper();
            _datasync = new RulesDataSync();
            _dbResponseMapper = new DBResponseMapper();
        }
        #region Version 2 Changes
        public List<TADARuleListData> getTADARules(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, ref string pMsg)
        {
            List<TADARuleListData> TADARules = new List<TADARuleListData>();
            try
            {
                dt = _datasync.getTADARules(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TADARules.Add(_mapper.Map_TADARuleListData(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex) { pMsg = ex.Message; }
            return TADARules;
        }
        public List<CustomCheckBoxOption> GetCatCodesForTADARule(DateTime EffectiveDate, ref string pMsg) 
        {
            List<CustomCheckBoxOption> result = new List<CustomCheckBoxOption>();
            try
            {
                dt = _datasync.GetCatCodesForTADARule(EffectiveDate, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_dbResponseMapper.Map_CustomCheckBoxOption(dt.Rows[i]));
                    }                    
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public TADARuleV2 GetLastTADARuleV2(DateTime EffectiveDate, ref string pMsg) 
        {
            TADARuleV2 result = new TADARuleV2();
            try
            {
                dt = _datasync.GetLastTADARuleV2(EffectiveDate,ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _mapper.Map_TADARuleV2(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public bool SetTADARuleV2(TADARuleV2 data, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.SetTADARuleV2(data, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public List<CustomComboOptionsWithString> GetCatCodesForTADARuleView(DateTime EffectiveDate, ref string pMsg) 
        {
            List<CustomComboOptionsWithString> result = new List<CustomComboOptionsWithString>();
            try
            {
                dt = _datasync.GetCatCodesForTADARuleView(EffectiveDate, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result.Add(_dbResponseMapper.Map_CustomOptionsWithString(dt.Rows[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public bool FinalSubmitTADARuleV2(DateTime EffectiveDate, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.FinalSubmitTADARuleV2(EffectiveDate, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public TADARuleV2 GetTADARuleV2(DateTime EffectiveDate, string CategoryIDs, ref string pMsg) 
        {
            TADARuleV2 result = new TADARuleV2();
            try
            {
                dt = _datasync.GetTADARuleV2(EffectiveDate, CategoryIDs, ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return _mapper.Map_TADARuleV2(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return result;
        }
        public bool RemoveTADARuleV2(DateTime EffectiveDate, string CategoryIDs, ref string pMsg) 
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.RemoveTADARuleV2(EffectiveDate, CategoryIDs, ref pMsg), ref pMsg, ref result);
            return result;
        }




        #endregion
        public IEnumerable<TADARule> GetTADARules(ref string pMsg)
        {
            List<TADARule> tadaRules = new List<TADARule>();
            try
            {
                dt = _datasync.getTADARules(ref pMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tadaRules.Add(_mapper.Map_TADARule(dt.Rows[i],i+1));
                    }
                }
            }
            catch { }
            return tadaRules;
        }
        public TADARuleDetails GetLastTADARule(ref string pMsg)
        {
            TADARuleDetails result = new TADARuleDetails();
            try
            {
                ds = _datasync.getLastTADARules(ref pMsg);
                if (ds != null)
                {
                    DataTable categories = null;
                    DataTable pubtrans = null;
                    DataTable comptrans = null;
                    DataTable tadaparam = null;
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        categories = ds.Tables[1];
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        pubtrans = ds.Tables[2];
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        comptrans = ds.Tables[3];
                    if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        tadaparam = ds.Tables[4];
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        result = _mapper.Map_TADARuleDetails(ds.Tables[0].Rows[0], categories, comptrans, pubtrans, tadaparam);
                    }
                    else 
                    {                        
                        result = _mapper.Map_TADARuleDetails(null, categories, comptrans, pubtrans, tadaparam);
                    }
                }
            }
            catch { }
            return result;
        }
        public TADARuleDetails GetTADARuleByID(int ID, ref string pMsg)
        {
            TADARuleDetails result = new TADARuleDetails();
            try
            {
                ds = _datasync.getTADARulesByID(ID, ref pMsg);
                if (ds != null)
                {
                    DataTable categories = null;
                    DataTable pubtrans = null;
                    DataTable comptrans = null;
                    DataTable tadaparam=null;
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        categories = ds.Tables[1];
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        pubtrans = ds.Tables[2];
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        comptrans = ds.Tables[3];
                    if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        tadaparam = ds.Tables[4];
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        result = _mapper.Map_TADARuleDetails(ds.Tables[0].Rows[0],categories,comptrans,pubtrans,tadaparam);
                    }
                }
            }
            catch { }
            return result;
        }
        public bool RemoveTADARule(int ID, ref string pMsg)
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.removeTADARule(ID, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool CreateNewTADARule(TADARuleDetails trd, ref string pMsg)
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.createNewTADARule(trd, ref pMsg), ref pMsg, ref result);
            return result;
        }
        public bool IsValidTadARule(TADARuleDetails trd, ref string pMsg)
        {
            bool result = false;
            _dbResponseMapper.Map_DBResponse(_datasync.IsValidTADARule(trd, ref pMsg), ref pMsg, ref result);
            return result;
        }
    }
}
