using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL
{
    public static class MyCodeHelper
    {
        public static int GetEmpNoFromString(string EmpNonName) 
        {
            int result = 0;
            try
            {
                if(EmpNonName!=null && EmpNonName.IndexOf("/")>=0)
                    result = int.Parse(EmpNonName.Split('/')[0]);
            }
            catch { }
            return result;
        }
    }
}
