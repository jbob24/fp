using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class BudgetCategoryItem
    {
        public string Name { get; set; }
        public string BudgetCategoryName { get; set; }
        public List<TransactionMatch> TransactionMatches { get; set; }

        public BudgetCategoryItem(string name)
        {
            this.Name = name;
            TransactionMatches = new List<TransactionMatch>();
        }

        public BudgetCategoryItem(string name, string budgetCategoryName)
            : this(name)
        {
            this.BudgetCategoryName = budgetCategoryName;
        }
    }
}
