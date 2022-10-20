using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CustomModels;
using CBBW.BOL.Master;
using CBBW.BOL.TADA;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{   
    public class TADARulesRepository : ITADARulesRepository
    {
        TADAEntities _tadaEntities;
        MasterEntities _masterentities;
        public TADARulesRepository()
        {
            _tadaEntities = new TADAEntities();
            _masterentities = new MasterEntities();
        }
        public bool CreateNewTADARule(TADARuleDetails trd, ref string pMsg)
        {
            trd.EntryDate = DateTime.Now;
            trd.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
            return _tadaEntities.CreateNewTADARule(trd, ref pMsg);
        }

        public TADARuleDetails GetLastTADARule(ref string pMsg)
        {
            TADARuleDetails result = _tadaEntities.GetLastTADARule(ref pMsg);
            if (result.EffectiveDate.Year == 1)
                result.EffectiveDate = DateTime.Today;
            //if (result.EntryDate.Year == 1)
            //    result.EntryDate = DateTime.Today;
            //result.EffectiveDate= DateTime.Today;
            result.EntryDate = DateTime.Today;
            result.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
            result.NewConnectingID = Guid.NewGuid().ToString("n");
            if (result.Categories != null && result.Categories.Count > 0)
            {
                result.SelectedCategoryIds = result.Categories.Where(o => o.IsSelected == true).Select(o => o.ID).ToList();
            }
            else 
            {
                result.SelectedCategoryIds = new List<int>();
            }
            return result;
        }

        public IEnumerable<TADAPubTransOption> GetPublicTransportClassTypes(int ID, ref string pMsg)
        {
            return _masterentities.getPublicTransportClassTypes(ID, ref pMsg);
        }

        public IEnumerable<PublicTransportType> GetPublicTransportTypes(ref string pMsg)
        {
            return _masterentities.getPublicTransportTypes(ref pMsg);
        }

        public TADARuleDetails GetTADARuleByID(int ID, ref string pMsg)
        {
            TADARuleDetails result= _tadaEntities.GetTADARuleByID(ID, ref pMsg);
            if (result.Categories != null && result.Categories.Count > 0)
            {
                result.SelectedCategoryIds = result.Categories.Where(o => o.IsSelected == true).Select(o => o.ID).ToList();
            }
            //result.SelectedCategoryIds = result.Categories.Where(o => o.IsSelected == true).Select(o => o.ID).ToList();
            return result;
        }
        public IEnumerable<TADARule> GetTADARules(ref string pMsg)
        {
            return _tadaEntities.GetTADARules(ref pMsg);
        }

        public bool IsValidRule(TADARuleDetails trd, ref string pMsg)
        {
            return _tadaEntities.IsValidTadARule(trd, ref pMsg);
        }

        public bool RemoveTADARule(int ID, ref string pMsg)
        {
            return _tadaEntities.RemoveTADARule(ID,ref pMsg);
        }
    }
}
