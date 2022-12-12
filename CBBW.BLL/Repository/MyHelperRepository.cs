using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BLL.IRepository;

namespace CBBW.BLL.Repository
{
    public class MyHelperRepository : IMyHelperRepository
    {
        public int getFirstIntegerFromString(string mString, char separator)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(mString)) 
            {
                if (mString.IndexOf(separator) > 0) 
                {result = int.Parse(mString.Split(separator)[0].Trim()); }
            }
            return result;
        }
    }
}
