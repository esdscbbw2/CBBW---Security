using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
