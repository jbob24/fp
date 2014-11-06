using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceProjector.Utilities;

namespace FinanceProjector.Model
{
    public class RecurringTransaction
    {
        //public TransactionRecurrenceType RecurrenceType { get; set; }
        //public TransactionRecurrence Recurrence { get; set; }

        public DateTime StartDate { get; private set; }
        public int NumberOfMonths { get; set; }
        public string BudgetCategoryItemName { get; set; }
        public decimal Amount { get; set; } 

        public RecurringTransaction(DateTime startDate, int numberOfMonths, string categoryItemName, decimal amount)
        {
            this.StartDate = startDate.ToFirstOfMonth();
            this.NumberOfMonths = numberOfMonths;
            this.BudgetCategoryItemName = categoryItemName;
            this.Amount = amount;
        }

        public IEnumerable<DateTime> GetNextRecurrences(int numberOfRecurrences)
        {
            var recurrences = new List<DateTime>();
            var nextOccurence = StartDate;
            var today = DateTime.Today;

            if (nextOccurence < today)
            {
                var monthsBetween = DateTime.Today.Subtract(nextOccurence).Days / (365.12 / 12);

                if (monthsBetween % NumberOfMonths == 0)
                {
                    nextOccurence = new DateTime(today.Year, today.Month, 1);
                }
                else
                {
                    int monthsToAdd = NumberOfMonths * ((int)(monthsBetween / NumberOfMonths) + 1);
                    nextOccurence = StartDate.AddMonths(monthsToAdd);
                }
            }

            recurrences.Add(nextOccurence);

            for (int i = 1; i < numberOfRecurrences; i++)
            {
                nextOccurence = nextOccurence.AddMonths(NumberOfMonths);
                recurrences.Add(nextOccurence);
            }

            return recurrences;
        }
    }

    //public abstract class TransactionRecurrence
    //{
    //    public DateTime StartDate { get; set; }
    //    public DateTime? EndDate { get; set; }

    //    public virtual DateTime? GetNextRecurrence()
    //    {
    //        return null;
    //    }
    //}

    //public class DailyTransactionRecurrence : TransactionRecurrence
    //{
    //    public int NumberOfDays { get; set; }
    //}

    //public class WeeklyTransactionRecurrence : TransactionRecurrence
    //{
    //    public int NumberOfWeeks { get; set; }
    //}

    //public class MonthlyTransactionRecurrence : TransactionRecurrence
    //{
    //    public int NumberOfMonths { get; set; }
    //    public bool SplitWeekly { get; set; }
    //    public decimal WeeklySplitAmount { get; set; }
    //}

    //public class YearlyTransactionRecurrence : TransactionRecurrence
    //{
    //    public int NumberOfYears { get; set; }
    //}

    //public enum TransactionRecurrenceType
    //{
    //    None = 0,
    //    Daily,
    //    Weekly,
    //    Monthly,
    //    Yearly
    //}
}
