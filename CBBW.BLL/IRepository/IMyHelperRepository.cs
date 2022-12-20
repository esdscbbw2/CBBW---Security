using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BLL.IRepository
{
    public interface IMyHelperRepository
    {
        int getFirstIntegerFromString(string mString, char separator);
    }
}
