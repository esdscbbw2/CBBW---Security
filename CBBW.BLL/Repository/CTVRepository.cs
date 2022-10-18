using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class CTVRepository : ICTVRepository
    {
        CTVEntities _CTVEntities;
        public CTVRepository()
        {
            _CTVEntities = new CTVEntities();
        }
        public string getNewTripScheduleNo(string SchPattern, ref string pMsg)
        {
           return _CTVEntities.getNewCTVNoteNo(SchPattern, ref pMsg);
        }

        public TripScheduleHdr NewTripScheduleNo(string SchPattern, ref string pMsg)
        {
            TripScheduleHdr result = new TripScheduleHdr();
            result.NoteNo = _CTVEntities.getNewCTVNoteNo(SchPattern, ref pMsg);
            result.EntryDate = DateTime.Today;
            result.EntryTime = DateTime.Now.ToString("hh:mm:ss tt");
            result.FortheMonth = DateTime.Today.Month;
            result.FortheYear= DateTime.Today.Year;
            if (DateTime.Today.Day <= 15)
            {
                result.FromDate = new DateTime(result.FortheYear, result.FortheMonth, 1);
                result.ToDate= new DateTime(result.FortheYear, result.FortheMonth, 15);
            }
            else 
            {
                result.FromDate = new DateTime(result.FortheYear, result.FortheMonth, 16);
                result.ToDate = new DateTime(result.FortheYear, result.FortheMonth, 1).AddMonths(1).AddDays(-1);
            }
            return result;
        }
    }
}
