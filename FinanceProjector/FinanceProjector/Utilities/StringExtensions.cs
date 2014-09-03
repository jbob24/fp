using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Utilities
{
    public static class StringExtensions
    {
        public static decimal ToDecimal(this string str)
        {
            decimal value = 0m;

            decimal.TryParse(str.ToString(), out value);
            return value;
        }
    }                        
}
