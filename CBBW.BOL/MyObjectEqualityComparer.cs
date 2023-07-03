using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL
{
    public class PageInformationEqualityComparer : IEqualityComparer<PageInformation>
    {
        public bool Equals(PageInformation x, PageInformation y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.ControllerName == y.ControllerName && x.ActionName == y.ActionName;
        }
        public int GetHashCode(PageInformation obj)
        {
            int hashCode = obj.ControllerName.GetHashCode();
            hashCode = (hashCode * 397) ^ obj.ActionName.GetHashCode();
            return hashCode;
        }
    }
}
