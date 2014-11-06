using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Utilities
{
    public static class DateTimeExtensions
    {
        public static DateTime ToFirstOfMonth(this DateTime date)
        {
            if (date.Day != 1)
            {
                date = date.AddDays(-(date.Day - 1));
            }

            return date;
        }

        //public static int GetDatePart(this string str, DateInterval interval)
        //{
        //    try
        //    {
        //        switch (interval)
        //        {
        //            case DateInterval.Year:
        //                return str.Substring(0, 4).GetValueOrDefault<int>();
        //            case DateInterval.Month:
        //            case DateInterval.Day:
        //            case DateInterval.Hour:
        //            case DateInterval.Minute:
        //            case DateInterval.Second:
        //        }


        //                                        str.Substring(4, 2).GetValueOrDefault<int>(),
        //                        str.Substring(6, 2).GetValueOrDefault<int>(),
        //                        str.Substring(8, 2).GetValueOrDefault<int>(),
        //                        str.Substring(10, 2).GetValueOrDefault<int>(),
        //                        str.Substring(12, 2).GetValueOrDefault<int>());

        //        return 0;                
        //    }
        //}
    }
}
