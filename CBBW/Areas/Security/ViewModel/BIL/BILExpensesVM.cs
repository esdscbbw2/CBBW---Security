using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.BIL;

namespace CBBW.Areas.Security.ViewModel.BIL
{
    public class BILExpensesVM
    {
        public BILExpensesVM()
        {
            TADABill = new TADABillGeneration();
            
        }
        public TADABillGeneration TADABill { get;set;}

    }
}