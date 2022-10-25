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
    }
}
