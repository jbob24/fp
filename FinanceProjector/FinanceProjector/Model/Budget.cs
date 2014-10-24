using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class Budget
    {
        public DateTime Month { get; set; }
        public List<BudgetItem> PlannedItems { get; set; }
        public List<BudgetItem> ActualItems { get; set; }

        public Budget()
        {
            PlannedItems = new List<BudgetItem>();
            ActualItems = new List<BudgetItem>();
        }

        public static Budget Create(DateTime month)
        {
            var budget = new Budget();
            month = new DateTime(month.Year, month.Month, 1);
            budget.Month = month;

            return budget;
        }
    }

    public class BudgetItem
    {
        public string CategoryName { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
    }
}
