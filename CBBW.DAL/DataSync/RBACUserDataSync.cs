using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DataSync
{
    public class RBACUserDataSync
    {
        public DataTable GetListOfActiveEmployees(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetListOfActiveEmployees]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetCentreList(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [MTR].[GetCentreList](1)", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetListOfRoles(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("select * from [RBAC].[GetListOfRoles]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }






    }
}
