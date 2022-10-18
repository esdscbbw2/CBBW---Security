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
