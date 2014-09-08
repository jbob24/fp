using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FinanceProjector.Web.Helpers
{
    public static class Extensions
    {
        public static SelectList TransactionTypeSelectList()
        {
            var values = Enum.GetValues(typeof (FinanceProjector.Enums.TransactionType));
            Array.Sort(values);

            return new SelectList(values);
        }
    }
}