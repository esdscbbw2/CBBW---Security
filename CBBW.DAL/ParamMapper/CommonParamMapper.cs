using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.ParamMapper
{
    public class CommonParamMapper
    {
        public SqlParameter[] MapParam_DisplayList(int DisplayLength,
            int DisplayStart, int SortColumn, string SortDirection,
            string SearchText, ref string pMsg)
        {
            int paracount = 0;
            SqlParameter[] para = new SqlParameter[5];
            try
            {
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection.Substring(0, 1).ToUpper();
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar);
                para[paracount++].Value = SearchText;
            }
            catch { }
            return para;
        }
        public SqlParameter[] MapParam_DisplayListWithCentreCode(int DisplayLength,
            int DisplayStart, int SortColumn, string SortDirection,
            string SearchText,int CentreCode, ref string pMsg)
        {
            int paracount = 0;
            SqlParameter[] para = new SqlParameter[6];
            try
            {
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection.Substring(0, 1).ToUpper();
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
            }
            catch { }
            return para;
        }

        public SqlParameter[] MapParam_DisplayListWithCentreCodenIsApproved(int DisplayLength,
            int DisplayStart, int SortColumn, string SortDirection,
            string SearchText, int CentreCode,bool IsApproved, ref string pMsg)
        {
            int paracount = 0;
            SqlParameter[] para = new SqlParameter[7];
            try
            {
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection.Substring(0, 1).ToUpper();
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar);
                para[paracount++].Value = SearchText;
                para[paracount] = new SqlParameter("@CentreCode", SqlDbType.Int);
                para[paracount++].Value = CentreCode;
                para[paracount] = new SqlParameter("@IsApproved", SqlDbType.Bit);
                para[paracount++].Value = IsApproved;
            }
            catch { }
            return para;
        }





    }
}
