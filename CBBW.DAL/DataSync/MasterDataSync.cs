using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DataSync
{
    public class MasterDataSync
    {
        public DataTable getServiceTypes(int ID,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [MTR].[master_GetServiceTypes](" + ID + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message;return null; }
        }
        public DataTable getPublicTransType(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [RUL].[getPublicTransType]()", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getPubTransClassType(int ID,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [RUL].[getPublicTransClassType]("+ ID + ")", CommandType.Text))
                {
                    return sql.GetDataTable();
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }


    }
}
