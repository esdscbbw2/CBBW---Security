using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.DAL.DBLogic
{
    public static class MyDBLogic
    {
        public static int ReturnDaysFromDistance(float Distance)
        {
            int mdays = 0;
            if (Distance <= 155)
                mdays = 1;
            else if (Distance <= 355)
                mdays = 2;
            else if (Distance <= 555)
                mdays = 3;
            else if (Distance <= 755)
                mdays = 4;
            else
                mdays = 5;
            if (mdays > 0) mdays -= 1;
            return mdays;
        }
        public static int getFirstIntegerFromString(string mString, char separator)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(mString))
            {
                if (mString.IndexOf(separator) > 0)
                { result = int.Parse(mString.Split(separator)[0].Trim()); }
            }
            return result;
        }
        public static string Change_ToComma(string mString) 
        {
            if (mString.Substring(0, 1) == "_")
                mString = mString.Substring(1, mString.Length - 1);
            return mString.Replace('_', ',');
        }
    }
}
