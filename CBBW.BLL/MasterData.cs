using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.Master;
using CBBW.DAL.Entities;

namespace CBBW.BLL
{
    public sealed class MasterData
    {
        private static readonly Lazy<MasterData> instance = new Lazy<MasterData>();
        public static MasterData GetInstance
        {
            get { return instance.Value; }
        }
        public MasterData()
        {
            GetLocations();
        }
        private void GetLocations()
        {
            SingletonEntity obj1 = new SingletonEntity();
            AllLocations = obj1.GetLocationsFromCommaSeparatedTypes("99,11,4,3,2,1");
        }
        public List<LocationMaster> AllLocations { get; set; }
    }
}
