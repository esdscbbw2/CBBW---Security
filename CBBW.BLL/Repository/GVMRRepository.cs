using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;
using CBBW.BOL.GVMR;
using CBBW.DAL.Entities;

namespace CBBW.BLL.Repository
{
    public class GVMRRepository : IGVMRRepository
    {

        GVMREntities _GVMREntities;
        //MasterEntities _MasterEntities;
        //UserRepository _user;
        public GVMRRepository()
        {
            _GVMREntities = new GVMREntities();
        }


        public IEnumerable<GVMRNoteNumber> GetNoteNumbers(int CenterCode, int status, ref string pMsg)
        {
            return _GVMREntities.GetNoteNumbers(CenterCode,status, ref pMsg);
        }

        public List<GVMRDetails> GetGVMRDetails(string NoteNumber, int CenterCode, ref string pMsg)
        {
            return _GVMREntities.GetGVMRDetails(NoteNumber, CenterCode, ref pMsg);
        }


        public bool setGVMRDetails(GVMRDataSave gvmrdata, ref string pMsg)
        {
            return _GVMREntities.setGVMRDetails(gvmrdata, ref pMsg);
        }

        public List<GVMRNoteList> getGVMRDetailsforListPage(int DisplayLength, int DisplayStart, int SortColumn, string SortDirection, string SearchText,int CenterCode, ref string pMsg)
        {
            return _GVMREntities.getGVMRDetailsforListPage(DisplayLength, DisplayStart, SortColumn, SortDirection, SearchText, CenterCode, ref pMsg);
        }
        public List<GVMRDetails> getGVMRDetailsForView(string NoteNumber, int CenterCode, ref string pMsg)
        {
            return _GVMREntities.getGVMRDetailsForView(NoteNumber, CenterCode, ref pMsg);
        }
    }
}
